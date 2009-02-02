namespace SbB.Geometry
{
    public class InsideBoundary
    {
        private Edge edge;
        private SubDomain mortar;
        private SubDomain nonmortar;
        private int n;

        public InsideBoundary(int n, Edge edge, SubDomain mortar, SubDomain nonmortar)
        {
            this.n = n;
            this.edge = edge;
            this.mortar = mortar;
            this.nonmortar = nonmortar;
        }

        public Edge E
        {
            get { return edge; }
        }
        public SubDomain Mortar
        {
            get { return mortar; }
        }
        public SubDomain NonMortar
        {
            get { return nonmortar; }
        }
        public int N
        {
            get { return n; }
        }

        public void changeMortars()
        {
            SubDomain sd = mortar;
            mortar = nonmortar;
            nonmortar = sd;
        }
    }
}
