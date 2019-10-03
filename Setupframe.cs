using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerDisplay
{
    public partial class Setupframe : Form
    {
        public delegate void ScannerOffsetChange(object sender, bool IsSet);

        public event ScannerOffsetChange ValueChanged;

        private Mainframe Main
        { get; set; }

        public Setupframe(Mainframe main)
        {
            InitializeComponent();

            LogFiler = new Logger("Setupframe");

            Main = main;

            LoadConfig();

            InitEvents();
        }

        private Logger LogFiler
        { get; set; }

        public void LoadConfig()
        {
            //InitSingleChartValues();
            InitCombinerChartValues();
            InitAnalysisGraphChartValues();
            InitRecorderGraphChartValues();
            InitAnalysisValues();
            InitGeneralValues();
        }

        private void InitEvents()
        {
            //textBoxSingleChartScanner1AxisYMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner2AxisYMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner1AxisYMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner2AxisYMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner1AxisZMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner2AxisZMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner1AxisZMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            //textBoxSingleChartScanner2AxisZMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerChartAxisYMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerChartAxisYMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerChartAxisZMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxCombinerChartAxisZMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisGraphChartAxisYMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisGraphChartAxisYMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisGraphChartAxisZMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisGraphChartAxisZMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisComparedPoints.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisPitchPoints.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisDeviationValue.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisDeviationYThreshold.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisYThresholdObject.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisZThresholdObject.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisDeviationYThreshold.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisDeviationZThreshold.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisAngleLow.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisAngleHigh.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxAnalysisTimer.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxGeneralOffsetTong.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxGeneralScannerOffset.TextChanged += new EventHandler(OnValueChangeTextBox);
            checkBoxGeneralMatchingCoordinatesystem.CheckedChanged += new EventHandler(checkBoxAnalysisMatchingCoordinatesystem_CheckedChanged);
            checkBoxGeneralAutomaticScanneroffset.CheckedChanged += new EventHandler(checkBoxGeneralAutomaticScanneroffset_CheckedChanged);
            textBoxRecorderChartAxisYMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxRecorderChartAxisYMax.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxRecorderChartAxisZMin.TextChanged += new EventHandler(OnValueChangeTextBox);
            textBoxRecorderChartAxisZMax.TextChanged += new EventHandler(OnValueChangeTextBox);
        }

        //private void InitSingleChartValues()
        //{
        //    textBoxSingleChartScanner1AxisYMin.Text = Mainframe.SingleChartAxisYMin[int.Parse(textBoxSingleChartScanner1AxisYMin.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner2AxisYMin.Text = Mainframe.SingleChartAxisYMin[int.Parse(textBoxSingleChartScanner2AxisYMin.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner1AxisYMax.Text = Mainframe.SingleChartAxisYMax[int.Parse(textBoxSingleChartScanner1AxisYMax.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner2AxisYMax.Text = Mainframe.SingleChartAxisYMax[int.Parse(textBoxSingleChartScanner2AxisYMax.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner1AxisZMin.Text = Mainframe.SingleChartAxisZMin[int.Parse(textBoxSingleChartScanner1AxisZMin.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner2AxisZMin.Text = Mainframe.SingleChartAxisZMin[int.Parse(textBoxSingleChartScanner2AxisZMin.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner1AxisZMax.Text = Mainframe.SingleChartAxisZMax[int.Parse(textBoxSingleChartScanner1AxisZMax.Tag.ToString())].ToString();
        //    textBoxSingleChartScanner2AxisZMax.Text = Mainframe.SingleChartAxisZMax[int.Parse(textBoxSingleChartScanner2AxisZMax.Tag.ToString())].ToString();

        //    if (Mainframe.SingleChartTypes[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
        //    {
        //        radioButtonSingleChartScanner1Spline.Checked = true;
        //    }
        //    else if (Mainframe.SingleChartTypes[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
        //    {
        //        radioButtonSingleChartScanner1Point.Checked = true;
        //    }

        //    if (Mainframe.SingleChartTypes[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
        //    {
        //        radioButtonSingleChartScanner2Spline.Checked = true;
        //    }
        //    else if (Mainframe.SingleChartTypes[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
        //    {
        //        radioButtonSingleChartScanner2Point.Checked = true;
        //    }
        //}

        private void InitCombinerChartValues()
        {
            textBoxCombinerChartAxisYMin.Text = Mainframe.CombinerChartAxisYMin.ToString();
            textBoxCombinerChartAxisYMax.Text = Mainframe.CombinerChartAxisYMax.ToString();
            textBoxCombinerChartAxisZMin.Text = Mainframe.CombinerChartAxisZMin.ToString();
            textBoxCombinerChartAxisZMax.Text = Mainframe.CombinerChartAxisZMax.ToString();

            if (Mainframe.CombinerChartType[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
            {
                radioButtonCombinerScanner1Spline.Checked = true;
            }
            else if (Mainframe.CombinerChartType[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
            {
                radioButtonCombinerScanner1Point.Checked = true;
            }

            if (Mainframe.CombinerChartType[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
            {
                radioButtonCombinerScanner2Spline.Checked = true;
            }
            else if (Mainframe.CombinerChartType[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
            {
                radioButtonCombinerScanner2Point.Checked = true;
            }
        }

        private void InitAnalysisGraphChartValues()
        {
            textBoxAnalysisGraphChartAxisYMin.Text = Mainframe.AnalysisGraphChartAxisYMin.ToString();
            textBoxAnalysisGraphChartAxisYMax.Text = Mainframe.AnalysisGraphChartAxisYMax.ToString();
            textBoxAnalysisGraphChartAxisZMin.Text = Mainframe.AnalysisGraphChartAxisZMin.ToString();
            textBoxAnalysisGraphChartAxisZMax.Text = Mainframe.AnalysisGraphChartAxisZMax.ToString();

            if (Mainframe.AnalysisGraphChartType[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
            {
                radioButtonAnalysisGraphSpline.Checked = true;
            }
            else if (Mainframe.AnalysisGraphChartType[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
            {
                radioButtonAnalysisGraphPoint.Checked = true;
            }

            if (Mainframe.AnalysisGraphChartType[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
            {
                radioButtonAnalysisGraphPOISpline.Checked = true;
            }
            else if (Mainframe.AnalysisGraphChartType[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
            {
                radioButtonAnalysisGraphPOIPoint.Checked = true;
            }
        }

        private void InitRecorderGraphChartValues()
        {
            textBoxRecorderChartAxisYMin.Text = Mainframe.RecorderGraphChartAxisYMin.ToString();
            textBoxRecorderChartAxisYMax.Text = Mainframe.RecorderGraphChartAxisYMax.ToString();
            textBoxRecorderChartAxisZMin.Text = Mainframe.RecorderGraphChartAxisZMin.ToString();
            textBoxRecorderChartAxisZMax.Text = Mainframe.RecorderGraphChartAxisZMax.ToString();

            if (Mainframe.RecorderGraphChartType[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
            {
                radioButtonRecorderScanner1Spline.Checked = true;
            }
            else if (Mainframe.RecorderGraphChartType[0] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
            {
                radioButtonRecorderScanner1Point.Checked = true;
            }

            if (Mainframe.RecorderGraphChartType[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline)
            {
                radioButtonRecorderScanner2Spline.Checked = true;
            }
            else if (Mainframe.RecorderGraphChartType[1] == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point)
            {
                radioButtonRecorderScanner2Point.Checked = true;
            }
        }

        private void InitAnalysisValues()
        {
            textBoxAnalysisComparedPoints.Text = Mainframe.AnalysizedPoints.ToString();
            textBoxAnalysisPitchPoints.Text = Mainframe.AnalysizedPointsPitch.ToString();
            textBoxAnalysisDeviationValue.Text = Mainframe.AnalysizedDeviation.ToString();
            textBoxAnalysisDeviationYThreshold.Text = Mainframe.AnalysizedYDeviationEdge.ToString();
            textBoxAnalysisDeviationZThreshold.Text = Mainframe.AnalysizedZDeviationEdge.ToString();
            textBoxAnalysisYThresholdObject.Text = Mainframe.AnalysizedYThresholdObject.ToString();
            textBoxAnalysisZThresholdObject.Text = Mainframe.AnalysizedZThresholdObject.ToString();
            textBoxAnalysisAngleLow.Text = Mainframe.AnalysizedAngleLow.ToString();
            textBoxAnalysisAngleHigh.Text = Mainframe.AnalysizedAngleHigh.ToString();
            textBoxAnalysisTimer.Text = Mainframe.AnalyserTimer.ToString();
        }

        private void InitGeneralValues()
        {
            checkBoxGeneralMatchingCoordinatesystem.Checked = Mainframe.IsMatchingCoordinateSystem;
            textBoxGeneralOffsetTong.Text = Mainframe.TongOffset.ToString();
            checkBoxGeneralAutomaticScanneroffset.Checked = Mainframe.IsNormalizingYAutomatic;
            textBoxGeneralScannerOffset.Text = Mainframe.ScannerOffset.ToString();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            SetConfig();
        }

        private void SetConfig()
        {
            SetConfigChartTypes();
            SetConfigChartAxis();
            SetConfigAnalysis();
            SetConfigGeneral();
        }

        private void SetConfigChartTypes()
        {
            SetConfigSingleChartTypes();
            SetConfigCombinerChartTypes();
            SetConfigAnalysisGraphChartTypes();
            SetConfigRecorderGraphChartTypes();
        }

        private void SetConfigSingleChartTypes()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Scanner.Chart.Type"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Scanner.Chart.Type", $"{Mainframe.SingleChartTypes[0]};{Mainframe.SingleChartTypes[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Scanner.Chart.Type"].Value = $"{Mainframe.SingleChartTypes[0]};{Mainframe.SingleChartTypes[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigCombinerChartTypes()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Combiner.Chart.Type"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Combiner.Chart.Type", $"{Mainframe.CombinerChartType[0]};{Mainframe.CombinerChartType[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Combiner.Chart.Type"].Value = $"{Mainframe.CombinerChartType[0]};{Mainframe.CombinerChartType[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisGraphChartTypes()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analyser.Chart.Type"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analyser.Chart.Type", $"{Mainframe.AnalysisGraphChartType[0]};{Mainframe.AnalysisGraphChartType[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Analyser.Chart.Type"].Value = $"{Mainframe.AnalysisGraphChartType[0]};{Mainframe.AnalysisGraphChartType[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigRecorderGraphChartTypes()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Recorder.Chart.Type"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Recorder.Chart.Type", $"{Mainframe.RecorderGraphChartType[0]};{Mainframe.RecorderGraphChartType[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Recorder.Chart.Type"].Value = $"{Mainframe.RecorderGraphChartType[0]};{Mainframe.RecorderGraphChartType[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigChartAxis()
        {
            SetConfigSingleChartAxis();
            SetConfigCombinerChartAxis();
            SetConfigAnalysisGraphChartAxis();
            SetConfigRecorderGraphChartAxis();
        }

        private void SetConfigSingleChartAxis()
        {
            SetConfigSingleChartAxisXMin();
            SetConfigSingleChartAxisXMax();
            SetConfigSingleChartAxisYMin();
            SetConfigSingleChartAxisYMax();
        }

        private void SetConfigSingleChartAxisXMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Scanner.Chart.Axis.Y.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Scanner.Chart.Axis.Y.Min", $"{Mainframe.SingleChartAxisYMin[0]};{Mainframe.SingleChartAxisYMin[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Scanner.Chart.Axis.Y.Min"].Value = $"{Mainframe.SingleChartAxisYMin[0]};{Mainframe.SingleChartAxisYMin[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigSingleChartAxisXMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Scanner.Chart.Axis.Y.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Scanner.Chart.Axis.Y.Max", $"{Mainframe.SingleChartAxisYMax[0]};{Mainframe.SingleChartAxisYMax[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Scanner.Chart.Axis.Y.Max"].Value = $"{Mainframe.SingleChartAxisYMax[0]};{Mainframe.SingleChartAxisYMax[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigSingleChartAxisYMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Scanner.Chart.Axis.Z.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Scanner.Chart.Axis.Z.Min", $"{Mainframe.SingleChartAxisZMin[0]};{Mainframe.SingleChartAxisZMin[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Scanner.Chart.Axis.Z.Min"].Value = $"{Mainframe.SingleChartAxisZMin[0]};{Mainframe.SingleChartAxisZMin[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigSingleChartAxisYMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Scanner.Chart.Axis.Z.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Scanner.Chart.Axis.Z.Max", $"{Mainframe.SingleChartAxisZMax[0]};{Mainframe.SingleChartAxisZMax[1]}");
                }
                else
                {
                    config.AppSettings.Settings["Scanner.Chart.Axis.Z.Max"].Value = $"{Mainframe.SingleChartAxisZMax[0]};{Mainframe.SingleChartAxisZMax[1]}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigCombinerChartAxis()
        {
            SetConfigCombinerChartAxisXMin();
            SetConfigCombinerChartAxisXMax();
            SetConfigCombinerChartAxisYMin();
            SetConfigCombinerChartAxisYMax();
        }

        private void SetConfigCombinerChartAxisXMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Combiner.Chart.Axis.Y.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Combiner.Chart.Axis.Y.Min", $"{Mainframe.CombinerChartAxisYMin}");
                }
                else
                {
                    config.AppSettings.Settings["Combiner.Chart.Axis.Y.Min"].Value = $"{Mainframe.CombinerChartAxisYMin}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigCombinerChartAxisXMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Combiner.Chart.Axis.Y.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Combiner.Chart.Axis.Y.Max", $"{Mainframe.CombinerChartAxisYMax}");
                }
                else
                {
                    config.AppSettings.Settings["Combiner.Chart.Axis.Y.Max"].Value = $"{Mainframe.CombinerChartAxisYMax}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigCombinerChartAxisYMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Combiner.Chart.Axis.Z.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Combiner.Chart.Axis.Z.Min", $"{Mainframe.CombinerChartAxisZMin}");
                }
                else
                {
                    config.AppSettings.Settings["Combiner.Chart.Axis.Z.Min"].Value = $"{Mainframe.CombinerChartAxisZMin}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigCombinerChartAxisYMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Combiner.Chart.Axis.Z.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Combiner.Chart.Axis.Z.Max", $"{Mainframe.CombinerChartAxisZMax}");
                }
                else
                {
                    config.AppSettings.Settings["Combiner.Chart.Axis.Z.Max"].Value = $"{Mainframe.CombinerChartAxisZMax}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisGraphChartAxis()
        {
            SetConfigAnalysisGraphChartAxisXMin();
            SetConfigAnalysisGraphChartAxisXMax();
            SetConfigAnalysisGraphChartAxisYMin();
            SetConfigAnalysisGraphChartAxisYMax();
        }

        private void SetConfigAnalysisGraphChartAxisXMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analyser.Chart.Axis.Y.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analyser.Chart.Axis.Y.Min", $"{Mainframe.AnalysisGraphChartAxisYMin}");
                }
                else
                {
                    config.AppSettings.Settings["Analyser.Chart.Axis.Y.Min"].Value = $"{Mainframe.AnalysisGraphChartAxisYMin}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisGraphChartAxisXMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analyser.Chart.Axis.Y.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analyser.Chart.Axis.Y.Max", $"{Mainframe.AnalysisGraphChartAxisYMax}");
                }
                else
                {
                    config.AppSettings.Settings["Analyser.Chart.Axis.Y.Max"].Value = $"{Mainframe.AnalysisGraphChartAxisYMax}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisGraphChartAxisYMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analyser.Chart.Axis.Z.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analyser.Chart.Axis.Z.Min", $"{Mainframe.AnalysisGraphChartAxisZMin}");
                }
                else
                {
                    config.AppSettings.Settings["Analyser.Chart.Axis.Z.Min"].Value = $"{Mainframe.AnalysisGraphChartAxisZMin}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisGraphChartAxisYMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analyser.Chart.Axis.Z.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analyser.Chart.Axis.Z.Max", $"{Mainframe.AnalysisGraphChartAxisZMax}");
                }
                else
                {
                    config.AppSettings.Settings["Analyser.Chart.Axis.Z.Max"].Value = $"{Mainframe.AnalysisGraphChartAxisZMax}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigRecorderGraphChartAxis()
        {
            SetConfigRecorderGraphChartAxisXMin();
            SetConfigRecorderGraphChartAxisXMax();
            SetConfigRecorderGraphChartAxisYMin();
            SetConfigRecorderGraphChartAxisYMax();
        }

        private void SetConfigRecorderGraphChartAxisXMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Recorder.Chart.Axis.Y.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Recorder.Chart.Axis.Y.Min", $"{Mainframe.RecorderGraphChartAxisYMin}");
                }
                else
                {
                    config.AppSettings.Settings["Recorder.Chart.Axis.Y.Min"].Value = $"{Mainframe.RecorderGraphChartAxisYMin}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigRecorderGraphChartAxisXMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Recorder.Chart.Axis.Y.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Recorder.Chart.Axis.Y.Max", $"{Mainframe.AnalysisGraphChartAxisYMax}");
                }
                else
                {
                    config.AppSettings.Settings["Recorder.Chart.Axis.Y.Max"].Value = $"{Mainframe.AnalysisGraphChartAxisYMax}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigRecorderGraphChartAxisYMin()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Recorder.Chart.Axis.Z.Min"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Recorder.Chart.Axis.Z.Min", $"{Mainframe.AnalysisGraphChartAxisZMin}");
                }
                else
                {
                    config.AppSettings.Settings["Recorder.Chart.Axis.Z.Min"].Value = $"{Mainframe.AnalysisGraphChartAxisZMin}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigRecorderGraphChartAxisYMax()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Recorder.Chart.Axis.Z.Max"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Recorder.Chart.Axis.Z.Max", $"{Mainframe.AnalysisGraphChartAxisZMax}");
                }
                else
                {
                    config.AppSettings.Settings["Recorder.Chart.Axis.Z.Max"].Value = $"{Mainframe.AnalysisGraphChartAxisZMax}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysis()
        {
            SetConfigAnalysisComparedData();
            SetConfigAnalysisPitchPoints();
            SetConfigAnalysisDeviation();
            SetConfigAnalysisAngleLow();
            SetConfigAnalysisAngleHigh();
            SetConfigAnalysisTimer();
            SetConfigAnalysisYDeviation();
            SetConfigAnalysisZDeviation();
            SetConfigAnalysisYThreshold();
            SetConfigAnalysisZThreshold();
        }

        private void SetConfigAnalysisComparedData()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Points"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Points", $"{Mainframe.AnalysizedPoints}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Points"].Value = $"{Mainframe.AnalysizedPoints}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisPitchPoints()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Points.Pitch"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Points.Pitch", $"{Mainframe.AnalysizedPointsPitch}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Points.Pitch"].Value = $"{Mainframe.AnalysizedPointsPitch}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisDeviation()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Deviation"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Deviation", $"{Mainframe.AnalysizedDeviation}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Deviation"].Value = $"{Mainframe.AnalysizedDeviation}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisAngleLow()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Angle.Low"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Angle.Low", $"{Mainframe.AnalysizedAngleLow}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Angle.Low"].Value = $"{Mainframe.AnalysizedAngleLow}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisAngleHigh()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Angle.High"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Angle.High", $"{Mainframe.AnalysizedAngleHigh}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Angle.High"].Value = $"{Mainframe.AnalysizedAngleHigh}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisTimer()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Timer"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Timer", $"{Mainframe.AnalyserTimer}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Timer"].Value = $"{Mainframe.AnalyserTimer}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisYDeviation()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Y.Deviation.Edge"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Y.Deviation.Edge", $"{Mainframe.AnalysizedYDeviationEdge}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Y.Deviation.Edge"].Value = $"{Mainframe.AnalysizedYDeviationEdge}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisZDeviation()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Z.Deviation.Edge"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Z.Deviation.Edge", $"{Mainframe.AnalysizedZDeviationEdge}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Z.Deviation.Edge"].Value = $"{Mainframe.AnalysizedZDeviationEdge}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisYThreshold()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Y.Threshold.Object"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Y.Threshold.Object", $"{Mainframe.AnalysizedYThresholdObject}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Y.Threshold.Object"].Value = $"{Mainframe.AnalysizedYThresholdObject}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigAnalysisZThreshold()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["Analysing.Value.Z.Threshold.Object"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("Analysing.Value.Z.Threshold.Object", $"{Mainframe.AnalysizedZThresholdObject}");
                }
                else
                {
                    config.AppSettings.Settings["Analysing.Value.Z.Threshold.Object"].Value = $"{Mainframe.AnalysizedZThresholdObject}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigGeneral()
        {
            SetConfigGeneralYAutomatic();
            SetConfigGeneralYOffset();
            SetConfigGeneralTongOffset();
            SetConfigGeneralMatchedCoord();
        }

        private void SetConfigGeneralYAutomatic()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["General.Scanner.Offset"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("General.Scanner.Offset", $"{Mainframe.IsNormalizingYAutomatic}");
                }
                else
                {
                    config.AppSettings.Settings["General.Scanner.Offset"].Value = $"{Mainframe.IsNormalizingYAutomatic}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigGeneralYOffset()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["General.Scanner.Offset.Value"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("General.Scanner.Offset.Value", $"{Mainframe.ScannerOffset}");
                }
                else
                {
                    config.AppSettings.Settings["General.Scanner.Offset.Value"].Value = $"{Mainframe.ScannerOffset}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigGeneralTongOffset()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["General.Tong.Offset"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("General.Tong.Offset", $"{Mainframe.TongOffset}");
                }
                else
                {
                    config.AppSettings.Settings["General.Tong.Offset"].Value = $"{Mainframe.TongOffset}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void SetConfigGeneralMatchedCoord()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var entry = config.AppSettings.Settings["General.MatchingCoordinateSystem"];

                if (entry == null)
                {
                    config.AppSettings.Settings.Add("General.MatchingCoordinateSystem", $"{Mainframe.IsMatchingCoordinateSystem}");
                }
                else
                {
                    config.AppSettings.Settings["General.MatchingCoordinateSystem"].Value = $"{Mainframe.IsMatchingCoordinateSystem}";
                }

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        //private void textBoxSingleChartScanner1AxisXMin_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisYMin[int.Parse(textBoxSingleChartScanner1AxisYMin.Tag.ToString())] = int.Parse(textBoxSingleChartScanner1AxisYMin.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner1AxisXMax_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisYMax[int.Parse(textBoxSingleChartScanner1AxisYMax.Tag.ToString())] = int.Parse(textBoxSingleChartScanner1AxisYMax.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner1AxisYMin_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisZMin[int.Parse(textBoxSingleChartScanner1AxisZMin.Tag.ToString())] = int.Parse(textBoxSingleChartScanner1AxisZMin.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner1AxisYMax_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisZMax[int.Parse(textBoxSingleChartScanner1AxisZMax.Tag.ToString())] = int.Parse(textBoxSingleChartScanner1AxisZMax.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner2AxisXMin_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisYMin[int.Parse(textBoxSingleChartScanner2AxisYMin.Tag.ToString())] = int.Parse(textBoxSingleChartScanner2AxisYMin.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner2AxisXMax_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisYMax[int.Parse(textBoxSingleChartScanner2AxisYMax.Tag.ToString())] = int.Parse(textBoxSingleChartScanner2AxisYMax.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner2AxisYMin_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisZMin[int.Parse(textBoxSingleChartScanner2AxisZMin.Tag.ToString())] = int.Parse(textBoxSingleChartScanner2AxisZMin.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        //private void textBoxSingleChartScanner2AxisYMax_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ValueNumeric(sender))
        //        {
        //            Mainframe.SingleChartAxisZMin[int.Parse(textBoxSingleChartScanner2AxisZMax.Tag.ToString())] = int.Parse(textBoxSingleChartScanner2AxisZMax.Text);
        //            OnValueSafeTextBox(sender, e);
        //        }
        //    }
        //}

        private void textBoxCombinerChartAxisXMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.CombinerChartAxisYMin = int.Parse(textBoxCombinerChartAxisYMin.Text);
                    Main.chartCombiner.ChartAreas[0].AxisX.Minimum = Mainframe.CombinerChartAxisYMin;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerChartAxisXMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.CombinerChartAxisYMax = int.Parse(textBoxCombinerChartAxisYMax.Text);
                    Main.chartCombiner.ChartAreas[0].AxisX.Maximum = Mainframe.CombinerChartAxisYMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerChartAxisYMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.CombinerChartAxisZMin = int.Parse(textBoxCombinerChartAxisZMin.Text);
                    Main.chartCombiner.ChartAreas[0].AxisY.Minimum = Mainframe.CombinerChartAxisZMin;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxCombinerChartAxisYMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.CombinerChartAxisZMax = int.Parse(textBoxCombinerChartAxisZMax.Text);
                    Main.chartCombiner.ChartAreas[0].AxisY.Maximum = Mainframe.CombinerChartAxisZMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisComparedPoints_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedPoints = int.Parse(textBoxAnalysisComparedPoints.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisPitchPoints_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedPointsPitch = int.Parse(textBoxAnalysisPitchPoints.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisDeviationValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedDeviation = int.Parse(textBoxAnalysisDeviationValue.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisDeviationYThreshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedYDeviationEdge = int.Parse(textBoxAnalysisDeviationYThreshold.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisDeviationZThreshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedZDeviationEdge = int.Parse(textBoxAnalysisDeviationZThreshold.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisYThresholdObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedYThresholdObject = int.Parse(textBoxAnalysisYThresholdObject.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisZThresholdObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedZThresholdObject = int.Parse(textBoxAnalysisZThresholdObject.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisAngleLow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedAngleLow = int.Parse(textBoxAnalysisAngleLow.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisAngleHigh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysizedAngleHigh = int.Parse(textBoxAnalysisAngleHigh.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisTimer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalyserTimer = int.Parse(textBoxAnalysisTimer.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxGeneralOffsetTong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.TongOffset = int.Parse(textBoxGeneralOffsetTong.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxGeneralScannerOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.ScannerOffset = int.Parse(textBoxGeneralScannerOffset.Text);
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisGraphChartAxisYMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysisGraphChartAxisYMin = int.Parse(textBoxAnalysisGraphChartAxisYMin.Text);
                    Main.chartAnalyser.ChartAreas[0].AxisX.Minimum = Mainframe.AnalysisGraphChartAxisYMin;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisGraphChartAxisYMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysisGraphChartAxisYMax = int.Parse(textBoxAnalysisGraphChartAxisYMax.Text);
                    Main.chartAnalyser.ChartAreas[0].AxisX.Maximum = Mainframe.AnalysisGraphChartAxisYMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisGraphChartAxisZMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysisGraphChartAxisZMin = int.Parse(textBoxAnalysisGraphChartAxisZMin.Text);
                    Main.chartAnalyser.ChartAreas[0].AxisY.Minimum = Mainframe.AnalysisGraphChartAxisZMin;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxAnalysisGraphChartAxisZMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.AnalysisGraphChartAxisZMax = int.Parse(textBoxAnalysisGraphChartAxisZMax.Text);
                    Main.chartAnalyser.ChartAreas[0].AxisY.Maximum = Mainframe.AnalysisGraphChartAxisYMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxRecorderChartAxisYMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.RecorderGraphChartAxisZMax = int.Parse(textBoxRecorderChartAxisYMin.Text);
                    Main.chartRecorder.ChartAreas[0].AxisY.Maximum = Mainframe.RecorderGraphChartAxisZMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxRecorderChartAxisYMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.RecorderGraphChartAxisZMax = int.Parse(textBoxRecorderChartAxisYMax.Text);
                    Main.chartRecorder.ChartAreas[0].AxisY.Maximum = Mainframe.RecorderGraphChartAxisZMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxRecorderChartAxisZMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.RecorderGraphChartAxisZMax = int.Parse(textBoxRecorderChartAxisZMin.Text);
                    Main.chartRecorder.ChartAreas[0].AxisY.Maximum = Mainframe.RecorderGraphChartAxisZMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        private void textBoxRecorderChartAxisZMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValueNumeric(sender))
                {
                    Mainframe.RecorderGraphChartAxisZMax = int.Parse(textBoxRecorderChartAxisZMax.Text);
                    Main.chartRecorder.ChartAreas[0].AxisY.Maximum = Mainframe.RecorderGraphChartAxisZMax;
                    OnValueSafeTextBox(sender, e);
                }
            }
        }

        //private void radioButtonSingleChartScanner1Spline_Click(object sender, EventArgs e)
        //{
        //    Mainframe.SingleChartTypes[int.Parse(radioButtonSingleChartScanner1Spline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        //}

        //private void radioButtonSingleChartScanner1Point_Click(object sender, EventArgs e)
        //{
        //    Mainframe.SingleChartTypes[int.Parse(radioButtonSingleChartScanner1Point.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        //}

        //private void radioButtonSingleChartScanner2Spline_Click(object sender, EventArgs e)
        //{
        //    Mainframe.SingleChartTypes[int.Parse(radioButtonSingleChartScanner2Spline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        //}

        //private void radioButtonSingleChartScanner2Point_Click(object sender, EventArgs e)
        //{
        //    Mainframe.SingleChartTypes[int.Parse(radioButtonSingleChartScanner2Point.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        //}

        private void radioButtonCombinerScanner1Spline_Click(object sender, EventArgs e)
        {
            Mainframe.CombinerChartType[int.Parse(radioButtonCombinerScanner1Spline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartCombiner.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartAnalyser.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void radioButtonCombinerScanner1Point_Click(object sender, EventArgs e)
        {
            Mainframe.CombinerChartType[int.Parse(radioButtonCombinerScanner1Point.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            Main.chartCombiner.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            Main.chartAnalyser.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void radioButtonCombinerScanner2Spline_Click(object sender, EventArgs e)
        {
            Mainframe.CombinerChartType[int.Parse(radioButtonCombinerScanner2Spline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartCombiner.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartAnalyser.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void radioButtonCombinerScanner2Point_Click(object sender, EventArgs e)
        {
            Mainframe.CombinerChartType[int.Parse(radioButtonCombinerScanner2Point.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            Main.chartCombiner.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            Main.chartAnalyser.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void radioButtonAnalysisGraphSpline_Click(object sender, EventArgs e)
        {
            Mainframe.AnalysisGraphChartType[int.Parse(radioButtonAnalysisGraphSpline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartAnalyser.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void radioButtonAnalysisGraphPoint_Click(object sender, EventArgs e)
        {
            Mainframe.AnalysisGraphChartType[int.Parse(radioButtonAnalysisGraphPoint.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            Main.chartAnalyser.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void radioButtonAnalysisGraphPOISpline_Click(object sender, EventArgs e)
        {
            Mainframe.AnalysisGraphChartType[int.Parse(radioButtonAnalysisGraphPOISpline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartAnalyser.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void radioButtonAnalysisGraphPOIPoint_Click(object sender, EventArgs e)
        {
            Mainframe.AnalysisGraphChartType[int.Parse(radioButtonAnalysisGraphPOIPoint.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            Main.chartAnalyser.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void radioButtonRecorderScanner1Spline_Click(object sender, EventArgs e)
        {
            Mainframe.RecorderGraphChartType[int.Parse(radioButtonRecorderScanner1Spline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartRecorder.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void radioButtonRecorderScanner1Point_Click(object sender, EventArgs e)
        {
            Mainframe.RecorderGraphChartType[int.Parse(radioButtonRecorderScanner1Point.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartRecorder.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void radioButtonRecorderScanner2Spline_Click(object sender, EventArgs e)
        {
            Mainframe.RecorderGraphChartType[int.Parse(radioButtonRecorderScanner2Spline.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartRecorder.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void radioButtonRecorderScanner2Point_Click(object sender, EventArgs e)
        {
            Mainframe.RecorderGraphChartType[int.Parse(radioButtonRecorderScanner2Point.Tag.ToString())] = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Main.chartRecorder.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void checkBoxAnalysisMatchingCoordinatesystem_CheckedChanged(object sender, EventArgs e)
        {
            Mainframe.IsMatchingCoordinateSystem = checkBoxGeneralMatchingCoordinatesystem.Checked;
        }

        private void checkBoxGeneralAutomaticScanneroffset_CheckedChanged(object sender, EventArgs e)
        {
            Mainframe.IsNormalizingYAutomatic = checkBoxGeneralAutomaticScanneroffset.Checked;
            ValueChanged(this, checkBoxGeneralAutomaticScanneroffset.Checked);
        }

        private void OnValueChangeTextBox(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.BackColor = Color.Orange;
        }

        private void OnValueSafeTextBox(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.BackColor = Color.Empty;
        }

        private bool ValueNumeric(object obj)
        {
            TextBox box = (TextBox)obj;

            bool ok = int.TryParse(box.Text, out int value);

            return ok;
        }

        private void Setupframe_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogFiler.Close();
        }
    }
}
