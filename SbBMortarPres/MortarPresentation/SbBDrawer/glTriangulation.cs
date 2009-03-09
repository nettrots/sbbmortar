using System.Collections.Generic;
using System.Drawing;
using SbBMortar.SbB;
using Tao.OpenGl;

namespace MortarPresentation
{
    public class glTriangulation:DrawingElement
    {
        private List<Element> elems;
        public glTriangulation(List<Element> elems)
        {
            this.elems = elems;
            double minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
            foreach (Element elem in elems)
            {
                for (int i = 0; i < elem.NodesCount; i++)
                {
                    if (minX > elem[i].X) minX = elem[i].X;
                    if (minY > elem[i].Y) minY = elem[i].Y;
                    if (maxX < elem[i].X) maxX = elem[i].X;
                    if (maxY < elem[i].Y) maxY = elem[i].Y;
                }
               
            }
            float h = (float)(maxY - minY);
            float w = (float)(maxX - minX);
            float dx = 0;// w / 20;
            float dy = 0;//h / 20;
            this.DrawBox = new RectangleF(new PointF((float)minX, (float)maxY),
                                          new SizeF(w, -h));
            Props.Add("Main Color", Color.Black);
            Props.Add("Numbers", false);
        }



        public override void draw()
        {
            if (Hide) return;
            
            int j = 0;
            Vertex vv = new Vertex();
            bool numb = (bool) Props["Numbers"];
            Color c = (Color) Props["Main Color"];
            Gl.glDisable(Gl.GL_LINE_STIPPLE);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINES);
            Gl.glLineWidth(1f);
            Gl.glColor3d(c.R / 255.0, c.G / 255.0, c.B / 255.0);
            foreach (Element elem in elems)
            {
                int count = (elem.NodesCount%3 == 0) ? 3 : 4; 
                Gl.glBegin(Gl.GL_LINE_LOOP);
                for (int i = 0; i < count; i++)
                {
                    Gl.glVertex2d(elem[i].X, elem[i].Y);
                    if (numb) vv += elem[i];
                }
                Gl.glEnd();
                Gl.glFlush();

                if (numb)
                {
                    vv *= 1/count;
                    SbBglDrawer.Text(vv, (j++).ToString());
                }
            }
        }
    }
    }
