namespace SbBMortar.SbB
{
    public class QuadraticTriangle: Triangle
    {
        #region Fields
        private GaussPont gaussPoint = new GaussPont();
        #endregion

        #region Constructors
        public QuadraticTriangle(Vertex a, Vertex b, Vertex c)
        {
            nodes = new Vertex[] {a, b, c, 0.5*(b + c), 0.5*(a + c), 0.5*(a + b)};
        }
        #endregion

        #region Methods
        #region Private
        private Vector dNi(int i, double L1, double L2)
        {
            Vector dN = new Vector(2);
            switch(i)
            {
                case 0: dN[0] = 4*L1 - 1;
                        dN[1] = 0;
                        break;
                case 1: dN[0] = 0;
                        dN[1] = 4*L2 - 1;
                        break;
                case 2: dN[0] = 4*(L1 + L2) - 3;
                        dN[1] = 4*(L1 + L2) - 3;
                        break;
                case 3: dN[0] = -4*L2;
                        dN[1] = 4*(1 - L1 - 2*L2);
                        break;
                case 4: dN[0] = 4*(1 - 2*L1 - L2);
                        dN[1] = -4*L1;
                        break;
                case 5: dN[0] = 4*L2;
                        dN[1] = 4*L1;
                        break;
            }
            return dN;
        }
        #endregion

        #region Public
        public override void FEM(Matrix K, Matrix D)
        {
            Matrix local = new Matrix(12,12);
            double detJ = 2*S;
            Matrix J_1 = new Matrix(new double[][]{new double[]{nodes[1].Y-nodes[2].Y, nodes[2].Y-nodes[0].Y},
                                                   new double[]{nodes[2].X-nodes[1].X, nodes[0].X-nodes[2].X}});
            J_1 = J_1/detJ;
            for (int i = 0; i < gaussPoint.Count; i++)
            {
                Vector[] dN = new Vector[6];
                for (int j = 0; j < 6; j++)
                    dN[j] = J_1*dNi(j, gaussPoint.L1(i), gaussPoint.L2(i));
                Matrix B = new Matrix(3,12);
                for (int j = 0; j < 6; j++)
                {
                    B[0][j] = B[2][6 + j] = dN[j][0];
                    B[1][6 + j] = B[2][j] = dN[j][1];
                }
                local += (B.transposed()*D*B)*detJ*gaussPoint.w(i);
            }

            for (int k = 0; k < 2; k++)
                for (int l = 0; l < 2; l++)
                    for (int i = 0; i < 6; i++)
                        for (int j = 0; j < 6; j++)
                            K[2*this[i].Number + k][2*this[j].Number + l] += local[3*k + i][3*l + j];
        }
        public override double phi(int i, Vertex v)
        {
            Triangle tr = new LinearTriangle(nodes[0], nodes[1], nodes[2]);
            double phii = tr.phi(0, v);
            double phij = tr.phi(1, v);
            double phim = tr.phi(2, v);
            switch (i)
            {
                case 0:
                    return phii*(2*phii - 1);
                case 1:
                    return phij*(2*phij - 1);
                case 2:
                    return phim*(2*phim - 1);
                case 3:
                    return 4*phij*phim;
                case 4:
                    return 4*phii*phim;
                case 5:
                    return 4*phij*phii;
                default:
                    return 0;
            }
        }
        public override double[] dphi(int i, Vertex v)
        {
            double S1 = (new LinearTriangle(v, this[1], this[2])).S;
            double S2 = (new LinearTriangle(v, this[2], this[0])).S;
            double L1 = S1/S;
            double L2 = S2/S;

            double detJ = 2*S;
            Matrix J_1 = new Matrix(new double[][]{new double[]{nodes[1].Y-nodes[2].Y, nodes[2].Y-nodes[0].Y},
                                                   new double[]{nodes[2].X-nodes[1].X, nodes[0].X-nodes[2].X}});
            J_1 = J_1 / detJ;

            return (J_1*dNi(i, L1, L2)).ToArray();
        }
        #endregion
        #endregion
    }
}