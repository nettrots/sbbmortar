using System;
using System.Collections;
using System.Collections.Generic;

namespace MortarPresentation
{
    public abstract class SbBDrawer
    {
        private ElementsList<DrawingElement> elements = new ElementsList<DrawingElement>();

        public ElementsList<DrawingElement> Elemenst
        {
            get { return elements; }
            set
            {

                elements = value;
                
            }
        }

        public abstract void parseProperties();
        public abstract void drawScene();
     
    }
}
