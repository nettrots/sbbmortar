using SbB.FEM;
using SbB.Geometry;
using Tao.OpenGl;

namespace SbBGL
{
    public class TriangulatedDomain : GLDraw
    {
        private GlobalSystem gs;
        private bool drawNumbers=false;

        public TriangulatedDomain(GlobalSystem gs)
        {
            this.gs = gs;
        }

        public bool DrawNumbers
        {
            get { return drawNumbers; }
            set { drawNumbers = value; }
        }

        public override void drawGl()
        {
            //DRAWING TRIANGLES (1)
            //TODO: in case of diferent domains triangles - different colours
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            Gl.glColor3d(0.2, 0.6, 0.6);
            for (int i = 0; i < gs.Femdatas.Length; i++)
                foreach (Triangle tr in gs.Femdatas[i].Triangles)
                {
                    Gl.glBegin(Gl.GL_POLYGON);
                        Gl.glVertex2d(tr.A.X, tr.A.Y);
                        Gl.glVertex2d(tr.B.X, tr.B.Y);
                        Gl.glVertex2d(tr.C.X, tr.C.Y);
                    /*for (int j = 0; j < tr.nodesCount(); j++)
                    {
                        Gl.glVertex2d(tr[j].X, tr[j].Y);
                    }*/
                    Gl.glEnd();
                }
            //...


            //Drawind points
            Gl.glPointSize(3f);
            Gl.glColor3d(0.999, 0.1, 0.1);
            Gl.glBegin(Gl.GL_POINTS);
            foreach (Vertex vertex in gs.Vertexes)
            {
                Gl.glVertex2d(vertex.X, vertex.Y);
                Text(vertex, vertex.Number.ToString());
            }
            Gl.glEnd();

            if (DrawNumbers)
                foreach (Vertex vertex in gs.Vertexes)
                {
                    if (vertex.Number == 0)
                        Text(vertex, "0");
                    Text(vertex, vertex.Number.ToString());
                }
            

            //...


            //Flush
            Gl.glFlush();
        }
    }
}