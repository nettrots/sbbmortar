using SbB.Geometry;
using Tao.OpenGl;

namespace SbBGL
{
    public class DomainGL : GLDraw
    {
        private Domain domain;

        public DomainGL(Domain domain)
        {
            this.domain = domain;
        }

        public override void drawGl()
        {
            //domain.P

            //Drawind points
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_POINT);
            Gl.glPointSize(3f);
            Gl.glColor3d(0.5, 0.6, 0.4);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < domain.P.N; i++)
            {
                Gl.glVertex2d(domain.P[i].X, domain.P[i].Y);
            }
            Gl.glEnd();
            //...

            //DRAWING LINES (1)
            //TODO: in case of domain boundary - different colours
            //for project...
            //double[] mvm = new double[16];
            //gl.GetDouble(OpenGL.MODELVIEW_MATRIX, mvm);
            //double[] prjm = new double[16];
            //gl.GetDouble(OpenGL.PROJECTION_MATRIX, prjm);
            //int[] vwp = new int[4];
            //gl.GetInteger(OpenGL.VIEWPORT, vwp);
            //double[] x=new double[1];
            //double[] y = new double[1];
            //double[] z = new double[1];
            //...

            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            Gl.glColor3d(0.9, 0.6, 0.1);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < domain.P.N; i++)
            {
                Gl.glVertex2d(domain.P[i].X, domain.P[i].Y);
                double wx = domain.P[i].X;
                double wy = domain.P[i].Y;
                // double wz = 0;
                //Gl.glProject(wx, wy, wz, mvm, prjm, vwp, x, y, z);
                //Gl.glGDIGraphics.DrawString(domain.P[i].ToString(), new Font(FontFamily.Families[6], 8),
                //                          Brushes.Black, new PointF((float) x[0], (float) (vwp[3] - y[0])));
            }
            Gl.glEnd();
            //...

            // ...IBN))))
            Gl.glColor3d(0.0, 0.9, 0.0);
            for (int i = 0; i < domain.IBN; i++)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(domain.insideBoundary(i).E.A.X, domain.insideBoundary(i).E.A.Y);
                Gl.glVertex2d(domain.insideBoundary(i).E.B.X, domain.insideBoundary(i).E.B.Y);
                Gl.glEnd();
            }
            //...


            //Flushing...
            Gl.glFlush();
        }
    }
}