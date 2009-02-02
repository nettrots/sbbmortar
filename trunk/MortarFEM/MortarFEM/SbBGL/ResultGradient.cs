using SbB.Algebra;
using SbB.FEM;
using SbB.Geometry;
using Tao.OpenGl;

namespace SbBGL
{
    public class ResultGradient : GLDraw
    {
        private Vector data = new Vector();
        private Fxy f;
        private GlobalSystem gs;
        private double max;
        private double min;
        private int nmax;
        private int nmin;
        private double xmax;
        private double xmin;
        private double ymax;
        private double ymin;

        public ResultGradient(GlobalSystem gs, Fxy f)
        {
            this.gs = gs;
            this.f = f;
            max = double.MinValue;
            min = double.MaxValue;
            nmin = -1;
            nmax = -1;
            for (int i = 0; i < gs.Femdatas.Length; i++)
            {
                foreach (Vertex v in gs.Femdatas[i].Vertexes)
                {
                    if (data.Length < v.Number + 1) data.Length = v.Number + 1;
                    data[v.Number] = f(v.X, v.Y);
                    if (max < data[v.Number])
                    {
                        max = data[v.Number];
                        nmax = v.Number;
                        xmax = v.X;
                        ymax = v.Y;
                    }
                    if (min > data[v.Number])
                    {
                        min = data[v.Number];
                        nmin = v.Number;
                        xmin = v.X;
                        ymin = v.Y;
                    }
                }
            }
        }

        //...
        //gl.PolygonMode(OpenGL.FRONT_AND_BACK, OpenGL.FILL);
        //for(float t=0;t<=1;t+=0.01f)
        //{
        //    float[] f_t = RGBfunction(t);
        //    gl.Color(f_t[0], f_t[1], f_t[2]);
        //    gl.Begin(OpenGL.POLYGON);
        //        gl.Vertex(t,t);
        //        gl.Vertex(t , t + 0.1);
        //        gl.Vertex(t + 0.1,t+0.01);
        //        gl.Vertex(t, t + 0.1 );
        //    gl.End();
        //}
        //gl.Flush();
        public override void drawGl()
        {
            //DRAWING Result 
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);

            for (int i = 0; i < gs.Femdatas.Length; i++)
                foreach (Triangle tr in gs.Femdatas[i].Triangles)
                {
                    Gl.glBegin(Gl.GL_POLYGON);
                    for (int j = 0; j < 3/*tr.nodesCount()*/; j++)
                    {
                        double[] cl = RGBfunction((data[tr[j].Number] - min)/(max - min));
                        Gl.glColor3d(cl[0], cl[1], cl[2]);
                        Gl.glVertex2d(tr[j].X, tr[j].Y);
                    }
                    Gl.glEnd();
                }


            //for project...

            //double[] mvm = new double[16];
            //Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, mvm);
            //double[] prjm = new double[16];
            //Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, prjm);
            //int[] vwp = new int[4];
            //Gl.glGetIntegerv(Gl.GL_VIEWPORT, vwp);
            //double[] x = new double[1];
            //double[] y = new double[1];
            //double[] z = new double[1];
            //SbB.Geometry.Vertex v = new SbB.Geometry.Vertex(xmax, ymax); //gs.Vertexes[nmax];
            //double wx = v.X;
            //double wy = v.Y;
            //double wz = 0;
            //Glu.gluProject(wx, wy, wz, mvm, prjm, vwp, x, y, z);
            //Gl.glGDIGraphics.DrawString("Max" + max, new Font(FontFamily.Families[6], 8),
            //                          Brushes.Black, new PointF((float)x[0], (float)(vwp[3] - y[0])));
            //v = new SbB.Geometry.Vertex(xmin, ymin); //gs.Vertexes[nmin];
            //wx = v.X;
            //wy = v.Y;
            //wz = 0;
            //gl.Project(wx, wy, wz, mvm, prjm, vwp, x, y, z);
            //gl.GDIGraphics.DrawString("Min" + min, new Font(FontFamily.Families[6], 8),
            //                          Brushes.Black, new PointF((float)x[0], (float)(vwp[3] - y[0])));
        }

        private double[] RGBfunction(double t)
        {
            if (t >= 0 && t <= 0.5)
                return new double[] {0, 2*t, 1 - 2*t};
            else if (t > 0.5 && t <= 1)
                return new double[] {2*(t - 0.5f), 1 - 2*(t - 0.5f), 0};

            return new double[] {0, 0, 0};
        }
    }
}