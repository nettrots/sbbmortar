using System;
using System.Collections.Generic;

namespace SbBMortar.SbB
{
    public abstract class Triangulation
    {
        #region Fields
        protected Polygon polygon;
        protected List<Vertex> vertexes;
        protected List<Element> elements;
        protected List<FEMEdge>[] boundaries;
        #endregion

        #region Properties
        public List<Vertex> Vertexes
        {
            get
            {
                if (vertexes == null) throw new Exception("Triangulation did not complete");
                return vertexes;
            }
        }
        public List<Element> Elements
        {
            get
            {
                if (elements == null) throw new Exception("Triangulation did not complete");
                return elements;
            }
 
        }
        public List<FEMEdge>[] Boundaries
        {
            get
            {
                if (boundaries == null) throw new Exception("Triangulation did not complete");
                return boundaries;
            }
        }
        #endregion

        #region Methods
        public abstract void triangulate(double minAngle, double maxArea);
        #endregion
    }
}