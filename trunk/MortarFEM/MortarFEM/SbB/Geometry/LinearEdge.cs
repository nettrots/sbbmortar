namespace SbB.Geometry
{
    public class LinearEdge: Edge
    {
        public LinearEdge(Vertex a, Vertex b): base(a,b) {}
        public LinearEdge(Edge e) : base(e.A, e.B) {}

        public override Vertex this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        public override int nodesCount()
        {
            return 2;
        }
        public override int rank()
        {
            return 2;
        }
        public override int[] indexes()
        {
            return new int[] {a.Number, b.Number};
        }
        public override double Ni(int i, Vertex v)
        {
            if (!hasVertex(v)) return 0;
            if (i != 0 && i != 1) return 0;
            return (v - this[(i + 1) % 2]).Length / Length;
        }
    }
}
