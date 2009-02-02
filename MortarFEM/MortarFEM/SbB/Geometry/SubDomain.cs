using System;
using SbB.FEM;

namespace SbB.Geometry
{
    public class SubDomain: IComparable
    {
        private double youngModulus = 21000;
        private double poissonRatio = 0.3;
        private double maxArea = 0.05;
        private double minAngle = 20;
        private Polygon polygon;
        private BoundaryClass[] boundaryClasses;
        private ApproximationOrder order = ApproximationOrder.LINEAR;

        public SubDomain(Polygon polygon)
        {
            this.polygon = polygon;
            boundaryClasses = new BoundaryClass[polygon.N];
        }
        public SubDomain(Polygon polygon, BoundaryClass[] bcArray)
        {
            this.polygon = polygon;
            boundaryClasses = new BoundaryClass[polygon.N];
            for (int i = 0; i < bcArray.Length && i < boundaryClasses.Length; i++)
                boundaryClasses[i] = bcArray[i];
        }

        public Polygon P
        {
            get { return polygon; }
        }
        public double YoungModulus
        {
            get { return youngModulus; }
            set { youngModulus = value; }
        }
        public double PoissonRatio
        {
            get { return poissonRatio; }
            set { poissonRatio = value; }
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

        public ApproximationOrder Order
        {
            get { return order; }
            set { order = value; }
        }


        public BoundaryClass boundary(int index)
        {
            if (index >= polygon.N) return null;
            return boundaryClasses[index];
        }
        public void setBoundary(int index, BoundaryClass boundaryClass)
        {
            if (index >= 0 && index < polygon.N)
                boundaryClasses[index] = boundaryClass;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < polygon.N; i++)
                s += P.edge(i) + "\t" + boundary(i).type()+"\n";
            return s;
        }

        int IComparable.CompareTo(object o)
        {
            SubDomain sd = (SubDomain)o;
            Vertex v1 = sd.P[0];
            for (int i = 1; i < sd.P.N; i++)
                if (v1 > sd.P[i]) v1 = sd.P[i];
            Vertex v2 = this.P[0];
            for (int i = 1; i < this.P.N; i++)
                if (v1 > this.P[i]) v1 = this.P[i];
            return ((IComparable)v1).CompareTo(v2);
        }
    }
}
