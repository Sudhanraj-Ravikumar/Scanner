using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace ScannerDisplay
{
    public interface IScanner
    {
        UdpClient UdpClient
        { get; }

        string Name
        { get; }

        int Tag
        { get; set; }
    }

    public class Scanner : IScanner
    {
        public delegate void ScannerDataInput(object sender, List<Tuple<int, int, int>> data);
        
        public event ScannerDataInput ScannerInputRaw;
        public event ScannerDataInput ScannerInputNormalized;

        private List<Tuple<int, int, int>> normalizedData;
        private List<Tuple<int, int, int>> analyzedData;

        public Scanner(int tag)
        {
            try
            {
                Name = "Scanner" + (tag + 1);
                Tag = tag;
                IsStopped = false;

                InitValues();

                XYCoordinate = new List<Tuple<int, int, int>>();
                XYCollection = new List<Tuple<int, int, int>>[Mainframe.NormalizebyTimeCountStoredData[Tag]];
                XYColl = new List<List<Tuple<int, int, int>>>[Mainframe.NormalizebyTimeCountStoredData[Tag]];
                XYCoord = new List<List<Tuple<int, int, int>>>();
                NormalizedData = new List<Tuple<int, int, int>>();
                ToNormalizeData = new List<Tuple<int, int, int>>();
                AnalyseData = new List<Tuple<int, int, int>>();
                Id6Distance = new List<short>();
                Distance = new List<List<short>>();

                LogFiler = new Logger($"Scanner{Tag.ToString()}");
                LogFiler.Log(Category.Info, MethodBase.GetCurrentMethod().DeclaringType.Name + ": initialized.");
                ScannerFunc = new ScannerFunctionality(Tag, LogFiler);

                try
                {
                    UdpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(Mainframe.ServerIp), Mainframe.Port[Tag]));
                }
                catch (ArgumentNullException aex)
                {
                    LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
                return;
            }
        }

        public UdpClient UdpClient
        { get; }

        private ScannerFunctionality ScannerFunc
        { get; set; }

        public Logger LogFiler
        { get; set; }

        public string Name
        { get; }

        // necessary to count the amount of XYArray information
        private int RingBufferCount
        { get; set; }

        private int CountTimeAverage
        { get; set; }

        public int Tag
        { get; set; }

        // necessary for calculation of the XY pairs
        public float Degree
        { get; set; }

        public float AngleDifference
        { get; set; }

        // amount of Id6 packages to receive
        private int Id6Blocks
        { get; set; }

        // counter of received Id6 blocks
        private int Id6Count
        { get; set; }

        // information about latest received block
        private int Id6LastBlock
        { get; set; }

        private int Id6LastIndex
        { get; set; }
        
        // byte[] to store the complete Id6 message (all blocks)
        private byte[] Id6Whole
        { get; set; }

        private bool IsStopped
        { get; set; }
        
        private List<short> Id6Distance
        { get; set; }

        private List<short> Id6DistanceCorrected
        { get; set; }

        public List<List<short>> Distance
        { get; set; }

        public List<Tuple<int, int, int>> XYCoordinate
        { get; set; }

        private List<List<Tuple<int, int, int>>> XYCoord
        { get; set; }

        private List<List<Tuple<int, int, int>>>[] XYColl
        { get; set; }

        public List<Tuple<int, int, int>>[] XYCollection
        { get; set; }
        
        public List<Tuple<int, int, int>> NormalizedData
        {
            get
            {
                return new List<Tuple<int, int, int>>(normalizedData);
            }

            set
            {
                normalizedData = value;
            }
        }
        
        private List<Tuple<int, int, int>> ToNormalizeData
        { get; set; }

        public List<Tuple<int, int, int>> AnalyseData
        {
            get
            {
                return new List<Tuple<int, int, int>>(analyzedData);
            }

            set
            {
                analyzedData = value;
            }
        }

        protected void OnScannerRaw(object sender, List<Tuple<int, int, int>> data)
        {
            ScannerInputRaw?.Invoke(sender, data);
        }

        protected void OnScannerNormalized(object sender, List<Tuple<int, int, int>> data)
        {
            ScannerInputNormalized?.Invoke(sender, data);
        }

        private void InitValues()
        {
            RingBufferCount = 0;
            CountTimeAverage = 0;

            Degree = 0;
            AngleDifference = 0;

            Id6Blocks = 0;
            Id6LastBlock = -1;
            Id6LastIndex = 0;
            Id6Count = 0;
            Id6Whole = new byte[6000];
        }

        public void ProcessReceive()
        {
            LogFiler.Log(Category.Info, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": started receiving data.");

            try
            {
                while (!IsStopped)
                {                    
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    Task<byte[]> receive = TReceive();
                    ScannerDecode Info = new ScannerDecode().Decode(receive.Result);

                    if (Info.ID == 1)
                    {
                        
                        RefreshScanner(Info);
                        Tuple<float, float> angleinfo = ScannerFunctionality.GetAngleId1(receive.Result);
                        Degree = angleinfo.Item1;
                        AngleDifference = angleinfo.Item2;
                    }
                    else if (Info.ID == 6)
                    {
                        if (BlockOrderCorrect(Info.Block))
                        {
                            ExpandScanner(Info, receive.Result);

                            // if the id6 telegramblocks are finished - split the distance based on the scannergap information (not visible area)
                            // if its not getting split, the averaging will create datapoints where the scanner couldnt be able to see at all - leads to consequential errors
                            if (Id6Complete())
                            {
                                Distance = ScannerFunc.GetProcessedDistance(Id6Distance.ToList());

                                if (Mainframe.IsNormalizedbyMedian[Tag])
                                {
                                    Distance = ScannerFunc.GetMedianAverageArray(Distance.ToList());
                                }
                                
                                XYCoord = ScannerFunc.GetXYCoordinate(Distance.ToList(), this);
                                
                                XYCoordinate = ScannerFunctionality.MergeXYArray(XYCoord.ToList());
                                OnScannerRaw(this, XYCoordinate.ToList());

                                List<List<Tuple<int, int, int>>> Normalizer = new List<List<Tuple<int, int, int>>>();
                                List<Tuple<int, int, int>> Normalized = new List<Tuple<int, int, int>>();

                                // try to accomplish the necessary kinds of averaging the distance sent by the scanner
                                // data averaged by time
                                if (Mainframe.IsNormalizedbyTime[Tag])
                                {
                                    List<Tuple<int, int, int>> xycollection = new List<Tuple<int, int, int>>(XYCoordinate);
                                    XYCollection[RingBufferCount] = xycollection;

                                    List<List<Tuple<int, int, int>>> xycolle = new List<List<Tuple<int, int, int>>>(XYCoord);
                                    XYColl[RingBufferCount] = xycolle;

                                    RingBufferCount++;
                                    CountTimeAverage++;

                                    if (RingBufferCount == Mainframe.NormalizebyTimeCountStoredData[Tag])
                                    {
                                        RingBufferCount = 0;
                                    }

                                    if (CountTimeAverage == Mainframe.NormalizebyTimeCountDataAverage[Tag])
                                    {
                                        Normalizer = ScannerFunc.NormalizationByTime(RingBufferCount, XYColl);
                                        CountTimeAverage = 0;
                                    }
                                }
                                else
                                {
                                    Normalizer = XYCoord;
                                }

                                if (Normalizer.Count != 0)
                                {
                                    if (Mainframe.IsNormalizedbyTriangle[Tag])
                                    {
                                        Normalizer = ScannerFunc.Normalization(Normalizer.ToList());
                                    }

                                    Normalized = ScannerFunctionality.MergeNormalizeArray(Normalizer.ToList());

                                    Normalized = ScannerFunc.ChangePositionOfXY(Normalized.ToList());

                                    NormalizedData = Normalized;
                                    OnScannerNormalized(this, new List<Tuple<int, int, int>>(NormalizedData));

                                    List<Tuple<int, int, int>> Analyser = new List<Tuple<int, int, int>>(Normalized);
                                    AnalyseData = Analyser;
                                }                                                               

                                if (watch.IsRunning)
                                {
                                    watch.Stop();
                                    LogFiler.Log(Category.Info, "normalise" + watch.ElapsedMilliseconds.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void RefreshScanner(ScannerDecode scannerDecode)
        {
            Degree = 0;
            AngleDifference = 0;

            Id6Blocks = scannerDecode.Block_From_Calc;
            Id6LastBlock = -1;
            Id6LastIndex = 0;
            Id6Count = 0;
            
            Id6Distance.Clear();
            Array.Clear(Id6Whole, 0, Id6Whole.Length);
        }

        private bool Id6Complete()
        {
            if (Id6Blocks == Id6Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool BlockOrderCorrect(int newblock)
        {
            if ((Id6LastBlock == -1 && newblock == 0) || (Id6LastBlock == 0 && newblock == 1) || (Id6LastBlock == 1 && newblock == 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExpandScanner(ScannerDecode scannerDecode, byte[] data)
        {
            Id6Count++;

            Array.Copy(data, 0, Id6Whole, Id6LastIndex, data.Length);
            Id6Distance.AddRange(scannerDecode.Distance);

            Id6LastBlock = scannerDecode.Block;
            Id6LastIndex = Id6LastIndex + data.Length;
        }

        private async Task<byte[]> TReceive()
        {
            UdpReceiveResult result;

            try
            {
                result = await UdpClient.ReceiveAsync();
            }
            catch (ObjectDisposedException ode)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ode.Message);
            }
            catch (SocketException se)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + se.Message);
            }

            return result.Buffer;
        }

        public void Close()
        {
            IsStopped = true;

            LogFiler.Close();
        }
    }
}
		
