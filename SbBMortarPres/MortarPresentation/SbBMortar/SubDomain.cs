using System;
using System.Collections.Generic;

namespace SbBMortar.SbB
{
    public class SubDomain
    {
        #region Fields
        private Polygon polygon;
        private Triangulation triangulation;
        private double maxArea = 0.05;
        private double minAngle = 20;
        private double youngModulus = 21000;
        private double poissonRatio = 0.3;
        private Matrix d = new Matrix(3,3);
        private List<Vertex> vertexes;
        private List<Element> elements;
        private List<FEMEdge>[] boundaries;
        private int n;
        #endregion

        #region Constructors
        public SubDomain(Polygon polygon)
        {
            this.polygon = polygon;
            triangulation = new LinialTriangleTriangulation(this.polygon);
            refreshD();
        }
        #endregion

        #region Properties
        public Polygon Polygon
        {
            get { return polygon; }
        }
        public Triangulation Triangulation
        {
            get { return triangulation; }
            set { triangulation = value; }
        }
        public double MaxArea
        {
            get { return maxArea; }
            set { maxArea = value; }
        }
        public double MinAngle
        {
            get { return minAngle; }
            set { minAngle = value; }
        }
        public double YoungModulus
        {
            get { return youngModulus; }
            set
            {
                youngModulus = value;
                refreshD();
            }
        }
        public double PoissonRatio
        {
            get { return poissonRatio; }
            set
            {
                poissonRatio = value;
                refreshD();
            }
        }
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
        public Matrix D
        {
            get { return d; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        #endregion

        #region Methods
        #region Private
        private void refreshD()
        {
            double d1 = (1 - poissonRatio) * youngModulus / ((1 + poissonRatio) * (1 - 2 * poissonRatio));
            D[0][0] = 1;
            D[0][1] = D[1][0] = poissonRatio / (1 - poissonRatio);
            D[1][1] = 1;
            D[2][2] = (1 - 2 * poissonRatio) / (2 * (1 - poissonRatio));
            d = d1*D;
        }
        #endregion

        #region Public
        public void triangulate()
        {
            triangulation.triangulate(minAngle, maxArea);
            vertexes = triangulation.Vertexes;
            elements = triangulation.Elements;
            boundaries = triangulation.Boundaries;
        }
        #endregion
        #endregion
    }
}