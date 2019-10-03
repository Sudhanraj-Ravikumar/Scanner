using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Configuration;

namespace ScannerDisplay
{
    public class ScannerFunctionality
    {
        private static Logger ScanLogger;

        static ScannerFunctionality()
        {
            ScanLogger = new Logger("ScannerFunctionality");
        }

        public ScannerFunctionality(int tag, Logger logger)
        {
            ScannerTag = tag;
            LogFiler = logger;
        }

        private int ScannerTag
        { get; set; }

        private Logger LogFiler
        { get; set; }
        
        public static ushort GetID(byte[] package)
        {
            byte[] byteid = new byte[2];
            ushort id = 0;

            try
            {
                Array.Copy(package, 12, byteid, 0, 2);

                id = BitConverter.ToUInt16(byteid, 0);
            }
            catch (ArgumentNullException anex)
            {
                ScanLogger.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + anex.Message);
            }

            return id;
        }
        
        public static ushort GetBlock(byte[] package)
        {
            byte[] byteblock = new byte[2];
            ushort block = 0;

            try
            {
                Array.Copy(package, 14, byteblock, 0, 2);

                block = BitConverter.ToUInt16(byteblock, 0);
            }
            catch (ArgumentNullException anex)
            {
                ScanLogger.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + anex.Message);
            }

            return block;
        }
        
        public static int GetTotalLength(byte[] package)
        {
            byte[] byteTotalLength = new byte[4];
            int TotalLength = 0;

            try
            {
                Array.Copy(package, 0, byteTotalLength, 0, 4);

                TotalLength = BitConverter.ToInt16(byteTotalLength, 0);
            }
            catch (ArgumentNullException anex)
            {
                ScanLogger.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + anex.Message);
            }

            return TotalLength;
        }
        
        public static int GetBlockFromCalculation(byte[] ba)
        {
            decimal n;

            ushort ID = GetID(ba);
            int block = new int();

            if (ID == 1)
            {
                ushort StartIndex = GetStartIndex(ba);
                ushort stopindex = GetStopIndex(ba);
                ushort Resolution = GetResolution(ba);
                n = (1 + (((stopindex - StartIndex) / Resolution))) * 2;
                block = (int)Math.Ceiling(n / 1000);
            }

            return block;
        }
        
        public static ushort GetStartIndex(byte[] package)
        {
            int id = GetID(package);
            byte[] byteStartIndex = new byte[2];
            ushort StartIndextoInt = 0;

            if (id == 1)
            {
                try
                {
                    Array.Copy(package, 40, byteStartIndex, 0, 2);

                    StartIndextoInt = BitConverter.ToUInt16(byteStartIndex, 0);
                }
                catch (ArgumentNullException anex)
                {
                    ScanLogger.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + anex.Message);
                }
            }

            return StartIndextoInt;
        }
        
        public static ushort GetStopIndex(byte[] package)
        {
            int id = GetID(package);
            byte[] byteStopIndex = new byte[2];
            ushort StopIndextoInt = 0;

            if (id == 1)
            {
                try
                {
                    Array.Copy(package, 42, byteStopIndex, 0, 2);

                    StopIndextoInt = BitConverter.ToUInt16(byteStopIndex, 0);
                }
                catch (ArgumentNullException anex)
                {
                    ScanLogger.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + anex.Message);
                }
            }

            return StopIndextoInt;
        }
        
        public static ushort GetResolution(byte[] package)
        {
            int id = GetID(package);
            byte[] byteResolution = new byte[2];
            ushort ResolutiontoInt = 0;

            if (id == 1)
            {
                try
                {
                    Array.Copy(package, 44, byteResolution, 0, 2);

                    ResolutiontoInt = BitConverter.ToUInt16(byteResolution, 0);
                }
                catch (ArgumentNullException anex)
                {
                    ScanLogger.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + anex.Message);
                }
            }

            return ResolutiontoInt;
        }
        
        public static float GetAngleDifferece(byte[] ba)
        {
            int n;
            int StartDeg; int StopDeg;
            ushort ID = GetID(ba);

            float AngleGap = new float();

            if (ID == 1)
            {
                ushort StartIndex = GetStartIndex(ba);
                ushort stopindex = GetStopIndex(ba);
                ushort Resolution = GetResolution(ba);

                n = 1 + (((stopindex - StartIndex) / Resolution));
                StartDeg = (StartIndex / 10) - 135;
                StopDeg = (stopindex / 10) - 135;
                int NumberOfAngles = Math.Abs((stopindex / 10) - (StartIndex / 10));
                AngleGap = (float)NumberOfAngles / n;
            }

            return AngleGap;
        }
        
        public static float GetStartDegree(byte[] ba)
        {
            float StartDeg = new float();
            ushort ID = GetID(ba);

            if (ID == 1)
            {
                ushort StartIndex = GetStartIndex(ba);
                StartDeg = (StartIndex / 10) - 135;
            }

            return StartDeg;
        }
        
        public static List<short> GetDistanceList(byte[] ba)
        {
            int id = GetID(ba);
            List<short> Distance = new List<short>();

            if (id == 6)
            {
                int datalength = ba.Length - 20;

                byte[] managedArrayDF = new byte[datalength];

                Array.Copy(ba, 20, managedArrayDF, 0, datalength);

                int cnt = 0;
                short DFtoInt;
                short[] managedDataFullArray = new short[datalength / 2];

                for (int i = 0; i < managedArrayDF.Length; i += 2)
                {
                    DFtoInt = BitConverter.ToInt16(managedArrayDF, i);

                    managedDataFullArray[cnt] = DFtoInt;
                    cnt++;
                    Distance.Add(DFtoInt);
                }
            }

            return Distance;
        }

        public static Tuple<float, float> GetAngleId1(byte[] ba)
        {
            float difference = new float();
            float degree = new float();

            difference = GetAngleDifferece(ba);
            degree = GetStartDegree(ba);

            return new Tuple<float, float>(degree, difference);
        }
        
        public List<List<Tuple<int, int, int>>> GetXYCoordinate(List<List<short>> Distance, object sender)
        {
            List<List<Tuple<int, int, int>>> XYCoord = GetXYArrayCoordinate(Distance, sender);
            List<List<Tuple<int, int, int>>> CorrectedXYCoord = CorrectXYCoord(XYCoord);

            return CorrectedXYCoord;
        }

        private List<List<Tuple<int, int, int>>> GetXYArrayCoordinate(List<List<short>> Distance, object sender)
        {
            Scanner scanner = (Scanner)sender;

            List<List<Tuple<int, int, int>>> XYCoord = new List<List<Tuple<int, int, int>>>();

            try
            {
                for (int i = 0; i < Distance.Count; i++)
                {
                    XYCoord.Add(GetXYRework(Distance[i], scanner));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return XYCoord;
        }

        private List<Tuple<int, int, int>> GetXYRework(List<short> distance, Scanner scanner)
        {
            int angledistortion = Mainframe.NormalizeDistortionYAngle[ScannerTag];
            int distancedistortion = Mainframe.NormalizeDistortionZAngle[ScannerTag];

            List<Tuple<int, int, int>> XYCoord = new List<Tuple<int, int, int>>();

            try
            {
                foreach (int Dist in distance)
                {
                    if (Dist != -1)
                    {
                        int actDist = 0;

                        int x = 0;
                        int y = 0;
                        int D = 0;

                        double angletilt = Math.PI * distancedistortion / 180.0;
                        double angledist = Math.PI * angledistortion / 180.0;
                        double angle = Math.PI * scanner.Degree / 180.0;

                        actDist = (int)(Math.Cos(angletilt) * Dist);
                        x = (int)(Math.Sin(angle - angledist) * actDist);
                        y = (int)(Math.Cos(angle - angledist) * actDist);
                        D = actDist;

                        XYCoord.Add(Tuple.Create(x, y, D));
                    }

                    scanner.Degree += scanner.AngleDifference;
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return XYCoord;
        }

        private List<List<Tuple<int, int, int>>> CorrectXYCoord(List<List<Tuple<int, int, int>>> xyCoord)
        {
            List<List<Tuple<int, int, int>>> corrXYCoord = new List<List<Tuple<int, int, int>>>();

            for (int i = 0; i < xyCoord.Count; i++)
            {
                if (xyCoord[i].Count != 0)
                {
                    corrXYCoord.Add(xyCoord[i]);
                }
            }

            return corrXYCoord;
        }

        public static List<Tuple<int, int, int>> MergeXYArray(List<List<Tuple<int, int, int>>> xycoord)
        {
            List<Tuple<int, int, int>> XYCoordinate = new List<Tuple<int, int, int>>();

            try
            {
                for (int i = 0; i < xycoord.Count; i++)
                {
                    XYCoordinate.AddRange(xycoord[i]);
                }
            }
            catch (Exception)
            {

            }

            return XYCoordinate;
        }

        public static List<Tuple<int, int, int>> MergeNormalizeArray(List<List<Tuple<int, int, int>>> normData)
        {
            List<Tuple<int, int, int>> NormalizeData = new List<Tuple<int, int, int>>();

            try
            {
                for (int i = 0; i < normData.Count; i++)
                {
                    NormalizeData.AddRange(normData[i]);
                }
            }
            catch (Exception)
            {

            }

            return NormalizeData;
        }

        public List<List<Tuple<int, int, int>>> Normalization(List<List<Tuple<int, int, int>>> XYCoord)
        {
            List<List<Tuple<int, int, int>>> NormalizedAverageDataList = new List<List<Tuple<int, int, int>>>();

            for (int i = 0; i < Mainframe.NormalizeTriangleIteration[ScannerTag]; i++)
            {
                NormalizedAverageDataList = GetNormalizedTriangleListArray(XYCoord);
            }

            return NormalizedAverageDataList;
        }

        public List<Tuple<int, int, int>> Normalization(List<Tuple<int, int, int>> XYCoordinate)
        {
            List<Tuple<int, int, int>> NormalizedAverageData = new List<Tuple<int, int, int>>(XYCoordinate);

            for (int i = 0; i < Mainframe.NormalizeTriangleIteration[ScannerTag]; i++)
            {
                NormalizedAverageData = SmoothNormalizedData(NormalizedAverageData.ToList());
            }

            return NormalizedAverageData;
        }

        private List<List<Tuple<int, int, int>>> GetNormalizedTriangleListArray(List<List<Tuple<int, int, int>>> xycoord)
        {
            List<List<Tuple<int, int, int>>> NormalizedAverageDataArray = new List<List<Tuple<int, int, int>>>();

            for (int i = 0; i < xycoord.Count; i++)
            {
                List<Tuple<int, int, int>> NormalizedAverageData = SmoothNormalizedData(xycoord[i]);

                NormalizedAverageDataArray.Add(NormalizedAverageData);
            }
            
            return NormalizedAverageDataArray;
        }

        //public List<Tuple<int, int, int>> NormalizationbyTime(int lastEntry, List<Tuple<int, int, int>>[] xycollection)
        //{
        //    List<Tuple<int, int, int>> ByTimeAveragePoint = new List<Tuple<int, int, int>>();

        //    try
        //    {
        //        int[] indices = GetIndices(lastEntry);

        //        List<Tuple<int, int, int>>[] reducedCollection = new List<Tuple<int, int, int>>[indices.Length];

        //        for (int i = 0; i < indices.Length; i++)
        //        {
        //            reducedCollection[i] = xycollection[indices[i]];
        //        }

        //        ByTimeAveragePoint = GetAveragebyTime(reducedCollection);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
        //    }

        //    return ByTimeAveragePoint;
        //}

        public List<List<Tuple<int, int, int>>> NormalizationByTime(int lastEntry, List<List<Tuple<int, int, int>>>[] xycollection)
        {
            List<List<Tuple<int, int, int>>> ByTimeAveragePoint = new List<List<Tuple<int, int, int>>>();

            try
            {
                int[] indices = GetIndices(lastEntry);

                List<List<Tuple<int, int, int>>>[] reducdedCollection = new List<List<Tuple<int, int, int>>>[indices.Length];

                for (int i = 0; i < indices.Length; i++)
                {
                    reducdedCollection[i] = xycollection[indices[i]];
                }

                ByTimeAveragePoint = GetAverageByTime(reducdedCollection);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return ByTimeAveragePoint;
        }

        private List<Tuple<int, int, int>> GetAveragebyTime(List<Tuple<int, int, int>>[] reducedData)
        {
            List<Tuple<int, int, int>> ByTimeAverageList = new List<Tuple<int, int, int>>();

            try
            {
                if (reducedData.Length != 0)
                {
                    int xAve = 0, yAve = 0, dAve = 0;

                    for (int i = 0; i < reducedData[0].Count; i++)
                    {
                        xAve = reducedData.Sum(x => x[i].Item1) / reducedData.Length;
                        yAve = reducedData.Sum(x => x[i].Item2) / reducedData.Length;
                        dAve = reducedData.Sum(x => x[i].Item3) / reducedData.Length;

                        ByTimeAverageList.Add(new Tuple<int, int, int>(xAve, yAve, dAve));
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return ByTimeAverageList;
        }

        private List<List<Tuple<int, int, int>>> GetAverageByTime(List<List<Tuple<int, int, int>>>[] reducedData)
        {
            List<List<Tuple<int, int, int>>> ByTimeAverageListArray = new List<List<Tuple<int, int, int>>>();

            try
            {
                if (reducedData.Length != 0)
                {
                    if (SameAmountReducedData(reducedData))
                    {
                        for (int i = 0; i < reducedData[0].Count; i++)
                        {
                            ByTimeAverageListArray.Add(GetAverageList(reducedData, i));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return ByTimeAverageListArray;
        }

        private List<Tuple<int, int, int>> GetAverageList(List<List<Tuple<int, int, int>>>[] data, int iterator)
        {
            List<Tuple<int, int, int>> ByTimeAverageList = new List<Tuple<int, int, int>>();

            try
            {
                int xAve = 0, yAve = 0, dAve = 0;

                int maxPoints = GetSmallestAmountOfPoints(data, iterator);

                for (int i = 0; i < maxPoints; i++)
                {
                    xAve = data.Sum(x => x[iterator][i].Item1) / data.Length;
                    yAve = data.Sum(x => x[iterator][i].Item2) / data.Length;
                    dAve = data.Sum(x => x[iterator][i].Item3) / data.Length;

                    ByTimeAverageList.Add(new Tuple<int, int, int>(xAve, yAve, dAve));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return ByTimeAverageList;
        }

        private int GetSmallestAmountOfPoints(List<List<Tuple<int, int, int>>>[] data, int iterator)
        {
            int minValue = 0;
            
            minValue = data[0][iterator].Count;

            for (int i = 1; i < data.Length; i++)
            {
                int value = data[i][iterator].Count;

                if (minValue > value)
                {
                    minValue = value;
                }
            }

            return minValue;
        }

        private bool SameAmountReducedData(List<List<Tuple<int, int, int>>>[] data)
        {
            int minArray = data.Min(x => x.Count);
            int maxArray = data.Max(x => x.Count);

            if (minArray == maxArray)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int[] GetIndices(int lastindex)
        {
            int[] indices = new int[Mainframe.NormalizebyTimeCountDataAverage[ScannerTag]];

            try
            {
                int firstindex = lastindex - Mainframe.NormalizebyTimeCountDataAverage[ScannerTag];

                if (firstindex < 0)
                {
                    firstindex = 10 + firstindex;
                }

                for (int i = 0; i < Mainframe.NormalizebyTimeCountDataAverage[ScannerTag]; i++)
                {
                    indices[i] = firstindex;
                    firstindex++;

                    if (firstindex == Mainframe.NormalizebyTimeCountStoredData[ScannerTag])
                    {
                        firstindex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return indices;
        }

        // this method relocates the xy coordinate based on angle change of the graph, mirroring of the data, adjusting of the data
        public List<Tuple<int, int, int>> ChangePositionOfXY(List<Tuple<int, int, int>> xycoordinate)
        {
            List<Tuple<int, int, int>> RelocatedXY = new List<Tuple<int, int, int>>();

            try
            {
                foreach (var distance in xycoordinate)
                {
                    int XA, YA, DA;

                    XA = distance.Item1;
                    XA = XA + Mainframe.NormalizeY[ScannerTag];

                    YA = distance.Item2;
                    YA = YA + Mainframe.NormalizeZ[ScannerTag];

                    DA = distance.Item3;

                    if (Mainframe.NormalizeMirror[ScannerTag])
                    {
                        XA = -XA;
                    }

                    RelocatedXY.Add(Tuple.Create(XA, YA, DA));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return RelocatedXY;
        }

        // this method is supposted to calculate the median average over the distance pairs of the significant parts of the distance array
        public List<List<short>> GetMedianAverageArray(List<List<short>> distance)
        {
            List<List<short>> median = new List<List<short>>();

            try
            {
                for (int i = 0; i < distance.Count; i++)
                {
                    median.Add(GetPartMedian(distance[i]));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return median;
        }

        private List<short> GetPartMedian(List<short> distance)
        {
            List<short> median = new List<short>();

            try
            {
                for (int i = 1; i < distance.Count - Mainframe.NormalizeMedianRange[ScannerTag]; i++)
                {
                    median.Add(AdjustDistanceMedian(i, distance));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return median;
        }

        private short AdjustDistanceMedian(int index, List<short> data)
        {
            List<short> relevantpoints = new List<short>();
            short medianDistance = 0;

            try
            {
                short[] points = data.ToArray();
                int cnt = 0;

                for (int i = index; cnt < Mainframe.NormalizeMedianRange[ScannerTag]; i++)
                {
                    relevantpoints.Add(points[i - 1]);
                    cnt++;
                }

                relevantpoints.Sort((x, y) => string.Compare(x.ToString(), y.ToString()));

                medianDistance = (short)GetRelevantMedianValue(relevantpoints, Mainframe.NormalizeMedianRange[ScannerTag]);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return medianDistance;
        }
        
        private int GetRelevantMedianValue(List<short> points, int range)
        {
            double lowIndex = range / 2.0;
            int distance = ((points[(int)lowIndex] + points[(int)(lowIndex + 0.5)]) / 2);

            return distance;
        }

        // this method calculates the aritmetic average over the X/Y pairs
        private List<Tuple<int, int, int>> SmoothNormalizedData(List<Tuple<int, int, int>> normalizedData)
        {
            List<Tuple<int, int, int>> averagedNormalizedData = new List<Tuple<int, int, int>>();

            int triangleRange = GetTriangleRange(normalizedData.Count);
            double maxValue = GetMiddleValue(triangleRange);

            for (int i = 0; i < normalizedData.Count - triangleRange; i++)
            {
                int Xd = GetTriangleValueX(normalizedData, i, maxValue);
                int Yd = GetTriangleValueY(normalizedData, i, maxValue);
                int Dxy = GetTriangleValueD(normalizedData, i, maxValue);

                averagedNormalizedData.Add(Tuple.Create(Xd, Yd, Dxy));
            }

            return averagedNormalizedData;
        }

        private int GetTriangleRange(int dataRange)
        {
            if (dataRange < Mainframe.NormalizeTriangleRange[ScannerTag])
            {
                return dataRange;
            }
            else
            {
                return Mainframe.NormalizeTriangleRange[ScannerTag];
            }
        }

        private int GetTriangleValueX(List<Tuple<int, int, int>> normalizedData, int index, double middle)
        {
            int adicity = 1;
            int adicityvalue = 0;
            int value = 0;
            int newX = 0;

            for (int i = 0; i + adicity <= Mainframe.NormalizeTriangleRange[ScannerTag]; i++)
            {
                if (adicity <= (Mainframe.NormalizeTriangleRange[ScannerTag] / 2))
                {
                    value = value + adicity * normalizedData[index + i].Item1;
                    value = value + adicity * normalizedData[(index + (Mainframe.NormalizeTriangleRange[ScannerTag])) - (i + 1)].Item1;

                    adicityvalue = adicityvalue + 2 * adicity;
                    adicity++;
                }
                else
                {
                    value = value + adicity * normalizedData[index + i].Item1;
                    adicityvalue = adicityvalue + adicity;
                }
            }

            newX = value / adicityvalue;

            return newX;
        }

        private int GetTriangleValueY(List<Tuple<int, int, int>> normalizedData, int index, double middle)
        {
            int adicity = 1;
            int adicityvalue = 0;
            int value = 0;
            int newY = 0;

            for (int i = 0; i + adicity <= Mainframe.NormalizeTriangleRange[ScannerTag]; i++)
            {
                if (adicity <= (Mainframe.NormalizeTriangleRange[ScannerTag] / 2))
                {
                    value = value + adicity * normalizedData[index + i].Item2;
                    value = value + adicity * normalizedData[(index + (Mainframe.NormalizeTriangleRange[ScannerTag])) - (i + 1)].Item2;

                    adicityvalue = adicityvalue + 2 * adicity;
                    adicity++;
                }
                else
                {
                    value = value + adicity * normalizedData[index + i].Item2;
                    adicityvalue = adicityvalue + adicity;
                }
            }

            newY = value / adicityvalue;

            return newY;
        }

        private int GetTriangleValueD(List<Tuple<int, int, int>> normalizedData, int index, double middle)
        {
            int adicity = 1;
            int adicityvalue = 0;
            int value = 0;
            int newD = 0;

            for (int i = 0; i + adicity <= Mainframe.NormalizeTriangleRange[ScannerTag]; i++)
            {
                if (adicity <= (Mainframe.NormalizeTriangleRange[ScannerTag] / 2))
                {
                    value = value + adicity * normalizedData[index + i].Item3;
                    value = value + adicity * normalizedData[(index + (Mainframe.NormalizeTriangleRange[ScannerTag])) - (i + 1)].Item3;

                    adicityvalue = adicityvalue + 2 * adicity;
                    adicity++;
                }
                else
                {
                    value = value + adicity * normalizedData[index + i].Item3;
                    adicityvalue = adicityvalue + adicity;
                }
            }

            newD = value / adicityvalue;

            return newD;
        }

        private double GetMiddleValue(int triangleRange)
        {
            double middle = Math.Ceiling(triangleRange / 2.0);

            return middle;
        }

        // this method will filter the incoming distance values for errors (0 distance) and for the selected threshold (min/max distance values)
        public List<List<short>> GetProcessedDistance(List<short> origDistance)
        {
            List<short> thresholdDistance = FilterDistanceOverThreshold(origDistance);
            List<List<short>> splitDistances = SplitDistanceOverGap(thresholdDistance);
            List<List<short>> corrSplitDistances = CorrectSplitDistance(splitDistances);

            return corrSplitDistances;
        }

        private List<List<short>> CorrectSplitDistance(List<List<short>> splitDistances)
        {
            List<List<short>> corrSplitDistance = new List<List<short>>();

            for (int i = 0; i < splitDistances.Count; i++)
            {
                if (splitDistances[i].Count > 5)
                {
                    corrSplitDistance.Add(splitDistances[i]);
                }
            }

            return corrSplitDistance;
        }

        private List<short> FilterDistanceOverThreshold(List<short> distance)
        {
            List<short> corrDistance = new List<short>();

            try
            {
                for (int i = 0; i < distance.Count; i++)
                {
                    if (distance[i] <= Mainframe.DistanceMaxThreshold[ScannerTag] && distance[i] >= Mainframe.DistanceMinThreshold[ScannerTag])
                    {
                        corrDistance.Add(distance[i]);
                    }
                    else
                    {
                        corrDistance.Add(-1);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return corrDistance;
        }

        private List<List<short>> SplitDistanceOverGap(List<short> distance)
        {
            List<List<short>> Distance = new List<List<short>>();

            try
            {
                int oldDistance = 0, newDistance = 0, oldIndex = 0;

                for (int i = 0; i < distance.Count; i++)
                {
                    if (oldDistance == 0)
                    {
                        oldDistance = distance[i];
                        oldIndex = 0;
                    }
                    else
                    {
                        newDistance = distance[i];

                        if (CheckDistanceGap(oldDistance, newDistance))
                        {
                            Distance.Add(GetPartDistance(distance.ToList(), i - oldIndex, oldIndex));
                            oldIndex = i;
                        }
                        else if (i == distance.Count - 1)
                        {
                            Distance.Add(GetPartDistance(distance.ToList(), i - oldIndex, oldIndex));
                        }

                        oldDistance = newDistance;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return Distance;
        }

        private List<short> GetPartDistance(List<short> distance, int range, int oldIndex)
        {
            return new List<short>(distance.GetRange(oldIndex, range));
        }

        private bool CheckDistanceGap(int distance1, int distance2)
        {
            if (Math.Abs(distance1 - distance2) > 60)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
