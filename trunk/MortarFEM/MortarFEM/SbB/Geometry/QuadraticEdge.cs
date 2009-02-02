
namespace SbB.Geometry
{
    public class QuadraticEdge: Edge
    {
        private Vertex c;

        public QuadraticEdge(Vertex a, Vertex b):base(a,b)
        {
            c = 0.5 * (a + b);
        }
        public QuadraticEdge(Edge e): this(e.A, e.B)
        {
        }

        public override Vertex this[int index]
        {
            get
            {
                if (index == 2) return c;
                return base[index];
            }
            set
            {
                if (index == 2) {c = value; return;}
                base[index] = value;
            }
        }
        public override int nodesCount()
        {
            return 3;
        }
        public override int rank()
        {
            return 3;
        }
        public override int[] indexes()
        {
            return new int[] { a.Number, b.Number, c.Number };
        }
        public override double Ni(int i, Vertex v)
        {
            if (!hasVertex(v)) return 0;
            if (i != 0 && i != 1 && i!=2) return 0;
            LinearEdge le = new LinearEdge(this);
            double phii = le.Ni(0, v);
            double phij = le.Ni(1, v);
            switch (i)
            {
                case 0:
                    return phii*(2*phii - 1);
                case 1:
                    return phij*(2*phij - 1);
                case 2:
                    return 4*phii*phij;
                default:
                    return 0;
            }
        }
    }
}
