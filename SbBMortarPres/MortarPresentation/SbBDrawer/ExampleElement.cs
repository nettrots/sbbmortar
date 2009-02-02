using System.Collections;
using System.Drawing;
using Tao.OpenGl;

namespace MortarPresentation
{
    public class ExampleElement:glElement
    {
        public ExampleElement()
        {
            Props=new Hashtable();
            Props.Add("Name","ExampleElement");
            Props.Add("Draw Box",new RectangleF(new Point(-3, 3), new Size(4, -4)));
            Props.Add("Color",Color.Red);
        }

        public override void draw()
        {

            //Drawind points
            //Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_POINT);
            Gl.glPointSize(3f);
            Gl.glColor3d(0.5, 0.6, 0.4);
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glVertex2i(1, 1);
            Gl.glVertex2i(-1, 1);
            Gl.glVertex2i(0, 0);
            Gl.glEnd();
            //...
        }
    }
}