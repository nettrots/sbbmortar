using System;
using SbB;
using SbB.Algebra;
using SbB.FEM;
using SbB.Geometry;
using Tao.OpenGl;

namespace SbBGL
{
    public class Graphic : GLDraw
    {
        private Fx f;
        private double[] I;
        private int n;
        private Vector rez;
        private double[] color;
        private double scale;
        private double a;
        double[] maxmin0;
        double[] maxmin()
        {
            double max = double.MinValue, min=double.MaxValue;
            for (int i = 0; i < rez.Length; i++)
            {
                if (max < rez[i]) max = rez[i];
                if (min > rez[i]) min = rez[i];
          }
            maxmin0=new double[2]{max,min};
            if (MaxF<max) MaxF = max;
            if (MinF>min) MinF = min;
            //Scale = (MaxF < 0.0 ? 0.0 : MaxF) - (MinF > 0.0 ? 0.0 : MinF);
            return maxmin0;
        }

        public Graphic(Fx f, double[] I, int n)
        {
            this.f = f;
            this.I = I;
            this.n = n;
            double arg = I[0];
            a = (I[1] - I[0]) / (n-1);
            rez = new Vector();
            /*for (int i = 0; i < rez.Length; i++)
            {
                rez[i] = f(arg);
                arg += a;
            }*/
            while (arg<I[1])
            {
                rez.add(f(arg));
                arg += a;
            }
            rez.add(f(I[1]));
            color = new double[]{0.3,0,0.6};
            scale = 1 / (maxmin()[0] - maxmin()[1]);
        }
        public Graphic(Fx f, double[] I, int n,double[] color):this(f,I,n)
        {
            this.color = color;
        }

        public override void drawGl()
        {
            Scale = MaxF - MinF;
            double tr=0;
            if (MaxF * MinF > 0)
                tr = (MaxF + MinF)/2;
            double arg = I[0];
            double maX = 2, miX = -2, maY = 2, miY = -2;
            double dy = (maY - miY) / 100, dx = (maX - miX) / 100;

            Gl.glPushMatrix();

            Gl.glColor3d(0, 0, 0);
            /*Gl.glEnable(Gl.GL_LINE_STIPPLE);
            Gl.glLineStipple(1,100);*/
            Gl.glBegin(Gl.GL_LINES);
            //Arrow X
            Gl.glVertex2d(miX, 0);
            Gl.glVertex2d(maX, 0);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_LINE_STIPPLE);
            Gl.glBegin(Gl.GL_LINES);
            
            Gl.glVertex2d(maX,0 );
            Gl.glVertex2d(maX-dx, dy);
            Gl.glVertex2d(maX, 0);
            Gl.glVertex2d(maX - dx, -dy);
            //Arrow Y
            Gl.glVertex2d(0, miY);
            Gl.glVertex2d(0, maY);

            Gl.glVertex2d(0, maY);
            Gl.glVertex2d(dx, maY - dy);
            Gl.glVertex2d(0, maY);
            Gl.glVertex2d(-dx, maY - dy);

            Gl.glEnd();

            //Gl.glScaled(1.0f, scale, 1.0f);
        
            Gl.glColor3dv(color);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            for (int i = 0; i < rez.Length; i++)
            {
                if (!double.IsNaN(rez[i]))
                    Gl.glVertex2d(arg, (rez[i]-tr)/Scale);
                arg += a;
            }
            Gl.glEnd();

            arg = I[0];
            Gl.glPointSize(2f);
            Gl.glColor3dv(color);
            Gl.glBegin(Gl.GL_POINTS);
            for (int i = 0; i < rez.Length; i++)
            {
                if (!double.IsNaN(rez[i]))
                    Gl.glVertex2d(arg, (rez[i]-tr) / Scale);
                arg += a;
            }
            Gl.glEnd();

            Gl.glColor3d(0, 0, 0);
            
            //Text(0, maxmin0[0], (maxmin0[0]).ToString("E2"));
            //Text(0, maxmin0[1], (maxmin0[1]).ToString("E2"));
            Text(-0.5, (MaxF -tr)/ Scale, (MaxF).ToString("e1"));
            Text(-0.5, ((MaxF+MinF)/2 - tr) / Scale, ((MaxF+MinF)/2).ToString("e1"));
            Text(-0.5, (MinF-tr) / Scale, (MinF).ToString("e1"));

            Text(I[1], -0.1, ((int)I[1]).ToString());
            Text(I[0], -0.1, ((int)I[0]).ToString());

            
            Gl.glFlush();

            Gl.glPopMatrix();
        }
    }
}