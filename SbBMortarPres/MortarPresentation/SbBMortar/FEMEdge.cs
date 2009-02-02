namespace SbBMortar.SbB
{
    public abstract class FEMEdge: Edge
    {
        #region Constructors
        public FEMEdge(Vertex a, Vertex b): base(a,b) { }
        protected FEMEdge(){}
        #endregion

        #region Properties
        public abstract int Rank
        { get; }
        #endregion

        #region Methods
        public abstract void FEM(Vector V, Vertex p);
        public abstract double phi(int i, double x, double y);
        public void mortar(Mortar mortarvisitor)
        {
            for (int i = 0; i < Rank; i++)
                mortarvisitor.visitFunction(i,nodes[i].Number, phi);
        }
        #endregion
    }
}