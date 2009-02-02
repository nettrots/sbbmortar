using SbB.FEM;
using SbB.Geometry;
using Tao.OpenGl;

namespace SbBGL
{
    public class VisualTransmission : GLDraw
    {
        private GlobalSystem gs;

        public VisualTransmission(GlobalSystem gs)
        {
            this.gs = gs;
        }

        public override void drawGl()
        {
            Gl.glColor3d(0f, 0.5f, 0.5f);
            for (int i = 0; i < gs.Femdatas.Length; i++)
                foreach (Boundary boundary in gs.Femdatas[i].Boundaries)
                {
                    Gl.glBegin(Gl.GL_LINES);
                    foreach (Edge edge in boundary)
                    {
                        Gl.glVertex2d(edge.A.X, edge.A.Y);
                        Gl.glVertex2d(edge.B.X, edge.B.Y);
                    }
                    Gl.glEnd();
                }


            Gl.glColor3d(1f, 0f, 1f);
            for (int i = 0; i < gs.Femdatas.Length; i++)
                foreach (Boundary boundary in gs.Femdatas[i].Boundaries)
                {
                    Gl.glBegin(Gl.GL_LINES);
                    foreach (Edge edge in boundary)
                    {
                        Gl.glVertex2d(edge.A.X + gs.Result[2*edge.A.Number], edge.A.Y + gs.Result[2*edge.A.Number + 1]);
                        Gl.glVertex2d(edge.B.X + gs.Result[2*edge.B.Number], edge.B.Y + gs.Result[2*edge.B.Number + 1]);
                    }
                    Gl.glEnd();
                }
        }
    }
}