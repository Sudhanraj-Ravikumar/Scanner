namespace ScannerDisplay
{
    partial class Analysisframe
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartAnalyser = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxAnalysisFunctions = new System.Windows.Forms.GroupBox();
            this.labelDeviationCenterValue = new System.Windows.Forms.Label();
            this.labelDeviationCenter = new System.Windows.Forms.Label();
            this.labelWidthTopObjectValue = new System.Windows.Forms.Label();
            this.labelWidthTopObject = new System.Windows.Forms.Label();
            this.labelDistancefromRightValue = new System.Windows.Forms.Label();
            this.labelDistanceFromLeftValue = new System.Windows.Forms.Label();
            this.labelDistanceFromRight = new System.Windows.Forms.Label();
            this.labelDistanceFromLeft = new System.Windows.Forms.Label();
            this.buttonPOI = new System.Windows.Forms.Button();
            this.panelAnalysisFunction = new System.Windows.Forms.Panel();
            this.panelSetupSingleScanners = new System.Windows.Forms.Panel();
            this.groupBoxSetupSingleChart = new System.Windows.Forms.GroupBox();
            this.groupBoxSetupSingleScanner2 = new System.Windows.Forms.GroupBox();
            this.radioButtonSingleChartScanner2Point = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleChartScanner2Spline = new System.Windows.Forms.RadioButton();
            this.textBoxSingleChartScanner2AxisZMax = new System.Windows.Forms.TextBox();
            this.textBoxSingleChartScanner2AxisZMin = new System.Windows.Forms.TextBox();
            this.labelSingleChartScanner2AxisZMax = new System.Windows.Forms.Label();
            this.labelSingleChartScanner2AxisZMin = new System.Windows.Forms.Label();
            this.textBoxSingleChartScanner2AxisYMax = new System.Windows.Forms.TextBox();
            this.textBoxSingleChartScanner2AxisYMin = new System.Windows.Forms.TextBox();
            this.labelSingleChartScanner2AxisYMax = new System.Windows.Forms.Label();
            this.labelSingleChartScanner2AxisYMin = new System.Windows.Forms.Label();
            this.groupBoxSetupSingleScanner1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSingleChartScanner1Point = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleChartScanner1Spline = new System.Windows.Forms.RadioButton();
            this.textBoxSingleChartScanner1AxisZMax = new System.Windows.Forms.TextBox();
            this.textBoxSingleChartScanner1AxisZMin = new System.Windows.Forms.TextBox();
            this.labelSingleChartScanner1AxisZMax = new System.Windows.Forms.Label();
            this.labelSingleChartScanner1AxisZMin = new System.Windows.Forms.Label();
            this.textBoxSingleChartScanner1AxisYMax = new System.Windows.Forms.TextBox();
            this.textBoxSingleChartScanner1AxisYMin = new System.Windows.Forms.TextBox();
            this.labelSingleChartScanner1AxisYMax = new System.Windows.Forms.Label();
            this.labelSingleChartScanner1AxisYMin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalyser)).BeginInit();
            this.groupBoxAnalysisFunctions.SuspendLayout();
            this.panelAnalysisFunction.SuspendLayout();
            this.panelSetupSingleScanners.SuspendLayout();
            this.groupBoxSetupSingleChart.SuspendLayout();
            this.groupBoxSetupSingleScanner2.SuspendLayout();
            this.groupBoxSetupSingleScanner1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartAnalyser
            // 
            chartArea1.AxisX.Interval = 100D;
            chartArea1.AxisX.Maximum = 1000D;
            chartArea1.AxisX.Minimum = -1000D;
            chartArea1.AxisX.Title = "Y";
            chartArea1.AxisY.Interval = 100D;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = -1500D;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisY.Title = "Z";
            chartArea1.Name = "ChartArea1";
            this.chartAnalyser.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartAnalyser.Legends.Add(legend1);
            this.chartAnalyser.Location = new System.Drawing.Point(12, 12);
            this.chartAnalyser.Name = "chartAnalyser";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Analysis Graph";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Points of Interest";
            this.chartAnalyser.Series.Add(series1);
            this.chartAnalyser.Series.Add(series2);
            this.chartAnalyser.Size = new System.Drawing.Size(804, 497);
            this.chartAnalyser.SuppressExceptions = true;
            this.chartAnalyser.TabIndex = 1;
            this.chartAnalyser.Text = "chart1";
            // 
            // groupBoxAnalysisFunctions
            // 
            this.groupBoxAnalysisFunctions.AutoSize = true;
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelDeviationCenterValue);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelDeviationCenter);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelWidthTopObjectValue);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelWidthTopObject);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelDistancefromRightValue);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelDistanceFromLeftValue);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelDistanceFromRight);
            this.groupBoxAnalysisFunctions.Controls.Add(this.labelDistanceFromLeft);
            this.groupBoxAnalysisFunctions.Controls.Add(this.buttonPOI);
            this.groupBoxAnalysisFunctions.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAnalysisFunctions.Name = "groupBoxAnalysisFunctions";
            this.groupBoxAnalysisFunctions.Size = new System.Drawing.Size(240, 173);
            this.groupBoxAnalysisFunctions.TabIndex = 2;
            this.groupBoxAnalysisFunctions.TabStop = false;
            this.groupBoxAnalysisFunctions.Text = "Analysis Functions";
            // 
            // labelDeviationCenterValue
            // 
            this.labelDeviationCenterValue.AutoSize = true;
            this.labelDeviationCenterValue.Location = new System.Drawing.Point(173, 97);
            this.labelDeviationCenterValue.Name = "labelDeviationCenterValue";
            this.labelDeviationCenterValue.Size = new System.Drawing.Size(0, 13);
            this.labelDeviationCenterValue.TabIndex = 12;
            // 
            // labelDeviationCenter
            // 
            this.labelDeviationCenter.AutoSize = true;
            this.labelDeviationCenter.Location = new System.Drawing.Point(6, 97);
            this.labelDeviationCenter.Name = "labelDeviationCenter";
            this.labelDeviationCenter.Size = new System.Drawing.Size(155, 13);
            this.labelDeviationCenter.TabIndex = 11;
            this.labelDeviationCenter.Text = Properties.Resources.DeviationTop;
            // 
            // labelWidthTopObjectValue
            // 
            this.labelWidthTopObjectValue.AutoSize = true;
            this.labelWidthTopObjectValue.Location = new System.Drawing.Point(173, 75);
            this.labelWidthTopObjectValue.Name = "labelWidthTopObjectValue";
            this.labelWidthTopObjectValue.Size = new System.Drawing.Size(0, 13);
            this.labelWidthTopObjectValue.TabIndex = 10;
            // 
            // labelWidthTopObject
            // 
            this.labelWidthTopObject.AutoSize = true;
            this.labelWidthTopObject.Location = new System.Drawing.Point(6, 75);
            this.labelWidthTopObject.Name = "labelWidthTopObject";
            this.labelWidthTopObject.Size = new System.Drawing.Size(109, 13);
            this.labelWidthTopObject.TabIndex = 9;
            this.labelWidthTopObject.Text = Properties.Resources.WidthObject;
            // 
            // labelDistancefromRightValue
            // 
            this.labelDistancefromRightValue.AutoSize = true;
            this.labelDistancefromRightValue.Location = new System.Drawing.Point(173, 141);
            this.labelDistancefromRightValue.Name = "labelDistancefromRightValue";
            this.labelDistancefromRightValue.Size = new System.Drawing.Size(0, 13);
            this.labelDistancefromRightValue.TabIndex = 7;
            // 
            // labelDistanceFromLeftValue
            // 
            this.labelDistanceFromLeftValue.AutoSize = true;
            this.labelDistanceFromLeftValue.Location = new System.Drawing.Point(173, 119);
            this.labelDistanceFromLeftValue.Name = "labelDistanceFromLeftValue";
            this.labelDistanceFromLeftValue.Size = new System.Drawing.Size(0, 13);
            this.labelDistanceFromLeftValue.TabIndex = 6;
            // 
            // labelDistanceFromRight
            // 
            this.labelDistanceFromRight.AutoSize = true;
            this.labelDistanceFromRight.Location = new System.Drawing.Point(6, 141);
            this.labelDistanceFromRight.Name = "labelDistanceFromRight";
            this.labelDistanceFromRight.Size = new System.Drawing.Size(160, 13);
            this.labelDistanceFromRight.TabIndex = 4;
            this.labelDistanceFromRight.Text = Properties.Resources.GrippingDepthTopEast;
            // 
            // labelDistanceFromLeft
            // 
            this.labelDistanceFromLeft.AutoSize = true;
            this.labelDistanceFromLeft.Location = new System.Drawing.Point(6, 119);
            this.labelDistanceFromLeft.Name = "labelDistanceFromLeft";
            this.labelDistanceFromLeft.Size = new System.Drawing.Size(154, 13);
            this.labelDistanceFromLeft.TabIndex = 3;
            this.labelDistanceFromLeft.Text = Properties.Resources.GrippingDepthTopWest;
            // 
            // buttonPOI
            // 
            this.buttonPOI.Location = new System.Drawing.Point(6, 19);
            this.buttonPOI.Name = "buttonPOI";
            this.buttonPOI.Size = new System.Drawing.Size(113, 23);
            this.buttonPOI.TabIndex = 0;
            this.buttonPOI.Text = "Start Analysis";
            this.buttonPOI.UseVisualStyleBackColor = true;
            this.buttonPOI.Click += new System.EventHandler(this.buttonPOI_Click);
            // 
            // panelAnalysisFunction
            // 
            this.panelAnalysisFunction.AutoSize = true;
            this.panelAnalysisFunction.Controls.Add(this.groupBoxAnalysisFunctions);
            this.panelAnalysisFunction.Location = new System.Drawing.Point(822, 12);
            this.panelAnalysisFunction.Name = "panelAnalysisFunction";
            this.panelAnalysisFunction.Size = new System.Drawing.Size(246, 179);
            this.panelAnalysisFunction.TabIndex = 3;
            // 
            // panelSetupSingleScanners
            // 
            this.panelSetupSingleScanners.AutoSize = true;
            this.panelSetupSingleScanners.Controls.Add(this.groupBoxSetupSingleChart);
            this.panelSetupSingleScanners.Location = new System.Drawing.Point(604, 218);
            this.panelSetupSingleScanners.Name = "panelSetupSingleScanners";
            this.panelSetupSingleScanners.Size = new System.Drawing.Size(464, 203);
            this.panelSetupSingleScanners.TabIndex = 4;
            this.panelSetupSingleScanners.Visible = false;
            // 
            // groupBoxSetupSingleChart
            // 
            this.groupBoxSetupSingleChart.AutoSize = true;
            this.groupBoxSetupSingleChart.Controls.Add(this.groupBoxSetupSingleScanner2);
            this.groupBoxSetupSingleChart.Controls.Add(this.groupBoxSetupSingleScanner1);
            this.groupBoxSetupSingleChart.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSetupSingleChart.Name = "groupBoxSetupSingleChart";
            this.groupBoxSetupSingleChart.Size = new System.Drawing.Size(458, 197);
            this.groupBoxSetupSingleChart.TabIndex = 1;
            this.groupBoxSetupSingleChart.TabStop = false;
            this.groupBoxSetupSingleChart.Text = "Single Scanners";
            // 
            // groupBoxSetupSingleScanner2
            // 
            this.groupBoxSetupSingleScanner2.AutoSize = true;
            this.groupBoxSetupSingleScanner2.Controls.Add(this.radioButtonSingleChartScanner2Point);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.radioButtonSingleChartScanner2Spline);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.textBoxSingleChartScanner2AxisZMax);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.textBoxSingleChartScanner2AxisZMin);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.labelSingleChartScanner2AxisZMax);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.labelSingleChartScanner2AxisZMin);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.textBoxSingleChartScanner2AxisYMax);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.textBoxSingleChartScanner2AxisYMin);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.labelSingleChartScanner2AxisYMax);
            this.groupBoxSetupSingleScanner2.Controls.Add(this.labelSingleChartScanner2AxisYMin);
            this.groupBoxSetupSingleScanner2.Location = new System.Drawing.Point(230, 19);
            this.groupBoxSetupSingleScanner2.Name = "groupBoxSetupSingleScanner2";
            this.groupBoxSetupSingleScanner2.Size = new System.Drawing.Size(222, 159);
            this.groupBoxSetupSingleScanner2.TabIndex = 10;
            this.groupBoxSetupSingleScanner2.TabStop = false;
            this.groupBoxSetupSingleScanner2.Text = "Scanner 2";
            // 
            // radioButtonSingleChartScanner2Point
            // 
            this.radioButtonSingleChartScanner2Point.AutoSize = true;
            this.radioButtonSingleChartScanner2Point.Location = new System.Drawing.Point(136, 121);
            this.radioButtonSingleChartScanner2Point.Name = "radioButtonSingleChartScanner2Point";
            this.radioButtonSingleChartScanner2Point.Size = new System.Drawing.Size(80, 17);
            this.radioButtonSingleChartScanner2Point.TabIndex = 9;
            this.radioButtonSingleChartScanner2Point.TabStop = true;
            this.radioButtonSingleChartScanner2Point.Tag = "1";
            this.radioButtonSingleChartScanner2Point.Text = "Chart: Point";
            this.radioButtonSingleChartScanner2Point.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleChartScanner2Spline
            // 
            this.radioButtonSingleChartScanner2Spline.AutoSize = true;
            this.radioButtonSingleChartScanner2Spline.Location = new System.Drawing.Point(10, 121);
            this.radioButtonSingleChartScanner2Spline.Name = "radioButtonSingleChartScanner2Spline";
            this.radioButtonSingleChartScanner2Spline.Size = new System.Drawing.Size(85, 17);
            this.radioButtonSingleChartScanner2Spline.TabIndex = 8;
            this.radioButtonSingleChartScanner2Spline.TabStop = true;
            this.radioButtonSingleChartScanner2Spline.Tag = "1";
            this.radioButtonSingleChartScanner2Spline.Text = "Chart: Spline";
            this.radioButtonSingleChartScanner2Spline.UseVisualStyleBackColor = true;
            // 
            // textBoxSingleChartScanner2AxisZMax
            // 
            this.textBoxSingleChartScanner2AxisZMax.Location = new System.Drawing.Point(116, 95);
            this.textBoxSingleChartScanner2AxisZMax.Name = "textBoxSingleChartScanner2AxisZMax";
            this.textBoxSingleChartScanner2AxisZMax.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner2AxisZMax.TabIndex = 7;
            this.textBoxSingleChartScanner2AxisZMax.Tag = "1";
            // 
            // textBoxSingleChartScanner2AxisZMin
            // 
            this.textBoxSingleChartScanner2AxisZMin.Location = new System.Drawing.Point(116, 69);
            this.textBoxSingleChartScanner2AxisZMin.Name = "textBoxSingleChartScanner2AxisZMin";
            this.textBoxSingleChartScanner2AxisZMin.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner2AxisZMin.TabIndex = 6;
            this.textBoxSingleChartScanner2AxisZMin.Tag = "1";
            // 
            // labelSingleChartScanner2AxisZMax
            // 
            this.labelSingleChartScanner2AxisZMax.AutoSize = true;
            this.labelSingleChartScanner2AxisZMax.Location = new System.Drawing.Point(7, 98);
            this.labelSingleChartScanner2AxisZMax.Name = "labelSingleChartScanner2AxisZMax";
            this.labelSingleChartScanner2AxisZMax.Size = new System.Drawing.Size(86, 13);
            this.labelSingleChartScanner2AxisZMax.TabIndex = 5;
            this.labelSingleChartScanner2AxisZMax.Tag = "1";
            this.labelSingleChartScanner2AxisZMax.Text = "Axis Z Maximum:";
            // 
            // labelSingleChartScanner2AxisZMin
            // 
            this.labelSingleChartScanner2AxisZMin.AutoSize = true;
            this.labelSingleChartScanner2AxisZMin.Location = new System.Drawing.Point(7, 72);
            this.labelSingleChartScanner2AxisZMin.Name = "labelSingleChartScanner2AxisZMin";
            this.labelSingleChartScanner2AxisZMin.Size = new System.Drawing.Size(83, 13);
            this.labelSingleChartScanner2AxisZMin.TabIndex = 4;
            this.labelSingleChartScanner2AxisZMin.Tag = "1";
            this.labelSingleChartScanner2AxisZMin.Text = "Axis Z Minimum:";
            // 
            // textBoxSingleChartScanner2AxisYMax
            // 
            this.textBoxSingleChartScanner2AxisYMax.Location = new System.Drawing.Point(116, 43);
            this.textBoxSingleChartScanner2AxisYMax.Name = "textBoxSingleChartScanner2AxisYMax";
            this.textBoxSingleChartScanner2AxisYMax.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner2AxisYMax.TabIndex = 3;
            this.textBoxSingleChartScanner2AxisYMax.Tag = "1";
            // 
            // textBoxSingleChartScanner2AxisYMin
            // 
            this.textBoxSingleChartScanner2AxisYMin.Location = new System.Drawing.Point(116, 17);
            this.textBoxSingleChartScanner2AxisYMin.Name = "textBoxSingleChartScanner2AxisYMin";
            this.textBoxSingleChartScanner2AxisYMin.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner2AxisYMin.TabIndex = 2;
            this.textBoxSingleChartScanner2AxisYMin.Tag = "1";
            // 
            // labelSingleChartScanner2AxisYMax
            // 
            this.labelSingleChartScanner2AxisYMax.AutoSize = true;
            this.labelSingleChartScanner2AxisYMax.Location = new System.Drawing.Point(7, 46);
            this.labelSingleChartScanner2AxisYMax.Name = "labelSingleChartScanner2AxisYMax";
            this.labelSingleChartScanner2AxisYMax.Size = new System.Drawing.Size(86, 13);
            this.labelSingleChartScanner2AxisYMax.TabIndex = 1;
            this.labelSingleChartScanner2AxisYMax.Tag = "1";
            this.labelSingleChartScanner2AxisYMax.Text = "Axis Y Maximum:";
            // 
            // labelSingleChartScanner2AxisYMin
            // 
            this.labelSingleChartScanner2AxisYMin.AutoSize = true;
            this.labelSingleChartScanner2AxisYMin.Location = new System.Drawing.Point(7, 20);
            this.labelSingleChartScanner2AxisYMin.Name = "labelSingleChartScanner2AxisYMin";
            this.labelSingleChartScanner2AxisYMin.Size = new System.Drawing.Size(83, 13);
            this.labelSingleChartScanner2AxisYMin.TabIndex = 0;
            this.labelSingleChartScanner2AxisYMin.Tag = "1";
            this.labelSingleChartScanner2AxisYMin.Text = "Axis Y Minimum:";
            // 
            // groupBoxSetupSingleScanner1
            // 
            this.groupBoxSetupSingleScanner1.AutoSize = true;
            this.groupBoxSetupSingleScanner1.Controls.Add(this.radioButtonSingleChartScanner1Point);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.radioButtonSingleChartScanner1Spline);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.textBoxSingleChartScanner1AxisZMax);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.textBoxSingleChartScanner1AxisZMin);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.labelSingleChartScanner1AxisZMax);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.labelSingleChartScanner1AxisZMin);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.textBoxSingleChartScanner1AxisYMax);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.textBoxSingleChartScanner1AxisYMin);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.labelSingleChartScanner1AxisYMax);
            this.groupBoxSetupSingleScanner1.Controls.Add(this.labelSingleChartScanner1AxisYMin);
            this.groupBoxSetupSingleScanner1.Location = new System.Drawing.Point(3, 19);
            this.groupBoxSetupSingleScanner1.Name = "groupBoxSetupSingleScanner1";
            this.groupBoxSetupSingleScanner1.Size = new System.Drawing.Size(221, 159);
            this.groupBoxSetupSingleScanner1.TabIndex = 0;
            this.groupBoxSetupSingleScanner1.TabStop = false;
            this.groupBoxSetupSingleScanner1.Text = "Scanner 1";
            // 
            // radioButtonSingleChartScanner1Point
            // 
            this.radioButtonSingleChartScanner1Point.AutoSize = true;
            this.radioButtonSingleChartScanner1Point.Location = new System.Drawing.Point(135, 121);
            this.radioButtonSingleChartScanner1Point.Name = "radioButtonSingleChartScanner1Point";
            this.radioButtonSingleChartScanner1Point.Size = new System.Drawing.Size(80, 17);
            this.radioButtonSingleChartScanner1Point.TabIndex = 9;
            this.radioButtonSingleChartScanner1Point.TabStop = true;
            this.radioButtonSingleChartScanner1Point.Tag = "0";
            this.radioButtonSingleChartScanner1Point.Text = "Chart: Point";
            this.radioButtonSingleChartScanner1Point.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleChartScanner1Spline
            // 
            this.radioButtonSingleChartScanner1Spline.AutoSize = true;
            this.radioButtonSingleChartScanner1Spline.Location = new System.Drawing.Point(10, 121);
            this.radioButtonSingleChartScanner1Spline.Name = "radioButtonSingleChartScanner1Spline";
            this.radioButtonSingleChartScanner1Spline.Size = new System.Drawing.Size(85, 17);
            this.radioButtonSingleChartScanner1Spline.TabIndex = 8;
            this.radioButtonSingleChartScanner1Spline.TabStop = true;
            this.radioButtonSingleChartScanner1Spline.Tag = "0";
            this.radioButtonSingleChartScanner1Spline.Text = "Chart: Spline";
            this.radioButtonSingleChartScanner1Spline.UseVisualStyleBackColor = true;
            // 
            // textBoxSingleChartScanner1AxisZMax
            // 
            this.textBoxSingleChartScanner1AxisZMax.Location = new System.Drawing.Point(115, 95);
            this.textBoxSingleChartScanner1AxisZMax.Name = "textBoxSingleChartScanner1AxisZMax";
            this.textBoxSingleChartScanner1AxisZMax.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner1AxisZMax.TabIndex = 7;
            this.textBoxSingleChartScanner1AxisZMax.Tag = "0";
            // 
            // textBoxSingleChartScanner1AxisZMin
            // 
            this.textBoxSingleChartScanner1AxisZMin.Location = new System.Drawing.Point(115, 69);
            this.textBoxSingleChartScanner1AxisZMin.Name = "textBoxSingleChartScanner1AxisZMin";
            this.textBoxSingleChartScanner1AxisZMin.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner1AxisZMin.TabIndex = 6;
            this.textBoxSingleChartScanner1AxisZMin.Tag = "0";
            // 
            // labelSingleChartScanner1AxisZMax
            // 
            this.labelSingleChartScanner1AxisZMax.AutoSize = true;
            this.labelSingleChartScanner1AxisZMax.Location = new System.Drawing.Point(7, 98);
            this.labelSingleChartScanner1AxisZMax.Name = "labelSingleChartScanner1AxisZMax";
            this.labelSingleChartScanner1AxisZMax.Size = new System.Drawing.Size(86, 13);
            this.labelSingleChartScanner1AxisZMax.TabIndex = 5;
            this.labelSingleChartScanner1AxisZMax.Tag = "0";
            this.labelSingleChartScanner1AxisZMax.Text = "Axis Z Maximum:";
            // 
            // labelSingleChartScanner1AxisZMin
            // 
            this.labelSingleChartScanner1AxisZMin.AutoSize = true;
            this.labelSingleChartScanner1AxisZMin.Location = new System.Drawing.Point(7, 72);
            this.labelSingleChartScanner1AxisZMin.Name = "labelSingleChartScanner1AxisZMin";
            this.labelSingleChartScanner1AxisZMin.Size = new System.Drawing.Size(83, 13);
            this.labelSingleChartScanner1AxisZMin.TabIndex = 4;
            this.labelSingleChartScanner1AxisZMin.Tag = "0";
            this.labelSingleChartScanner1AxisZMin.Text = "Axis Z Minimum:";
            // 
            // textBoxSingleChartScanner1AxisYMax
            // 
            this.textBoxSingleChartScanner1AxisYMax.Location = new System.Drawing.Point(115, 43);
            this.textBoxSingleChartScanner1AxisYMax.Name = "textBoxSingleChartScanner1AxisYMax";
            this.textBoxSingleChartScanner1AxisYMax.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner1AxisYMax.TabIndex = 3;
            this.textBoxSingleChartScanner1AxisYMax.Tag = "0";
            // 
            // textBoxSingleChartScanner1AxisYMin
            // 
            this.textBoxSingleChartScanner1AxisYMin.Location = new System.Drawing.Point(115, 17);
            this.textBoxSingleChartScanner1AxisYMin.Name = "textBoxSingleChartScanner1AxisYMin";
            this.textBoxSingleChartScanner1AxisYMin.Size = new System.Drawing.Size(100, 20);
            this.textBoxSingleChartScanner1AxisYMin.TabIndex = 2;
            this.textBoxSingleChartScanner1AxisYMin.Tag = "0";
            // 
            // labelSingleChartScanner1AxisYMax
            // 
            this.labelSingleChartScanner1AxisYMax.AutoSize = true;
            this.labelSingleChartScanner1AxisYMax.Location = new System.Drawing.Point(7, 46);
            this.labelSingleChartScanner1AxisYMax.Name = "labelSingleChartScanner1AxisYMax";
            this.labelSingleChartScanner1AxisYMax.Size = new System.Drawing.Size(86, 13);
            this.labelSingleChartScanner1AxisYMax.TabIndex = 1;
            this.labelSingleChartScanner1AxisYMax.Tag = "0";
            this.labelSingleChartScanner1AxisYMax.Text = "Axis Y Maximum:";
            // 
            // labelSingleChartScanner1AxisYMin
            // 
            this.labelSingleChartScanner1AxisYMin.AutoSize = true;
            this.labelSingleChartScanner1AxisYMin.Location = new System.Drawing.Point(7, 20);
            this.labelSingleChartScanner1AxisYMin.Name = "labelSingleChartScanner1AxisYMin";
            this.labelSingleChartScanner1AxisYMin.Size = new System.Drawing.Size(83, 13);
            this.labelSingleChartScanner1AxisYMin.TabIndex = 0;
            this.labelSingleChartScanner1AxisYMin.Tag = "0";
            this.labelSingleChartScanner1AxisYMin.Text = "Axis Y Minimum:";
            // 
            // Analysisframe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 521);
            this.Controls.Add(this.panelSetupSingleScanners);
            this.Controls.Add(this.panelAnalysisFunction);
            this.Controls.Add(this.chartAnalyser);
            this.Name = "Analysisframe";
            this.Text = "Analysisframe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Analysisframe_FormClosed);
            this.Shown += new System.EventHandler(this.Analysisframe_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalyser)).EndInit();
            this.groupBoxAnalysisFunctions.ResumeLayout(false);
            this.groupBoxAnalysisFunctions.PerformLayout();
            this.panelAnalysisFunction.ResumeLayout(false);
            this.panelAnalysisFunction.PerformLayout();
            this.panelSetupSingleScanners.ResumeLayout(false);
            this.panelSetupSingleScanners.PerformLayout();
            this.groupBoxSetupSingleChart.ResumeLayout(false);
            this.groupBoxSetupSingleChart.PerformLayout();
            this.groupBoxSetupSingleScanner2.ResumeLayout(false);
            this.groupBoxSetupSingleScanner2.PerformLayout();
            this.groupBoxSetupSingleScanner1.ResumeLayout(false);
            this.groupBoxSetupSingleScanner1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAnalyser;
        private System.Windows.Forms.GroupBox groupBoxAnalysisFunctions;
        private System.Windows.Forms.Button buttonPOI;
        private System.Windows.Forms.Label labelDistanceFromRight;
        private System.Windows.Forms.Label labelDistanceFromLeft;
        private System.Windows.Forms.Label labelDistancefromRightValue;
        private System.Windows.Forms.Label labelDistanceFromLeftValue;
        private System.Windows.Forms.Label labelWidthTopObjectValue;
        private System.Windows.Forms.Label labelWidthTopObject;
        private System.Windows.Forms.Label labelDeviationCenterValue;
        private System.Windows.Forms.Label labelDeviationCenter;
        private System.Windows.Forms.Panel panelAnalysisFunction;
        private System.Windows.Forms.Panel panelSetupSingleScanners;
        private System.Windows.Forms.GroupBox groupBoxSetupSingleChart;
        private System.Windows.Forms.GroupBox groupBoxSetupSingleScanner2;
        private System.Windows.Forms.RadioButton radioButtonSingleChartScanner2Point;
        private System.Windows.Forms.RadioButton radioButtonSingleChartScanner2Spline;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner2AxisZMax;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner2AxisZMin;
        private System.Windows.Forms.Label labelSingleChartScanner2AxisZMax;
        private System.Windows.Forms.Label labelSingleChartScanner2AxisZMin;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner2AxisYMax;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner2AxisYMin;
        private System.Windows.Forms.Label labelSingleChartScanner2AxisYMax;
        private System.Windows.Forms.Label labelSingleChartScanner2AxisYMin;
        private System.Windows.Forms.GroupBox groupBoxSetupSingleScanner1;
        private System.Windows.Forms.RadioButton radioButtonSingleChartScanner1Point;
        private System.Windows.Forms.RadioButton radioButtonSingleChartScanner1Spline;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner1AxisZMax;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner1AxisZMin;
        private System.Windows.Forms.Label labelSingleChartScanner1AxisZMax;
        private System.Windows.Forms.Label labelSingleChartScanner1AxisZMin;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner1AxisYMax;
        private System.Windows.Forms.TextBox textBoxSingleChartScanner1AxisYMin;
        private System.Windows.Forms.Label labelSingleChartScanner1AxisYMax;
        private System.Windows.Forms.Label labelSingleChartScanner1AxisYMin;
    }
}

