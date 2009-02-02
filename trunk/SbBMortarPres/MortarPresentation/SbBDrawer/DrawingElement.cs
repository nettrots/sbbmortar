using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MortarPresentation
{
    public abstract class DrawingElement
    {
        private Hashtable properties=new Hashtable();
        private RectangleF drawBox;
        private bool hide = false;

        public Hashtable Props
        {
            get { return properties; }
            set { properties = value; }
        }

        public RectangleF DrawBox
        {
            get { return drawBox; }
            set { drawBox = value; }
        }

        public bool Hide
        {
            get { return hide; }
            set { hide = value; }
        }

        public void HideAll()
        {
            Hide = true;
        }
        public abstract void draw();
    }
}
