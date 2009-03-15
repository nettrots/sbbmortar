using System;
using System.Collections.Generic;
using System.Text;

namespace SbBMortar.SbB
{
    public abstract class Quadrangle: Element
    {
        #region Properties
        public double S
        {
            get
            {
                double s = 0.0;
                for (int i = 0; i < 4; i++)
                    s += (this[i].X + this[i + 1].X)*(this[i].Y - this[i + 1].Y);
                return Math.Abs(s)/2;
            }
        }
        #endregion

        #region Methods
        public bool isVertexInQuadrangle(Vertex v)
        {
            if (isVertexOnQuadrangle(v)) return false;
            Polygon p = new Polygon(new Vertex[] { nodes[0], nodes[1], nodes[2], nodes[3] });
            return p.isVertexInPolygon(v);
        }
        public bool isVertexOnQuadrangle(Vertex v)
        {
            for (int i = 0; i < 4; i++)
            {
                Edge edge = new Edge(nodes[i], nodes[(i+1)%4]);
                if (edge.hasVertex(v)) return true;
            }
            return false;
        }
        public override bool hasVertex(Vertex v)
        {
            return isVertexOnQuadrangle(v) || isVertexInQuadrangle(v);
        }
        #endregion
    }
}
