using SbB.Algebra;
using SbB.Collections;
using SbB.Geometry;

namespace SbB.FEM
{
    public abstract class Mortar
    {
        protected Vertexes vertexes = new Vertexes();

        public abstract int rank();
        public abstract Matrix bilinear(Edge edge);
    }
}
