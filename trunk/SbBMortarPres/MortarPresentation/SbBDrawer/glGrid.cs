using System;
using System.Drawing;
using Tao.OpenGl;

namespace MortarPresentation
{
    public class glGrid:DrawingElement
    {
        public glGrid()
        {
            Props.Add("Need New Box",null);
            Props.Add("Lines Count", 10);
            Props.Add("Main Color", Color.AliceBlue);
        }

        void drawOneLine(double x1, double y1, double x2, double y2)
        {

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(x1, y1);
            Gl.glVertex2d(x2, y2);
            Gl.glEnd();
        }

        public override void draw()
        {
            if (Hide) return;
            
            //if (DrawBox==new RectangleF()) throw new Exception("Error in drawing grid: no drawing box!");
            //props pars
            Color c = (Color) Props["Main Color"];
            int xd = (int)Props["Lines Count"], yd = xd;
            RectangleF box = DrawBox;

            Gl.glEnable(Gl.GL_LINE_WIDTH);
            Gl.glLineWidth(0.3f);
            Gl.glEnable(Gl.GL_LINE_STIPPLE);
            Gl.glLineStipple(1, 0x0F0F);
           
            
            
            string s;
            float x1, y1;
            for (int i = 0; i < xd; i++)
            {
                x1 = box.X + box.Width/xd*i;
                y1 = box.Top;
                s = x1.ToString("e2");
                if (s.Contains("e-001") || s.Contains("e+000") || s.Contains("e+001")) s = x1.ToString("f2");
                //                s = (x1 > 0)
                //                        ? ((x1.ToString("e2")).Substring(0, 4))
                //                        : s = (x1.ToString("e2")).Substring(0, 5);
                SbBglDrawer.Text(x1, y1 + box.Height/50, " "+s);
                Gl.glColor4d(0,0,0,0.5);
                drawOneLine(x1, y1, (box.X + box.Width/xd*i), box.Bottom);
            }
            for (int i = 0; i < yd; i++)
            {
                x1 = box.Left;
                y1 = box.Top + box.Height/yd*i;
                s = y1.ToString("e2");
                if (s.Contains("e-001") || s.Contains("e+000") || s.Contains("e+001")) s = y1.ToString("f2");
                //                s = (y1 > 0)
                //                   ? ((y1.ToString("e2")).Substring(0, 4))
                //                   : s = (y1.ToString("e2")).Substring(0, 5);
                SbBglDrawer.Text(x1, y1, " "+s);
                Gl.glColor4d(0, 0, 0, 0.5);
//                Gl.glColor3b(c.R, c.G, c.B);
                drawOneLine(x1, y1, box.Right, (box.Top + box.Height/yd*i));
            }

            Gl.glLineWidth(1f);
            Gl.glDisable(Gl.GL_LINE_STIPPLE);
        }
    }
}