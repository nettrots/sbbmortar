using SbB.Collections;
using SbB.Geometry;

namespace SbB.FEM
{
    public abstract class FEMData
    {
        //Data
        protected Triangles triangles;
        protected Vertexes vertexes;
        protected Boundaries boundaries;
        protected double youngModulus;
        protected double poissonRatio;


        public Triangles Triangles
        {
            get { return triangles; }
        }
        public Vertexes Vertexes
        {
            get { return vertexes; }
        }
        public Boundaries Boundaries
        {
            get { return boundaries; }
        }
        public double YoungModulus
        {
            get { return youngModulus; }
        }
        public double PoissonRatio
        {
            get { return poissonRatio; }
        }
   

        public FEMData(SubDomain subdomain){}
    }
}
