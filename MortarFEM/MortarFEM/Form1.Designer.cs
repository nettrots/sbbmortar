namespace MortarFEM
{
    partial class Form1
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFromFileToolStripMenuItem
            // 
            this.openFromFileToolStripMenuItem.Name = "openFromFileToolStripMenuItem";
            this.openFromFileToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.openFromFileToolStripMenuItem.Text = "Open";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMortarTestToolStripMenuItem,
            this.loadFEMTestToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
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
            this.triangulateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.triangulateToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.triangulateToolStripMenuItem.Text = "Triangulate";
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
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
            this.postprocessToolStripMenuItem.Name = "postprocessToolStripMenuItem";
            this.postprocessToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.postprocessToolStripMenuItem.Text = "Postprocesing";
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
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox3);
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(735, 390);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "rez=";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Y=";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "X=";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(266, 68);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(42, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "F5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(175, 327);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(181, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Print";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "0";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "U",
            "V"});
            this.comboBox1.Location = new System.Drawing.Point(27, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.ValueMember = "0";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(61, 97);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(247, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "___";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(61, 68);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "0";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 131);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(237, 190);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 355);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show Numbers";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 414);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MortarFEM";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem domainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangulatedDomainToolStripMenuItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
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
    }
}

