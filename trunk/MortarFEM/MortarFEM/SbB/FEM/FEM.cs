using SbB.Algebra;
using SbB.Geometry;

namespace SbB.FEM
{
    public abstract class FEM
    {
        protected double youngModulus;
        protected double poissonRatio;

        public abstract Matrix bilinear(Triangle triangle);
        public abstract Matrix linear(Edge edge);
    }

}
