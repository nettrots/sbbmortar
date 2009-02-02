using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SbB.FEM;
using SbB.Geometry;
using SbBGL;
using Tao.OpenGl;

namespace MortarFEM
{
    public partial class MainForm : Form
    {
        private FEMGLtools FEMtools;

        public MainForm()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            initGL();
            FEMtools = new FEMGLtools(this);
            simpleOpenGlControl1.Paint += FEMtools.OpenGLDraw;
            simpleOpenGlControl1.MouseDown += FEMtools.mouseDown;
            simpleOpenGlControl1.MouseMove += FEMtools.mouseMove;
            simpleOpenGlControl1.MouseUp += FEMtools.mouseUp;
            simpleOpenGlControl1.MouseWheel += FEMtools.mouseWheel;

            // Events
            this.openFromFileToolStripMenuItem.Click += FEMtools.openFromFile;
            this.loadMortarTestToolStripMenuItem.Click += FEMtools.loadMortarTest;
            this.loadFEMTestToolStripMenuItem.Click += FEMtools.loadFEMTest;
            this.SaveNotesValueToolStripMenuItem.Click += FEMtools.saveNodesValue;
            this.subDomainSettingsToolStripMenuItem.Click += FEMtools.subdomainSettings;
            this.boundarySettingsToolStripMenuItem.Click += FEMtools.boundarySettings;
            this.insideBoundarySettingsToolStripMenuItem.Click += FEMtools.insideBoundarySettings;
            this.triangulateToolStripMenuItem.Click += FEMtools.initTriangulation;
            this.solveToolStripMenuItem.Click += FEMtools.initSolve;
            this.gradientUToolStripMenuItem.Click += FEMtools.postprocess;
            this.gradientVToolStripMenuItem.Click += FEMtools.postprocess;

            this.drawGradientbutton.Click += FEMtools.drawGradient;
            this.drawPlotButton.Click += FEMtools.drawPlot;

            this.simpleOpenGlControl1.MouseMove += FEMtools.coordinates;
            resultsToolStripMenuItem.Click += FEMtools.resultTable;
        }

        public double Xconst
        {
            get
            {
                double x;
                double.TryParse(xConstTextBox.Text, out x);
                return x;
            }
        }
        public double Yconst
        {
            get
            {
                double y;
                double.TryParse(yConstTextBox.Text, out y);
                return y;
            }
        }
        public int PointCount
        {
            get
            {
                int pc;
                int.TryParse(pointCountTextBox.Text, out pc);
                return pc;
            }
        }

        // old events

        private void initGL()
        {
            Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height); // Reset The Current Viewport
            Gl.glMatrixMode(Gl.GL_PROJECTION); // Select The Projection Matrix
            Gl.glLoadIdentity(); // Reset The Projection Matrix
            Glu.gluPerspective(45, simpleOpenGlControl1.Width / (double)simpleOpenGlControl1.Height, 0.1, 100);
            // Calculate The Aspect Ratio Of The Window
            Gl.glMatrixMode(Gl.GL_MODELVIEW); // Select The Modelview Matrix
            Gl.glLoadIdentity();

            Gl.glShadeModel(Gl.GL_SMOOTH); // Enable Smooth Shading
            //Gl.glClearColor(0, 0, 0, 0.5f); // Black Background
            Gl.glClearDepth(1); // Depth Buffer Setup
            Gl.glEnable(Gl.GL_DEPTH_TEST); // Enables Depth Testing
            Gl.glDepthFunc(Gl.GL_LEQUAL); // The Type Of Depth Testing To Do
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST); // Really Nice Perspective Calculations
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            initGL();
            simpleOpenGlControl1.Invalidate();
        }

        private void resultVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.FunctionParams = new FunctionParams(1, ParamNum.FIRST, new double[] { 0, 1 }, 20);
            FEMtools.show(FigureDraw.GRAPHIC_V);
        }

        private bool solved = false;

        private void graphictUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.FunctionParams = new FunctionParams(1, ParamNum.FIRST, new double[] { 0, 1 }, 20);
            FEMtools.show(FigureDraw.GRAPHIC_U);
        }

        private void addTranmisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.show(FigureDraw.TRANSMISSION, true);

        }

        private void addDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.show(FigureDraw.DOMAIN, true);

        }


        /*private void button1_Click(object sender, EventArgs e)
        {
            if (FEMtools.OpenGlDic.ContainsKey(FigureDraw.TRIANGULATED_DOMAIN))
                ((TriangulatedDomain)FEMtools.OpenGlDic[FigureDraw.TRIANGULATED_DOMAIN]).DrawNumbers = !((TriangulatedDomain)FEMtools.OpenGlDic[FigureDraw.TRIANGULATED_DOMAIN]).DrawNumbers;
        }*/

        private void domainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.show(FigureDraw.DOMAIN);
        }

        private void triangulatedDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.show(FigureDraw.TRIANGULATED_DOMAIN);
        }






        private void simpleOpenGlControl1_Resize(object sender, EventArgs e)
        {
            //initGL();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
           // initGL();
        }

        private void simpleOpenGlControl1_Load(object sender, EventArgs e)
        {
            //initGL();
        }

        // New events
        private void gradientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawGradientbutton.Text = "Draw: " + gradientComboBox.Text;
            FEMtools.drawGradient(drawGradientbutton, e);
        }
        private void plotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawPlotButton.Text = "Draw: " + plotComboBox.Text;
            FEMtools.drawPlot(drawPlotButton, e);
        }

        public void Stop()
        {
            toolStripStatusLabel1.Text = "OpenGl Paused...";
            this.timer1.Enabled = false;
        }
        public void Play()
        {
            this.timer1.Enabled = true;
            toolStripStatusLabel1.Text = "Done.";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Button thisB = (Button)sender;
            if( thisB.Tag.ToString()=="Play")
            {
                thisB.Tag = "Stop";
                Stop();
            }
            else
            {
                thisB.Tag = "Play";
                Play();
            }
        }

      
    }
}