using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScannerDisplay
{
    public static class AnalysisFunctionality
    {
        public enum AnalyseType
        {
            Undef,
            Lift,
            Release
        }

        public enum AreaType
        {
            Undef,
            TopLine,
            BotLine,
            PileLine,
            LeftLine,
            RightLine
        }

        public delegate void POIDetected(List<Tuple<int, int, int>> POI);

        public static event POIDetected GetInformationfromPOI;

        static AnalysisFunctionality()
        {
            LogFiler = new Logger("AnalysisFunctionality");
        }

        private static Logger LogFiler
        { get; set; }

        private static List<Tuple<int, int, int>> AnalyzedData
        { get; set; }

        public static void OnPOIDetected(List<Tuple<int, int, int>> POI)
        {
            GetInformationfromPOI?.Invoke(POI);
        }

        //public static List<List<Tuple<int, int, int>>> GetPointsofInterest(AnalyseType type, List<Tuple<int, int, int>> combinedData, out List<Tuple<int, int, int>> EdgePoints)
        //{
        //    //based on the analysetype we have to alter the combine data - select only a specific field of view for lift OR release
        //    combinedData = AdjustCombineData(type, combinedData.ToList());

        //    List<Tuple<AreaType, List<Tuple<int, int, int>>>> Crosslines = GetNecessaryCrossLines(combinedData.ToList());

        //    EdgePoints = GetEdgesByCrossline(Crosslines.ToList());

        //    List<List<Tuple<int, int, int>>> Lines = GetLists(Crosslines);

        //    return Lines;
        //}

        //public static List<List<Tuple<int, int, int>>> GetPointsofInterest(AnalyseType type, List<Tuple<int, int, int>> combinedData, out List<Tuple<int, int, int>> sectionPoints)
        //{
        //    //combinedData = AdjustCombineData(type, combinedData.ToList());
        //    List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> intersectionPoints = GetPointsforIntersection(combinedData.ToList());
            
        //    List<List<Tuple<int, int, int>>> IntersectionPoints = new List<List<Tuple<int, int, int>>>();

        //    List<Tuple<int, int, int>> EdgePoints = GetEdgesbyCalculation(intersectionPoints);

        //    foreach (Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>> points in intersectionPoints)
        //    {
        //        if (points.Item2 != null)
        //        {
        //            List<Tuple<int, int, int>> PointsIntersection = new List<Tuple<int, int, int>>();

        //            PointsIntersection.Add(points.Item2.Item1);
        //            PointsIntersection.Add(points.Item2.Item2);
        //            IntersectionPoints.Add(PointsIntersection);
        //        }
        //    }

        //    if (EdgePoints.Count != 4)
        //    {
        //    }
        //        sectionPoints = EdgePoints;

        //    return IntersectionPoints;
        //}

        private static List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> GetPointsforIntersection(List<Tuple<int, int, int>> data)
        {
            List<Tuple<AreaType, List<Tuple<int, int, int>>>> Crosslines = GetNecessaryCrossLines(data.ToList());

            List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> intersectionPoints = GetLinePoints(Crosslines);

            return intersectionPoints;
        }

        private static List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> GetLinePoints(List<Tuple<AreaType, List<Tuple<int, int, int>>>> crosslines)
        {
            List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> linePoints = new List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>>();

            foreach (Tuple<AreaType, List<Tuple<int, int, int>>> line in crosslines)
            {
                Tuple<Tuple<int, int, int>, Tuple<int, int, int>> Points = GetPointsfromLine(line.Item2);

                linePoints.Add(new Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>(line.Item1, Points));
            }

            return linePoints;
        }

        private static Tuple<Tuple<int, int, int>, Tuple<int, int, int>> GetPointsfromLine(List<Tuple<int, int, int>> data)
        {
            if (data.Count < 5)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": Line got not enough points to create a intersectionline.");
                return null;
            }

            List<Tuple<int, int, int>> linePoints = GetAvePoints(data);

            if (linePoints.Count != 2)
            {
                return null;
            }

            return new Tuple<Tuple<int, int, int>, Tuple<int, int, int>>(linePoints[0], linePoints[1]);
        }

        private static List<Tuple<int, int, int>> GetAvePoints(List<Tuple<int, int, int>> data)
        {
            List<List<Tuple<int, int, int>>> lists = new List<List<Tuple<int, int, int>>>();
            List<Tuple<int, int, int>> points = new List<Tuple<int, int, int>>();

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    lists.Add(data.Take(3).ToList());
                }

                if (i == 1)
                {
                    data.Reverse();
                    lists.Add(data.Take(3).ToList());
                }
            }

            foreach (List<Tuple<int, int, int>> list in lists)
            {
                int aveX = list.Sum(x => x.Item1) / list.Count();
                int aveY = list.Sum(x => x.Item2) / list.Count();

                points.Add(new Tuple<int, int, int>(aveX, aveY, 0));
            }

            return points;
        }

        private static List<Tuple<int, int, int>> GetEdgesbyCalculation(List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> intersections)
        {
            List<Tuple<int, int, int>> EdgePoints = new List<Tuple<int, int, int>>();

            Tuple<Tuple<int, int, int>, Tuple<int, int, int>> TopPoints = GetPoints(intersections, AreaType.TopLine);
            Tuple<Tuple<int, int, int>, Tuple<int, int, int>> LeftPoints = GetPoints(intersections, AreaType.LeftLine);
            Tuple<Tuple<int, int, int>, Tuple<int, int, int>> RightPoints = GetPoints(intersections, AreaType.RightLine);
            Tuple<Tuple<int, int, int>, Tuple<int, int, int>> BotPoints = GetPoints(intersections, AreaType.BotLine);

            if (TopPoints?.Item2 != null && RightPoints?.Item2 != null)
            {
                EdgePoints.Add(GetIntersectionPoint(TopPoints, RightPoints));
            }

            if (TopPoints?.Item2 != null && LeftPoints?.Item2 != null)
            {
                EdgePoints.Add(GetIntersectionPoint(TopPoints, LeftPoints));
            }

            if (BotPoints?.Item2 != null && LeftPoints?.Item2 != null)
            {
                EdgePoints.Add(GetIntersectionPoint(BotPoints, LeftPoints));
            }

            if (BotPoints?.Item2 != null && RightPoints?.Item2 != null)
            {
                EdgePoints.Add(GetIntersectionPoint(BotPoints, RightPoints));
            }

            return EdgePoints;
        }

        private static Tuple<int, int, int> GetIntersectionPoint(Tuple<Tuple<int, int, int>, Tuple<int, int, int>> intersection1, Tuple<Tuple<int, int, int>, Tuple<int, int, int>> intersection2)
        {
            Tuple<double, double, int> equation1 = GetEquation(intersection1);
            Tuple<double, double, int> equation2 = GetEquation(intersection2);

            Tuple<int, int, int> Edge = InterceptEquation(equation1, equation2);

            return Edge;
        }

        private static Tuple<double, double, int> GetEquation(Tuple<Tuple<int, int, int>, Tuple<int, int, int>> points)
        {
            double slope = 0, intercept = 0;
            int yParallel = 0;

            if (points.Item1.Item1 < points.Item2.Item1)
            {
                slope = CalculateSlope(points.Item1, points.Item2);
            }
            else
            {
                slope = CalculateSlope(points.Item2, points.Item1);
            }

            if (double.IsInfinity(slope))
            {
                intercept = 0;
                yParallel = points.Item1.Item1;
            }
            else
            {
                intercept = CalculateIntercept(points.Item1, slope);
            }

            return new Tuple<double, double, int>(slope, intercept, yParallel);
        }

        private static double CalculateSlope(Tuple<int, int, int> p1, Tuple<int, int, int> p2)
        {
            double slope = ((double)(p1.Item2 - p2.Item2) / (double)(p1.Item1 - p2.Item1));

            return slope;
        }

        private static double CalculateIntercept(Tuple<int, int, int> p, double slope)
        {
            double intercept = p.Item2 - (double)(slope * p.Item1);

            return intercept;
        }

        private static Tuple<int, int, int> InterceptEquation(Tuple<double, double, int> eq1, Tuple<double, double, int> eq2)
        {
            int x = 0, y = 0;

            if (eq1.Item3 == 0 && eq2.Item3 == 0)
            {
                x = (int)((eq2.Item2 - eq1.Item2) / (eq1.Item1 - eq2.Item1));

                if (x >= Mainframe.AnalysisGraphChartAxisYMax || x <= Mainframe.AnalysisGraphChartAxisYMin)
                { }

                y = (int)(eq1.Item1 * x + eq1.Item2);

                if (y >= Mainframe.AnalysisGraphChartAxisZMax || y <= Mainframe.AnalysisGraphChartAxisZMin)
                { }
            }
            else
            {
                if (eq1.Item3 != 0)
                {
                    x = eq1.Item3;
                    y = (int)(eq2.Item1 * eq1.Item3 + eq2.Item2);
                }
                else if (eq2.Item3 != 0)
                {
                    x = eq2.Item3;
                    y = (int)(eq1.Item1 * eq2.Item3 + eq1.Item2);
                }
            }

            return new Tuple<int, int, int>(x, y, 0);
        }

        private static List<Tuple<int, int, int>> AdjustCombineData(AnalyseType type, List<Tuple<int, int, int>> data)
        {
            List<Tuple<int, int, int>> selectedData = new List<Tuple<int, int, int>>();
            
            if (type == AnalyseType.Lift)
            {
                selectedData = data.Where(x => x.Item2 < 500).ToList();
            }
            else if (type == AnalyseType.Release)
            {
                selectedData = data.Where(x => x.Item2 > 700).ToList();
            }

            return selectedData;
        }

        //private static List<List<Tuple<int, int, int>>> GetLists(List<Tuple<AreaType, List<Tuple<int, int, int>>>> crosslines)
        //{
        //    List<List<Tuple<int, int, int>>> lines = new List<List<Tuple<int, int, int>>>();

        //    foreach(Tuple<AreaType, List<Tuple<int, int, int>>> value in crosslines)
        //    {
        //        lines.Add(value.Item2);
        //    }

        //    return lines;
        //}

        //private static List<Tuple<int, int, int>> GetEdgesByCrossline(List<Tuple<AreaType, List<Tuple<int, int, int>>>> crossLine)
        //{
        //    List<Tuple<int, int, int>> EdgePoints = new List<Tuple<int, int, int>>();

        //    List<Tuple<int, int, int>> TopLine = GetLine(crossLine, AreaType.TopLine);
        //    List<Tuple<int, int, int>> LeftLine = GetLine(crossLine, AreaType.LeftLine);
        //    List<Tuple<int, int, int>> RightLine = GetLine(crossLine, AreaType.RightLine);
        //    List<Tuple<int, int, int>> BotLine = GetLine(crossLine, AreaType.BotLine);

        //    if (TopLine.Count != 0 && LeftLine.Count != 0)
        //    {
        //        EdgePoints.Add(GetEdgeCrossline(TopLine, LeftLine));
        //    }

        //    if (TopLine.Count != 0 && RightLine.Count != 0)
        //    {
        //        EdgePoints.Add(GetEdgeCrossline(TopLine, RightLine));
        //    }

        //    if (BotLine.Count != 0 && LeftLine.Count != 0)
        //    {
        //        EdgePoints.Add(GetEdgeCrossline(BotLine, LeftLine));
        //    }

        //    if (BotLine.Count != 0 && RightLine.Count != 0)
        //    {
        //        EdgePoints.Add(GetEdgeCrossline(BotLine, RightLine));
        //    }

        //    return EdgePoints;
        //}

        //private static Tuple<int, int, int> GetEdgeCrossline(List<Tuple<int, int, int>> line1, List<Tuple<int, int, int>> line2)
        //{
        //    Tuple<int, int, int> edge = new Tuple<int, int, int>(0, 0, 0);

        //    for (int i = 0; i < line1.Count; i++)
        //    {
        //        bool exist = line2.Contains(line1[i]);

        //        if (exist)
        //        {
        //            return line1[i];
        //        }
        //    }

        //    return edge;
        //}

        //private static int GetSmallerValue(int value1, int value2)
        //{
        //    if (value1 == value2)
        //    {
        //        return 1;
        //    }
        //    else if (value1 > value2)
        //    {
        //        return 2;
        //    }
        //    else if (value1 < value2)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //private static List<Tuple<int, int, int>> GetLine(List<Tuple<AreaType, List<Tuple<int, int, int>>>> crosslines, AreaType type)
        //{
        //    List<Tuple<AreaType, List<Tuple<int, int, int>>>> Line = crosslines.Where(x => x.Item1 == type).ToList();

        //    return Line[0].Item2;
        //}

        private static Tuple<Tuple<int, int, int>, Tuple<int, int, int>> GetPoints(List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> sections, AreaType type)
        {
            List<Tuple<AreaType, Tuple<Tuple<int, int, int>, Tuple<int, int, int>>>> value = sections.Where(x => x.Item1 == type).ToList();

            return value[0].Item2;
        }

        private static List<Tuple<AreaType, List<Tuple<int, int, int>>>> GetNecessaryCrossLines(List<Tuple<int, int, int>> data)
        {
            List<Tuple<AreaType, List<Tuple<int, int, int>>>> NecessaryAreas = GetAreas(data);

            return NecessaryAreas;
        }

        private static List<Tuple<AreaType, List<Tuple<int, int, int>>>> GetAreas(List<Tuple<int, int,int>> data)
        {
            List<Tuple<AreaType, List<Tuple<int, int, int>>>> LineAreas = new List<Tuple<AreaType, List<Tuple<int, int, int>>>>();

            // get the highest point - get all the points between highest point and bandlimit
            int minZ = data.Min(x => x.Item2);
            List<Tuple<int, int, int>> MinZArea = data.Where(x => x.Item2 > minZ && x.Item2 < (minZ + Mainframe.AnalysingZBand)).ToList();

            // get the lower and higher edge of the pointarray (left / right)
            int minY = MinZArea.Min(x => x.Item1);
            int maxY = MinZArea.Max(x => x.Item1);

            MinZArea = ReduceArea(MinZArea.ToList());

            // get the left and right side array of points based on the topside array
            List<Tuple<int, int, int>> MinYArea = data.Where(x => x.Item1 > (minY - Mainframe.AnalysingYBand) && x.Item1 < (minY + Mainframe.AnalysingYBand)).ToList();
            List<Tuple<int, int, int>> MaxYArea = data.Where(x => x.Item1 < (maxY + Mainframe.AnalysingYBand) && x.Item1 > (maxY - Mainframe.AnalysingYBand)).Reverse().ToList();
            
            MinYArea = CorrectArea(MinYArea.ToList());
            MaxYArea = CorrectArea(MaxYArea.ToList());

            // based on the side arrays - get the highest point (possible wall behind the object)
            int maxZAve = GetBotlineAverage(MinYArea, MaxYArea);

            List<Tuple<int, int, int>> MaxZArea = data.Where(x => x.Item2 > (maxZAve - Mainframe.AnalysingZBand) && x.Item2 < (maxZAve + Mainframe.AnalysingZBand)).ToList();

            MinYArea = ReduceArea(MinYArea.ToList());
            MaxYArea = ReduceArea(MaxYArea.ToList());
            //MaxZArea = CorrectArea(MaxZArea.OrderBy(x => x.Item1).ToList());
            MaxZArea = ReduceArea(MaxZArea.ToList());

            //if (MinZArea.Count > 20)
            //{
            //    MinZArea = ReworkAreaToLineZ(MinZArea.ToList(), true, out double slope, out double angle);

            //    if (MinYArea.Count > 10)
            //    {
            //        MinYArea = ReworkAreaToLineY(MinYArea.ToList(), slope, angle);
            //    }

            //    if (MaxYArea.Count > 10)
            //    {
            //        MaxYArea = ReworkAreaToLineY(MaxYArea.ToList(), slope, angle);
            //    }

            //    if (MaxZArea.Count > 1)
            //    {
            //        MaxZArea = ReworkAreaToLineZ(MaxZArea.ToList(), false, out slope, out angle);
            //    }
            //}

            LineAreas.Add(new Tuple<AreaType, List<Tuple<int, int, int>>>(AreaType.TopLine, MinZArea));
            LineAreas.Add(new Tuple<AreaType, List<Tuple<int, int, int>>>(AreaType.LeftLine, MinYArea));
            LineAreas.Add(new Tuple<AreaType, List<Tuple<int, int, int>>>(AreaType.RightLine, MaxYArea));
            LineAreas.Add(new Tuple<AreaType, List<Tuple<int, int, int>>>(AreaType.BotLine, MaxZArea));

            return LineAreas;
        }

        private static int GetBotlineAverage(List<Tuple<int, int, int>> minY, List<Tuple<int, int, int>> maxY)
        {
            int maxZleft = 0, maxZright = 0, maxZAve = 0;

            if (minY.Count > 15)
            {
                maxZleft = minY.Max(x => x.Item2);
            }

            if (maxY.Count > 15)
            {
                maxZright = maxY.Max(x => x.Item2);
            }

            if (maxZleft != 0 && maxZright != 0)
            {
                maxZAve = (maxZleft + maxZright) / 2;
            }
            else if (maxZleft != 0)
            {
                maxZAve = maxZleft;
            }
            else if (maxZright != 0)
            {
                maxZAve = maxZright;
            }

            return maxZAve;
        }

        //private static List<Tuple<int, int, int>> ReworkAreaToLineZ(List<Tuple<int, int, int>> data, bool withAngle, out double slope, out double Angle)
        //{
        //    List<Tuple<int, int, int>> ReworkedLine = new List<Tuple<int, int, int>>();

        //    int ave = GetAverageZ(data);
        //    slope = GetSlope(data, out double angle);
        //    Angle = angle;

        //    if (Math.Abs(angle) < 3 || !withAngle)
        //    {
        //        ReworkedLine = ReworkListZ(ave);
        //    }
        //    else
        //    {
        //        ReworkedLine = ReworkListZ(ave, slope);
        //    }

        //    return ReworkedLine;
        //}

        //private static List<Tuple<int, int, int>> ReworkAreaToLineY(List<Tuple<int, int, int>> data, double slope, double Angle)
        //{
        //    List<Tuple<int, int, int>> ReworkedLine = new List<Tuple<int, int, int>>();

        //    int ave = GetAverageY(data);
        //    slope = GetSlopeY(data, out double angle, out int point);
        //    Angle = angle;

        //    if (Math.Abs(angle) < 3)
        //    {
        //        ReworkedLine = ReworkListY(ave);
        //    }
        //    else
        //    {
        //        ReworkedLine = ReworkListY(ave, slope, point);
        //    }

        //    return ReworkedLine;
        //}

        //private static List<Tuple<int, int, int>> ReworkListZ(int ave)
        //{
        //    List<Tuple<int, int, int>> ReworkList = new List<Tuple<int, int, int>>();

        //    for (int i = Mainframe.AnalysisGraphChartAxisYMin; i < Mainframe.AnalysisGraphChartAxisYMax; i++)
        //    {
        //        ReworkList.Add(new Tuple<int, int, int>(i, ave, 0));
        //    }

        //    return ReworkList;
        //}

        //private static List<Tuple<int, int, int>> ReworkListZ(int ave, double slope)
        //{
        //    List<Tuple<int, int, int>> ReworkList = new List<Tuple<int, int, int>>();

        //    for (int i = Mainframe.AnalysisGraphChartAxisYMin; i < Mainframe.AnalysisGraphChartAxisYMax; i++)
        //    {
        //        if (i == 0)
        //        {

        //        }
        //        int y = (int)(slope * i) + ave;

        //        ReworkList.Add(new Tuple<int, int, int>(i, y, 0));
        //    }

        //    return ReworkList;
        //}

        //private static List<Tuple<int, int, int>> ReworkListY(int ave)
        //{
        //    List<Tuple<int, int, int>> ReworkList = new List<Tuple<int, int, int>>();

        //    for (int i = Mainframe.AnalysisGraphChartAxisZMin; i < Mainframe.AnalysisGraphChartAxisZMax; i++)
        //    {
        //        ReworkList.Add(new Tuple<int, int, int>(ave, i, 0));
        //    }

        //    return ReworkList;
        //}

        //private static List<Tuple<int, int, int>> ReworkListY(int ave, double slope, int point)
        //{
        //    List<Tuple<int, int, int>> ReworkList = new List<Tuple<int, int, int>>();

        //    for (int i = Mainframe.AnalysisGraphChartAxisZMin; i < Mainframe.AnalysisGraphChartAxisZMax; i++)
        //    {
        //        int x = (int)((i - point) / slope);

        //        ReworkList.Add(new Tuple<int, int, int>(x, i, 0));
        //    }

        //    return ReworkList;
        //}

        private static double GetSlope(List<Tuple<int, int, int>> data, out double angle)
        {
            double slope = ((double)(data[0].Item2 - data[data.Count - 1].Item2) / (data[0].Item1 - data[data.Count - 1].Item1));
            angle = Math.Tan(slope) * (180 / Math.PI);

            return slope;
        }

        private static double GetSlopeY(List<Tuple<int, int, int>> data, out double angle, out int point)
        {
            double slope = ((double)(data[0].Item2 - data[data.Count - 1].Item2) / (data[0].Item1 - data[data.Count - 1].Item1));

            point = GetYCrosspoint(data[0], slope);
            angle = Math.Tan(slope) * (180 / Math.PI);

            return slope;
        }

        private static int GetYCrosspoint(Tuple<int, int, int> point, double slope)
        {
            int cross = (point.Item2 - (int)(slope * point.Item1));

            return cross;
        }

        private static int GetAverageZ(List<Tuple<int, int, int>> data)
        {
            int ave = data.Sum(x => x.Item2) / data.Count;

            return ave;
        }

        private static int GetAverageY(List<Tuple<int, int, int>> data)
        {
            int ave = data.Sum(x => x.Item1) / data.Count;

            return ave;
        }

        private static List<Tuple<int, int, int>> CorrectArea(List<Tuple<int, int, int>> data)
        {
            int iterator = 0;

            for (int i = 0; i < data.Count - 1; i++)
            {
                double distance = GetDistance(data[i], data[i + 1]);

                if (distance > 50)
                {
                    iterator = i + 1;
                    break;
                }
            }

            if (iterator == 0)
            {
                iterator = data.Count;
            }

            return new List<Tuple<int, int, int>>(data.Take(iterator));
        }

        private static List<Tuple<int, int, int>> ReduceArea(List<Tuple<int, int, int>> data)
        {
            int amount = data.Count;
            int min = amount * 20 / 100;
            int max = amount * 80 / 100;

            data = data.Skip(min).Take(max - min).ToList();

            return data;
        }

        // this method should work fine - but the incoming scanner data is horrible unstable and so the edges cant get detected on a clear base
        public static List<List<Tuple<int, int, int>>> GetPointsofInterest(List<Tuple<int, int, int>> combinedData, out List<Tuple<int, int, int>> EdgePoints)
        {
            // originally the mid should have been the middle of the data - but this most probably wont be the middle of the 
            int mid = combinedData.Count / 2;

            List<Tuple<int, int, int>>[] bothsides = new List<Tuple<int, int, int>>[2];

            bothsides[0] = combinedData.Skip(mid).ToList();
            bothsides[1] = combinedData.Take(mid).ToList();

            List<Tuple<int, int, int>> CorrectedData = new List<Tuple<int, int, int>>();
            List<Tuple<int, int, int>> FilteredPointsofInterest = new List<Tuple<int, int, int>>();
            List<Tuple<int, int, int>> ReducedPointsofInterest = new List<Tuple<int, int, int>>();
            List<Tuple<int, int, int>> POI = new List<Tuple<int, int, int>>();
            List<List<Tuple<int, int, int>>> areas = new List<List<Tuple<int, int, int>>>();

            for (int i = 0; i < bothsides.Length; i++)
            {
                FilteredPointsofInterest = GetPossiblePointsOfInterest(bothsides[i]);
                List<List<Tuple<int, int, int>>> possibleAreas = GetEdgesAreas(FilteredPointsofInterest.ToList());
                List<Tuple<int, int, int>> edge = GetEdgePoints(possibleAreas);
                areas.AddRange(possibleAreas);
                ReducedPointsofInterest.AddRange(edge);
                if (i == 1)
                {
                    bothsides[i].Reverse();
                }
                ReducedPointsofInterest.AddRange(GetJumpEdges(bothsides[i]));
            }

            EdgePoints = ReducedPointsofInterest;
            
            //OnPOIDetected(FilteredPointsofInterest);

            return areas;
            //return ReducedPointsofInterest;
        }

        private static List<Tuple<int, int, int>> GetJumpEdges(List<Tuple<int, int, int>> data)
        {
            List<Tuple<int, int, int>> GapEdges = new List<Tuple<int, int, int>>();

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (GapDetected(data, i))
                {
                    GapEdges.Add(data[i]);
                }
            }

            return GapEdges;
        }

        private static bool GapDetected(List<Tuple<int, int, int>> data, int iterator)
        {
            double distance = GetDistance(data[iterator], data[iterator + 1]);

            if (distance > 40)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Tuple<int, int, int>> FilterGapPoints(List<Tuple<int, int, int>> data)
        {
            try
            {
                List<Tuple<int, int>> removeIndex = new List<Tuple<int, int>>();
                int aveCnt = 1, aveMaxCnt = 20;
                bool IsAveraged = false, IsSequence = false;
                int firstIndex = 0, IndexRange = 0;
                double aveDist = 0;

                for (int i = 0; i < data.Count - 1; i++)
                {
                    if (aveCnt <= aveMaxCnt)
                    {
                        double dist = GetDistance(data[i], data[i + 1]);
                        aveDist = aveDist + dist;
                        aveCnt++;
                    }
                    else
                    {
                        if (!IsAveraged)
                        {
                            aveDist = aveDist / aveMaxCnt;
                            IsAveraged = true;
                        }
                    }

                    if (IsAveraged)
                    {
                        double distance = GetDistance(data[i], data[i + 1]);

                        if (distance > (aveDist * 4))
                        {
                            if (firstIndex != 0)
                            {
                                if (i == firstIndex + IndexRange)
                                {
                                    IndexRange++;
                                }
                                else
                                {
                                    removeIndex.Add(new Tuple<int, int>(firstIndex, IndexRange));
                                    firstIndex = 0;
                                    IsSequence = false;
                                }
                            }
                            else
                            {
                                IsSequence = true;
                                firstIndex = i;
                                IndexRange++;
                            }
                        }
                    }
                }

                if (IsSequence)
                {
                    removeIndex.Add(new Tuple<int, int>(firstIndex, IndexRange));
                }

                // reserve the order so the last index is deleted before the first index
                removeIndex.Reverse();

                foreach (Tuple<int, int> range in removeIndex)
                {
                    data.RemoveRange(range.Item1, range.Item2);
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return data;
        }

        //private static List<List<Tuple<int, int, int>>> GetPointsofInterest(List<Tuple<int, int, int>> combinedData, out List<Tuple<int, int, int>> edgePoints)
        //{
        //    List<List<Tuple<int, int, int>>> areas = new List<List<Tuple<int, int, int>>>();

        //    int mid = combinedData.Count / 2;

        //    // the following code will most probably only work if the combined data is combined over the middle of the object
        //    // since the graph should be normalizied over the middle of the object - this shouldnt become a bigger issue
        //    List<Tuple<int, int, int>>[] bothsides = new List<Tuple<int, int, int>>[2];

        //    bothsides[0] = combinedData.Skip(mid).ToList();
        //    bothsides[1] = combinedData.Take(mid).ToList();

        //    edgePoints = GetTopLinePoints(bothsides);

        //    edgePoints = null;

        //    return areas;
        //}

        //private static List<Tuple<int, int, int>> GetTopLinePoints(List<Tuple<int, int, int>>[] sides)
        //{
        //    // first of all try to get the line used to compare the points for the edge jump
        //    int slope = (sides[1][40].Item2 - sides[0][40].Item2) / (sides[1][40].Item1 - sides[0][40].Item1);

        //    return new List<Tuple<int, int, int>>();
        //}

        //public static List<List<Tuple<int, int, int>>> GetPointsofInterest(List<Tuple<int, int, int>> combinedData, out List<Tuple<int, int, int>> EdgePoints, out List<Tuple<int, int, int>> HorizontalPairs, out List<Tuple<int, int, int>> VerticalPairs)
        //{
        //    List<Tuple<int, int, int>> PossiblePointsofInterest = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> FilteredPointsofInterest = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> ReducedPointsofInterest = new List<Tuple<int, int, int>>();

        //    FilteredPointsofInterest = GetPossiblePointsOfInterest(combinedData);

        //    List<List<Tuple<int, int, int>>> areas = GetEdgesAreas(FilteredPointsofInterest.ToList());

        //    EdgePoints = GetEdgePoints(areas);
        //    GetPairedPoints(EdgePoints, out List<Tuple<int, int, int>> horizontalPair, out List<Tuple<int, int, int>> verticalPair);

        //    HorizontalPairs = horizontalPair.ToList();
        //    VerticalPairs = verticalPair.ToList();
        //    //OnPOIDetected(FilteredPointsofInterest);

        //    return areas;
        //    //return ReducedPointsofInterest;
        //}

        private static List<Tuple<int, int, int>> GetPossiblePointsOfInterest(List<Tuple<int, int, int>> combinedData)
        {
            List<Tuple<int, int, int>> PointsOfInterest = new List<Tuple<int, int, int>>();
            List<Tuple<int, int, int>> EdgePoints = new List<Tuple<int, int, int>>();
            List<Tuple<int, int, int>> EdgePoint = new List<Tuple<int, int, int>>();

            if (combinedData?.Count == 0)
            {
                return null;
            }

            AnalyzedData = new List<Tuple<int, int, int>>(combinedData);

            if (combinedData.Count < Mainframe.AnalysizedPoints)
            {
                return null;
            }

            try
            {
                for (int i = 0; i < AnalyzedData.Count - Mainframe.AnalysizedPoints; i += Mainframe.AnalysizedPointsPitch)
                {
                    GetNecessaryDistances(i, AnalyzedData, out double firstlastDistance, out double overalldistance, out double Degree);

                    if (CheckDistanceDifference(firstlastDistance, overalldistance))
                    {
                        for (int j = 0; j < Mainframe.AnalysizedPoints; j++)
                        {
                            PointsOfInterest.Add(AnalyzedData[i + j]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return new List<Tuple<int, int, int>>(PointsOfInterest.Distinct());
        }

        private static List<List<Tuple<int, int, int>>> GetEdgesAreas(List<Tuple<int, int, int>> filteredPoints)
        {
            List<List<Tuple<int, int, int>>> areas = new List<List<Tuple<int, int, int>>>();
            List<Tuple<int, int, int>> area = new List<Tuple<int, int, int>>();

            bool notEmpty = true;

            while (notEmpty)
            {
                area = GetArea(filteredPoints);

                if (area.Count > 15)
                {
                    areas.Add(area);
                }

                filteredPoints.RemoveRange(0, area.Count);

                if (filteredPoints.Count == 0)
                {
                    notEmpty = false;
                }
            }

            return new List<List<Tuple<int, int, int>>>(areas);
        }

        // this method checks the possible points for their connection between each other - if the distance between 2 points is to big - the area will be split
        private static List<Tuple<int, int, int>> GetArea(List<Tuple<int, int, int>> possibleArea)
        {
            int oldy = 0, oldz = 0, range = 0;

            foreach (Tuple<int, int, int> point in possibleArea)
            {
                if (range == 0)
                {
                    oldy = point.Item1;
                    oldz = point.Item2;

                    range++;

                    continue;
                }

                if (Math.Abs(point.Item1 - oldy) > Mainframe.AnalysizedYDeviationEdge)
                {
                    break;
                }

                if (Math.Abs(point.Item2 - oldz) > Mainframe.AnalysizedZDeviationEdge)
                {
                    break;
                }

                oldy = point.Item1;
                oldz = point.Item2;

                range++;
            }

            return new List<Tuple<int, int, int>>(possibleArea.Take(range));
        }

        private static List<Tuple<int, int, int>> GetEdgePoints(List<List<Tuple<int, int, int>>> edgeAreas)
        {
            List<Tuple<int, int, int>> EdgePoint = new List<Tuple<int, int, int>>();
            List<Tuple<int, int, int>> EdgePoints = new List<Tuple<int, int, int>>();

            for (int i = 0; i < edgeAreas.Count; i++)
            {
                List<Tuple<int, int, int>> correctedArea = CorrectAreaDirection(edgeAreas[i].ToList());
                EdgePoint = GetEdges(correctedArea);

                if (EdgePoint.Count != 0)
                {
                    EdgePoints.Add(EdgePoint[0]);
                    EdgePoint.Clear();
                }
            }

            return EdgePoints;
        }

        private static List<Tuple<int, int, int>> CorrectAreaDirection(List<Tuple<int, int, int>> originalArea)
        {
            int count = 1;
            int startZ = originalArea[0].Item2;

            for (int i = 1; i < originalArea.Count / 2; i++)
            {
                if (originalArea[i].Item2 < startZ + Mainframe.AnalysizedAreaCorrectionThreshold && originalArea[i].Item2 > startZ - Mainframe.AnalysizedAreaCorrectionThreshold)
                {
                    count++;
                }
            }

            if (count >= originalArea.Count / 2)
            {
                return originalArea;
            }
            else
            {
                originalArea.Reverse();
                return originalArea;
            }
        }

        //private static List<Tuple<int, int, int>> GetPairPoints(List<Tuple<int, int, int>> analyseData)
        //{
        //    var y_max = analyseData.Min(y => y.Item2);
        //    var toppair = analyseData.Where(y => (y.Item2 <= (y_max + 50)) && y.Item2 >= (y_max - 50));

        //    return new List<Tuple<int, int, int>>(toppair);
        //}

        private static bool CheckDistanceDifference(double point, double all)
        {
            double percent = point / 100;
            double deviation = all / percent;
            double diff = deviation - 100;

            if (Math.Abs(diff) > Mainframe.AnalysizedDeviation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void GetNecessaryDistances(int index, List<Tuple<int, int, int>> data, out double firstlastDistance, out double overallDistance, out double Degree)
        {
            double Opp, Adj, Angle, degree;

            firstlastDistance = GetDistance(data[index], data[index + Mainframe.AnalysizedPoints]);

            // this method had some issues with the amount of noise in our received scanner data
            //for (int i = 0; i < Analysisframe.AnalysizedPoints - 1; i++)
            //{
            //    double pointDistance = GetDistance(data[index + i + 1], data[index + i]);
            //    pointToPointDistance = pointToPointDistance + pointDistance;
            //}

            //overallDistance = pointToPointDistance;

            // using only 2 distances between start - mid / mid - end reduces the amount of noise - better results
            //int mid_point = (index + (index + Mainframe.AnalysizedPoints)) / 2;
            int mid_point = index + (Mainframe.AnalysizedPoints / 2);
            Adj = GetDistance(data[index], data[mid_point]);
            Opp = GetDistance(data[mid_point], data[index + Mainframe.AnalysizedPoints]);
            Angle = (((Math.Pow(Adj, 2) + Math.Pow(Opp, 2) - Math.Pow(firstlastDistance, 2)) / (2 * Adj * Opp)));

            degree = Math.Acos(Angle);
            Degree = (degree * 180) / Math.PI;
            overallDistance = Opp + Adj;
        }

        public static double GetDistance(Tuple<int, int, int> lowValue, Tuple<int, int, int> highValue)
        {
            double xdif = Math.Pow((highValue.Item1 - lowValue.Item1), 2);
            double ydif = Math.Pow((highValue.Item2 - lowValue.Item2), 2);
            double distance = Math.Sqrt(xdif + ydif);

            return distance;
        }

        //private static bool CheckAngleDifference(double overall, double average)
        //{
        //    if (Math.Abs((Math.Abs(overall) - Math.Abs(average))) > Mainframe.AnalysizedDeviation)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private static void GetNecessaryAngles(int index, List<Tuple<int, int, int>> data, out double spanangle, out double pointAverageAngle, out double allPointAverageAngle)
        //{
        //    // get the angle between our origin point and the last viewpoint
        //    spanangle = GetAngle(data[index], data[index + Mainframe.AnalysizedPoints]);

        //    double pointToPointAngle = 0, allPointsAngle = 0;
        //    int pointToPointCount = 1, allPointsCount = 1;

        //    for (int i = 0; i < Mainframe.AnalysizedPoints - 1; i++)
        //    {
        //        double angle = GetAngle(data[index + i], data[index + i + 1]);
        //        pointToPointAngle = (pointToPointAngle + angle) / pointToPointCount;
        //        allPointsAngle = allPointsAngle + angle;
        //        allPointsCount++;

        //        if (i == 0)
        //        {
        //            pointToPointCount = 2;
        //        }
        //    }

        //    allPointsAngle = allPointsAngle / allPointsCount;

        //    pointAverageAngle = pointToPointAngle;
        //    allPointAverageAngle = allPointsAngle;
        //}

        public static double GetAngle(Tuple<int, int, int> lowValue, Tuple<int, int, int> highValue)
        {
            double angle = 0;

            try
            {
                double radiantangle = Math.Atan2(((double)highValue.Item2 - lowValue.Item2), ((double)highValue.Item1 - lowValue.Item1));
                angle = radiantangle / (Math.PI / 180);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return angle;
        }

        public static List<Tuple<int, int, int>> GetTopCorner(List<Tuple<int, int, int>> poi)
        {
            var topPoints = poi.Where(x => x.Item2 <= poi.Min(y => y.Item2 + Mainframe.AnalysingYBand) && x.Item2 >= poi.Min(y => y.Item2));

            return new List<Tuple<int, int, int>>(topPoints.ToList());
        }

        public static List<Tuple<int, int, int>> GetBotCorner(List<Tuple<int, int, int>> poi)
        {
            var botPoints = poi.Where(x => x.Item2 >= poi.Max(y => y.Item2 - Mainframe.AnalysingZBand) && x.Item2 <= poi.Max(y => y.Item2));

            return new List<Tuple<int, int, int>>(botPoints.ToList());
        }

        private static bool CheckAngle(double degree)
        {
            if ((degree >= Mainframe.AnalysizedAngleLow) && (degree <= Mainframe.AnalysizedAngleHigh))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<Tuple<int, int, int>> GetEdges(List<Tuple<int, int, int>> data)
        {
            List<Tuple<int, int, int>> Edges = new List<Tuple<int, int, int>>();

            try
            {
                double opposite, adjacent, angleRadian, angleDegree;

                List<Tuple<int, int, int, double>> dummy = new List<Tuple<int, int, int, double>>();

                for (int i = 1; i < data.Count - 1; i++)
                {
                    adjacent = GetDistance(data[0], new Tuple<int, int, int>(data[i].Item1, data[0].Item2, 0));

                    if (adjacent < 8)
                    {
                        continue;
                    }

                    opposite = GetDistance(data[i], new Tuple<int, int, int>(data[i].Item1, data[0].Item2, 0));

                    //if (opposite < 6)
                    //{
                    //    continue;
                    //}

                    angleRadian = Math.Tan(opposite / adjacent);

                    angleDegree = (angleRadian * 180) / Math.PI;

                    if (CheckAngle(angleDegree))
                    {
                        dummy.Add(Tuple.Create(data[i].Item1, data[i].Item2, data[i].Item3, angleDegree));

                    }
                }

                if (dummy.Count != 0)
                {
                    // this linQ takes the points into consideration for being the edge with the lowest (+15) degree and combines them to an average which
                    // is getting compared with the original values - based on this the original point which is closest to the average will be taken as edge - something like that
                    List<Tuple<int, int, int, double>> edgesList = dummy.Where(x => x.Item4 < dummy.Min(y => y.Item4 + Mainframe.AnalysizedAngleEdge)).ToList();

                    int listIndex = 0, newX = 0, newY = 0, newD = 0;

                    if (edgesList.Count > 1)
                    {
                        newX = edgesList.Sum(x => x.Item1) / edgesList.Count;
                        newY = edgesList.Sum(x => x.Item2) / edgesList.Count;
                        newD = edgesList.Sum(x => x.Item3) / edgesList.Count;

                        listIndex = GetNearestPoint(edgesList, newX, newY);
                    }
                    // should no longer happen
                    else if (edgesList.Count == 1)
                    {
                        newX = edgesList[0].Item1;
                        newY = edgesList[0].Item2;
                        newD = edgesList[0].Item3;

                        listIndex = 0;
                    }

                    Edges.Add(Tuple.Create(newX, newY, newD/*edgesList[listIndex].Item1, edgesList[listIndex].Item2, edgesList[listIndex].Item3)*/));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return Edges;
        }

        // Keeping the hypotenuse constant and changing the mid point to find the corner point
        //private static List<Tuple<int, int, int>> GetEdges(List<Tuple<int, int, int>> data)
        //{
        //    List<Tuple<int, int, int>> Edges = new List<Tuple<int, int, int>>();

        //    try
        //    {
        //        double opposite, adjacent, angle, radian, degree;

        //        List<Tuple<int, int, int, double>> dummy = new List<Tuple<int, int, int, double>>();

        //        double hypotenuse = GetDistance(data[0], data[data.Count - 1]);

        //        for (int i = 1; i < data.Count - 1; i++)
        //        {
        //            adjacent = GetDistance(data[0], data[i]);
        //            opposite = GetDistance(data[i], data[data.Count - 1]);
        //            angle = ((Math.Pow(adjacent, 2) + Math.Pow(opposite, 2) - Math.Pow(hypotenuse, 2)) / (2 * adjacent * opposite));

        //            radian = Math.Acos(angle);
        //            degree = (radian * 180) / Math.PI;

        //            if ((Math.Abs(data[data.Count - 1].Item2) - Math.Abs(data[i].Item2) > 0) && (Math.Abs(data[i].Item2) - Math.Abs(data[0].Item2) > 0))
        //            {
        //                if (CheckAngle(degree))
        //                {
        //                    dummy.Add(Tuple.Create(data[i].Item1, data[i].Item2, data[i].Item3, degree));

        //                }
        //            }
        //            else if ((Math.Abs(data[data.Count - 1].Item2) - Math.Abs(data[i].Item2) < 0) && (Math.Abs(data[i].Item2) - Math.Abs(data[0].Item2) < 0))
        //            {
        //                if (CheckAngle(degree))
        //                {
        //                    dummy.Add(Tuple.Create(data[i].Item1, data[i].Item2, data[i].Item3, degree));

        //                }
        //            }
        //        }

        //        if (dummy.Count != 0)
        //        {
        //            // this linQ takes the points into consideration for being the edge with the lowest (+15) degree and combines them to an average which
        //            // is getting compared with the original values - based on this the original point which is closest to the average will be taken as edge - something like that
        //            List<Tuple<int, int, int, double>> edgesList = dummy.Where(x => x.Item4 < dummy.Min(y => y.Item4 + Mainframe.AnalysizedAngleEdge)).ToList();

        //            int listIndex = 0, newX = 0, newY = 0, newD = 0;

        //            if (edgesList.Count > 1)
        //            {
        //                newX = edgesList.Sum(x => x.Item1) / edgesList.Count;
        //                newY = edgesList.Sum(x => x.Item2) / edgesList.Count;
        //                newD = edgesList.Sum(x => x.Item3) / edgesList.Count;

        //                listIndex = GetNearestPoint(edgesList, newX, newY);
        //            }
        //            // should no longer happen
        //            else if (edgesList.Count == 1)
        //            {
        //                newX = edgesList[0].Item1;
        //                newY = edgesList[0].Item2;
        //                newD = edgesList[0].Item3;

        //                listIndex = 0;
        //            }

        //            Edges.Add(Tuple.Create(newX, newY, newD/*edgesList[listIndex].Item1, edgesList[listIndex].Item2, edgesList[listIndex].Item3)*/));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
        //    }

        //    return Edges;
        //}

        private static int GetNearestPoint(List<Tuple<int, int, int, double>> edgePoints, int x, int y)
        {
            Tuple<int, int, int> avePoint = new Tuple<int, int, int>(x, y, 0);
            double shortestDistance = 0;
            int index = 0;

            for (int i = 0; i < edgePoints.Count; i++)
            {
                Tuple<int, int, int> possiblePoint = new Tuple<int, int, int>(edgePoints[i].Item1, edgePoints[i].Item2, 0);

                double distance = GetDistance(possiblePoint, avePoint);

                if (shortestDistance == 0)
                {
                    shortestDistance = distance;
                    index = i;
                }
                else
                {
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        index = i;
                    }
                }
            }

            return index;
        }

        //private static void GetPairedPoints(List<Tuple<int, int, int>> Pointdata, out List<Tuple<int, int, int>> Horizontalpairs, out List<Tuple<int, int, int>> VerticalPairs)
        //{
        //    Horizontalpairs = GetHorizontalpair(Pointdata);
        //    VerticalPairs = GetVerticalPair(Pointdata);
        //}

        //public static List<Tuple<int, int, int>> GetHorizontalpair(List<Tuple<int, int, int>> edgePoints)
        //{
        //    List<Tuple<int, int, int>> comparepoints = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> points = new List<Tuple<int, int, int>>(edgePoints);
        //    List<Tuple<int, int, int>> Horizontalpairs = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> listpairs = new List<Tuple<int, int, int>>();
        //    List<int> pointcount = new List<int>();

        //    int count = 0;

        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        for (int j = i + 1; j < points.Count - 1; j++)
        //        {
        //            comparepoints = MatchHorizontalPair(points[i], points[j]);

        //            if (comparepoints.Count == 2)
        //            {
        //                listpairs.AddRange(comparepoints);
        //                pointcount.Add(i);
        //                pointcount.Add(j);
        //            }

        //            if (j == points.Count - 1)
        //            {
        //                if (listpairs.Count != 0 && listpairs.Count == 2)
        //                {
        //                    if (Horizontalpairs.Count > 0)
        //                    {
        //                        Horizontalpairs.Add(listpairs[0]);
        //                        Horizontalpairs.Add(listpairs[1]);
        //                        points.Remove(Horizontalpairs[Horizontalpairs.Count - 1]);
        //                        points.TrimExcess();
        //                        //list.Add(Tuple.Create(0, 0, 0));
        //                        count++;
        //                    }
        //                    else if (Horizontalpairs.Count == 0)
        //                    {
        //                        Horizontalpairs.Add(listpairs[0]);
        //                        Horizontalpairs.Add(listpairs[1]);
        //                        points.Remove(Horizontalpairs[Horizontalpairs.Count - 1]);
        //                        points.TrimExcess();
        //                        //list.Add(Tuple.Create(0, 0, 0));
        //                        count++;
        //                    }
        //                }
        //                else if (listpairs.Count != 0 && listpairs.Count > 2)
        //                {
        //                    if (((listpairs.Count / 2) % 2) == 0)
        //                    {
        //                        comparepoints = DifferenceXPairpoints(listpairs);

        //                        if (comparepoints.Count != 0)
        //                        {
        //                            Horizontalpairs.Add(comparepoints[0]);
        //                            Horizontalpairs.Add(comparepoints[1]);
        //                            points.Remove(Horizontalpairs[Horizontalpairs.Count - 1]);
        //                            points.TrimExcess();
        //                            //list.Add(Tuple.Create(0, 0, 0)); // adding 0 tuple list to avoid out of range exception
        //                            count++;
        //                        }

        //                    }
        //                    else if (((listpairs.Count / 2) % 2) != 0)
        //                    {
        //                        if ((pointcount.Capacity != 0) && ((pointcount[1] - pointcount[0]) == 1)) //finding the starting point to pair the point if the number of points are odd
        //                        {                                                                      // and the point has no pair 
        //                            Horizontalpairs.Add(listpairs[0]);
        //                            Horizontalpairs.Add(Tuple.Create(0, 0, 0));
        //                            count++;
        //                        }
        //                        else if ((pointcount.Capacity != 0) && ((pointcount[1] - pointcount[0]) > 1))  // odd number of points and having the pair
        //                        {
        //                            Horizontalpairs.Add(listpairs[0]);
        //                            Horizontalpairs.Add(listpairs[1]);
        //                            count++;
        //                        }
        //                    }
        //                }

        //                listpairs.Clear();
        //            }
        //        }

        //        if (count == 0)
        //        {
        //            Horizontalpairs.Add(points[i]);
        //            Horizontalpairs.Add(Tuple.Create(0, 0, 0)); // adding 0 tuple if there is no pair corresponding to that edge
        //        }

        //        count = 0;
        //    }

        //    return Horizontalpairs;
        //}

        //public static List<Tuple<int, int, int>> GetVerticalPair(List<Tuple<int, int, int>> list)
        //{
        //    List<Tuple<int, int, int>> ComparePoints = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> lists = new List<Tuple<int, int, int>>(list);
        //    List<Tuple<int, int, int>> VerticalpointPair = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> ListPairs = new List<Tuple<int, int, int>>();
        //    int count = 0;

        //    for (int i = 0; i < lists.Count; i++)
        //    {
        //        for (int j = i + 1; j < lists.Count; j++)
        //        {
        //            ComparePoints = MatchVerticalPair(lists[i], lists[j]);

        //            if (ComparePoints.Count != 0)
        //            {
        //                ListPairs.Add(ComparePoints[0]);
        //                ListPairs.Add(ComparePoints[1]);
        //            }

        //            if (j == lists.Count - 1)
        //            {
        //                if (ListPairs.Count == 2)
        //                {
        //                    VerticalpointPair.Add(ListPairs[0]);
        //                    VerticalpointPair.Add(ListPairs[1]);
        //                    count++;
        //                }
        //                else if (ListPairs.Count > 2)
        //                {
        //                    VerticalpointPair.Add(ListPairs[0]);
        //                    VerticalpointPair.Add(ListPairs[1]);
        //                    lists.Remove(VerticalpointPair[VerticalpointPair.Count - 1]);
        //                    lists.TrimExcess();
        //                    //lists.Add(Tuple.Create(0, 0, 0));
        //                    count++;
        //                }

        //                ListPairs.Clear();
        //            }
        //        }

        //        if ((count == 0 /*&& i!= lists.Count-1) || (count==0 && i==lists.Count-1*/))
        //        {
        //            if (VerticalpointPair.Count > 0)
        //            {
        //                if (VerticalpointPair[VerticalpointPair.Count - 1].Item1 != lists[i].Item1)
        //                {
        //                    VerticalpointPair.Add(lists[i]);
        //                    VerticalpointPair.Add(Tuple.Create(0, 0, 0));
        //                }
        //            }
        //            else if (VerticalpointPair.Count == 0)
        //            {
        //                VerticalpointPair.Add(lists[i]);
        //                VerticalpointPair.Add(Tuple.Create(0, 0, 0));
        //            }
        //        }

        //        count = 0;
        //    }

        //    return VerticalpointPair;
        //}

        //private static List<Tuple<int, int, int>> MatchHorizontalPair(Tuple<int, int, int> list1, Tuple<int, int, int> list2)
        //{
        //    int Y1 = list1.Item2;
        //    int Y2 = list2.Item2;

        //    List<Tuple<int, int, int>> HorizontalPair = new List<Tuple<int, int, int>>();

        //    // if the difference between the points is big enough - it should be our object
        //    if (Math.Abs(Y2 - Y1) > Mainframe.AnalysizedZDeviationEdge)
        //    {
        //        HorizontalPair.Add(list1);
        //        HorizontalPair.Add(list2);
        //    }

        //    //if (Y2 <= (Y1 + Mainframe.AnalysizedZDeviation) && Y2 >= (Y1 - Mainframe.AnalysizedZDeviation))
        //    //{
        //    //    HorizontalPair.Add(list1);
        //    //    HorizontalPair.Add(list2);
        //    //}

        //    return HorizontalPair;
        //}

        //private static List<Tuple<int, int, int>> MatchVerticalPair(Tuple<int, int, int> list1, Tuple<int, int, int> list2)
        //{
        //    int X1 = list1.Item1;
        //    int X2 = list2.Item1;

        //    List<Tuple<int, int, int>> VerticalPair = new List<Tuple<int, int, int>>();

        //    if (X2 <= (X1 + Mainframe.AnalysizedYDeviationEdge) && X2 >= (X1 - Mainframe.AnalysizedYDeviationEdge))
        //    {
        //        VerticalPair.Add(list1);
        //        VerticalPair.Add(list2);
        //    }

        //    return VerticalPair;
        //}

        //private static List<Tuple<int, int, int>> DifferenceXPairpoints(List<Tuple<int, int, int>> list)
        //{
        //    List<Tuple<int, int, int>> differentPairPoint = new List<Tuple<int, int, int>>();
        //    List<Tuple<int, int, int>> PairPoints = new List<Tuple<int, int, int>>();

        //    differentPairPoint = list.Distinct().ToList();
        //    PairPoints.Add(differentPairPoint[0]);
        //    PairPoints.Add(differentPairPoint[differentPairPoint.Count - 1]);

        //    return PairPoints;
        //}
    }
}