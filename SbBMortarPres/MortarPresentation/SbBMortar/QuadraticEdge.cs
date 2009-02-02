namespace SbBMortar.SbB
{
    public class QuadraticEdge: FEMEdge
    {
        #region Constructors
        public QuadraticEdge(Vertex a, Vertex b)
        {
            nodes = new Vertex[] { a, b, 0.5 * (a + b)};
        }
        #endregion

        #region Properties
        public override int Rank
        {
            get { return 3; }
        }
        #endregion

        #region Methods
        public override void FEM(Vector V, Vertex p)
        {
            double[] e = new double[] {Length/6, Length/6, 2*Length/3};
            for (int i = 0; i < 3; i++)
            {
                V[2*this[i].Number] += p.X*e[i];
                V[2*this[i].Number + 1] += p.Y*e[i];
            }
        }
        public override double phi(int i, double x, double y)
        {
            Vertex v = new Vertex(x, y);
            if (!hasVertex(v)) return 0;
            if (i != 0 && i != 1 && i != 2) return 0;
            LinearEdge le = new LinearEdge(A,B);
            double phii = le.phi(0, x, y);
            double phij = le.phi(1, x, y);
            switch (i)
            {
                case 0: return phii*(2*phii - 1);
                case 1: return phij*(2*phij - 1);
                case 2: return 4*phii*phij;
                default: return 0;
            }
        }
        #endregion
    }
}