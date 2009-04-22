using System;
using System.Collections.Generic;
using System.Text;

namespace SbBMortar.SbB
{
    public class LinearQuadrangle: Quadrangle
    {
        #region Fields
        private const int gaussOrger = 2;
        private static double[] gaussNodes;
        private static double[] gaussWeight;
        private Matrix isoCoord = new Matrix(new double[4][]
                                                 {
                                                     new double[]{-1.0, -1.0},
                                                     new double[]{1.0, -1.0},
                                                     new double[]{1.0, 1.0},
                                                     new double[]{-1.0, 1.0}
                                                 });

        private Matrix A;
        #endregion

        #region Constructors
        public LinearQuadrangle(Vertex a, Vertex b, Vertex c, Vertex d) : this(a, b, c, d, 0) {}
        public LinearQuadrangle(Vertex a, Vertex b, Vertex c, Vertex d, int number)
        {
            this.number = number;
            nodes = new Vertex[] {a,b,c,d};
        }
        public LinearQuadrangle(Vertex[] vertexes) : this(vertexes, 0) {}
        public LinearQuadrangle(Vertex[] vertexes, int number)
        {
            if (vertexes.Length>4) throw new Exception("Too much elements in 'vertexes'");
            if (vertexes.Length<4) throw new Exception("Not enought elements in 'vertexes'");
            nodes = new Vertex[] {vertexes[0], vertexes[1], vertexes[2], vertexes[3]};
            this.number = number;
        }

        // Статичний конструктор для обрахунку точок і ваг Гауса
        static LinearQuadrangle()
        {
            gaussNodes = new double[gaussOrger];
            gaussWeight = new double[gaussOrger];

            for(int i = 0; i <= (gaussOrger+1)/2-1; i++)
            {
                double r = Math.Cos(Math.PI*(4*i+3)/(4*gaussOrger+2));
                double dp3, r1;
                do
                {
                    double p2 = 0, p3 = 1;
                    for(int j = 0; j <= gaussOrger-1; j++)
                    {
                        double p1 = p2;
                        p2 = p3;
                        p3 = ((2*j+1)*r*p2-j*p1)/(j+1);
                    }
                    dp3 = gaussOrger*(r*p3-p2)/(r*r-1);
                    r1 = r;
                    r = r-p3/dp3;
                } while(Math.Abs(r-r1)>=Constants.EPS*(1+Math.Abs(r))*100);

                gaussNodes[i] = r;
                gaussNodes[gaussOrger-1-i] = -r;
                gaussWeight[i] = 2.0/((1-r*r)*dp3*dp3);
                gaussWeight[gaussOrger-1-i] = 2.0/((1-r*r)*dp3*dp3);
            }
        }
        #endregion

        #region Methods
        private Vector dN(int i, double ksi, double eta)
        {
            Vector dN = new Vector(2);
            dN[0] = isoCoord[i][0]*(1 + isoCoord[i][1]*eta)/4.0;
            dN[1] = isoCoord[i][1]*(1 + isoCoord[i][0]*ksi)/4.0;
            return dN;
        }
        private void inverce()
        {
            double x0 = this[0].X, y0 = this[0].Y;
            double x1 = this[1].X, y1 = this[1].Y;
            double x2 = this[2].X, y2 = this[2].Y;
            double x3 = this[3].X, y3 = this[3].Y;
            double detA = x1*y2*x3*y3 - x1*x2*y2*y3 - x2*y1*x3*y3 + x2*x1*y1*y3 + x3*y1*x2*y2 - x3*x1*y1*y2 -
                          x0*y2*x3*y3 + x0*x2*y2*y3 + x2*y0*x3*y3 - x2*x0*y0*y3 - x3*y0*x2*y2 + x3*x0*y0*y2 +
                          x0*y1*x3*y3 - x0*x1*y1*y3 - x1*y0*x3*y3 + x1*x0*y0*y3 + x3*y0*x1*y1 - x3*x0*y0*y1 -
                          x0*y1*x2*y2 + x0*x1*y1*y2 + x1*y0*x2*y2 - x1*x0*y0*y2 - x2*y0*x1*y1 + x2*x0*y0*y1;
            A = new Matrix(4,4);

            A[0][0] = x1*y2*x3*y3 - x1*x2*y2*y3 - x2*y1*x3*y3 + x2*x1*y1*y3 + x3*y1*x2*y2 - x3*x1*y1*y2;
            A[0][1] = -x0*y2*x3*y3 + x0*x2*y2*y3 + x2*y0*x3*y3 - x2*x0*y0*y3 - x3*y0*x2*y2 + x3*x0*y0*y2;
            A[0][2] = x0*y1*x3*y3 - x0*x1*y1*y3 - x1*y0*x3*y3 + x1*x0*y0*y3 + x3*y0*x1*y1 - x3*x0*y0*y1;
            A[0][3] = -x0*y1*x2*y2 + x0*x1*y1*y2 + x1*y0*x2*y2 - x1*x0*y0*y2 - x2*y0*x1*y1 + x2*x0*y0*y1;

            A[1][0] = -y2*x3*y3 + x2*y2*y3 + y1*x3*y3 - x1*y1*y3 - y1*x2*y2 + x1*y1*y2;
            A[1][1] = y2*x3*y3 - x2*y2*y3 - y0*x3*y3 + x0*y0*y3 + y0*x2*y2 - x0*y0*y2;
            A[1][2] = -y1*x3*y3 + x1*y1*y3 + y0*x3*y3 - x0*y0*y3 - y0*x1*y1 + x0*y0*y1;
            A[1][3] = y1*x2*y2 - x1*y1*y2 - y0*x2*y2 + x0*y0*y2 + y0*x1*y1 - x0*y0*y1;

            A[2][0] = x2*x3*y3 - x2*y2*x3 - x1*x3*y3 + x1*y1*x3 + x1*x2*y2 - x1*y1*x2;
            A[2][1] = -x2*x3*y3 + x2*y2*x3 + x0*x3*y3 - x0*y0*x3 - x0*x2*y2 + x0*y0*x2;
            A[2][2] = x1*x3*y3 - x1*y1*x3 - x0*x3*y3 + x0*y0*x3 + x0*x1*y1 - x0*y0*x1;
            A[2][3] = -x1*x2*y2 + x1*y1*x2 + x0*x2*y2 - x0*y0*x2 - x0*x1*y1 + x0*y0*x1;

            A[3][0] = -x2*y3 + y2*x3 + x1*y3 - y1*x3 - x1*y2 + y1*x2;
            A[3][1] = x2*y3 - y2*x3 - x0*y3 + y0*x3 + x0*y2 - y0*x2;
            A[3][2] = -x1*y3 + y1*x3 + x0*y3 - y0*x3 - x0*y1 + y0*x1;
            A[3][3] = x1*y2 - y1*x2 - x0*y2 + y0*x2 + x0*y1 - y0*x1;

            A /= detA;
        }

        public override void FEM(Matrix K, Matrix D)
        {
            Matrix local = new Matrix(8,8);
            for (int i = 0; i < gaussOrger; i++)
                for (int j = 0; j < gaussOrger; j++)
                {
                    double ksi = gaussNodes[i];
                    double eta = gaussNodes[j];
                    double ksiH = gaussWeight[i];
                    double etaH = gaussWeight[j];

                    Matrix A1 = new Matrix(2,4);
                    Matrix A2 = new Matrix(4,2);
                    for (int k = 0; k < 4; k++)
                    {
                        Vector temp = dN(k, ksi, eta);
                        A1[0][k] = temp[0];
                        A1[1][k] = temp[1];
                        A2[k][0] = this[k].X;
                        A2[k][1] = this[k].Y;
                    }

                    Matrix J = A1*A2;
                    double detJ = J[0][0]*J[1][1] - J[1][0]*J[0][1];
                    Matrix J_1 = new Matrix(new double[2][]{new double[]{J[1][1], -J[0][1]},
                                                            new double[]{-J[1][0], J[0][0]}});
                    J_1 /= detJ;
                    Matrix B = new Matrix(3,8);
                    for (int k = 0; k < 4; k++)
                    {
                        Vector temp = J_1*A1.Column(k);
                        B[0][k] = B[2][4 + k] = temp[0];
                        B[1][4 + k] = B[2][k] = temp[1];
                    }
                    local += (B.Transposed()*D*B)*detJ*ksiH*etaH;
                }

            for (int k = 0; k < 2; k++)
                for (int l = 0; l < 2; l++)
                    for (int i = 0; i < 4; i++)
                        for (int j = 0; j < 4; j++)
                            K[2*this[i].Number + k][2*this[j].Number + l] += local[4*k + i][4*l + j];

        }

        public override double phi(int i, Vertex v)
        {
            if (A == null) inverce();

            Vector alpha = A.Column(i);
            return alpha[0] + alpha[1]*v.X + alpha[2]*v.Y + alpha[3]*v.X*v.Y;
        }

        public override double[] dphi(int i, Vertex v)
        {
            if (A == null) inverce();

            Vector alpha = A.Column(i);
            double[] dphixy = new double[2];
            dphixy[0] = alpha[1] + alpha[3]*v.Y;
            dphixy[1] = alpha[2] + alpha[3]*v.X;
            return dphixy;
        }
        #endregion
    }
}
