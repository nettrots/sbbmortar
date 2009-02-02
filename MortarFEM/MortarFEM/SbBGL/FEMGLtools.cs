using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MortarFEM;
using MortarFEM.Dialogs;
using SbB;
using SbB.FEM;
using SbB.Geometry;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace SbBGL
{
    public enum FigureDraw
    {
        DOMAIN,
        TRIANGULATED_DOMAIN,
        RESULT_GRADIENT_U,
        RESULT_GRADIENT_V,
        TRANSMISSION,
        GRAPHIC_U,
        GRAPHIC_V,
        GRAPHIC_EXX,
        GRAPHIC_EYY,
        GRAPHIC_EXY,
        GRAPHIC_SXX,
        GRAPHIC_SYY,
        GRAPHIC_SXY,

    }

    public class FEMGLtools
    {
        private Domain domain;
        private Manager processor;
        private FunctionParams functionParams;
        //private Dictionary<FigureDraw, GLDraw> openGlDic = new Dictionary<FigureDraw, GLDraw>();
        private ArrayList openGlDic = new ArrayList();
        private MainForm mainForm;

        public FunctionParams FunctionParams
        {
            get { return functionParams; }
            set { functionParams = value; }
        }
        public Manager Processor
        {
            get { return processor; }
        }
        public ArrayList OpenGlDic
        {
            get { return openGlDic; }
        }

        public FEMGLtools(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        
        public void openFromFile(object sender, EventArgs e)
        {
            mainForm.Stop();
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dlg.FileName);
                //Read polygon
                int n = int.Parse(sr.ReadLine()); //number of points
                Polygon p = new Polygon();
                for (int i = 0; i < n; i++)
                {
                    string s = sr.ReadLine();
                    p.addVertex(
                        new Vertex(double.Parse(s.Substring(0, s.IndexOf(" "))),
                                   double.Parse(s.Substring(s.IndexOf(" ") + 1))));
                }
                domain = new Domain(p);
                n = int.Parse(sr.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    string s = sr.ReadLine();
                    string s1 = s.Substring(s.IndexOf(" ") + 1);
                    Vertex v1 = new Vertex(float.Parse(s.Substring(0, s.IndexOf(" "))), float.Parse(s1.Substring(0, s1.IndexOf(" "))));

                    s = s1.Substring(s1.IndexOf(" ") + 1);
                    s1 = s.Substring(s.IndexOf(" ") + 1);
                    Vertex v2 = new Vertex(float.Parse(s.Substring(0, s.IndexOf(" "))), float.Parse(s1.Substring(0, s1.IndexOf(" "))));
                    s1 = s1.Substring(s1.IndexOf(" ") + 1);
                    domain.addInsideBoundary(new Edge(v1, v2));
                }
                OpenGlDic.Clear();
                OpenGlDic.Add( new DomainGL(domain));
                sr.Close();
            }
            mainForm.Play();
        }
        public void loadMortarTest(object sender, EventArgs e)
        {
            mainForm.Stop();
            Vertex[] vs = new Vertex[] {new Vertex(0, 0), new Vertex(2, 0), 
                                        new Vertex(2, 1), new Vertex(0, 1)};
            BoundaryClass[] bc = new BoundaryClass[] {new StaticBoundary(), new StaticBoundary(),
                                                      new StaticBoundary(0,-1), new CinematicBoundary()};
            domain = new Domain(new Polygon(vs), bc);
            domain.addInsideBoundary(new Edge(new Vertex(1, 0), new Vertex(1, 1)));
            domain[0].MaxArea = 0.01;
            OpenGlDic.Clear();
            OpenGlDic.Add( new DomainGL(domain));
            mainForm.Play();
        }
        public void loadFEMTest(object sender, EventArgs e)
        {
            mainForm.Stop();
            Vertex[] vs = new Vertex[] {new Vertex(0, 0), new Vertex(2, 0), 
                                        new Vertex(2, 1), new Vertex(0, 1)};
            BoundaryClass[] bc = new BoundaryClass[] {new StaticBoundary(), new StaticBoundary(),
                                                      new StaticBoundary(0,-1), new CinematicBoundary()};
            domain = new Domain(new Polygon(vs),bc);
            //domain[0].MaxArea = 0.1;
            OpenGlDic.Clear();
            OpenGlDic.Add(new DomainGL(domain));
            mainForm.Play();
        }
        public void saveNodesValue(object sender, EventArgs e)
        {
            if (processor != null)
                if (processor.Gs.Result != null)
                    (new SaveNodesValueDialog(processor.Gs)).ShowDialog();
        }
        public void exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void subdomainSettings(object sender, EventArgs e)
        {
            (new SubDomainDialog(domain)).ShowDialog();
        }
        public void boundarySettings(object sender, EventArgs e)
        {
            (new BoundaryDialog(domain)).ShowDialog();
        }
        public void insideBoundarySettings(object sender, EventArgs e)
        {
            (new InsideBoundaryDialog(domain)).ShowDialog();
        }
        public void initTriangulation(object sender, EventArgs e)
        {
            mainForm.Stop();
            if (domain != null)
            {
                processor = new Manager(domain);
                Processor.collectData();
                OpenGlDic.Clear();
                OpenGlDic.Add( new TriangulatedDomain(Processor.Gs));
            }
            mainForm.Play();
        }
        public void initSolve(object sender, EventArgs e)
        {
            mainForm.Stop();
            if (Processor != null)
            {
                Processor.solve();
                OpenGlDic.Clear();
                OpenGlDic.Add(new ResultGradient(Processor.Gs, Processor.u));
            }
            mainForm.Play();
        }
        public void postprocess(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            switch (item.Name)
            {
                case "gradientUToolStripMenuItem":
                    show(FigureDraw.RESULT_GRADIENT_U);
                    break;
                case "gradientVToolStripMenuItem":
                    show(FigureDraw.RESULT_GRADIENT_V);
                    break;
                default:
                    break;
            }
        }
        public void drawGradient(object sender, EventArgs e)
        {
            string text = ((Control) sender).Text;
            switch (text)
            {
                case "Draw: U":
                    show(FigureDraw.RESULT_GRADIENT_U);
                    break;
                case "Draw: V":
                    show(FigureDraw.RESULT_GRADIENT_V);
                    break;
                default:
                    break;
            }
        }
        public void drawPlot(object sender, EventArgs e)
        {
            string text = ((Control)sender).Text;
            double x = mainForm.Xconst;
            double y = mainForm.Yconst;
            int pc = mainForm.PointCount;
            double[] interval;
            switch (text)
            {
                case "Draw: U_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_U);
                    break;
                case "Draw: U_y":
                    interval = xCoor(y);
                    functionParams = new FunctionParams(y, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_U);
                    break;
                case "Draw: V_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_V);
                    break;
                case "Draw: V_y":
                    interval = xCoor(y);
                    functionParams = new FunctionParams(y, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_V);
                    break;
                case "Draw: Exx_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_EXX);
                    break;
                case "Draw: Exx_y":
                    interval = xCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_EXX);
                    break;
                case "Draw: Eyy_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_EYY);
                    break;
                case "Draw: Eyy_y":
                    interval = xCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_EYY);
                    break;
                case "Draw: 2Exy_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_EXX);
                    break;
                case "Draw: 2Exy_y":
                    interval = xCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_EXY);
                    break;
                case "Draw: Sxx_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_SXX);
                    break;
                case "Draw: Sxx_y":
                    interval = xCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_SXX);
                    break;
                case "Draw: Syy_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_SYY);
                    break;
                case "Draw: Syy_y":
                    interval = xCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_SYY);
                    break;
                case "Draw: Sxy_x":
                    interval = yCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.FIRST, interval, pc);
                    show(FigureDraw.GRAPHIC_SXY);
                    break;
                case "Draw: Sxy_y":
                    interval = xCoor(x);
                    functionParams = new FunctionParams(x, ParamNum.SEDOND, interval, pc);
                    show(FigureDraw.GRAPHIC_SXY);
                    break;
                default:
                    break;
            }
        }

        private double[] yCoor(double x)
        {
            double[] rez = {double.MaxValue, double.MinValue};
            for (int i=0; i<domain.P.N; i++)
            {
                Vertex a = domain.P[i], b = domain.P[i + 1];
                if (Math.Abs(b.X-a.X)>Constants.EPS)
                {
                    double y = (x - a.X)*(b.Y - a.Y)/(b.X - a.X) + a.Y;
                    if ((new Edge(a,b)).hasVertex(new Vertex(x,y)))
                    {
                        if (rez[0]>y) rez[0] = y;
                        if (rez[1]<y) rez[1] = y;
                    }
                }
            }
            //rez[0] += Constants.EPS;
            //rez[1] -= Constants.EPS;
            return rez;
        }
        private double[] xCoor(double y)
        {
            double[] rez = { double.MaxValue, double.MinValue };
            for (int i = 0; i < domain.P.N; i++)
            {
                Vertex a = domain.P[i], b = domain.P[i + 1];
                if (Math.Abs(b.Y - a.Y) > Constants.EPS)
                {
                    double x = (y - a.Y) * (b.X - a.X) / (b.Y - a.Y) + a.X;
                    if ((new Edge(a, b)).hasVertex(new Vertex(x, y)))
                    {
                        if (rez[0] > x) rez[0] = x;
                        if (rez[1] < x) rez[1] = x;
                    }
                }
            }
            //rez[0] += Constants.EPS;
            //rez[1] -= Constants.EPS;
            return rez;
        }



        public void OpenGLDraw(object sender, PaintEventArgs e)
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glScaled(scalefactor, scalefactor, 1.0);
            Gl.glTranslated(0.0f + _dx, 0.0f + _dy, -5.0f);
            Gl.glClearColor(1f, 0.99f, 0.99f, 1f);

            //4 points
            //Gl.glPointSize(4f);
            //Gl.glColor3i(0, 1, 0);
            //Gl.glBegin(Gl.GL_POINTS);
            //Gl.glVertex2i(-1, 0);
            //Gl.glVertex2i(1, 0);
            //Gl.glVertex2i(0, 1);
            //Gl.glVertex2i(0, -1);
            //Gl.glEnd();

            if (OpenGlDic.Count != 0)
                //foreach (KeyValuePair<FigureDraw, GLDraw> pair in OpenGlDic)
                for (int i = 0; i < openGlDic.Count; i++ )
                {
                    ((GLDraw)openGlDic[i]).drawGl();
                    //pair.Value.drawGl();
                }
            Gl.glFlush();
        }

        private void add(bool t)
        {
            if (!t) OpenGlDic.Clear();
        }

        public bool show(FigureDraw fg)
        {
            return show(fg, false);
        }

        public bool show(FigureDraw fg, bool addFigure)
        {
            GLDraw glDraw = new DomainGL(null);
            GLDraw.reset();

            switch (fg)
            {
                case FigureDraw.DOMAIN:
                    {
                        if (domain != null)
                            glDraw = new DomainGL(domain);
                        add(addFigure);
                        OpenGlDic.Add(glDraw);
                        break;
                    }
                case FigureDraw.TRIANGULATED_DOMAIN:
                    {
                        if (Processor.Gs.Femdatas[0].Triangles != null)
                            glDraw = new TriangulatedDomain(Processor.Gs);
                        else return false;
                        add(addFigure);
                        OpenGlDic.Add( glDraw);
                        break;
                    }
                case FigureDraw.RESULT_GRADIENT_U:
                    {
                        if (true)
                            glDraw = new ResultGradient(Processor.Gs, Processor.u);
                        add(addFigure);
                        OpenGlDic.Add(glDraw);
                        break;
                    }
                case FigureDraw.RESULT_GRADIENT_V:
                    {
                        if (true)
                            glDraw = new ResultGradient(Processor.Gs, Processor.v);
                        add(addFigure);
                        OpenGlDic.Add( glDraw);
                        break;
                    }
                case FigureDraw.TRANSMISSION:
                    {
                        if (true)
                            glDraw = new VisualTransmission(Processor.Gs);
                        add(addFigure);
                        OpenGlDic.Add( glDraw);
                        break;
                    }
                case FigureDraw.GRAPHIC_V:
                    {
                        if (true)
                        {
                            FxyToFxConvertor conv =
                                new FxyToFxConvertor(Processor.v, functionParams.paramNum, functionParams.parameter);
                            glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum);
                            add(addFigure);
                            OpenGlDic.Add( glDraw);
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_U:
                    {
                        if (true)
                        {
                            FxyToFxConvertor conv =
                                new FxyToFxConvertor(Processor.u, functionParams.paramNum, functionParams.parameter);

                            glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum);
                            add(addFigure);
                            OpenGlDic.Add(glDraw);
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_EXX:
                    {
                        if (true)
                        {
                            add(addFigure);
                            FmasToMasFConvector masconv = new FmasToMasFConvector(Processor.Exx);
                            for (int i = 0; i < domain.N; i++)
                            {
                                FxyToFxConvertor conv =
                                    new FxyToFxConvertor(masconv[i], functionParams.paramNum, functionParams.parameter);
                                double[] color=new double[3];
                                if(i==0) color[1] = 1;
                                if(i==1) color[0] = 1;
                                glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum,color);

                                OpenGlDic.Add(glDraw);
                            }
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_EYY:
                    {
                        if (true)
                        {
                            add(addFigure);
                            FmasToMasFConvector masconv = new FmasToMasFConvector(Processor.Eyy);
                            for (int i = 0; i < domain.N; i++)
                            {
                                FxyToFxConvertor conv =
                                    new FxyToFxConvertor(masconv[i], functionParams.paramNum, functionParams.parameter);
                                double[] color = new double[3];
                                if (i == 0) color[1] = 1;
                                if (i == 1) color[0] = 1;
                                glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum, color);

                                OpenGlDic.Add(glDraw);
                            }
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_EXY:
                    {
                        if (true)
                        {
                            add(addFigure);
                            FmasToMasFConvector masconv = new FmasToMasFConvector(Processor.Exy);
                            for (int i = 0; i < domain.N; i++)
                            {
                                FxyToFxConvertor conv =
                                    new FxyToFxConvertor(masconv[i], functionParams.paramNum, functionParams.parameter);
                                double[] color = new double[3];
                                if (i == 0) color[1] = 1;
                                if (i == 1) color[0] = 1;
                                glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum, color);

                                OpenGlDic.Add(glDraw);
                            }
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_SXX:
                    {
                        if (true)
                        {
                            add(addFigure);
                            FmasToMasFConvector masconv = new FmasToMasFConvector(Processor.Sxx);
                            for (int i = 0; i < domain.N; i++)
                            {
                                FxyToFxConvertor conv =
                                    new FxyToFxConvertor(masconv[i], functionParams.paramNum, functionParams.parameter);
                                double[] color = new double[3];
                                if (i == 0) color[2] = 1;
                                if (i == 1) color[0] = 1;
                                glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum, color);

                                OpenGlDic.Add(glDraw);
                            }
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_SYY:
                    {
                        if (true)
                        {
                            add(addFigure);
                            FmasToMasFConvector masconv = new FmasToMasFConvector(Processor.Syy);
                            for (int i = 0; i < domain.N; i++)
                            {
                                FxyToFxConvertor conv =
                                    new FxyToFxConvertor(masconv[i], functionParams.paramNum, functionParams.parameter);
                                double[] color = new double[3];
                                if (i == 0) color[2] = 1;
                                if (i == 1) color[0] = 1;
                                glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum, color);

                                OpenGlDic.Add(glDraw);
                            }
                        }
                        break;
                    }
                case FigureDraw.GRAPHIC_SXY:
                    {
                        if (true)
                        {
                            add(addFigure);
                            FmasToMasFConvector masconv = new FmasToMasFConvector(Processor.Sxy);
                            for (int i = 0; i < domain.N; i++)
                            {
                                FxyToFxConvertor conv =
                                    new FxyToFxConvertor(masconv[i], functionParams.paramNum, functionParams.parameter);
                                double[] color = new double[3];
                                if (i == 0) color[2] = 1;
                                if (i == 1) color[0] = 1;
                                glDraw = new Graphic(conv.f, functionParams.interval, functionParams.pointsNum, color);

                                OpenGlDic.Add(glDraw);
                            }
                        }
                        break;
                    }
                default:
                    {
                        OpenGlDic.Clear();
                        return true;
                    }
            }

            
            return true;
        }

        //Zoom
        public void zoomIn()
        {
            zoomIn(2);
        }
        public void zoomIn(int n)
        {
            scalefactor *= n;   
        }
        public void zoomOut()
        {
            zoomOut(2);
        }
        public void zoomOut(int n)
        {
            scalefactor /= n;
        }
        private double scalefactor = 1;
        private bool mdlBtn = false;
        public void mouseWheel(object sender, MouseEventArgs e)
        {
            if(!mdlBtn)return;
            if (e.Delta > 0) zoomIn();
            else zoomOut();
        }
        //Translate
        private void movedXdY(int dx, int dy)
        {
           double[] v1= GDItoOpenGL(0, 0);
           double[] v2 = GDItoOpenGL(1, 1);
            v2[0] -= v1[0];
            v2[1] -= v1[1];
            v2[1] *= -1;
            _dx += dx*v2[0];
            _dy -= dy*v2[0];
       }
        private double _dx = 0;
        private double _dy = 0;
        private Point location;
        public void mouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Middle) mdlBtn = true;
            location = e.Location;
        }
        public void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) mdlBtn = false;
        }
        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                movedXdY(e.Location.X - location.X, e.Location.Y - location.Y);
                location = e.Location;
            }
        }


        //Convert window coords to screen
        private static double[] GDItoOpenGL(int wx, int wy)
        {
            //for project...
            double[] mvm = new double[16];
            Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, mvm);
            double[] prjm = new double[16];
            Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, prjm);
            int[] vwp = new int[4];
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, vwp);
            //...
            float[] wz = new float[1];
            //.....xxx....
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glColor3i(0, 0, 0);
            int xxx = 10000;
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glVertex2d(-xxx, -xxx);
            Gl.glVertex2d(-xxx, xxx);
            Gl.glVertex2d(xxx, xxx);
            Gl.glVertex2d(xxx, -xxx);
            Gl.glEnd();
            Gl.glFlush();
            Gl.glReadPixels(wx, vwp[3] - wy - 1, 1, 1, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, wz);
            //...
            double x;
            double y;
            double z;
            //...
            Glu.gluUnProject(wx, vwp[3] - wy - 1, wz[0], mvm, prjm, vwp, out x, out y, out z);

            return new double[] {x, y, z};
        }

        //Convert screen coords to window
        private static double[] OpenGLtoGDI(float sx, float sy, float sz)
        {
            //for project...
            double[] mvm = new double[16];
            Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, mvm);
            double[] prjm = new double[16];
            Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, prjm);
            int[] vwp = new int[4];
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, vwp);
            double wx;
            double wy;
            double wz;
            Glu.gluProject(sx, sy, sz, mvm, prjm, vwp, out wx, out wy, out wz);
            return new double[] {wx, wy, wz};
        }

        public  void coordinates(object sender, MouseEventArgs e)
        {
            double[] coord = GDItoOpenGL(e.Location.X, e.Location.Y); 
        }

        public  void resultTable(object sender, EventArgs e)
        {
            Results result=new Results(processor);
            result.Show();
        }
    }
}