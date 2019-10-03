using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPS7Lnk.Advanced;

namespace ScannerDisplay.DatablockObjects
{
    public class ScannerInformation : PlcObject
    {
        [PlcMember("DB1.DBD 0")]
        public int TopSlabYDeviation;

        [PlcMember("DB1.DBD 4")]
        public int GroundSlabYDeviation;

        [PlcMember("DB1.DBD 8")]
        public int TopSlabWidth;

        [PlcMember("DB1.DBD 12")]
        public int YCoordinateTM;

        [PlcMember("DB1.DBD 16")]
        public int GrippingDepthTopCenter;

        [PlcMember("DB1.DBD 20")]
        public int GrippingDepthTopWest;

        [PlcMember("DB1.DBD 24")]
        public int GrippingDepthTopEast;

        [PlcMember("DB1.DBD 28")]
        public int GrippingDepthBottomEast;

        [PlcMember("DB1.DBD 32")]
        public int GrippingDepthBottomWest;

        [PlcMember("DB1.DBD 36")]
        public int TiltAngle;

        [PlcMember("DB1.DBX 40.0")]
        public bool EdgeDetect;

        [PlcMember("DB1.DBX 40.1")]
        public bool PileTiltWarning;

        [PlcMember("DB1.DBX 40.2")]
        public bool TongTiltWarning;

        [PlcMember("DB1.DBX 40.3")]
        public bool TongSwayWarning;

        [PlcMember("DB1.DBX 40.4")]
        public bool GrippingDepthDetected;

        private List<Tuple<int, int, int>> PointsofInterest
        { get; set; }
        private List<Tuple<int, int, int>> HorizontalPaired
        { get; set; }
        private List<Tuple<int, int, int>> VerticalPaired
        { get; set; }

        // all submethods used from this class are specified for poi in a most likely optimal world
        public void GetNecessaryInformation(List<Tuple<int, int, int>> poi /*, List<Tuple<int,int,int>> Horizontalpairedpoints,List<Tuple<int,int,int>> VerticalPairedpoints*/)
        {
            try
            {
                PointsofInterest = poi;
                //HorizontalPaired = Horizontalpairedpoints;
                //VerticalPaired = VerticalPairedpoints;
                TopSlabYDeviation = GetTopSlabDeviation();
                TopSlabWidth = GetTopSlabWidth();
                TiltAngle = GetTiltAngle();
                GrippingDepthTopCenter = GetGripTopCenter();
                GrippingDepthTopEast = GetGripTopEast();
                GrippingDepthTopWest = GetGripTopWest();
                GrippingDepthBottomEast = GetGripBottomEast();
                GrippingDepthBottomWest = GetGripBottomWest();
                GrippingDepthDetected = GetGrippingDepthDetected();
                PileTiltWarning = PileTiltdetector();
                EdgeDetect = EdgeDetection();
            }
            catch (Exception)
            {
                //todo
            }
        }

        private int GetTopSlabDeviation()
        {
            int deviation = 0;

            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            //List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetToppestCorner(PointsofInterest); // using the pairing method

            if (TopCorner.Count == 2)
            {
                deviation = (TopCorner[0].Item1 + TopCorner[1].Item1) / 2;
            }

            if (Mainframe.IsMatchingCoordinateSystem)
            {
                deviation = -deviation;
            }

            return deviation;
        }

        private int GetTopSlabWidth()
        {
            int width = 0;

            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            //List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetToppestCorner(PointsofInterest); // using the pairing method

            if (TopCorner.Count == 2)
            {
                width = (int)AnalysisFunctionality.GetDistance(TopCorner[0], TopCorner[1]);
            }

            return width;
        }

        private int GetTiltAngle()
        {
            int angle = 0;

            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            //List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetToppestCorner(PointsofInterest); // using the pairing method

            if (TopCorner.Count == 2)
            {
                //this select should normally return only 1 tuple out of the POI
                var minValue = TopCorner.Where(x => x.Item1 == TopCorner.Min(y => y.Item1));
                var maxValue = TopCorner.Where(x => x.Item1 == TopCorner.Max(y => y.Item1));

                List<Tuple<int, int, int>> lowValue = new List<Tuple<int, int, int>>(minValue.ToList());
                List<Tuple<int, int, int>> highValue = new List<Tuple<int, int, int>>(maxValue.ToList());

                angle = (int)AnalysisFunctionality.GetAngle(lowValue[0], highValue[0]);
            }
            
            return angle;
        }

        private int GetGripTopCenter()
        {
            int grip = 0;
            
            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            //List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetToppestCorner(PointsofInterest); // using the pairing method

            if (TopCorner.Count == 2)
            {
                grip = Mainframe.TongOffset - ((Math.Abs(TopCorner[0].Item2) + Math.Abs(TopCorner[1].Item2)) / 2);
            }

            return grip;
        }

        private int GetGripTopEast()
        {
            int grip = 0;

            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);

            //List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetToppestCorner(PointsofInterest); // using the pairing method

            if (TopCorner.Count == 2)
            {
                grip = Mainframe.TongOffset - Math.Abs(TopCorner[0].Item2);
            }

            return grip;
        }

        private int GetGripTopWest()
        {
            int grip = 0;

            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            //List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetToppestCorner(PointsofInterest); // using the pairing method
            if (TopCorner.Count == 2)
            {
                grip = Mainframe.TongOffset - Math.Abs(TopCorner[1].Item2);
            }

            return grip;
        }

        private int GetGripBottomWest()
        {
            int grip = 0;
            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            List<Tuple<int, int, int>> Bottompoint = new List<Tuple<int, int, int>>(HorizontalPaired);
            
            if (Bottompoint.Count > 2)
            {
                List<Tuple<int, int,int>> bottompairpointWest = new List<Tuple<int, int,int>>();
                Tuple<int, int, int> zero = new Tuple<int, int, int>(0, 0, 0);
                bottompairpointWest = Bottompoint.Skip(2).Take(2).ToList();
                if (bottompairpointWest[0].Item1 > 0)
                {
                    bottompairpointWest.Reverse();
                }
                if (TopCorner[0].Item1 > 0)
                {
                    TopCorner.Reverse();
                }
                if (!bottompairpointWest.Contains(zero))
                {
                    //grip = Mainframe.TongOffset - Math.Abs(bottompairpointWest[1].Item2);
                    grip =(int) AnalysisFunctionality.GetDistance(bottompairpointWest[0], TopCorner[0]);
                }
            }
            return grip;
        }

        private int GetGripBottomEast()
        {
            int grip = 0;
            List<Tuple<int, int, int>> TopCorner = AnalysisFunctionality.GetTopCorner(PointsofInterest);
            List<Tuple<int, int, int>> Bottompoint = new List<Tuple<int, int, int>>(HorizontalPaired);
            
            if (Bottompoint.Count > 2)
            {
                List<Tuple<int, int, int>> bottompairpointEast = new List<Tuple<int, int, int>>();
                Tuple<int, int, int> zero = new Tuple<int, int, int>(0, 0, 0);
                bottompairpointEast = Bottompoint.Skip(2).Take(2).ToList();
                if (bottompairpointEast[0].Item1 > 0)
                {
                    bottompairpointEast.Reverse();
                }
                if (TopCorner[0].Item1 > 0)
                {
                    TopCorner.Reverse();
                }
                if (!bottompairpointEast.Contains(zero))
                {
                    //grip = Mainframe.TongOffset - Math.Abs(bottompairpointEast[0].Item2);
                    grip = (int)AnalysisFunctionality.GetDistance(bottompairpointEast[1], TopCorner[1]);
                    //grip = (int)AnalysisFunctionality.GetDistance(bottompairpointEast[1], bottompairpointEast[0]);
                }

            }
            return grip;
            
        }

        private bool GetGrippingDepthDetected()
        {
            bool detected = false;
            List<Tuple<int, int, int>> Bottompoint = new List<Tuple<int, int, int>>(HorizontalPaired);
            if (Bottompoint.Count > 2)
            {
                List<Tuple<int, int, int>> bottompairpoint = new List<Tuple<int, int, int>>();
                Tuple<int, int, int> zero = new Tuple<int, int, int>(0, 0, 0);
                bottompairpoint = Bottompoint.Skip(2).Take(2).ToList();
                if (!bottompairpoint.Contains(zero))
                {
                    detected= true;
                }

            }

            return detected;
        }

        private bool PileTiltdetector()
        {
            bool detected = false;
            int piletilt = GetTiltAngle();
            
            if (piletilt > Mainframe.PileTiltOffset)
            {
                detected = true;
            }

            return detected;
        }

        private bool EdgeDetection()
        {
            bool detection = false;
            Tuple<int, int, int> zero = new Tuple<int, int, int>(0, 0, 0);
            List<Tuple<int, int, int>> pairpoints = new List<Tuple<int, int, int>>();

            if (VerticalPaired.Count >= 4)
            {
                for (int i = 0; i < VerticalPaired.Count - 1; i += 2)
                {
                    pairpoints = VerticalPaired.Skip(i).Take(2).ToList();
                    double length = AnalysisFunctionality.GetDistance(pairpoints[0], pairpoints[1]);

                    if (!pairpoints.Contains(zero) && length > Mainframe.EdgeOffset)
                    {
                        detection = true;
                        break;
                    }
                }
            }

            return detection;
        }
        
        // gap calculation of how far the slabs has been lifted
        // not included in the telegram. 
        private int GetGripBottomWestslab()
        {
            int grip = 0;

            if (VerticalPaired.Count >= 8)
            {
                List<Tuple<int, int, int>> grippingPointtop = VerticalPaired.Skip(2).Take(2).ToList();
                List<Tuple<int, int, int>> grippingPointBottom = VerticalPaired.Skip(6).Take(2).ToList();
                Tuple<int, int, int> zero = new Tuple<int, int, int>(0, 0, 0);

                if (!grippingPointtop.Contains(zero) && !grippingPointBottom.Contains(zero))
                {
                    grip = (int)AnalysisFunctionality.GetDistance(grippingPointtop[1], grippingPointBottom[0]);
                }
            }

            return grip;
        }
    }
}
