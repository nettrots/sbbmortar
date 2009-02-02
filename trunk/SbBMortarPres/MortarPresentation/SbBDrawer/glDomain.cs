using System;
using System.Drawing;
using SbBMortar.SbB;
using Tao.OpenGl;

namespace MortarPresentation
{
    public class glDomain:DrawingElement
    {
        private Domain domain;
        public  glDomain(Domain domain)
        {
            this.domain = domain;
            double minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
            for (int i = 0; i < domain.Polygon.Count; i++)
            {
                if (minX > domain.Polygon[i].X) minX = domain.Polygon[i].X;
                if (minY > domain.Polygon[i].Y) minY = domain.Polygon[i].Y;
                if (maxX < domain.Polygon[i].X) maxX = domain.Polygon[i].X;
                if (maxY < domain.Polygon[i].Y) maxY = domain.Polygon[i].Y;
            }
            float h = (float) (maxY - minY);
            float w = (float) (maxX - minX);
            float dx = 0;// w / 20;
            float dy = 0;// h / 20;
            this.DrawBox = new RectangleF(new PointF((float) minX, (float) maxY),
                                          new SizeF(w, -h));
            Props.Add("External Color", Color.Black);
            Props.Add("Dots Color", Color.DarkRed);
            Props.Add("Internal Color", Color.RosyBrown);
            
        }

        public override void draw()
        {
            if(Hide)return;
            Gl.glDisable(Gl.GL_LINE_STIPPLE);

            Color c = (Color)Props["Dots Color"];
            //Drawind points
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_POINT);
            Gl.glPointSize(3f);
            Gl.glColor3d(c.R, c.G, c.B);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < domain.Polygon.Count; i++)
            {
                Gl.glVertex2d(domain.Polygon[i].X, domain.Polygon[i].Y);
            }
            Gl.glEnd();
            //...

            //External Polygon
            c =(Color) Props["External Color"];
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            Gl.glColor3d(c.R, c.G, c.B);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < domain.Polygon.Count; i++)
            {
                Gl.glVertex2d(domain.Polygon[i].X, domain.Polygon[i].Y);
            }
            Gl.glEnd();

            //Gl.glEnable(Gl.GL_POLYGON_STIPPLE);
            //Gl.glPolygonStipple();
            //if(domain.)

            Random rand=new Random(100);
            c = (Color)Props["Internal Color"];
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            
            for (int i = 0; i < domain.SubDomainCount; i++)
            {
                Gl.glLineWidth(i);
                Gl.glColor3i(rand.Next()*10, 0, rand.Next()*10);
                Gl.glBegin(Gl.GL_POLYGON);
                for (int j = 0; j < domain[i].Polygon.Count; j++)
                {
                    Gl.glVertex2d(domain[i].Polygon[j].X, domain[i].Polygon[j].Y);
                }
                Gl.glEnd();
            }
        }
    }
}