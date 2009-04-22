using System;
using System.Collections.Generic;
using System.Drawing;
using SbBMortar.SbB;
using Tao.OpenGl;

namespace MortarPresentation
{
    
    public class glGraphic:DrawingElement
    {
        private ElementsList<List<Vertex>[]> funcTables;
        private List<string> names=new List<string>();  
        private Color[] stdColors = new Color[]
            {
                Color.Blue,
                Color.Green,
                Color.Red,
                Color.Magenta,
                Color.FromArgb(128,0,0),
                Color.FromArgb(128,64,0),
                Color.FromArgb(128,255,0),
                Color.FromArgb(128,0,128),
                Color.FromArgb(64,128,128),
                Color.FromArgb(255,0,255),
    };

        public glGraphic()
        {
            funcTables = new ElementsList<List<Vertex>[]>();
            funcTables.onListChage += refreshBox;
        }

        public ElementsList<List<Vertex>[]> FuncTables
        {
            get { return funcTables; }
            set { funcTables = value; }
        }

        public List<string> Names
        {
            get { return names; }
            set { names = value; }
        }


        void refreshBox()
        {
            double minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
            foreach (List<Vertex>[] fxy in FuncTables)
            {
                foreach (List<Vertex> list in fxy)
                {
                    foreach (Vertex vertex in list)
                    {
                        if (minX > vertex.X) minX = vertex.X;
                        if (minY > vertex.Y) minY = vertex.Y;
                        if (maxX < vertex.X) maxX = vertex.X;
                        if (maxY < vertex.Y) maxY = vertex.Y;
                    }
                }
            }
            float h = (float)(maxY - minY);
            float w = (float)(maxX - minX);
            float dx = 0;// w / 20;
            float dy = 0;// h / 20;
            this.DrawBox = new RectangleF(new PointF((float) minX, (float) maxY),
                                          new SizeF(w, -h));
        }

        public override void draw()
        {
            if(Hide)return;
            
            int j=0;
            Gl.glColor3d(stdColors[0].R / 255.0, stdColors[0].G / 255.0, stdColors[0].B / 255.0);
            foreach (List<Vertex>[] table in funcTables)
            {
                Gl.glColor3d(stdColors[j].R / 255.0, stdColors[j].G / 255.0, stdColors[j].B / 255.0);
               
                foreach (List<Vertex> list in table)
                {
                    Gl.glLineWidth(2f);
                    Gl.glBegin(Gl.GL_LINE_STRIP);
                    for (int i = 0; i < list.Count; i++)
                    {
                      Gl.glVertex2d(list[i].X, list[i].Y);
                    }
                    Gl.glEnd();

                    if (false)
                    {
                        Gl.glLineWidth(2f);
                        Gl.glBegin(Gl.GL_POINTS);
                        for (int i = 0; i < list.Count; i++)
                        {
                            Gl.glVertex2d(list[i].X, list[i].Y);
                        }
                        Gl.glEnd();
                    }
                }

             
                double dx = DrawBox.Width*0.02, dy = 0.02 * DrawBox.Height;
                double x = DrawBox.Right-dx, y = DrawBox.Top + (j + 1) * 0.02 * DrawBox.Height;//legend position

                Gl.glLineWidth(100F);
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK,Gl.GL_FILL);
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glVertex2d(x-dx, y-dy);
                Gl.glVertex2d(x, y);
                Gl.glVertex2d(x-dx,  y);
                Gl.glVertex2d( x, y-dy);
                Gl.glEnd();
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);

                Font f = SbBglDrawer.Font;
                
                SbBglDrawer.Font =new Font(f.FontFamily,f.Size,FontStyle.Bold); //new Font("Arial", 10, FontStyle.Bold);
                SbBglDrawer.Text(x, 1.01 * y, names[j]);
                SbBglDrawer.Font = f;
                j++;
                if (j == 10) j = 0;

            }
            Gl.glLineWidth(1f);
            
        }
    }
}