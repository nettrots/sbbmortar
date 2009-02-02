using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SbBMortar.SbB;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace MortarPresentation
{

    public class SbBglDrawer:SbBDrawer
    {
        #region Fields
        private RectangleF drawBox;
        private Size windowSize;
        private static Font font = new Font("Times New Roman", 9);
        #endregion

        #region Constructor
        public SbBglDrawer(Size windowSize)
        {
            WindowSize = windowSize;
            Elemenst.onListChage += parseProperties;
            Elemenst.onListChage += drawScene;
        }
        
        #endregion

        #region Properties

        public Size WindowSize
        {
            get { return windowSize; }
            set { windowSize = value; }
        }

        public static Font Font
        {
            get { return font; }
            set { font = value; }
        }

        #endregion

        #region DrawerInterface

        public override void parseProperties()
        {
            drawBox=new RectangleF();
            foreach (DrawingElement element in Elemenst)
            {
                if (!element.Props.ContainsKey("Need New Box")&&!element.Hide)
                {
                    RectangleF oldBox = drawBox;
                    drawBox = element.DrawBox;
                    drawBox = merage(drawBox, oldBox);
                }
            }
            foreach (DrawingElement element in Elemenst)
            {
                if (element.Props.ContainsKey("Need New Box"))
                {
                    element.DrawBox = drawBox;
                }
            }
        }
        public override void drawScene()
        {
            initGl();
            foreach (DrawingElement element in Elemenst)
            {
                element.draw();
            }
        }
        
        #endregion

        #region PrivateMethods
		
        private static RectangleF merage(RectangleF a,RectangleF b)
        {
            if(b==new RectangleF()) return a;
            float x, y, x1, y1;
            RectangleF nr=new RectangleF();
            x = a.Left;
            x1 = b.Left;
            y = a.Top;
            y1 = b.Top;
            x = (x < x1) ? x : x1;
            y = (y > y1) ? y : y1;
            nr.Location=new PointF(x,y);

            x = a.Right;
            x1 = b.Right;
            y = a.Bottom;
            y1 = b.Bottom;
            x = (x > x1) ? x : x1;
            y = (y < y1) ? y : y1;
            nr.Size=new SizeF(x-nr.X,y-nr.Y);
           
            return nr;
        }
        private void initGl()
        {

            // Reset The Current Viewport
            int a = 0,b=0;
            if(WindowSize.Width > WindowSize.Height)
            {
                a = WindowSize.Height;
                b = WindowSize.Width;
                Gl.glViewport((b-a)/2, 0, a, a);

            }
            else
            {
                b = WindowSize.Height;
                a = WindowSize.Width;
                Gl.glViewport(0, (b - a) / 2, a, a);

            }

          // Select The Modelview Matrix
            Gl.glMatrixMode(Gl.GL_MODELVIEW); 
            Gl.glLoadIdentity();
            // Enable Smooth Shading
            Gl.glShadeModel(Gl.GL_SMOOTH); 
            // Depth Buffer Setup
            Gl.glClearDepth(1);
            // Enables Depth Testing
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // The Type Of Depth Testing To Do
            Gl.glDepthFunc(Gl.GL_LEQUAL);
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            // Really Nice Perspective Calculations
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST); 


            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            double dx = drawBox.Width*0.05, dy =- drawBox.Height*0.05;
            Gl.glOrtho(drawBox.Left-dx, drawBox.Right+dx, drawBox.Bottom - dy, drawBox.Top+dy, 1, -1);

            Gl.glEnable(Gl.GL_POINT_SMOOTH);
           
            //Gl.glScaled(scalefactor, scalefactor, 1.0);
            //Gl.glTranslated(0.0f  , 0.0f , -5.0f);
            Gl.glClearColor(1f, 0.99f, 0.99f, 1f);
        }
 
     	#endregion     

        #region PublicStaticMethods

        public static void Text(double x, double y, string str)
        {
            Text(new Vertex(x, y), str);
        }
        public static void Text(Vertex pos, string str)
        {
            Gl.glColor3b(200, 0, 10);
            Gl.glDeleteLists(1000, 256);
            IntPtr hdc = Wgl.wglGetCurrentDC();


            Font f = font;
            Gdi.SelectObject(hdc, f.ToHfont());
            Wgl.wglUseFontBitmaps(hdc, 0, 256, 1000);
            Gl.glRasterPos2d(pos.X, pos.Y);
            Gl.glListBase(1000);
            Gl.glCallLists(str.Length, Gl.GL_SHORT, str);
        } 
        #endregion
    }
}
