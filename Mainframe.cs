using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ScannerDisplay.DatablockObjects;

namespace ScannerDisplay
{
    public enum User
    {
        Undef = 0,
        Admin = 1,
        Oberserver = 2
    }

    public partial class Mainframe : Form
    {
        delegate void FillChartCallback(Scanner scanner, Chart chart, string series);
        delegate void FillAnalyserChartCallback();
        delegate void FillCombinerChartCallback();
        delegate void CombineNormalizedData(List<Tuple<int, int, int>>[] splitData);

        private static string[] TagInfo;

        private static string[] PortInfo;

        private static string[] SingleChartTypesInfo;

        private static string[] SingleChartAxisYMinInfo;

        private static string[] SingleChartAxisYMaxInfo;

        private static string[] SingleChartAxisZMinInfo;

        private static string[] SingleChartAxisZMaxInfo;

        private static string[] CombinerChartTypeInfo;

        private static string[] AnalysisGraphChartTypeInfo;

        private static string[] RecorderGraphChartTypeInfo;

        private static string[] NormByTimeInfo;

        private static string[] NormByTimeCountStoredDataInfo;

        private static string[] NormByTimeCountDataAverageInfo;

        private static string[] NormMedianInfo;

        private static string[] NormMedianRangeInfo;

        private static string[] NormMedianIterationInfo;

        private static string[] NormTriangleInfo;

        private static string[] NormTriangleRangeInfo;

        private static string[] NormTriangleIterationInfo;

        private static string[] DistortionAngleXInfo;

        private static string[] DistortionAngleYInfo;

        private static string[] YInfo;

        private static string[] ZInfo;

        private static string[] MirrorInfo;

        private static string[] AdjustmentInfo;

        private static string[] MinThresInfo;

        private static string[] MaxThresInfo;

        private static string[] MinSensorRangeInfo;

        private static string[] MaxSensorRangeInfo;

        private static string[] DisplayScannerInfo;

        public Scanner Scanner1
        { get; set; }

        public Scanner Scanner2
        { get; set; }

        private Thread ScReceive1
        { get; set; }

        private Thread ScReceive2
        { get; set; }

        private Logger LogFiler
        { get; set; }

        private Recorder DataRecorder1
        { get; set; }

        private Recorder DataRecorder2
        { get; set; }

        private Analysisframe Analysis
        { get; set; }

        private Setupframe Setup
        { get; set; }

        private static OpcClient PlcClient
        { get; set; }

        private Dictionary<Scanner, Chart> _dicScannerInfo
        { get; set; }

        private Dictionary<Scanner, Recorder> _dicRecorderInfo
        { get; set; }

        public static string ServerIp
        { get; set; }

        public static string AnalyseType
        { get; set; }

        private static int[] ScannerTag
        { get; set; }

        public static int[] Port
        { get; set; }

        public static SeriesChartType[] SingleChartTypes
        { get; set; }

        public static int[] SingleChartAxisYMin
        { get; set; }

        public static int[] SingleChartAxisYMax
        { get; set; }

        public static int[] SingleChartAxisZMin
        { get; set; }

        public static int[] SingleChartAxisZMax
        { get; set; }

        public static SeriesChartType[] CombinerChartType
        { get; set; }

        public static int CombinerChartAxisYMin
        { get; set; }

        public static int CombinerChartAxisYMax
        { get; set; }

        public static int CombinerChartAxisZMin
        { get; set; }

        public static int CombinerChartAxisZMax
        { get; set; }

        public static SeriesChartType[] AnalysisGraphChartType
        { get; set; }

        public static int AnalysisGraphChartAxisYMin
        { get; set; }

        public static int AnalysisGraphChartAxisYMax
        { get; set; }

        public static int AnalysisGraphChartAxisZMin
        { get; set; }

        public static int AnalysisGraphChartAxisZMax
        { get; set; }

        public static SeriesChartType[] RecorderGraphChartType
        { get; set; }

        public static int RecorderGraphChartAxisYMin
        { get; set; }

        public static int RecorderGraphChartAxisYMax
        { get; set; }

        public static int RecorderGraphChartAxisZMin
        { get; set; }

        public static int RecorderGraphChartAxisZMax
        { get; set; }

        private static int AnalyserGraphTimer
        { get; set; }

        public static int AnalyserTimer
        { get; set; }

        public static bool[] IsNormalizedbyTime
        { get; set; }

        public static bool[] IsNormalizedbyMedian
        { get; set; }

        public static bool[] IsNormalizedbyTriangle
        { get; set; }

        public static int[] NormalizebyTimeCountStoredData
        { get; set; }

        public static int[] NormalizebyTimeCountDataAverage
        { get; set; }

        public static int[] NormalizeMedianRange
        { get; set; }

        public static int[] NormalizeMedianIteration
        { get; set; }

        public static int[] NormalizeTriangleRange
        { get; set; }

        public static int[] NormalizeTriangleIteration
        { get; set; }

        public static int[] NormalizeDistortionYAngle
        { get; set; }

        public static int[] NormalizeDistortionZAngle
        { get; set; }

        public static int[] NormalizeY
        { get; set; }

        public static int[] NormalizeZ
        { get; set; }

        public static int[] DistanceMinThreshold
        { get; set; }

        public static int[] DistanceMaxThreshold
        { get; set; }

        private static int[] MinSensorRange
        { get; set; }

        private static int[] MaxSensorRange
        { get; set; }

        public static double AnalysizedDeviation
        { get; set; }

        public static int DistanceGapThreshold
        { get; set; }

        public static int AnalysizedPoints
        { get; set; }

        public static int AnalysizedPointsPitch
        { get; set; }

        public static int AnalysizedAngleLow
        { get; set; }

        public static int AnalysizedAngleHigh
        { get; set; }

        public static int AnalysizedAngleEdge
        { get; set; }

        public static int TongOffset
        { get; set; }

        public static int EdgeOffset
        { get; set; }

        public static int PileTiltOffset
        { get; set; }

        public static int AnalysizedYDeviationEdge
        { get; set; }

        public static int AnalysizedZDeviationEdge
        { get; set; }

        public static int AnalysizedYThresholdObject
        { get; set; }

        public static int AnalysizedZThresholdObject
        { get; set; }

        public static int AnalysizedAreaCorrectionThreshold
        { get; set; }

        public static int AnalysingZBand
        { get; set; }

        public static int AnalysingYBand
        { get; set; }

        public static int ScannerOffset
        { get; set; }

        private static int RecordSpeed
        { get; set; }

        public static bool[] NormalizeMirror
        { get; set; }

        public static bool[] NormalizeAdjustment
        { get; set; }

        private static bool[] IsDisplayScanner
        { get; set; }

        public static bool IsNormalizingYAutomatic
        { get; set; }

        public static bool IsMatchingCoordinateSystem
        { get; set; }

        private static bool IsPlayingRecord
        { get; set; }

        private static bool IsDisplayCombined
        { get; set; }

        private static bool IsRecorderAnalysed
        { get; set; } = false;

        private List<List<Tuple<int, int, int>>> ReducedPointsofInterest
        { get; set; }

        private List<Tuple<int, int, int>> EdgePoints
        { get; set; }

        private static List<Tuple<int, int, int>>[] SplitNormData
        { get; set; }

        public static List<Tuple<int, int, int>> CombinedNormData
        { get; set; }

        private static List<List<Tuple<int, int, int>>> RecorderAnalyse
        { get; set; }

        private static List<List<List<Tuple<int, int, int>>>> RecorderReducedPoints
        { get; set; }

        private static List<List<Tuple<int, int, int>>> RecorderEdgePoints
        { get; set; } = new List<List<Tuple<int, int, int>>>();

        private System.Threading.Timer Raw
        { get; set; }

        private System.Threading.Timer Normalized
        { get; set; }

        private System.Threading.Timer CombinedData
        { get; set; }

        private System.Threading.Timer CombinerGraph
        { get; set; }

        private System.Threading.Timer AnalyserGraph
        { get; set; }

        private System.Threading.Timer Analyser
        { get; set; }

        private System.Threading.Timer RecordChecker
        { get; set; }

        private System.Threading.Timer Recorder
        { get; set; }

        private System.Threading.Timer RecordPlayer
        { get; set; }

        public Mainframe(User user)
        {
            InitializeComponent();

            Text = ConfigurationManager.AppSettings["AppName"];

            LogFiler = new Logger("Mainframe");
            PlcClient = new OpcClient();

            SplitNormData = new List<Tuple<int, int, int>>[2];
            SplitNormData.Initialize();
            CombinedNormData = new List<Tuple<int, int, int>>();

            // get data from the appconfig
            InitConfig();

            // initalize the values to the necessary values
            LoadConfig();

            // initialize the eventhandler for the textboxes
            InitEvents();

            //Metafile picture = new Metafile(@"‪C:\Users\Alexander Heinze\Desktop\ss.png");

            if (user == User.Undef || user == User.Oberserver)
            {
                RestrictUser();
            }
            else if (user == User.Admin)
            {
                RestrictAdmin();
            }
        }

        private void RestrictUser()
        {
            tabControlCharts.TabPages.Remove(tabPageSingleScanner);
            tabControlCharts.TabPages.Remove(tabPageCombinedScanner);
            buttonSaveConfig.Visible = false;
            buttonSetup.Visible = false;
        }

        private void RestrictAdmin()
        {
            tabControlCharts.TabPages.Remove(tabPageSingleScanner);
        }

        private void InitConfig()
        {
            try
            {
                TagInfo = ConfigurationManager.AppSettings["Scanner.Tag"].Split(';');
                ScannerTag = new int[TagInfo.Length];

                for (int i = 0; i < TagInfo.Length; i++)
                {
                    try
                    {
                        ScannerTag[i] = int.Parse(TagInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                ServerIp = ConfigurationManager.AppSettings["Scanner.Server.Ip"];
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                PortInfo = ConfigurationManager.AppSettings["Scanner.Server.Ports"].Split(';');
                Port = new int[PortInfo.Length];

                for (int i = 0; i < PortInfo.Length; i++)
                {
                    try
                    {
                        Port[i] = int.Parse(PortInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                SingleChartTypesInfo = ConfigurationManager.AppSettings["Scanner.Chart.Type"].Split(';');
                SingleChartTypes = new SeriesChartType[SingleChartTypesInfo.Length];

                for (int i = 0; i < SingleChartTypesInfo.Length; i++)
                {
                    try
                    {
                        SingleChartTypes[i] = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), SingleChartTypesInfo[i]);
                    }
                    catch (ArgumentException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (ConfigurationErrorsException ceex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ceex.Message);
            }

            try
            {
                SingleChartAxisYMinInfo = ConfigurationManager.AppSettings["Scanner.Chart.Axis.Y.Min"].Split(';');
                SingleChartAxisYMin = new int[SingleChartAxisYMinInfo.Length];

                for (int i = 0; i < SingleChartAxisYMinInfo.Length; i++)
                {
                    try
                    {
                        SingleChartAxisYMin[i] = int.Parse(SingleChartAxisYMinInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                SingleChartAxisYMaxInfo = ConfigurationManager.AppSettings["Scanner.Chart.Axis.Y.Max"].Split(';');
                SingleChartAxisYMax = new int[SingleChartAxisYMaxInfo.Length];

                for (int i = 0; i < SingleChartAxisYMaxInfo.Length; i++)
                {
                    try
                    {
                        SingleChartAxisYMax[i] = int.Parse(SingleChartAxisYMaxInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                SingleChartAxisZMinInfo = ConfigurationManager.AppSettings["Scanner.Chart.Axis.Z.Min"].Split(';');
                SingleChartAxisZMin = new int[SingleChartAxisZMinInfo.Length];

                for (int i = 0; i < SingleChartAxisZMinInfo.Length; i++)
                {
                    try
                    {
                        SingleChartAxisZMin[i] = int.Parse(SingleChartAxisZMinInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                SingleChartAxisZMaxInfo = ConfigurationManager.AppSettings["Scanner.Chart.Axis.Z.Max"].Split(';');
                SingleChartAxisZMax = new int[SingleChartAxisZMaxInfo.Length];

                for (int i = 0; i < SingleChartAxisZMaxInfo.Length; i++)
                {
                    try
                    {
                        SingleChartAxisZMax[i] = int.Parse(SingleChartAxisZMaxInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                CombinerChartTypeInfo = ConfigurationManager.AppSettings["Combiner.Chart.Type"].Split(';');
                CombinerChartType = new SeriesChartType[CombinerChartTypeInfo.Length];

                for (int i = 0; i < CombinerChartTypeInfo.Length; i++)
                {
                    try
                    {
                        CombinerChartType[i] = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), CombinerChartTypeInfo[i]);
                    }
                    catch (ArgumentException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (ConfigurationErrorsException ceex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ceex.Message);
            }

            try
            {
                CombinerChartAxisYMin = int.Parse(ConfigurationManager.AppSettings["Combiner.Chart.Axis.Y.Min"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                CombinerChartAxisYMax = int.Parse(ConfigurationManager.AppSettings["Combiner.Chart.Axis.Y.Max"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                CombinerChartAxisZMin = int.Parse(ConfigurationManager.AppSettings["Combiner.Chart.Axis.Z.Min"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                CombinerChartAxisZMax = int.Parse(ConfigurationManager.AppSettings["Combiner.Chart.Axis.Z.Max"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                AnalysisGraphChartTypeInfo = ConfigurationManager.AppSettings["Analyser.Chart.Type"].Split(';');
                AnalysisGraphChartType = new SeriesChartType[AnalysisGraphChartTypeInfo.Length];

                for (int i = 0; i < AnalysisGraphChartTypeInfo.Length; i++)
                {
                    try
                    {
                        AnalysisGraphChartType[i] = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), AnalysisGraphChartTypeInfo[i]);
                    }
                    catch (ArgumentException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (ConfigurationErrorsException ceex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ceex.Message);
            }

            try
            {
                AnalysisGraphChartAxisYMin = int.Parse(ConfigurationManager.AppSettings["Analyser.Chart.Axis.Y.Min"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                AnalysisGraphChartAxisYMax = int.Parse(ConfigurationManager.AppSettings["Analyser.Chart.Axis.Y.Max"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                AnalysisGraphChartAxisZMin = int.Parse(ConfigurationManager.AppSettings["Analyser.Chart.Axis.Z.Min"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                AnalysisGraphChartAxisZMax = int.Parse(ConfigurationManager.AppSettings["Analyser.Chart.Axis.Z.Max"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                AnalyserGraphTimer = int.Parse(ConfigurationManager.AppSettings["Analyser.Timer"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                RecorderGraphChartTypeInfo = ConfigurationManager.AppSettings["Recorder.Chart.Type"].Split(';');
                RecorderGraphChartType = new SeriesChartType[RecorderGraphChartTypeInfo.Length];

                for (int i = 0; i < RecorderGraphChartTypeInfo.Length; i++)
                {
                    try
                    {
                        RecorderGraphChartType[i] = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), RecorderGraphChartTypeInfo[i]);
                    }
                    catch (ArgumentException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                RecorderGraphChartAxisYMin = int.Parse(ConfigurationManager.AppSettings["Recorder.Chart.Axis.Y.Min"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                RecorderGraphChartAxisYMax = int.Parse(ConfigurationManager.AppSettings["Recorder.Chart.Axis.Y.Max"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                RecorderGraphChartAxisZMin = int.Parse(ConfigurationManager.AppSettings["Recorder.Chart.Axis.Z.Min"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                RecorderGraphChartAxisZMax = int.Parse(ConfigurationManager.AppSettings["Recorder.Chart.Axis.Z.Max"]);
            }
            catch (ArgumentNullException aex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
            }

            try
            {
                DisplayScannerInfo = ConfigurationManager.AppSettings["ContentGraph.Scanner"].Split(';');
                IsDisplayScanner = new bool[DisplayScannerInfo.Length];

                for (int i = 0; i < DisplayScannerInfo.Length; i++)
                {
                    try
                    {
                        IsDisplayScanner[i] = bool.Parse(DisplayScannerInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                IsDisplayCombined = bool.Parse(ConfigurationManager.AppSettings["ContentGraph.Combined"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormByTimeInfo = ConfigurationManager.AppSettings["Normalizing.ByTime"].Split(';');
                IsNormalizedbyTime = new bool[NormByTimeInfo.Length];

                for (int i = 0; i < NormByTimeInfo.Length; i++)
                {
                    try
                    {
                        IsNormalizedbyTime[i] = bool.Parse(NormByTimeInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormByTimeCountStoredDataInfo = ConfigurationManager.AppSettings["Normalizing.ByTime.CountStoredData"].Split(';');
                NormalizebyTimeCountStoredData = new int[NormByTimeCountStoredDataInfo.Length];

                for (int i = 0; i < NormByTimeCountStoredDataInfo.Length; i++)
                {
                    try
                    {
                        NormalizebyTimeCountStoredData[i] = int.Parse(NormByTimeCountStoredDataInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormByTimeCountDataAverageInfo = ConfigurationManager.AppSettings["Normalizing.ByTime.CountDataAverage"].Split(';');
                NormalizebyTimeCountDataAverage = new int[NormByTimeCountDataAverageInfo.Length];

                for (int i = 0; i < NormByTimeCountDataAverageInfo.Length; i++)
                {
                    try
                    {
                        NormalizebyTimeCountDataAverage[i] = int.Parse(NormByTimeCountDataAverageInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormMedianInfo = ConfigurationManager.AppSettings["Normalizing.Median"].Split(';');
                IsNormalizedbyMedian = new bool[NormMedianInfo.Length];

                for (int i = 0; i < NormMedianInfo.Length; i++)
                {
                    try
                    {
                        IsNormalizedbyMedian[i] = bool.Parse(NormMedianInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormMedianRangeInfo = ConfigurationManager.AppSettings["Normalizing.Median.Range"].Split(';');
                NormalizeMedianRange = new int[NormMedianRangeInfo.Length];

                for (int i = 0; i < NormMedianRangeInfo.Length; i++)
                {
                    try
                    {
                        NormalizeMedianRange[i] = int.Parse(NormMedianRangeInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormMedianIterationInfo = ConfigurationManager.AppSettings["Normalizing.Median.Iteration"].Split(';');
                NormalizeMedianIteration = new int[NormMedianIterationInfo.Length];

                for (int i = 0; i < NormMedianIterationInfo.Length; i++)
                {
                    try
                    {
                        NormalizeMedianIteration[i] = int.Parse(NormMedianIterationInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormTriangleInfo = ConfigurationManager.AppSettings["Normalizing.Triangle"].Split(';');
                IsNormalizedbyTriangle = new bool[NormTriangleInfo.Length];

                for (int i = 0; i < NormTriangleInfo.Length; i++)
                {
                    try
                    {
                        IsNormalizedbyTriangle[i] = bool.Parse(NormTriangleInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormTriangleRangeInfo = ConfigurationManager.AppSettings["Normalizing.Triangle.Range"].Split(';');
                NormalizeTriangleRange = new int[NormTriangleRangeInfo.Length];

                for (int i = 0; i < NormTriangleRangeInfo.Length; i++)
                {
                    try
                    {
                        NormalizeTriangleRange[i] = int.Parse(NormTriangleRangeInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                NormTriangleIterationInfo = ConfigurationManager.AppSettings["Normalizing.Triangle.Iteration"].Split(';');
                NormalizeTriangleIteration = new int[NormTriangleIterationInfo.Length];

                for (int i = 0; i < NormTriangleIterationInfo.Length; i++)
                {
                    try
                    {
                        NormalizeTriangleIteration[i] = int.Parse(NormTriangleIterationInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                DistortionAngleXInfo = ConfigurationManager.AppSettings["Normalizing.Graph.Angle.Distortion.X"].Split(';');
                NormalizeDistortionYAngle = new int[DistortionAngleXInfo.Length];

                for (int i = 0; i < DistortionAngleXInfo.Length; i++)
                {
                    try
                    {
                        NormalizeDistortionYAngle[i] = int.Parse(DistortionAngleXInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                DistortionAngleYInfo = ConfigurationManager.AppSettings["Normalizing.Graph.Angle.Distortion.Y"].Split(';');
                NormalizeDistortionZAngle = new int[DistortionAngleYInfo.Length];

                for (int i = 0; i < DistortionAngleYInfo.Length; i++)
                {
                    try
                    {
                        NormalizeDistortionZAngle[i] = int.Parse(DistortionAngleYInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                YInfo = ConfigurationManager.AppSettings["Normalizing.Value.Offset.Y"].Split(';');
                NormalizeY = new int[YInfo.Length];

                for (int i = 0; i < YInfo.Length; i++)
                {
                    try
                    {
                        NormalizeY[i] = int.Parse(YInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                ZInfo = ConfigurationManager.AppSettings["Normalizing.Value.Offset.Z"].Split(';');
                NormalizeZ = new int[ZInfo.Length];

                for (int i = 0; i < ZInfo.Length; i++)
                {
                    try
                    {
                        NormalizeZ[i] = int.Parse(ZInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                MirrorInfo = ConfigurationManager.AppSettings["Normalizing.Mirror"].Split(';');
                NormalizeMirror = new bool[MirrorInfo.Length];

                for (int i = 0; i < MirrorInfo.Length; i++)
                {
                    try
                    {
                        NormalizeMirror[i] = bool.Parse(MirrorInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AdjustmentInfo = ConfigurationManager.AppSettings["Normalizing.Adjustment"].Split(';');
                NormalizeAdjustment = new bool[AdjustmentInfo.Length];

                for (int i = 0; i < AdjustmentInfo.Length; i++)
                {
                    try
                    {
                        NormalizeAdjustment[i] = bool.Parse(AdjustmentInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedPoints = int.Parse(ConfigurationManager.AppSettings["Analysing.Points"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedPointsPitch = int.Parse(ConfigurationManager.AppSettings["Analysing.Points.Pitch"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedDeviation = double.Parse(ConfigurationManager.AppSettings["Analysing.Value.Deviation"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedYDeviationEdge = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Y.Deviation.Edges"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedZDeviationEdge = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Z.Deviation.Edges"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedYThresholdObject = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Y.Threshold.Object"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedZThresholdObject = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Z.Threshold.Object"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedAreaCorrectionThreshold = int.Parse(ConfigurationManager.AppSettings["Analysing.Correction.Threshold"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysingZBand = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Band.Z"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysingYBand = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Band.Y"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedAngleLow = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Angle.Low"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedAngleHigh = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Angle.High"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalysizedAngleEdge = int.Parse(ConfigurationManager.AppSettings["Analysing.Value.Angle.Edge"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalyserTimer = int.Parse(ConfigurationManager.AppSettings["Analysing.Timer"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                MinThresInfo = ConfigurationManager.AppSettings["Distance.Min.Threshold"].Split(';');
                DistanceMinThreshold = new int[MinThresInfo.Length];

                for (int i = 0; i < MinThresInfo.Length; i++)
                {
                    try
                    {
                        DistanceMinThreshold[i] = int.Parse(MinThresInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                MaxThresInfo = ConfigurationManager.AppSettings["Distance.Max.Threshold"].Split(';');
                DistanceMaxThreshold = new int[MaxThresInfo.Length];

                for (int i = 0; i < MaxThresInfo.Length; i++)
                {
                    try
                    {
                        DistanceMaxThreshold[i] = int.Parse(MaxThresInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                DistanceGapThreshold = int.Parse(ConfigurationManager.AppSettings["Distance.Threshold.Gap"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                MinSensorRangeInfo = ConfigurationManager.AppSettings["Distance.Sensorrange.Min"].Split(';');
                MinSensorRange = new int[MinSensorRangeInfo.Length];

                for (int i = 0; i < MinSensorRangeInfo.Length; i++)
                {
                    try
                    {
                        MinSensorRange[i] = int.Parse(MinSensorRangeInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                MaxSensorRangeInfo = ConfigurationManager.AppSettings["Distance.Sensorrange.Max"].Split(';');
                MaxSensorRange = new int[MaxSensorRangeInfo.Length];

                for (int i = 0; i < MaxSensorRangeInfo.Length; i++)
                {
                    try
                    {
                        MaxSensorRange[i] = int.Parse(MaxSensorRangeInfo[i]);
                    }
                    catch (ArgumentNullException aex)
                    {
                        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + aex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                IsMatchingCoordinateSystem = bool.Parse(ConfigurationManager.AppSettings["General.MatchingCoordinateSystem"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                AnalyseType = ConfigurationManager.AppSettings["General.Analysing"];

                if ((AnalyseType != "Lift") && (AnalyseType != "Release") && (AnalyseType != ""))
                {
                    LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": wrong AnalyseType!");
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                TongOffset = int.Parse(ConfigurationManager.AppSettings["General.Tong.Offset"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                EdgeOffset = int.Parse(ConfigurationManager.AppSettings["General.Edge.Offset"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                PileTiltOffset = int.Parse(ConfigurationManager.AppSettings["General.PileTilt.Offset"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                IsNormalizingYAutomatic = bool.Parse(ConfigurationManager.AppSettings["General.Scanner.Offset"]);

                if (IsNormalizingYAutomatic)
                {
                    textBoxOffsetY2.Enabled = false;
                    textBoxCombinerOffsetY2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                ScannerOffset = int.Parse(ConfigurationManager.AppSettings["General.Scanner.Offset.Value"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                RecordSpeed = int.Parse(ConfigurationManager.AppSettings["Record.Speed"]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void InitEvents()
        {
            textBoxOffsetY1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxOffsetY2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxOffsetZ1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxOffsetZ2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerOffsetY1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerOffsetZ1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerOffsetY2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerOffsetZ2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxFilterbyMedianCompareDataScanner1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxFilterbyMedianIterationScanner1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxFilterbyMedianCompareDataScanner2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxFilterbyMedianIterationScanner2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyMedianComparedDataScanner1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyMedianIterationScanner1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyMedianComparedDataScanner2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyMedianIterationScanner2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyTriangleComparedDataScanner1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyTriangleComparedDataScanner2.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyTriangleIterationScanner1.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerFilterbyTriangleIterationScanner2.TextChanged += new EventHandler(OnValueChangeTextBox);
            chartCombiner.MouseWheel += new MouseEventHandler(chart_MouseWheel);
            chartAnalyser.MouseWheel += new MouseEventHandler(chart_MouseWheel);
            textBoxRecorderRecordSpeed.TextChanged += new EventHandler(OnValueChangeTextBox);
        }

        private void Mainframe_Shown(object sender, EventArgs e)
        {
            Scanner1 = new Scanner(ScannerTag[0]/*, chart1*/);
            Scanner2 = new Scanner(ScannerTag[1]/*, chart2*/);

            DataRecorder1 = new Recorder(Scanner1.Name);
            DataRecorder2 = new Recorder(Scanner2.Name);

            _dicScannerInfo = new Dictionary<Scanner, Chart>
            {
                { Scanner1, chartScanner1 },
                { Scanner2, chartScanner2 }
            };

            ScReceive1 = new Thread(Scanner1.ProcessReceive);
            ScReceive2 = new Thread(Scanner2.ProcessReceive);

            ScReceive1.Start();
            ScReceive2.Start();

            _dicRecorderInfo = new Dictionary<Scanner, Recorder>
            {
                { Scanner1, DataRecorder1 },
                { Scanner2, DataRecorder2 }
            };

            CombineData(Scanner1);
            CombineData(Scanner2);

            //Raw = new System.Threading.Timer(OnDrawRaw, this, 1000, Timeout.Infinite);
            //Normalized = new System.Threading.Timer(OnDrawNormalized, this, 1000, Timeout.Infinite);
            //CombinedData = new System.Threading.Timer(OnCombineSplitNormData, this, 200, Timeout.Infinite);
            CombinerGraph = new System.Threading.Timer(OnDrawCombiner, this, 1000, Timeout.Infinite);
            AnalyserGraph = new System.Threading.Timer(OnDrawAnalyser, this, 1000, Timeout.Infinite);
            Analyser = new System.Threading.Timer(OnAnalysis, this, AnalyserTimer, Timeout.Infinite); 
        }

        private void CombineData(Scanner sc)
        {
            sc.ScannerInputNormalized += new Scanner.ScannerDataInput(CombineNormData);
        }

        private void CombineNormData(object sender, List<Tuple<int, int, int>> normData)
        {
            Scanner sc = (Scanner)sender;

            SplitNormData[sc.Tag] = normData;

            CombineData();
        }

        private void OnCombineSplitNormData(object sender)
        {
            CombinedData.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                CombineData();
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            CombinedData.Change(500, Timeout.Infinite);
        }

        private void CombineData()
        {
            try
            {
                List<Tuple<int, int, int>> combinedData = new List<Tuple<int, int, int>>();

                if (SplitNormData[0]?.Count != null)
                {
                    if (SplitNormData[0]?.Count != 0)
                    {
                        List<Tuple<int, int, int>> SplitScanner;

                        if (!NormalizeMirror[0])
                        {
                            SplitScanner = new List<Tuple<int, int, int>>(SplitNormData[0].Where(x => (x.Item1 < MinSensorRange[0]) && (x.Item1 > MaxSensorRange[0])));
                        }
                        else
                        {
                            SplitScanner = new List<Tuple<int, int, int>>(SplitNormData[0].Where(x => (x.Item1 > MinSensorRange[1]) && (x.Item1 < MaxSensorRange[1])));
                        }

                        combinedData.AddRange(SplitScanner);
                    }
                }

                if (SplitNormData[1]?.Count != null)
                {
                    if (SplitNormData[1]?.Count != 0)
                    {
                        List<Tuple<int, int, int>> SplitScanner;

                        if (!NormalizeMirror[1])
                        {
                            SplitScanner = new List<Tuple<int, int, int>>(SplitNormData[1].Where(x => (x.Item1 > MinSensorRange[1]) && (x.Item1 < MaxSensorRange[1])));
                        }
                        else
                        {
                            SplitScanner = new List<Tuple<int, int, int>>(SplitNormData[1].Where(x => (x.Item1 < MinSensorRange[0]) && (x.Item1 > MaxSensorRange[0])));
                        }

                        combinedData.AddRange(SplitScanner);
                    }
                }

                CombinedNormData = combinedData;
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void OnRecorder(object sender)
        {
            Recorder.Change(Timeout.Infinite, Timeout.Infinite);
            Recorder.Dispose();
            Recorder = null;

            InitTrackbar();
        }

        private void InitTrackbar()
        {
            if (DataRecorder1?.RecordData?.Count != 0 && DataRecorder2?.RecordData?.Count != 0)
            {
                if (DataRecorder1.RecordData.Count == DataRecorder2.RecordData.Count)
                {
                    RefreshTrackBarRecorder(DataRecorder1.RecordData.Count - 1);
                }
                else if (DataRecorder1.RecordData.Count > DataRecorder2.RecordData.Count)
                {
                    RefreshTrackBarRecorder(DataRecorder2.RecordData.Count - 1);
                }
                else if (DataRecorder2.RecordData.Count > DataRecorder1.RecordData.Count)
                {
                    RefreshTrackBarRecorder(DataRecorder1.RecordData.Count - 1);
                }
            }

            if (DataRecorder1?.RecordData != null)
            {
                if (DataRecorder1.RecordData.Count != 0)
                {
                    RefreshTrackBarRecorder(DataRecorder1.RecordData.Count - 1);
                }
            }

            if (DataRecorder2?.RecordData != null)
            {
                if (DataRecorder2.RecordData.Count != 0)
                {
                    RefreshTrackBarRecorder(DataRecorder2.RecordData.Count - 1);
                }
            }
            else
            {
                if (DataRecorder1?.RecordData != null)
                {
                    if (DataRecorder1.RecordData.Count != 0)
                    {
                        RefreshTrackBarRecorder(DataRecorder1.RecordData.Count - 1);
                    }
                }

                if (DataRecorder2?.RecordData != null)
                {
                    if (DataRecorder2.RecordData.Count != 0)
                    {
                        RefreshTrackBarRecorder(DataRecorder2.RecordData.Count - 1);
                    }
                }
            }

            ActivatePlayerFunction();
        }

        private void ActivatePlayerFunction()
        {
            Invoke(new Action(() =>
            {
                buttonRecorderPlay.Enabled = true;
                buttonRecorderStop.Enabled = true;
                textBoxRecorderRecordSpeed.Enabled = true;
            }));
        }

        private bool BothInitialized()
        {
            bool AreInitialized = false;
            int cnt = 0;

            foreach (KeyValuePair<Scanner, Recorder> rc in _dicRecorderInfo)
            {
                if (rc.Value.RecordData != null)
                {
                    cnt++;
                }
            }

            if (cnt == _dicRecorderInfo.Count)
            {
                AreInitialized = true;
            }

            return AreInitialized;
        }

        private void RefreshTrackBarRecorder(int maxCount)
        {
            Invoke(new Action(() =>
            {
                trackBarRecorder.Maximum = maxCount;
                trackBarRecorder.Value = 0;
            }));
        }

        private void DrawRaw()
        {
            foreach (KeyValuePair<Scanner, Chart> info in _dicScannerInfo)
            {
                RefreshChartRaw(info.Key, info.Value);
            }
        }

        private void RefreshChartRaw(Scanner scanner, Chart chart)
        {
            FillChart(scanner, chart, "Raw");
        }

        private void DrawNormalized()
        {
            foreach (KeyValuePair<Scanner, Chart> info in _dicScannerInfo)
            {
                RefreshChartNormalized(info.Key, info.Value);
            }
        }

        private void RefreshChartNormalized(Scanner scanner, Chart chart)
        {
            FillChart(scanner, chart, "Normalized");
        }

        private void FillChart(Scanner scanner, Chart chart, string series)
        {
            try
            {
                if (chart.InvokeRequired)
                {
                    FillChartCallback test = new FillChartCallback(FillChart);
                    chart.Invoke(test, new object[] { scanner, chart, series });
                }
                else
                {
                    chart.Series[series].Points.Clear();

                    if (series == "Raw")
                    {
                        if (scanner.XYCoordinate.Count != 0)
                        {
                            for (int i = 0; i < scanner.XYCoordinate?.Count; i++)
                            {
                                chart.Series[series].Points.AddXY(scanner.XYCoordinate[i].Item1, scanner.XYCoordinate[i].Item2);
                            }
                        }
                    }
                    else if (series == "Normalized")
                    {
                        if (scanner.NormalizedData.Count != 0)
                        {
                            for (int i = 0; i < scanner.NormalizedData?.Count; i++)
                            {
                                chart.Series[series].Points.AddXY(scanner.NormalizedData[i].Item1, scanner.NormalizedData[i].Item2);
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

        private void DrawCombiner()
        {
            try
            {
                if (chartCombiner.InvokeRequired)
                {
                    FillCombinerChartCallback test = new FillCombinerChartCallback(DrawCombiner);
                    chartCombiner.Invoke(test, new object[] { });
                }
                else
                {
                    chartCombiner.Series["Scanner1 Data"].Points.Clear();
                    chartCombiner.Series["Scanner2 Data"].Points.Clear();
                    chartCombiner.Series["Scanner1"].Points.Clear();
                    chartCombiner.Series["Scanner2"].Points.Clear();

                    if (Scanner1.NormalizedData.Count != 0)
                    {
                        List<Tuple<int, int, int>> normData = new List<Tuple<int, int, int>>(Scanner1.NormalizedData.ToList());

                        for (int i = 0; i < normData?.Count; i++)
                        {
                            chartCombiner.Series["Scanner1 Data"].Points.AddXY(normData[i].Item1, normData[i].Item2);
                        }
                    }

                    if (Scanner2.NormalizedData.Count != 0)
                    {
                        List<Tuple<int, int, int>> normData = new List<Tuple<int, int, int>>(Scanner2.NormalizedData.ToList());

                        for (int i = 0; i < normData?.Count; i++)
                        {
                            chartCombiner.Series["Scanner2 Data"].Points.AddXY(normData[i].Item1, normData[i].Item2);
                        }
                    }

                    Invoke(new Action(() =>
                    {
                        if (!NormalizeMirror[0])
                        {
                            chartAnalyser.Series["Scanner1"].Points.AddXY(NormalizeY[0], -NormalizeZ[0]);
                        }
                        else
                        {
                            chartAnalyser.Series["Scanner1"].Points.AddXY(NormalizeY[1], -NormalizeZ[1]);
                        }

                        if (!NormalizeMirror[1])
                        {
                            chartAnalyser.Series["Scanner2"].Points.AddXY(NormalizeY[1], -NormalizeZ[1]);
                        }
                        else
                        {
                            chartAnalyser.Series["Scanner2"].Points.AddXY(NormalizeY[0], -NormalizeZ[0]);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
                return;
            }
        }

        private void DrawAnalyser()
        {
            try
            {
                if (chartAnalyser.InvokeRequired)
                {
                    FillAnalyserChartCallback test = new FillAnalyserChartCallback(DrawAnalyser);
                    chartAnalyser.Invoke(test, new object[] { });
                }
                else
                {
                    chartAnalyser.Series["Analysis Graph"].Points.Clear();
                    chartAnalyser.Series["Points of Interest"].Points.Clear();
                    chartAnalyser.Series["Scanner1 Data"].Points.Clear();
                    chartAnalyser.Series["Scanner2 Data"].Points.Clear();
                    chartAnalyser.Series["Scanner1"].Points.Clear();
                    chartAnalyser.Series["Scanner2"].Points.Clear();
                    chartAnalyser.Series["Tong"].Points.Clear();
                    chartAnalyser.Series["Edges"].Points.Clear();

                    if (IsDisplayCombined)
                    {
                        if (CombinedNormData.Count != 0)
                        {
                            for (int i = 0; i < CombinedNormData?.Count; i++)
                            {
                                chartAnalyser.Series["Analysis Graph"].Points.AddXY(CombinedNormData[i].Item1, CombinedNormData[i].Item2);
                            }
                        }
                    }

                    if (ReducedPointsofInterest != null)
                    {
                        for (int j = 0; j < ReducedPointsofInterest?.Count; j++)
                        {
                            for (int i = 0; i < ReducedPointsofInterest[j]?.Count; i++)
                            {
                                chartAnalyser.Series["Points of Interest"].Points.AddXY(ReducedPointsofInterest[j][i].Item1, ReducedPointsofInterest[j][i].Item2);
                            }
                        }
                    }

                    if (EdgePoints != null)
                    {
                        for (int i = 0; i < EdgePoints.Count; i++)
                        {
                            chartAnalyser.Series["Edges"].Points.AddXY(EdgePoints[i].Item1, EdgePoints[i].Item2);
                        }
                    }

                    if (IsDisplayScanner[int.Parse(checkBoxScanner1.Tag.ToString())])
                    {
                        if (Scanner1.NormalizedData.Count != 0)
                        {
                            for (int i = 0; i < Scanner1.NormalizedData.Count; i++)
                            {
                                chartAnalyser.Series["Scanner1 Data"].Points.AddXY(Scanner1.NormalizedData[i].Item1, Scanner1.NormalizedData[i].Item2);
                            }
                        }
                    }

                    if (IsDisplayScanner[int.Parse(checkBoxScanner2.Tag.ToString())])
                    {
                        if (Scanner2.NormalizedData.Count != 0)
                        {
                            for (int i = 0; i < Scanner2.NormalizedData.Count; i++)
                            {
                                chartAnalyser.Series["Scanner2 Data"].Points.AddXY(Scanner2.NormalizedData[i].Item1, Scanner2.NormalizedData[i].Item2);
                            }
                        }
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        chartAnalyser.Series["Tong"].Points.AddXY(NormalizeY[i], ScannerOffset);
                    }

                    if (!NormalizeMirror[0])
                    {
                        chartAnalyser.Series["Scanner1"].Points.AddXY(NormalizeY[0], -NormalizeZ[0]);
                    }
                    else
                    {
                        chartAnalyser.Series["Scanner1"].Points.AddXY(NormalizeY[1], -NormalizeZ[1]);
                    }

                    if (!NormalizeMirror[1])
                    {
                        chartAnalyser.Series["Scanner2"].Points.AddXY(NormalizeY[1], -NormalizeZ[1]);
                    }
                    else
                    {
                        chartAnalyser.Series["Scanner2"].Points.AddXY(NormalizeY[0], -NormalizeZ[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void OnDrawRaw(object sender)
        {
            Raw.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                DrawRaw();
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            Raw.Change(2000, Timeout.Infinite);
        }

        private void OnDrawNormalized(object sender)
        {
            Normalized.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                DrawNormalized();
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            Normalized.Change(2000, Timeout.Infinite);
        }

        private void OnDrawCombiner(object sender)
        {
            CombinerGraph.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                DrawCombiner();
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            CombinerGraph.Change(2000, Timeout.Infinite);
        }

        private void OnDrawAnalyser(object sender)
        {
            AnalyserGraph.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                DrawAnalyser();
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            AnalyserGraph.Change(2000, Timeout.Infinite);
        }

        private void LoadConfig()
        {
            InitChartType();
            InitChart();
            InitChartControl();
            InitDistortionXAngle();
            InitDistortionYAngle();
            InitXValue();
            InitYValue();
            InitFilter();
            InitFilterValues();
            InitRecorder();
            InitAnalyser();
        }

        private void InitChartType()
        {
            try
            {
                // single charts only include 2 series (raw + normalized)
                for (int i = 0; i < chartScanner1.Series.Count; i++)
                {
                    chartScanner1.Series[i].ChartType = SingleChartTypes[int.Parse(chartScanner1.Tag.ToString())];
                }

                for (int i = 0; i < chartScanner2.Series.Count; i++)
                {
                    chartScanner2.Series[i].ChartType = SingleChartTypes[int.Parse(chartScanner2.Tag.ToString())];
                }

                // combiner chart includes 4 series - only the first 2 series are essential to be changed
                for (int i = 0; i < chartCombiner.Series.Count - 2; i++)
                {
                    chartCombiner.Series[i].ChartType = CombinerChartType[i];
                }

                for (int i = 0; i < 2; i++)
                {
                    chartAnalyser.Series[i].ChartType = AnalysisGraphChartType[i];
                }

                chartAnalyser.Series[2].ChartType = CombinerChartType[0];
                chartAnalyser.Series[3].ChartType = CombinerChartType[1];
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void InitChart()
        {
            try
            {
                chartScanner1.ChartAreas[0].AxisX.Minimum = SingleChartAxisYMin[int.Parse(chartScanner1.Tag.ToString())];
                chartScanner1.ChartAreas[0].AxisX.Maximum = SingleChartAxisYMax[int.Parse(chartScanner1.Tag.ToString())];
                chartScanner1.ChartAreas[0].AxisY.Minimum = SingleChartAxisZMin[int.Parse(chartScanner1.Tag.ToString())];
                chartScanner1.ChartAreas[0].AxisY.Maximum = SingleChartAxisZMax[int.Parse(chartScanner1.Tag.ToString())];
                chartScanner2.ChartAreas[0].AxisX.Minimum = SingleChartAxisYMin[int.Parse(chartScanner2.Tag.ToString())];
                chartScanner2.ChartAreas[0].AxisX.Maximum = SingleChartAxisYMax[int.Parse(chartScanner2.Tag.ToString())];
                chartScanner2.ChartAreas[0].AxisY.Minimum = SingleChartAxisZMin[int.Parse(chartScanner2.Tag.ToString())];
                chartScanner2.ChartAreas[0].AxisY.Maximum = SingleChartAxisZMax[int.Parse(chartScanner2.Tag.ToString())];
                chartCombiner.ChartAreas[0].AxisX.Minimum = CombinerChartAxisYMin;
                chartCombiner.ChartAreas[0].AxisX.Maximum = CombinerChartAxisYMax;
                chartCombiner.ChartAreas[0].AxisY.Minimum = CombinerChartAxisZMin;
                chartCombiner.ChartAreas[0].AxisY.Maximum = CombinerChartAxisZMax;
                chartAnalyser.ChartAreas[0].AxisX.Minimum = AnalysisGraphChartAxisYMin;
                chartAnalyser.ChartAreas[0].AxisX.Maximum = AnalysisGraphChartAxisYMax;
                chartAnalyser.ChartAreas[0].AxisY.Minimum = AnalysisGraphChartAxisZMin;
                chartAnalyser.ChartAreas[0].AxisY.Maximum = AnalysisGraphChartAxisZMax;
                chartAnalyser.Series["Scanner1 Data"].Enabled = IsDisplayScanner[int.Parse(checkBoxScanner1.Tag.ToString())];
                chartAnalyser.Series["Scanner2 Data"].Enabled = IsDisplayScanner[int.Parse(checkBoxScanner2.Tag.ToString())];
                chartAnalyser.Series["Analysis Graph"].Enabled = IsDisplayCombined;
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void InitChartControl()
        {
            checkBoxScanner1.Checked = IsDisplayScanner[int.Parse(checkBoxScanner1.Tag.ToString())];
            checkBoxScanner2.Checked = IsDisplayScanner[int.Parse(checkBoxScanner2.Tag.ToString())];
            checkBoxAnalyse.Checked = IsDisplayCombined;
        }

        private void InitDistortionXAngle()
        {
            trackBarAngleDistortionY1.Value = NormalizeDistortionYAngle[int.Parse(trackBarAngleDistortionY1.Tag.ToString())];
            trackBarAngleDistortionY2.Value = NormalizeDistortionYAngle[int.Parse(trackBarAngleDistortionY2.Tag.ToString())];
            trackBarCombinerDistortionY1.Value = NormalizeDistortionYAngle[int.Parse(trackBarCombinerDistortionY1.Tag.ToString())];
            trackBarCombinerDistortionY2.Value = NormalizeDistortionYAngle[int.Parse(trackBarCombinerDistortionY2.Tag.ToString())];
        }

        private void InitDistortionYAngle()
        {
            trackBarAngleDistortionZ1.Value = NormalizeDistortionZAngle[int.Parse(trackBarAngleDistortionZ1.Tag.ToString())];
            trackBarAngleDistortionZ2.Value = NormalizeDistortionZAngle[int.Parse(trackBarAngleDistortionZ2.Tag.ToString())];
            trackBarCombinerDistortionZ1.Value = NormalizeDistortionZAngle[int.Parse(trackBarCombinerDistortionZ1.Tag.ToString())];
            trackBarCombinerDistortionZ2.Value = NormalizeDistortionZAngle[int.Parse(trackBarCombinerDistortionZ2.Tag.ToString())];
        }

        private void InitXValue()
        {
            textBoxOffsetY1.Text = NormalizeY[int.Parse(textBoxOffsetY1.Tag.ToString())].ToString();
            textBoxOffsetY2.Text = NormalizeY[int.Parse(textBoxOffsetY2.Tag.ToString())].ToString();
            textBoxCombinerOffsetY1.Text = NormalizeY[int.Parse(textBoxCombinerOffsetY1.Tag.ToString())].ToString();
            textBoxCombinerOffsetY2.Text = NormalizeY[int.Parse(textBoxCombinerOffsetY2.Tag.ToString())].ToString();
        }

        private void InitYValue()
        {
            textBoxOffsetZ1.Text = NormalizeZ[int.Parse(textBoxOffsetZ1.Tag.ToString())].ToString();
            textBoxOffsetZ2.Text = NormalizeZ[int.Parse(textBoxOffsetZ2.Tag.ToString())].ToString();
            textBoxCombinerOffsetZ1.Text = NormalizeZ[int.Parse(textBoxCombinerOffsetZ1.Tag.ToString())].ToString();
            textBoxCombinerOffsetZ2.Text = NormalizeZ[int.Parse(textBoxCombinerOffsetZ2.Tag.ToString())].ToString();
        }

        private void InitFilter()
        {
            checkBoxFilterbyTimeScanner1.Checked = IsNormalizedbyTime[int.Parse(checkBoxFilterbyTimeScanner1.Tag.ToString())];
            checkBoxFilterbyTimeScanner2.Checked = IsNormalizedbyTime[int.Parse(checkBoxFilterbyTimeScanner2.Tag.ToString())];
            checkBoxFilterbyMedianScanner1.Checked = IsNormalizedbyMedian[int.Parse(checkBoxFilterbyMedianScanner1.Tag.ToString())];
            checkBoxFilterbyMedianScanner2.Checked = IsNormalizedbyMedian[int.Parse(checkBoxFilterbyMedianScanner2.Tag.ToString())];
            checkBoxCombinerFilterbyTimeScanner1.Checked = IsNormalizedbyTime[int.Parse(checkBoxCombinerFilterbyTimeScanner1.Tag.ToString())];
            checkBoxCombinerFilterbyTimeScanner2.Checked = IsNormalizedbyTime[int.Parse(checkBoxCombinerFilterbyTimeScanner2.Tag.ToString())];
            checkBoxCombinerFilterbyMedianScanner1.Checked = IsNormalizedbyMedian[int.Parse(checkBoxCombinerFilterbyMedianScanner1.Tag.ToString())];
            checkBoxCombinerFilterbyMedianScanner2.Checked = IsNormalizedbyMedian[int.Parse(checkBoxCombinerFilterbyMedianScanner2.Tag.ToString())];
            checkBoxCombinerFilterbyTriangleScanner1.Checked = IsNormalizedbyTriangle[int.Parse(checkBoxCombinerFilterbyTriangleScanner1.Tag.ToString())];
            checkBoxCombinerFilterbyTriangleScanner2.Checked = IsNormalizedbyTriangle[int.Parse(checkBoxCombinerFilterbyTriangleScanner2.Tag.ToString())];
        }

        private void InitFilterValues()
        {
            textBoxFilterbyTimeMaxDataScanner1.Text = NormalizebyTimeCountStoredData[int.Parse(textBoxFilterbyTimeMaxDataScanner1.Tag.ToString())].ToString();
            textBoxFilterbyTimeMaxDataScanner2.Text = NormalizebyTimeCountStoredData[int.Parse(textBoxFilterbyTimeMaxDataScanner2.Tag.ToString())].ToString();
            textBoxFilterbyTimeCompareDataScanner1.Text = NormalizebyTimeCountDataAverage[int.Parse(textBoxFilterbyTimeCompareDataScanner1.Tag.ToString())].ToString();
            textBoxFilterbyTimeCompareDataScanner2.Text = NormalizebyTimeCountDataAverage[int.Parse(textBoxFilterbyTimeCompareDataScanner2.Tag.ToString())].ToString();
            textBoxFilterbyMedianCompareDataScanner1.Text = NormalizeMedianRange[int.Parse(textBoxFilterbyMedianCompareDataScanner1.Tag.ToString())].ToString();
            textBoxFilterbyMedianCompareDataScanner2.Text = NormalizeMedianRange[int.Parse(textBoxFilterbyMedianCompareDataScanner2.Tag.ToString())].ToString();
            textBoxFilterbyMedianIterationScanner1.Text = NormalizeMedianIteration[int.Parse(textBoxFilterbyMedianIterationScanner1.Tag.ToString())].ToString();
            textBoxFilterbyMedianIterationScanner2.Text = NormalizeMedianIteration[int.Parse(textBoxFilterbyMedianIterationScanner2.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTimeStoredDataScanner1.Text = NormalizebyTimeCountStoredData[int.Parse(textBoxCombinerFilterbyTimeStoredDataScanner1.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTimeStoredDataScanner2.Text = NormalizebyTimeCountStoredData[int.Parse(textBoxCombinerFilterbyTimeStoredDataScanner2.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTimeComparedDataScanner1.Text = NormalizebyTimeCountDataAverage[int.Parse(textBoxCombinerFilterbyTimeComparedDataScanner1.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTimeComparedDataScanner2.Text = NormalizebyTimeCountDataAverage[int.Parse(textBoxCombinerFilterbyTimeComparedDataScanner2.Tag.ToString())].ToString();
            textBoxCombinerFilterbyMedianComparedDataScanner1.Text = NormalizeMedianRange[int.Parse(textBoxCombinerFilterbyMedianComparedDataScanner1.Tag.ToString())].ToString();
            textBoxCombinerFilterbyMedianComparedDataScanner2.Text = NormalizeMedianRange[int.Parse(textBoxCombinerFilterbyMedianComparedDataScanner2.Tag.ToString())].ToString();
            textBoxCombinerFilterbyMedianIterationScanner1.Text = NormalizeMedianIteration[int.Parse(textBoxCombinerFilterbyMedianIterationScanner1.Tag.ToString())].ToString();
            textBoxCombinerFilterbyMedianIterationScanner2.Text = NormalizeMedianIteration[int.Parse(textBoxCombinerFilterbyMedianIterationScanner2.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTriangleComparedDataScanner1.Text = NormalizeTriangleRange[int.Parse(textBoxCombinerFilterbyTriangleComparedDataScanner1.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTriangleComparedDataScanner2.Text = NormalizeTriangleRange[int.Parse(textBoxCombinerFilterbyTriangleComparedDataScanner2.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTriangleIterationScanner1.Text = NormalizeTriangleIteration[int.Parse(textBoxCombinerFilterbyTriangleIterationScanner1.Tag.ToString())].ToString();
            textBoxCombinerFilterbyTriangleIterationScanner2.Text = NormalizeTriangleIteration[int.Parse(textBoxCombinerFilterbyTriangleIterationScanner2.Tag.ToString())].ToString();

            labelCenterDeviationValue.Text = (Math.Abs(NormalizeY[0]) - Math.Abs(NormalizeY[1])).ToString();
        }

        private void InitRecorder()
        {
            textBoxRecorderRecordSpeed.Text = RecordSpeed.ToString();
        }

        private void InitAnalyser()
        {
            if (AnalyseType == "Lift")
            {
                radioButtonDataAnalyserLift.Checked = true;
            }
            else if (AnalyseType == "Release")
            {
                radioButtonDataAnalyserRelease.Checked = true;
            }
        }

        private void SetConfig()
        {
            SetConfigDistorionX();
            SetConfigDistorionY();
            SetConfigOffsetX();
            SetConfigOffSetY();
            SetConfigFilter();
            SetConfigGraphDisplay();
        }

        private void SetConfigDistorionX()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Graph.Angle.Distortion.X"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Graph.Angle.Distortion.X", $"{NormalizeDistortionYAngle[0]};{NormalizeDistortionYAngle[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Graph.Angle.Distortion.X"].Value = $"{NormalizeDistortionYAngle[0]};{NormalizeDistortionYAngle[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigDistorionY()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Graph.Angle.Distortion.Y"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Graph.Angle.Distortion.Y", $"{NormalizeDistortionZAngle[0]};{NormalizeDistortionZAngle[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Graph.Angle.Distortion.Y"].Value = $"{NormalizeDistortionZAngle[0]};{NormalizeDistortionZAngle[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigOffsetX()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Value.Offset.Y"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Value.Offset.Y", $"{NormalizeY[0]};{NormalizeY[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Value.Offset.Y"].Value = $"{NormalizeY[0]};{NormalizeY[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigOffSetY()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Value.Offset.Z"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Value.Offset.Z", $"{NormalizeZ[0]};{NormalizeZ[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Value.Offset.Z"].Value = $"{NormalizeZ[0]};{NormalizeZ[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilter()
        {
            SetConfigFilterType();
            SetConfigFilterValues();
        }

        private void SetConfigFilterType()
        {
            SetConfigFilterbyTime();
            SetConfigFilterbyMedian();
            SetConfigFilterbyTriangle();
        }

        private void SetConfigFilterbyTime()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.ByTime"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.ByTime", $"{IsNormalizedbyTime[0]};{IsNormalizedbyTime[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.ByTime"].Value = $"{IsNormalizedbyTime[0]};{IsNormalizedbyTime[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterbyMedian()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Median"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Median", $"{IsNormalizedbyMedian[0]};{IsNormalizedbyMedian[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Median"].Value = $"{IsNormalizedbyMedian[0]};{IsNormalizedbyMedian[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterbyTriangle()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Triangle"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Triangle", $"{IsNormalizedbyTriangle[0]};{IsNormalizedbyTriangle[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Triangle"].Value = $"{IsNormalizedbyTriangle[0]};{IsNormalizedbyTriangle[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterValues()
        {
            SetConfigFilterValuebyTime();
            SetConfigFilterValuebyMedian();
            SetConfigFilterValuebyTriangle();
        }

        private void SetConfigFilterValuebyTime()
        {
            SetConfigFilterValuebyTimeMaxData();
            SetConfigFilterValuebyTimeCompareData();
        }

        private void SetConfigFilterValuebyTimeMaxData()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.ByTime.CountStoredData"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.ByTime.CountStoredData", $"{NormalizebyTimeCountStoredData[0]};{NormalizebyTimeCountStoredData[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.ByTime.CountStoredData"].Value = $"{NormalizebyTimeCountStoredData[0]};{NormalizebyTimeCountStoredData[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterValuebyTimeCompareData()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.ByTime.CountDataAverage"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.ByTime.CountDataAverage", $"{NormalizebyTimeCountDataAverage[0]};{NormalizebyTimeCountDataAverage[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.ByTime.CountDataAverage"].Value = $"{NormalizebyTimeCountDataAverage[0]};{NormalizebyTimeCountDataAverage[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterValuebyMedian()
        {
            SetConfigFilterValueMedianRange();
            SetConfigFilterValueMedianIteration();
        }

        private void SetConfigFilterValueMedianRange()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Median.Range"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Median.Range", $"{NormalizeMedianRange[0]};{NormalizeMedianRange[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Median.Range"].Value = $"{NormalizeMedianRange[0]};{NormalizeMedianRange[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterValueMedianIteration()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Median.Iteration"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Median.Iteration", $"{NormalizeMedianIteration[0]};{NormalizeMedianIteration[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Median.Iteration"].Value = $"{NormalizeMedianIteration[0]};{NormalizeMedianIteration[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterValuebyTriangle()
        {
            SetConfigFilterValueTriangleRange();
            SetConfigFilterValueTriangleIteration();
        }

        private void SetConfigFilterValueTriangleRange()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Triangle.Range"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Triangle.Range", $"{NormalizeTriangleRange[0]};{NormalizeTriangleRange[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Triangle.Range"].Value = $"{NormalizeTriangleRange[0]};{NormalizeTriangleRange[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigFilterValueTriangleIteration()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Normalizing.Triangle.Iteration"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Normalizing.Triangle.Iteration", $"{NormalizeTriangleIteration[0]};{NormalizeTriangleIteration[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Normalizing.Triangle.Iteration"].Value = $"{NormalizeTriangleIteration[0]};{NormalizeTriangleIteration[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigGraphDisplay()
        {
            SetConfigGraphScanner();
            SetConfigGraphCombined();
        }

        private void SetConfigGraphScanner()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["ContentGraph.Scanner"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("ContentGraph.Scanner", $"{IsDisplayScanner[0]};{IsDisplayScanner[1]}");
                }
                else
                {
                    config.AppSettings.Settings["ContentGraph.Scanner"].Value = $"{IsDisplayScanner[0]};{IsDisplayScanner[1]}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigGraphCombined()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["ContentGraph.Combined"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("ContentGraph.Combined", $"{IsDisplayCombined}");
                }
                else
                {
                    config.AppSettings.Settings["ContentGraph.Combined"].Value = $"{IsDisplayCombined}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigRecorder()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Record.Speed"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Record.Speed", $"{RecordSpeed}");
                }
                else
                {
                    config.AppSettings.Settings["Record.Speed"].Value = $"{RecordSpeed}"; ;
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            //todo - extra class!
            SetConfig();
        }

        private void buttonStartAnalysis_Click(object sender, EventArgs e)
        {
            if (Analysis == null)
            {
                Analysis = new Analysisframe(CombinedNormData, PlcClient);
                Analysis.FormClosed += new FormClosedEventHandler(CloseAnalysis);
                Analysis.Show();
            }
        }

        private void CloseAnalysis(object sender, FormClosedEventArgs e)
        {
            Analysis = null;
        }

        private void OnValueSafeTextBox(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.BackColor = Color.Empty;
        }

        private void OnValueChangeTextBox(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.BackColor = Color.Orange;
        }

        private void trackBarAngleDistortionY1_ValueChanged(object sender, EventArgs e)
        {
            labelAngleDistortionYValue1.Text = trackBarAngleDistortionY1.Value.ToString();
            trackBarCombinerDistortionY1.Value = trackBarAngleDistortionY1.Value;
            NormalizeDistortionYAngle[int.Parse(trackBarAngleDistortionY1.Tag.ToString())] = trackBarAngleDistortionY1.Value;
        }

        private void trackBarAngleDistortionZ1_ValueChanged(object sender, EventArgs e)
        {
            labelAngleDistortionZValue1.Text = trackBarAngleDistortionZ1.Value.ToString();
            trackBarCombinerDistortionZ1.Value = trackBarAngleDistortionZ1.Value;
            NormalizeDistortionZAngle[int.Parse(trackBarAngleDistortionZ1.Tag.ToString())] = trackBarAngleDistortionZ1.Value;
        }

        private void trackBarAngleDistortionY2_ValueChanged(object sender, EventArgs e)
        {
            labelAngleDistortionYValue2.Text = trackBarAngleDistortionY2.Value.ToString();
            trackBarCombinerDistortionY2.Value = trackBarAngleDistortionY2.Value;
            NormalizeDistortionYAngle[int.Parse(trackBarAngleDistortionY2.Tag.ToString())] = trackBarAngleDistortionY2.Value;
        }

        private void trackBarAngleDistortionZ2_ValueChanged(object sender, EventArgs e)
        {
            labelAngleDistortionZValue2.Text = trackBarAngleDistortionZ2.Value.ToString();
            trackBarCombinerDistortionZ2.Value = trackBarAngleDistortionZ2.Value;
            NormalizeDistortionZAngle[int.Parse(trackBarAngleDistortionZ2.Tag.ToString())] = trackBarAngleDistortionZ2.Value;
        }

        private void trackBarCombinerDistortionY1_ValueChanged(object sender, EventArgs e)
        {
            labelCombinerDistortionY1Value.Text = trackBarCombinerDistortionY1.Value.ToString();
            trackBarAngleDistortionY1.Value = trackBarCombinerDistortionY1.Value;
            NormalizeDistortionYAngle[int.Parse(trackBarCombinerDistortionY1.Tag.ToString())] = trackBarCombinerDistortionY1.Value;
        }

        private void trackBarCombinerDistortionZ1_ValueChanged(object sender, EventArgs e)
        {
            labelCombinerDistortionZ1Value.Text = trackBarCombinerDistortionZ1.Value.ToString();
            trackBarAngleDistortionZ1.Value = trackBarCombinerDistortionZ1.Value;
            NormalizeDistortionZAngle[int.Parse(trackBarCombinerDistortionZ1.Tag.ToString())] = trackBarCombinerDistortionZ1.Value;
        }

        private void trackBarCombinerDistortionY2_ValueChanged(object sender, EventArgs e)
        {
            labelCombinerDistortionY2Value.Text = trackBarCombinerDistortionY2.Value.ToString();
            trackBarAngleDistortionY2.Value = trackBarCombinerDistortionY2.Value;
            NormalizeDistortionYAngle[int.Parse(trackBarCombinerDistortionY2.Tag.ToString())] = trackBarCombinerDistortionY2.Value;
        }

        private void trackBarCombinerDistortionZ2_ValueChanged(object sender, EventArgs e)
        {
            labelCombinerDistortionZ2Value.Text = trackBarCombinerDistortionZ2.Value.ToString();
            trackBarAngleDistortionZ2.Value = trackBarCombinerDistortionZ2.Value;
            NormalizeDistortionZAngle[int.Parse(trackBarCombinerDistortionZ2.Tag.ToString())] = trackBarCombinerDistortionZ2.Value;
        }

        private void textBoxOffsetY1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeY[int.Parse(textBoxOffsetY1.Tag.ToString())] = int.Parse(textBoxOffsetY1.Text);

                    if (IsNormalizingYAutomatic)
                    {
                        int value = GetCounterpartValue(int.Parse(textBoxOffsetY1.Text));
                        NormalizeY[1] = value;
                        textBoxOffsetY2.Text = value.ToString();
                        textBoxCombinerOffsetY2.Text = value.ToString();
                        OnValueSafeTextBox(textBoxOffsetY2, e);
                        OnValueSafeTextBox(textBoxCombinerOffsetY2, e);
                    }

                    textBoxCombinerOffsetY1.Text = textBoxOffsetY1.Text;
                    OnValueSafeTextBox(textBoxCombinerOffsetY1, e);
                    OnValueSafeTextBox(sender, e);
                    OnDeviationChange();
                }
            }
        }

        private void textBoxOffsetZ1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeZ[int.Parse(textBoxOffsetZ1.Tag.ToString())] = int.Parse(textBoxOffsetZ1.Text);
                    textBoxCombinerOffsetZ1.Text = textBoxOffsetZ1.Text;
                    OnValueSafeTextBox(textBoxCombinerOffsetZ1, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxOffsetY2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeY[int.Parse(textBoxOffsetY2.Tag.ToString())] = int.Parse(textBoxOffsetY2.Text);
                    textBoxCombinerOffsetY2.Text = textBoxOffsetY2.Text;
                    OnValueSafeTextBox(textBoxCombinerOffsetY2, e);
                    OnValueSafeTextBox(sender, e);
                    OnDeviationChange();
                }
            }
        }

        private void textBoxOffsetZ2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeZ[int.Parse(textBoxOffsetZ2.Tag.ToString())] = int.Parse(textBoxOffsetZ2.Text);
                    textBoxCombinerOffsetZ2.Text = textBoxOffsetZ2.Text;
                    OnValueSafeTextBox(textBoxCombinerOffsetZ2, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerOffsetY1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeY[int.Parse(textBoxCombinerOffsetY1.Tag.ToString())] = int.Parse(textBoxCombinerOffsetY1.Text);

                    if (IsNormalizingYAutomatic)
                    {
                        int value = GetCounterpartValue(int.Parse(textBoxCombinerOffsetY1.Text));
                        NormalizeY[1] = value;
                        textBoxOffsetY2.Text = value.ToString();
                        textBoxCombinerOffsetY2.Text = value.ToString();
                        OnValueSafeTextBox(textBoxOffsetY2, e);
                        OnValueSafeTextBox(textBoxCombinerOffsetY2, e);
                    }

                    textBoxOffsetY1.Text = textBoxCombinerOffsetY1.Text;
                    OnValueSafeTextBox(textBoxOffsetY1, e);
                    OnValueSafeTextBox(sender, e);
                    OnDeviationChange();
                }
            }
        }

        private void textBoxCombinerOffsetZ1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeZ[int.Parse(textBoxCombinerOffsetZ1.Tag.ToString())] = int.Parse(textBoxCombinerOffsetZ1.Text);
                    textBoxOffsetZ1.Text = textBoxCombinerOffsetZ1.Text;
                    OnValueSafeTextBox(textBoxOffsetZ1, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerOffsetY2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeY[int.Parse(textBoxCombinerOffsetY2.Tag.ToString())] = int.Parse(textBoxCombinerOffsetY2.Text);
                    textBoxOffsetY2.Text = textBoxCombinerOffsetY2.Text;
                    OnValueSafeTextBox(textBoxOffsetY2, e);
                    OnValueSafeTextBox(sender, e);
                    OnDeviationChange();
                }
            }
        }

        private void textBoxCombinerOffsetZ2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeZ[int.Parse(textBoxCombinerOffsetZ2.Tag.ToString())] = int.Parse(textBoxCombinerOffsetZ2.Text);
                    textBoxOffsetZ2.Text = textBoxCombinerOffsetZ2.Text;
                    OnValueSafeTextBox(textBoxOffsetZ2, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxFilterbyMedianCompareDataScanner1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianRange[int.Parse(textBoxFilterbyMedianCompareDataScanner1.Tag.ToString())] = int.Parse(textBoxFilterbyMedianCompareDataScanner1.Text);
                    textBoxCombinerFilterbyMedianComparedDataScanner1.Text = textBoxFilterbyMedianCompareDataScanner1.Text;
                    OnValueSafeTextBox(textBoxCombinerFilterbyMedianComparedDataScanner1, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxFilterbyMedianIterationScanner1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianIteration[int.Parse(textBoxFilterbyMedianIterationScanner1.Tag.ToString())] = int.Parse(textBoxFilterbyMedianIterationScanner1.Text);
                    textBoxCombinerFilterbyMedianIterationScanner1.Text = textBoxFilterbyMedianIterationScanner1.Text;
                    OnValueSafeTextBox(textBoxCombinerFilterbyMedianIterationScanner1, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxFilterbyMedianCompareDataScanner2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianRange[int.Parse(textBoxFilterbyMedianCompareDataScanner2.Tag.ToString())] = int.Parse(textBoxFilterbyMedianCompareDataScanner2.Text);
                    textBoxCombinerFilterbyMedianComparedDataScanner2.Text = textBoxFilterbyMedianCompareDataScanner2.Text;
                    OnValueSafeTextBox(textBoxCombinerFilterbyMedianComparedDataScanner2, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxFilterbyMedianIterationScanner2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianIteration[int.Parse(textBoxFilterbyMedianIterationScanner2.Tag.ToString())] = int.Parse(textBoxFilterbyMedianIterationScanner2.Text);
                    textBoxCombinerFilterbyMedianIterationScanner2.Text = textBoxFilterbyMedianIterationScanner2.Text;
                    OnValueSafeTextBox(textBoxCombinerFilterbyMedianIterationScanner2, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyMedianComparedDataScanner1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianRange[int.Parse(textBoxCombinerFilterbyMedianComparedDataScanner1.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyMedianComparedDataScanner1.Text);
                    textBoxFilterbyMedianCompareDataScanner1.Text = textBoxCombinerFilterbyMedianComparedDataScanner1.Text;
                    OnValueSafeTextBox(textBoxFilterbyMedianCompareDataScanner1, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyMedianIterationScanner1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianIteration[int.Parse(textBoxCombinerFilterbyMedianIterationScanner1.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyMedianIterationScanner1.Text);
                    textBoxFilterbyMedianIterationScanner1.Text = textBoxCombinerFilterbyMedianIterationScanner1.Text;
                    OnValueSafeTextBox(textBoxFilterbyMedianIterationScanner1, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyMedianComparedDataScanner2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianRange[int.Parse(textBoxCombinerFilterbyMedianComparedDataScanner2.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyMedianComparedDataScanner2.Text);
                    textBoxFilterbyMedianCompareDataScanner2.Text = textBoxCombinerFilterbyMedianComparedDataScanner2.Text;
                    OnValueSafeTextBox(textBoxFilterbyMedianCompareDataScanner2, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyMedianIterationScanner2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeMedianIteration[int.Parse(textBoxCombinerFilterbyMedianIterationScanner2.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyMedianIterationScanner2.Text);
                    textBoxFilterbyMedianIterationScanner2.Text = textBoxCombinerFilterbyMedianIterationScanner2.Text;
                    OnValueSafeTextBox(textBoxFilterbyMedianIterationScanner2, e);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyTriangleComparedDataScanner1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeTriangleRange[int.Parse(textBoxCombinerFilterbyTriangleComparedDataScanner1.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyTriangleComparedDataScanner1.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyTriangleIterationScanner1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeTriangleIteration[int.Parse(textBoxCombinerFilterbyTriangleIterationScanner1.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyTriangleIterationScanner1.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyTriangleComparedDataScanner2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeTriangleRange[int.Parse(textBoxCombinerFilterbyTriangleComparedDataScanner2.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyTriangleComparedDataScanner2.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerFilterbyTriangleIterationScanner2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    NormalizeTriangleIteration[int.Parse(textBoxCombinerFilterbyTriangleIterationScanner2.Tag.ToString())] = int.Parse(textBoxCombinerFilterbyTriangleIterationScanner2.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxRecorderRecordSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    RecordSpeed = int.Parse(textBoxRecorderRecordSpeed.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void checkBoxCombinerFilterbyTimeScanner1_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyTime[int.Parse(checkBoxCombinerFilterbyTimeScanner1.Tag.ToString())] = checkBoxCombinerFilterbyTimeScanner1.Checked;
            checkBoxFilterbyTimeScanner1.Checked = checkBoxCombinerFilterbyTimeScanner1.Checked;
        }

        private void checkBoxCombinerFilterbyTimeScanner2_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyTime[int.Parse(checkBoxCombinerFilterbyTimeScanner2.Tag.ToString())] = checkBoxCombinerFilterbyTimeScanner2.Checked;
            checkBoxFilterbyTimeScanner2.Checked = checkBoxCombinerFilterbyTimeScanner2.Checked;
        }

        private void checkBoxCombinerFilterbyMedianScanner1_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyMedian[int.Parse(checkBoxCombinerFilterbyMedianScanner1.Tag.ToString())] = checkBoxCombinerFilterbyMedianScanner1.Checked;
            checkBoxFilterbyMedianScanner1.Checked = checkBoxCombinerFilterbyMedianScanner1.Checked;
        }

        private void checkBoxCombinerFilterbyMedianScanner2_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyMedian[int.Parse(checkBoxCombinerFilterbyMedianScanner2.Tag.ToString())] = checkBoxCombinerFilterbyMedianScanner2.Checked;
            checkBoxFilterbyMedianScanner2.Checked = checkBoxCombinerFilterbyMedianScanner2.Checked;
        }

        private void checkBoxFilterbyTimeScanner1_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyTime[int.Parse(checkBoxFilterbyTimeScanner1.Tag.ToString())] = checkBoxFilterbyTimeScanner1.Checked;
            checkBoxCombinerFilterbyTimeScanner1.Checked = checkBoxFilterbyTimeScanner1.Checked;
        }

        private void checkBoxFilterbyTimeScanner2_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyTime[int.Parse(checkBoxFilterbyTimeScanner2.Tag.ToString())] = checkBoxFilterbyTimeScanner2.Checked;
            checkBoxCombinerFilterbyTimeScanner2.Checked = checkBoxFilterbyTimeScanner2.Checked;
        }

        private void checkBoxFilterbyMedianScanner1_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyMedian[int.Parse(checkBoxFilterbyMedianScanner1.Tag.ToString())] = checkBoxFilterbyMedianScanner1.Checked;
            checkBoxCombinerFilterbyMedianScanner1.Checked = checkBoxFilterbyMedianScanner1.Checked;
        }

        private void checkBoxFilterbyMedianScanner2_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyMedian[int.Parse(checkBoxFilterbyMedianScanner2.Tag.ToString())] = checkBoxFilterbyMedianScanner2.Checked;
            checkBoxCombinerFilterbyMedianScanner2.Checked = checkBoxFilterbyMedianScanner2.Checked;
        }

        private void checkBoxScanner1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScanner2.Checked || checkBoxAnalyse.Checked)
            {
                IsDisplayScanner[int.Parse(checkBoxScanner1.Tag.ToString())] = checkBoxScanner1.Checked;
                chartAnalyser.Series["Scanner1 Data"].Enabled = checkBoxScanner1.Checked;
            }
            else
            {
                checkBoxScanner1.Checked = true;
            }
        }

        private void checkBoxScanner2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScanner1.Checked || checkBoxAnalyse.Checked)
            {
                IsDisplayScanner[int.Parse(checkBoxScanner2.Tag.ToString())] = checkBoxScanner2.Checked;
                chartAnalyser.Series["Scanner2 Data"].Enabled = checkBoxScanner2.Checked;
            }
            else
            {
                checkBoxScanner2.Checked = true;
            }
        }

        private void checkBoxAnalyse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScanner1.Checked || checkBoxScanner2.Checked)
            {
                IsDisplayCombined = checkBoxAnalyse.Checked;
                chartAnalyser.Series["Analysis Graph"].Enabled = checkBoxAnalyse.Checked;
            }
            else
            {
                checkBoxAnalyse.Checked = true;
            }
        }

        private void checkBoxCombinerFilterbyTriangleScanner1_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyTriangle[int.Parse(checkBoxCombinerFilterbyTriangleScanner1.Tag.ToString())] = checkBoxCombinerFilterbyTriangleScanner1.Checked;
        }

        private void checkBoxCombinerFilterbyTriangleScanner2_CheckedChanged(object sender, EventArgs e)
        {
            IsNormalizedbyTriangle[int.Parse(checkBoxCombinerFilterbyTriangleScanner2.Tag.ToString())] = checkBoxCombinerFilterbyTriangleScanner2.Checked;
        }

        private void checkBoxDataRecorder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataRecorder.Checked)
            {
                DataRecorder1?.StartRecording(Scanner1);
                DataRecorder2?.StartRecording(Scanner2);

                checkBoxDataRecorder.Enabled = false;

                RecordChecker = new System.Threading.Timer(OnCheckTimer, this, 60000, Timeout.Infinite);
            }
            else if (!checkBoxDataRecorder.Checked)
            {
                DataRecorder1?.StopRecording(Scanner1);
                DataRecorder2?.StopRecording(Scanner2);
            }
        }

        private void OnCheckTimer(object sender)
        {
            RecordChecker.Change(Timeout.Infinite, Timeout.Infinite);

            ChangeCheckbox();
            checkBoxDataRecorder.Enabled = true;
        }

        private void ChangeCheckbox()
        {
            Invoke(new Action(() =>
            {
                checkBoxDataRecorder.Enabled = true;
            }));
        }

        private bool ValueNumeric(object obj)
        {
            TextBox box = (TextBox)obj;

            bool ok = int.TryParse(box.Text, out int value);

            return ok;
        }

        private void buttonSetup_Click(object sender, EventArgs e)
        {
            if (Setup == null)
            {
                Setup = new Setupframe(this);
                Setup.FormClosed += new FormClosedEventHandler(CloseSetup);
                Setup.ValueChanged += new Setupframe.ScannerOffsetChange(OnIsSet);
                Setup.Show();
            }
        }

        private void CloseSetup(object sender, FormClosedEventArgs e)
        {
            Setup = null;
        }

        private void OnIsSet(object sender, bool value)
        {
            if (value)
            {
                textBoxOffsetY2.Enabled = false;
                textBoxCombinerOffsetY2.Enabled = false;
            }
            else if (!value)
            {
                textBoxOffsetY2.Enabled = true;
                textBoxCombinerOffsetY2.Enabled = true;
            }
        }

        private void chart_MouseWheel(object sender, MouseEventArgs e)
        {
            Chart chart = (Chart)sender;

            try
            {
                if (e.Delta < 0)
                {
                    chart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = chart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = chart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = chart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 3;
                    double posXFinish = chart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 3;
                    double posYStart = chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 3;
                    double posYFinish = chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 3;

                    chart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    chart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void OnAnalysis(object sender)
        {
            Analyser.Change(Timeout.Infinite, Timeout.Infinite);

            if (CombinedNormData.Count != 0)
            {
                StartEdgeAnalyser();
            }

            Analyser.Change(AnalyserTimer, Timeout.Infinite);
        }

        private void StartEdgeAnalyser()
        {
            try
            {
                List<Tuple<int, int, int>> edgePoints = new List<Tuple<int, int, int>>();

                var watch = System.Diagnostics.Stopwatch.StartNew();
                DateTime start = DateTime.Now;
                //LogFiler.Log(Category.Info, start.Millisecond.ToString());

                if (AnalyseType == "Lift")
                {
                    ReducedPointsofInterest = AnalysisFunctionality.GetPointsofInterest(/*AnalysisFunctionality.AnalyseType.Lift,*/ CombinedNormData, out edgePoints);
                }
                else if (AnalyseType == "Release")
                {
                    ReducedPointsofInterest = AnalysisFunctionality.GetPointsofInterest(/*AnalysisFunctionality.AnalyseType.Release,*/ CombinedNormData, out edgePoints);
                }
                else
                {
                    return;
                }
                DateTime end = DateTime.Now;
                watch.Stop();

                var elapsedMs = watch.ElapsedMilliseconds;

                TimeSpan test = end - start;

                LogFiler.Log(Category.Info, "analyse:" + elapsedMs.ToString());

                EdgePoints = edgePoints;
                ScannerInformation info = new ScannerInformation();
                info.GetNecessaryInformation(edgePoints);

                int ErrorCounter = 0;

                if (info.TopSlabWidth == 0)
                {
                    ErrorCounter++;

                    if (ErrorCounter == 3)
                    {
                        Invoke(new Action(() =>
                        {
                            textBoxAnalyserTopWidth.Text = "Error";
                            textBoxAnalyserDeviationTop.Text = "Error";
                            textBoxAnalyserGrippingDepthTCenter.Text = "Error";
                            textBoxAnalyserGrippingDepthTEast.Text = "Error";
                            textBoxAnalyserGrippingDepthTWest.Text = "Error";
                            textBoxAnalyserPileTiltAngle.Text = "Error";
                            textBoxAnalyserGrippingDepthBottomEast.Text = "Error";
                            textBoxAnalyserGrippingDepthBottomWest.Text = "Error";
                            textBoxAnalyserBottomSlabDetected.Text = "Error";
                        }));

                        ErrorCounter = 0;
                    }
                }
                else if (info.TopSlabWidth != 0)
                {
                    Invoke(new Action(() =>
                    {
                        textBoxAnalyserTopWidth.Text = info.TopSlabWidth.ToString();
                        textBoxAnalyserDeviationTop.Text = info.TopSlabYDeviation.ToString();
                        textBoxAnalyserGrippingDepthTCenter.Text = info.GrippingDepthTopCenter.ToString();
                        textBoxAnalyserGrippingDepthTEast.Text = info.GrippingDepthTopEast.ToString();
                        textBoxAnalyserGrippingDepthTWest.Text = info.GrippingDepthTopWest.ToString();
                        textBoxAnalyserPileTiltAngle.Text = info.TiltAngle.ToString();
                        textBoxAnalyserGrippingDepthBottomEast.Text = info.GrippingDepthBottomEast.ToString();
                        textBoxAnalyserGrippingDepthBottomWest.Text = info.GrippingDepthBottomWest.ToString();
                        textBoxAnalyserBottomSlabDetected.Text = info.GrippingDepthDetected.ToString();
                    }));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private int GetCounterpartValue(int y1)
        {
            int y2 = y1 + ScannerOffset;

            return y2;
        }

        private void OnDeviationChange()
        {
            labelCenterDeviationValue.Text = (Math.Abs(NormalizeY[0]) - Math.Abs(NormalizeY[1])).ToString();
        }

        private void Mainframe_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetConfigGraphDisplay();
            SetConfigRecorder();

            LogFiler.Close();

            Thread[] thArr = new Thread[] { ScReceive1, ScReceive2 };

            foreach (Thread t in thArr)
            {
                if (t != null && t.IsAlive)
                {
                    // since the thread is running endless
                    t.Abort();
                }
            }

            ScReceive1 = null;
            ScReceive2 = null;

            if (Scanner1 != null)
            {
                Scanner1.Close();
                Scanner1 = null;
            }

            if (Scanner2 != null)
            {
                Scanner2.Close();
                Scanner2 = null;
            }

            if (PlcClient != null)
            {
                PlcClient.Close();
                PlcClient = null;
            }

            if (Raw != null)
            {
                Raw.Change(Timeout.Infinite, Timeout.Infinite);
                Raw.Dispose();
                Raw = null;
            }

            if (Normalized != null)
            {
                Normalized.Change(Timeout.Infinite, Timeout.Infinite);
                Normalized.Dispose();
                Normalized = null;
            }

            if (CombinedData != null)
            {
                CombinedData.Change(Timeout.Infinite, Timeout.Infinite);
                CombinedData.Dispose();
                CombinedData = null;
            }

            if (CombinerGraph != null)
            {
                CombinerGraph.Change(Timeout.Infinite, Timeout.Infinite);
                CombinerGraph.Dispose();
                CombinerGraph = null;
            }
        }

        private void buttonRecorderDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                DataRecorder1?.GetRecordData(folderBrowserDialog1.SelectedPath);
                DataRecorder2?.GetRecordData(folderBrowserDialog1.SelectedPath);

                checkBoxAnalyseRecorder.Enabled = true;
                IsRecorderAnalysed = false;

                Recorder = new System.Threading.Timer(OnRecorder, this, 50, Timeout.Infinite);
            }
        }

        private void trackBarRecorder_ValueChanged(object sender, EventArgs e)
        {
            foreach (KeyValuePair<Scanner, Recorder> rc in _dicRecorderInfo)
            {
                if (rc.Value.RecordData != null && rc.Value.RecordData.Count != 0)
                {
                    DrawRecorder(rc);
                }
            }

            try
            {
                labelRecorderTimestamp.Text = GetTimestampValue(_dicRecorderInfo, trackBarRecorder.Value);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            if (trackBarRecorder.Value == trackBarRecorder.Maximum)
            {
                IsPlayingRecord = false;

                UpdatePlayerButton(true);

                trackBarRecorder.Value = 0;
            }
        }

        private string GetTimestampValue(Dictionary<Scanner, Recorder> dict, int iterator)
        {
            string time = string.Empty;

            foreach(KeyValuePair<Scanner, Recorder> value in dict)
            {
                if (value.Value.RecordData.Count != 0)
                {
                    time = value.Value.RecordData[iterator].Item1.ToString();
                    break;
                }
            }

            return time;
        }

        private void DrawRecorder(KeyValuePair<Scanner, Recorder> rc)
        {
            try
            {
                if (rc.Value.RecordData[trackBarRecorder.Value].Item2.Count != 0)
                {
                    chartRecorder.Series[rc.Key.Name].Points.Clear();

                    for (int i = 0; i < rc.Value.RecordData[trackBarRecorder.Value].Item2.Count; i++)
                    {
                        chartRecorder.Series[rc.Key.Name].Points.AddXY(rc.Value.RecordData[trackBarRecorder.Value].Item2[i].Item1, rc.Value.RecordData[trackBarRecorder.Value].Item2[i].Item2);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            try
            {
                if (RecorderEdgePoints.Count != 0)
                {
                    chartRecorder.Series["Detected Edges"].Points.Clear();

                    for (int i = 0; i < RecorderEdgePoints[trackBarRecorder.Value].Count; i++)
                    {
                        chartRecorder.Series["Detected Edges"].Points.AddXY(RecorderEdgePoints[trackBarRecorder.Value][i].Item1, RecorderEdgePoints[trackBarRecorder.Value][i].Item2);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void buttonRecorderPlay_Click(object sender, EventArgs e)
        {
            UpdatePlayerButton(false);

            IsPlayingRecord = true;

            RecordPlayer = new System.Threading.Timer(OnPlayingRecord, this, RecordSpeed, Timeout.Infinite);
        }

        private void OnPlayingRecord(object sender)
        {
            RecordPlayer.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                if (IsPlayingRecord)
                {
                    IncreaseTrackbarPlayer();
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            RecordPlayer.Change(RecordSpeed, Timeout.Infinite);
        }

        private void buttonRecorderStop_Click(object sender, EventArgs e)
        {
            IsPlayingRecord = false;

            UpdatePlayerButton(true);
        }

        private void UpdatePlayerButton(bool active)
        {
            Invoke(new Action(() =>
            {
                buttonRecorderPlay.Enabled = active;
            }));
        }

        private void IncreaseTrackbarPlayer()
        {
            Invoke(new Action(() =>
            {
                trackBarRecorder.Value++;
            }));
        }

        private void checkBoxAnalyseRecorder_CheckedChanged(object sender, EventArgs e)
        {
            chartRecorder.Series["Detected Edges"].Enabled = checkBoxAnalyseRecorder.Checked;

            if (checkBoxAnalyseRecorder.Checked)
            {
                checkBoxAnalyseRecorder.Enabled = false;

                if (RecorderEdgePoints.Count == 0 || !IsRecorderAnalysed)
                {
                    Task test = new Task(() =>
                    {
                        GenerateRecorderData();

                        Invoke(new Action(() =>
                        {
                            checkBoxAnalyseRecorder.Enabled = true;
                            trackBarRecorder.Value = 0;
                        }));
                    });

                    test.Start();
                }
            }
        }

        private void GenerateRecorderData()
        {
            RecorderAnalyse = GetRecorderData();
            RecorderReducedPoints = new List<List<List<Tuple<int, int, int>>>>();
            RecorderEdgePoints = new List<List<Tuple<int, int, int>>>();

            try
            {
                // combine the possible recorder data
                for (int i = 0; i < trackBarRecorder.Maximum; i++)
                {
                    //List<List<Tuple<int, int, int>>> recorderReducedPoints = AnalysisFunctionality.GetPointsofInterest(AnalysisFunctionality.AnalyseType.Lift, RecorderAnalyse[i], out List<Tuple<int, int, int>> edges);
                    //RecorderReducedPoints.Add(recorderReducedPoints);

                    //RecorderEdgePoints.Add(edges);
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            IsRecorderAnalysed = true;
        }

        private List<List<Tuple<int, int, int>>> GetRecorderData()
        {
            List<List<Tuple<int, int, int>>> combineData = new List<List<Tuple<int, int, int>>>();

            if (DataRecorder1.RecordData.Count >= trackBarRecorder.Maximum && DataRecorder2.RecordData.Count >= trackBarRecorder.Maximum)
            {
                combineData.AddRange(CombineRecorderData(DataRecorder1.RecordData, DataRecorder2.RecordData));
            }
            else if (DataRecorder1.RecordData.Count >= trackBarRecorder.Maximum)
            {
                combineData.AddRange(CombineRecorderData(DataRecorder1.RecordData));
            }
            else if (DataRecorder2.RecordData.Count >= trackBarRecorder.Maximum)
            {
                combineData.AddRange(CombineRecorderData(DataRecorder2.RecordData));
            }

            return combineData;
        }

        private List<List<Tuple<int, int, int>>> CombineRecorderData(List<Tuple<string, List<Tuple<int, int, int>>>> data1, List<Tuple<string, List<Tuple<int, int, int>>>> data2)
        {
            List<List<Tuple<int, int, int>>> combineData = new List<List<Tuple<int, int, int>>>();

            for (int i = 0; i < trackBarRecorder.Maximum; i++)
            {
                List<Tuple<int, int, int>> data = new List<Tuple<int, int, int>>();

                data.AddRange(data1[i].Item2);
                data.AddRange(data2[i].Item2);

                data.OrderBy(x => x.Item1);

                combineData.Add(data);
            }

            return combineData;
        }

        private List<List<Tuple<int, int, int>>> CombineRecorderData(List<Tuple<string, List<Tuple<int, int, int>>>> data1)
        {
            List<List<Tuple<int, int, int>>> combineData = new List<List<Tuple<int, int, int>>>();

            for (int i = 0; i < trackBarRecorder.Maximum; i++)
            {
                List<Tuple<int, int, int>> data = new List<Tuple<int, int, int>>();

                data.AddRange(data1[i].Item2);

                combineData.Add(data);
            }

            return combineData;
        }

        private void radioButtonDataAnalyserLift_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDataAnalyserLift.Checked)
            {
                AnalyseType = "Lift";
            }
        }

        private void radioButtonDataAnalyserRelease_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDataAnalyserRelease.Checked)
            {
                AnalyseType = "Release";
            }
        }
    }
}