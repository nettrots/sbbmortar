namespace MortarPresentation
{
    partial class MortarPresentation
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mortarTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.femTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subdomainsSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boundarySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mortarSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableOfResulstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.solveToolStripMenuItem,
            this.showToolStripMenuItem,
            this.tableOfResulstToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(406, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mortarTestToolStripMenuItem,
            this.femTestToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Visible = false;
            // 
            // mortarTestToolStripMenuItem
            // 
            this.mortarTestToolStripMenuItem.Name = "mortarTestToolStripMenuItem";
            this.mortarTestToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.mortarTestToolStripMenuItem.Text = "Mortar Test";
            // 
            // femTestToolStripMenuItem
            // 
            this.femTestToolStripMenuItem.Name = "femTestToolStripMenuItem";
            this.femTestToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.femTestToolStripMenuItem.Text = "Fem Test";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(108, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subdomainsSettingsToolStripMenuItem,
            this.boundarySettingsToolStripMenuItem,
            this.mortarSettingsToolStripMenuItem,
            this.drawingSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // subdomainsSettingsToolStripMenuItem
            // 
            this.subdomainsSettingsToolStripMenuItem.Name = "subdomainsSettingsToolStripMenuItem";
            this.subdomainsSettingsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.subdomainsSettingsToolStripMenuItem.Text = "Subdomain settings";
            this.subdomainsSettingsToolStripMenuItem.Click += new System.EventHandler(this.subdomainsSettingsToolStripMenuItem_Click);
            // 
            // boundarySettingsToolStripMenuItem
            // 
            this.boundarySettingsToolStripMenuItem.Name = "boundarySettingsToolStripMenuItem";
            this.boundarySettingsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.boundarySettingsToolStripMenuItem.Text = "Boundary settings";
            this.boundarySettingsToolStripMenuItem.Click += new System.EventHandler(this.boundarySettingsToolStripMenuItem_Click);
            // 
            // mortarSettingsToolStripMenuItem
            // 
            this.mortarSettingsToolStripMenuItem.Name = "mortarSettingsToolStripMenuItem";
            this.mortarSettingsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.mortarSettingsToolStripMenuItem.Text = "Mortar Settings";
            this.mortarSettingsToolStripMenuItem.Click += new System.EventHandler(this.mortarSettingsToolStripMenuItem_Click);
            // 
            // drawingSettingsToolStripMenuItem
            // 
            this.drawingSettingsToolStripMenuItem.Name = "drawingSettingsToolStripMenuItem";
            this.drawingSettingsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.drawingSettingsToolStripMenuItem.Text = "Drawing settings";
            this.drawingSettingsToolStripMenuItem.Click += new System.EventHandler(this.drawingSettingsToolStripMenuItem_Click);
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.solveToolStripMenuItem.Text = "Solve";
            this.solveToolStripMenuItem.Click += new System.EventHandler(this.solveToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trianToolStripMenuItem,
            this.functionsToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // trianToolStripMenuItem
            // 
            this.trianToolStripMenuItem.Name = "trianToolStripMenuItem";
            this.trianToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.trianToolStripMenuItem.Text = "Mesh";
            this.trianToolStripMenuItem.Click += new System.EventHandler(this.trianToolStripMenuItem_Click);
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.functionsToolStripMenuItem.Text = "Functions";
            this.functionsToolStripMenuItem.Click += new System.EventHandler(this.functionsToolStripMenuItem_Click);
            // 
            // tableOfResulstToolStripMenuItem
            // 
            this.tableOfResulstToolStripMenuItem.Name = "tableOfResulstToolStripMenuItem";
            this.tableOfResulstToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.tableOfResulstToolStripMenuItem.Text = "Table";
            this.tableOfResulstToolStripMenuItem.Click += new System.EventHandler(this.tableOfResulstToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progStatus});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(406, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progStatus
            // 
            this.progStatus.Name = "progStatus";
            this.progStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 272);
            this.panel1.TabIndex = 1;
            // 
            // MortarPresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 318);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MortarPresentation";
            this.Text = "Mortar Presentation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subdomainsSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boundarySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mortarTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem femTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawingSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mortarSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableOfResulstToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel progStatus;
        private System.Windows.Forms.Panel panel1;

    }
}

