using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SbB.FEM;
using SbB.Geometry;
using SbBGL;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace MortarFEM
{
    public partial class Form1 : Form
    {
        private FEMGLtools FEMtools;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            simpleOpenGlControl1.InitializeContexts();
            initGL();
            FEMtools = new FEMGLtools();
            simpleOpenGlControl1.Paint += FEMtools.OpenGLDraw;
            simpleOpenGlControl1.MouseDown += FEMtools.mouseDown;
            simpleOpenGlControl1.MouseMove += FEMtools.mouseMove;
            simpleOpenGlControl1.MouseUp += FEMtools.mouseUp;
            simpleOpenGlControl1.MouseWheel += FEMtools.mouseWheel;

            // Events
            this.openFromFileToolStripMenuItem.Click += FEMtools.openFromFile;
            this.loadMortarTestToolStripMenuItem.Click += FEMtools.loadMortarTest;
            this.loadFEMTestToolStripMenuItem.Click += FEMtools.loadFEMTest;
            this.subDomainSettingsToolStripMenuItem.Click += FEMtools.subdomainSettings;
            this.boundarySettingsToolStripMenuItem.Click += FEMtools.boundarySettings;
            this.insideBoundarySettingsToolStripMenuItem.Click += FEMtools.insideBoundarySettings;
            this.triangulateToolStripMenuItem.Click += FEMtools.initTriangulation;
            this.solveToolStripMenuItem.Click += FEMtools.initSolve;
            this.gradientUToolStripMenuItem.Click += FEMtools.postprocess;
            this.gradientVToolStripMenuItem.Click += FEMtools.postprocess;
        }

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


        private void button1_Click(object sender, EventArgs e)
        {
            if (FEMtools.OpenGlDic.ContainsKey(FigureDraw.TRIANGULATED_DOMAIN))
                ((TriangulatedDomain)FEMtools.OpenGlDic[FigureDraw.TRIANGULATED_DOMAIN]).DrawNumbers = !((TriangulatedDomain)FEMtools.OpenGlDic[FigureDraw.TRIANGULATED_DOMAIN]).DrawNumbers;
        }

        private void domainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.show(FigureDraw.DOMAIN);
        }

        private void triangulatedDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEMtools.show(FigureDraw.TRIANGULATED_DOMAIN);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double d;
            TextBox tb = ((TextBox)sender);
            if (!Double.TryParse(((TextBox)sender).Text, out d))
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                if (tb.Text.Length == 0) tb.Text = "0";
                tb.SelectionStart = tb.Text.Length;
                tb.SelectionLength = 0;
            }
            recalc();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            recalc();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            recalc();
        }
        void recalc()
        {
            if (solved)
            {
                double x = double.Parse(textBox1.Text);
                double y = double.Parse(textBox2.Text);
                if (comboBox1.SelectedIndex == 0) textBox3.Text = FEMtools.Processor.u(x, y).ToString("E");
                if (comboBox1.SelectedIndex == 1) textBox3.Text = FEMtools.Processor.v(x, y).ToString("E");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            double x = double.Parse(textBox1.Text);
            int i = comboBox1.SelectedIndex;
            sb.AppendLine("-------------------");
            foreach (Vertex v in this.FEMtools.Processor.Gs.Vertexes)
            if (v.X == x)
            {
                sb.AppendLine(v + "   " + FEMtools.Processor.Gs.Result[2 * v.Number + i].ToString("E"));
            }
            richTextBox1.Clear();
            richTextBox1.Text += sb.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            double y = double.Parse(textBox2.Text);
            int i = comboBox1.SelectedIndex;
            sb.AppendLine("-------------------");
            foreach (Vertex v in this.FEMtools.Processor.Gs.Vertexes)
                if (v.Y == y)
                {
                    sb.AppendLine(v + "   " + FEMtools.Processor.Gs.Result[2 * v.Number + i].ToString("E"));
                }
            richTextBox1.Clear();
            richTextBox1.Text += sb.ToString();
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
    }
}