using System;
using System.Drawing;
using SbB.Geometry;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace SbBGL
{
    public abstract class GLDraw
    {
        public static double Scale = 1;
        public static double MaxF = 1;
        public static double MinF = 0;

        public static void reset()
        {
            Scale = 10e+6;
            MaxF = double.MinValue;
            MinF = double.MaxValue;
        }

        public abstract void drawGl();

        public static void Text(Vertex vertex)
        {
            Text(vertex, vertex.ToString());
        }
        public static void Text(double x,double y, string str)
        {
            Text(new Vertex(x,y),str);
        }
        public static void Text(Vertex pos, string str)
        {
            
            Gl.glDeleteLists(1000,256);
            IntPtr hdc = Wgl.wglGetCurrentDC();
            
            Font f = new Font("Times New Roman", 8);
            Gdi.SelectObject(hdc, f.ToHfont());
            Wgl.wglUseFontBitmaps(hdc, 0, 256, 1000);
            Gl.glRasterPos2d(pos.X,pos.Y);
            Gl.glListBase(1000);
            Gl.glCallLists(str.Length, Gl.GL_SHORT, str);
        }
    }
}