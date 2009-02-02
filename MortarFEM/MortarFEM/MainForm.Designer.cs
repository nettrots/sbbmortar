namespace MortarFEM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleOpenGlControl1 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMortarTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFEMTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveNotesValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subDomainSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boundarySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insideBoundarySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.problemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangulateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postprocessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphictUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTranmisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.domainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangulatedDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlResults = new System.Windows.Forms.TabControl();
            this.tabPageGraphicalResult = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.coordTB = new System.Windows.Forms.TextBox();
            this.pointCountLabel = new System.Windows.Forms.Label();
            this.pointCountTextBox = new System.Windows.Forms.TextBox();
            this.drawPlotButton = new System.Windows.Forms.Button();
            this.plotComboBox = new System.Windows.Forms.ComboBox();
            this.yConstLabel = new System.Windows.Forms.Label();
            this.xConstLabel = new System.Windows.Forms.Label();
            this.yConstTextBox = new System.Windows.Forms.TextBox();
            this.xConstTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.drawGradientbutton = new System.Windows.Forms.Button();
            this.gradientComboBox = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlResults.SuspendLayout();
            this.tabPageGraphicalResult.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleOpenGlControl1
            // 
            this.simpleOpenGlControl1.AccumBits = ((byte)(0));
            this.simpleOpenGlControl1.AutoCheckErrors = false;
            this.simpleOpenGlControl1.AutoFinish = false;
            this.simpleOpenGlControl1.AutoMakeCurrent = true;
            this.simpleOpenGlControl1.AutoSwapBuffers = true;
            this.simpleOpenGlControl1.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl1.ColorBits = ((byte)(32));
            this.simpleOpenGlControl1.DepthBits = ((byte)(16));
            this.simpleOpenGlControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleOpenGlControl1.Location = new System.Drawing.Point(0, 0);
            this.simpleOpenGlControl1.Name = "simpleOpenGlControl1";
            this.simpleOpenGlControl1.Size = new System.Drawing.Size(411, 390);
            this.simpleOpenGlControl1.StencilBits = ((byte)(0));
            this.simpleOpenGlControl1.TabIndex = 0;
            this.simpleOpenGlControl1.Load += new System.EventHandler(this.simpleOpenGlControl1_Load);
            this.simpleOpenGlControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl1_Paint);
            this.simpleOpenGlControl1.Resize += new System.EventHandler(this.simpleOpenGlControl1_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.problemToolStripMenuItem,
            this.postprocessToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(735, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFromFileToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.resultsToolStripMenuItem,
            this.SaveNotesValueToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFromFileToolStripMenuItem
            // 
            this.openFromFileToolStripMenuItem.Name = "openFromFileToolStripMenuItem";
            this.openFromFileToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openFromFileToolStripMenuItem.Text = "Open";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMortarTestToolStripMenuItem,
            this.loadFEMTestToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // loadMortarTestToolStripMenuItem
            // 
            this.loadMortarTestToolStripMenuItem.Name = "loadMortarTestToolStripMenuItem";
            this.loadMortarTestToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.loadMortarTestToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadMortarTestToolStripMenuItem.Text = "Mortar test";
            // 
            // loadFEMTestToolStripMenuItem
            // 
            this.loadFEMTestToolStripMenuItem.Name = "loadFEMTestToolStripMenuItem";
            this.loadFEMTestToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.loadFEMTestToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadFEMTestToolStripMenuItem.Text = "FEM test";
            // 
            // resultsToolStripMenuItem
            // 
            this.resultsToolStripMenuItem.Name = "resultsToolStripMenuItem";
            this.resultsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.resultsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.resultsToolStripMenuItem.Text = "Results Table";
            // 
            // SaveNotesValueToolStripMenuItem
            // 
            this.SaveNotesValueToolStripMenuItem.Name = "SaveNotesValueToolStripMenuItem";
            this.SaveNotesValueToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveNotesValueToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SaveNotesValueToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subDomainSettingsToolStripMenuItem,
            this.boundarySettingsToolStripMenuItem,
            this.insideBoundarySettingsToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Options";
            // 
            // subDomainSettingsToolStripMenuItem
            // 
            this.subDomainSettingsToolStripMenuItem.Name = "subDomainSettingsToolStripMenuItem";
            this.subDomainSettingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.subDomainSettingsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.subDomainSettingsToolStripMenuItem.Text = "Subdomain settings";
            // 
            // boundarySettingsToolStripMenuItem
            // 
            this.boundarySettingsToolStripMenuItem.Name = "boundarySettingsToolStripMenuItem";
            this.boundarySettingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.boundarySettingsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.boundarySettingsToolStripMenuItem.Text = "Boundary settings";
            // 
            // insideBoundarySettingsToolStripMenuItem
            // 
            this.insideBoundarySettingsToolStripMenuItem.Name = "insideBoundarySettingsToolStripMenuItem";
            this.insideBoundarySettingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.insideBoundarySettingsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.insideBoundarySettingsToolStripMenuItem.Text = "Inside boundary settings";
            // 
            // problemToolStripMenuItem
            // 
            this.problemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triangulateToolStripMenuItem,
            this.solveToolStripMenuItem});
            this.problemToolStripMenuItem.Name = "problemToolStripMenuItem";
            this.problemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.problemToolStripMenuItem.Text = "Problem";
            // 
            // triangulateToolStripMenuItem
            // 
            this.triangulateToolStripMenuItem.Name = "triangulateToolStripMenuItem";
            this.triangulateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.triangulateToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.triangulateToolStripMenuItem.Text = "Triangulate";
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.solveToolStripMenuItem.Text = "Solve";
            // 
            // postprocessToolStripMenuItem
            // 
            this.postprocessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gradientToolStripMenuItem,
            this.resultVToolStripMenuItem,
            this.graphictUToolStripMenuItem,
            this.addTranmisionToolStripMenuItem,
            this.addDomainToolStripMenuItem,
            this.fileToolStripMenuItem2,
            this.domainToolStripMenuItem,
            this.triangulatedDomainToolStripMenuItem});
            this.postprocessToolStripMenuItem.Enabled = false;
            this.postprocessToolStripMenuItem.Name = "postprocessToolStripMenuItem";
            this.postprocessToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.postprocessToolStripMenuItem.Text = "Postprocesing";
            this.postprocessToolStripMenuItem.Visible = false;
            // 
            // gradientToolStripMenuItem
            // 
            this.gradientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gradientUToolStripMenuItem,
            this.gradientVToolStripMenuItem});
            this.gradientToolStripMenuItem.Name = "gradientToolStripMenuItem";
            this.gradientToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.gradientToolStripMenuItem.Text = "Gradient result";
            // 
            // gradientUToolStripMenuItem
            // 
            this.gradientUToolStripMenuItem.Name = "gradientUToolStripMenuItem";
            this.gradientUToolStripMenuItem.Size = new System.Drawing.Size(81, 22);
            this.gradientUToolStripMenuItem.Text = "U";
            // 
            // gradientVToolStripMenuItem
            // 
            this.gradientVToolStripMenuItem.Name = "gradientVToolStripMenuItem";
            this.gradientVToolStripMenuItem.Size = new System.Drawing.Size(81, 22);
            this.gradientVToolStripMenuItem.Text = "V";
            // 
            // resultVToolStripMenuItem
            // 
            this.resultVToolStripMenuItem.Name = "resultVToolStripMenuItem";
            this.resultVToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.resultVToolStripMenuItem.Text = "Graphict_V";
            this.resultVToolStripMenuItem.Click += new System.EventHandler(this.resultVToolStripMenuItem_Click);
            // 
            // graphictUToolStripMenuItem
            // 
            this.graphictUToolStripMenuItem.Name = "graphictUToolStripMenuItem";
            this.graphictUToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.graphictUToolStripMenuItem.Text = "Graphict_U";
            this.graphictUToolStripMenuItem.Click += new System.EventHandler(this.graphictUToolStripMenuItem_Click);
            // 
            // addTranmisionToolStripMenuItem
            // 
            this.addTranmisionToolStripMenuItem.Name = "addTranmisionToolStripMenuItem";
            this.addTranmisionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addTranmisionToolStripMenuItem.Text = "Add Tranmision";
            this.addTranmisionToolStripMenuItem.Click += new System.EventHandler(this.addTranmisionToolStripMenuItem_Click);
            // 
            // addDomainToolStripMenuItem
            // 
            this.addDomainToolStripMenuItem.Name = "addDomainToolStripMenuItem";
            this.addDomainToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addDomainToolStripMenuItem.Text = "Add Domain";
            this.addDomainToolStripMenuItem.Click += new System.EventHandler(this.addDomainToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem2
            // 
            this.fileToolStripMenuItem2.Name = "fileToolStripMenuItem2";
            this.fileToolStripMenuItem2.Size = new System.Drawing.Size(169, 22);
            this.fileToolStripMenuItem2.Text = "File";
            // 
            // domainToolStripMenuItem
            // 
            this.domainToolStripMenuItem.Name = "domainToolStripMenuItem";
            this.domainToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.domainToolStripMenuItem.Text = "Domain";
            this.domainToolStripMenuItem.Click += new System.EventHandler(this.domainToolStripMenuItem_Click);
            // 
            // triangulatedDomainToolStripMenuItem
            // 
            this.triangulatedDomainToolStripMenuItem.Name = "triangulatedDomainToolStripMenuItem";
            this.triangulatedDomainToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.triangulatedDomainToolStripMenuItem.Text = "TriangulatedDomain";
            this.triangulatedDomainToolStripMenuItem.Click += new System.EventHandler(this.triangulatedDomainToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.simpleOpenGlControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlResults);
            this.splitContainer1.Size = new System.Drawing.Size(735, 390);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControlResults
            // 
            this.tabControlResults.Controls.Add(this.tabPageGraphicalResult);
            this.tabControlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlResults.Location = new System.Drawing.Point(0, 0);
            this.tabControlResults.Name = "tabControlResults";
            this.tabControlResults.SelectedIndex = 0;
            this.tabControlResults.Size = new System.Drawing.Size(320, 390);
            this.tabControlResults.TabIndex = 13;
            // 
            // tabPageGraphicalResult
            // 
            this.tabPageGraphicalResult.Controls.Add(this.groupBox2);
            this.tabPageGraphicalResult.Controls.Add(this.groupBox1);
            this.tabPageGraphicalResult.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraphicalResult.Name = "tabPageGraphicalResult";
            this.tabPageGraphicalResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraphicalResult.Size = new System.Drawing.Size(312, 364);
            this.tabPageGraphicalResult.TabIndex = 0;
            this.tabPageGraphicalResult.Text = "Graphical results";
            this.tabPageGraphicalResult.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.coordTB);
            this.groupBox2.Controls.Add(this.pointCountLabel);
            this.groupBox2.Controls.Add(this.pointCountTextBox);
            this.groupBox2.Controls.Add(this.drawPlotButton);
            this.groupBox2.Controls.Add(this.plotComboBox);
            this.groupBox2.Controls.Add(this.yConstLabel);
            this.groupBox2.Controls.Add(this.xConstLabel);
            this.groupBox2.Controls.Add(this.yConstTextBox);
            this.groupBox2.Controls.Add(this.xConstTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 238);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plot";
            // 
            // coordTB
            // 
            this.coordTB.Location = new System.Drawing.Point(203, 19);
            this.coordTB.Name = "coordTB";
            this.coordTB.Size = new System.Drawing.Size(100, 20);
            this.coordTB.TabIndex = 8;
            this.coordTB.Text = "0";
            // 
            // pointCountLabel
            // 
            this.pointCountLabel.AutoSize = true;
            this.pointCountLabel.Location = new System.Drawing.Point(106, 112);
            this.pointCountLabel.Name = "pointCountLabel";
            this.pointCountLabel.Size = new System.Drawing.Size(74, 13);
            this.pointCountLabel.TabIndex = 7;
            this.pointCountLabel.Text = "Points number";
            // 
            // pointCountTextBox
            // 
            this.pointCountTextBox.Location = new System.Drawing.Point(186, 109);
            this.pointCountTextBox.Name = "pointCountTextBox";
            this.pointCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.pointCountTextBox.TabIndex = 6;
            this.pointCountTextBox.Text = "50";
            // 
            // drawPlotButton
            // 
            this.drawPlotButton.Location = new System.Drawing.Point(171, 178);
            this.drawPlotButton.Name = "drawPlotButton";
            this.drawPlotButton.Size = new System.Drawing.Size(115, 23);
            this.drawPlotButton.TabIndex = 5;
            this.drawPlotButton.Text = "Draw: U_x";
            this.drawPlotButton.UseVisualStyleBackColor = true;
            // 
            // plotComboBox
            // 
            this.plotComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "U_x",
            "U_y",
            "V_x",
            "V_y",
            "Exx_x",
            "Exx_y",
            "Eyy_x",
            "Eyy_y",
            "2Exy_x",
            "2Exy_y",
            "Sxx_x",
            "Sxx_y",
            "Syy_x",
            "Syy_y",
            "Sxy_x",
            "Sxy_y"});
            this.plotComboBox.FormattingEnabled = true;
            this.plotComboBox.Items.AddRange(new object[] {
            "U_x",
            "U_y",
            "V_x",
            "V_y",
            "Exx_x",
            "Exx_y",
            "Eyy_x",
            "Eyy_y",
            "2Exy_x",
            "2Exy_y",
            "Sxx_x",
            "Sxx_y",
            "Syy_x",
            "Syy_y",
            "Sxy_x",
            "Sxy_y"});
            this.plotComboBox.Location = new System.Drawing.Point(22, 178);
            this.plotComboBox.Name = "plotComboBox";
            this.plotComboBox.Size = new System.Drawing.Size(121, 21);
            this.plotComboBox.TabIndex = 4;
            this.plotComboBox.Text = "U_x";
            this.plotComboBox.SelectedIndexChanged += new System.EventHandler(this.plotComboBox_SelectedIndexChanged);
            // 
            // yConstLabel
            // 
            this.yConstLabel.AutoSize = true;
            this.yConstLabel.Location = new System.Drawing.Point(19, 68);
            this.yConstLabel.Name = "yConstLabel";
            this.yConstLabel.Size = new System.Drawing.Size(40, 13);
            this.yConstLabel.TabIndex = 3;
            this.yConstLabel.Text = "Yconst";
            // 
            // xConstLabel
            // 
            this.xConstLabel.AutoSize = true;
            this.xConstLabel.Location = new System.Drawing.Point(19, 42);
            this.xConstLabel.Name = "xConstLabel";
            this.xConstLabel.Size = new System.Drawing.Size(40, 13);
            this.xConstLabel.TabIndex = 2;
            this.xConstLabel.Text = "Xconst";
            // 
            // yConstTextBox
            // 
            this.yConstTextBox.Location = new System.Drawing.Point(65, 65);
            this.yConstTextBox.Name = "yConstTextBox";
            this.yConstTextBox.Size = new System.Drawing.Size(100, 20);
            this.yConstTextBox.TabIndex = 1;
            this.yConstTextBox.Text = "0,5";
            // 
            // xConstTextBox
            // 
            this.xConstTextBox.Location = new System.Drawing.Point(65, 39);
            this.xConstTextBox.Name = "xConstTextBox";
            this.xConstTextBox.Size = new System.Drawing.Size(100, 20);
            this.xConstTextBox.TabIndex = 0;
            this.xConstTextBox.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drawGradientbutton);
            this.groupBox1.Controls.Add(this.gradientComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gradient";
            // 
            // drawGradientbutton
            // 
            this.drawGradientbutton.Location = new System.Drawing.Point(171, 54);
            this.drawGradientbutton.Name = "drawGradientbutton";
            this.drawGradientbutton.Size = new System.Drawing.Size(115, 23);
            this.drawGradientbutton.TabIndex = 1;
            this.drawGradientbutton.Text = "Draw: U";
            this.drawGradientbutton.UseVisualStyleBackColor = true;
            // 
            // gradientComboBox
            // 
            this.gradientComboBox.FormattingEnabled = true;
            this.gradientComboBox.Items.AddRange(new object[] {
            "U",
            "V"});
            this.gradientComboBox.Location = new System.Drawing.Point(22, 54);
            this.gradientComboBox.Name = "gradientComboBox";
            this.gradientComboBox.Size = new System.Drawing.Size(121, 21);
            this.gradientComboBox.TabIndex = 0;
            this.gradientComboBox.Text = "U";
            this.gradientComboBox.SelectedIndexChanged += new System.EventHandler(this.gradientComboBox_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(689, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 2;
            this.button1.Tag = "Play";
            this.button1.Text = "StopGl";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 392);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(735, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "Done";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 414);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MortarFEM";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControlResults.ResumeLayout(false);
            this.tabPageGraphicalResult.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem postprocessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphictUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTranmisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem domainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangulatedDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMortarTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFEMTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subDomainSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boundarySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insideBoundarySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem problemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangulateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradientUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradientVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveNotesValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlResults;
        private System.Windows.Forms.TabPage tabPageGraphicalResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox coordTB;
        private System.Windows.Forms.Label pointCountLabel;
        private System.Windows.Forms.TextBox pointCountTextBox;
        private System.Windows.Forms.Button drawPlotButton;
        private System.Windows.Forms.ComboBox plotComboBox;
        private System.Windows.Forms.Label yConstLabel;
        private System.Windows.Forms.Label xConstLabel;
        private System.Windows.Forms.TextBox yConstTextBox;
        private System.Windows.Forms.TextBox xConstTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button drawGradientbutton;
        private System.Windows.Forms.ComboBox gradientComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

