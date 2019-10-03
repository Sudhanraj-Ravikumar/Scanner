using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScannerDisplay.DatablockObjects;

namespace ScannerDisplay
{
    public partial class Analysisframe : Form
    {
        delegate void FillChartCallback();
        delegate void FillLabelValues();

        private Logger LogFiler
        { get; set; }

        private OpcClient PlcClient
        { get; set; }

        private List<Tuple<int, int, int>> CombinedScannerData
        { get; set; }

        private List<Tuple<int, int, int>> PossiblePointsofInterest
        { get; set; }

        private List<Tuple<int, int, int>> ReducedPointsofInterest
        { get; set; }

        private System.Threading.Timer Analyser
        { get; set; }

        public Analysisframe(List<Tuple<int, int, int>> combinedData, OpcClient client)
        {
            InitializeComponent();

            LogFiler = new Logger("Analysisframe");
            PlcClient = client;

            CombinedScannerData = combinedData;
        }

        private void Analysisframe_Shown(object sender, EventArgs e)
        {
            Analyser = new System.Threading.Timer(OnAnalyser, this, 0, Timeout.Infinite);
        }

        private void DrawAnalyser()
        {
            try
            {
                if (chartAnalyser.InvokeRequired)
                {
                    FillChartCallback test = new FillChartCallback(DrawAnalyser);
                    chartAnalyser.Invoke(test, new object[] { });
                }
                else
                {
                    chartAnalyser.Series["Analysis Graph"].Points.Clear();
                    chartAnalyser.Series["Points of Interest"].Points.Clear();

                    if (CombinedScannerData.Count != 0)
                    {
                        for (int i = 0; i < CombinedScannerData?.Count; i++)
                        {
                            chartAnalyser.Series["Analysis Graph"].Points.AddXY(CombinedScannerData[i].Item1, CombinedScannerData[i].Item2);
                        }
                    }

                    if (ReducedPointsofInterest != null)
                    {
                        for (int i = 0; i < ReducedPointsofInterest?.Count; i++)
                        {
                            chartAnalyser.Series["Points of Interest"].Points.AddXY(ReducedPointsofInterest[i].Item1, ReducedPointsofInterest[i].Item2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void OnAnalyser(object sender)
        {
            Analyser.Change(Timeout.Infinite, Timeout.Infinite);
            
            DrawAnalyser();

            Analyser.Change(2000, Timeout.Infinite);
        }

        private void buttonPOI_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < 200; i++)
                {
                    Thread.Sleep(1000);

                    //ReducedPointsofInterest = AnalysisFunctionality.GetPointsofInterest(CombinedScannerData);

                    //ScannerInformation info = new ScannerInformation();
                    //info.GetNecessaryInformation(ReducedPointsofInterest);
                    ////PossiblePointsofInterest = AnalysisFunctionality.GetPossiblePointsOfInterest(CombinedScannerData);
                    ////ReducedPointsofInterest = AnalysisFunctionality.FilterPossiblePoints(PossiblePointsofInterest);

                    ////ReducedPointsofInterest = AnalysisFunctionality.GetCornerPoints(CombinedScannerData, out int widthtopobject, out int leftside, out int rightside, out int deviationcenter, out int scannerdistance);

                    ////ReducedPointsofInterest = AnalysisFunctionality.GetCornerPoints(PossiblePointsofInterest, out int widthtopobject);

                    //Invoke(new Action(() =>
                    //{
                    //    labelWidthTopObjectValue.Text = info.TopSlabWidth.ToString();
                    //    labelDeviationCenterValue.Text = info.TopSlabYDeviation.ToString();
                    //    labelDistanceFromLeftValue.Text = info.GrippingDepthTopEast.ToString();
                    //    labelDistancefromRightValue.Text = info.GrippingDepthTopWest.ToString();
                    //}));
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void Analysisframe_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogFiler.Close();
            Dispose();
            Close();
        }
    }
}
