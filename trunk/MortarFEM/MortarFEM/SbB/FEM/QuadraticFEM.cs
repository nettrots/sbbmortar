using System;
using System.Collections.Generic;
using System.Text;
using SbB.Algebra;
using SbB.Geometry;

namespace SbB.FEM
{
    public class QuadraticFEM: FEM
    {
        // Задання L-координат вузлів Гаусса та вагових коефіцієнтів
        private double[][] Lk = new double[2][] { 
                new double[] {1.0/3, 1, 0, 0, 0, 0.5, 0.5},
                new double[] {1.0/3, 0, 1, 0, 0.5, 0, 0.5}};
        private double[] wk = new double[] {0.225, 0.025, 0.025, 0.025, 8.0/120, 8.0/120, 8.0/120};

        public QuadraticFEM(double youngModulus, double poissonRatio)
        {
            this.youngModulus = youngModulus;
            this.poissonRatio = poissonRatio;
        }

        private Matrix[] dNL()
        {
            Matrix[] rez = new Matrix[] {new Matrix(6,7), new Matrix(6,7)};
            for (int j = 0; j < 7; j++)
            {
                double L1 = Lk[0][j];
                double L2 = Lk[1][j];

                rez[0][0][j] = 4*L1 - 1;
                rez[0][1][j] = 0;
                rez[0][2][j] = 4*(L1 + L2) - 3;
                rez[0][3][j] = -4*L2;
                rez[0][4][j] = 4*(1 - 2*L1 - L2);
                rez[0][5][j] = 4*L2;

                rez[1][0][j] = 0;
                rez[1][1][j] = 4*L2 - 1;
                rez[1][2][j] = 4*(L1 + L2) - 3;
                rez[1][3][j] = 4*(1 - L1 - 2*L2);
                rez[1][4][j] = -4*L1;
                rez[1][5][j] = 4*L1;
            }
            return rez;
        }
        private Matrix[] dN(double[] b, double[] c)
        {
            Matrix[] rez = new Matrix[2];
            Matrix[] Nl = dNL();
            rez[0] = b[0]*Nl[0] + b[1]*Nl[1];
            rez[1] = c[0]*Nl[0] + c[1]*Nl[1];
            return rez;
        }
        public override Matrix bilinear(Triangle triangle)
        {
            Matrix local = new Matrix(12,12);
            double D1 = (1 - poissonRatio)*youngModulus/((1 + poissonRatio)*(1 - 2*poissonRatio));
            double D2 = D1*(1 - 2*poissonRatio)/(2 - 2*poissonRatio);
            double D3 = D1*poissonRatio/(1 - poissonRatio);

            double detJ = 2*triangle.S;

            double[] b = new double[] {(triangle[1].Y - triangle[2].Y)/detJ, -(triangle[0].Y - triangle[2].Y)/detJ};
            double[] c = new double[] {-(triangle[1].X - triangle[2].X)/detJ, (triangle[0].X - triangle[2].X)/detJ};

            Matrix[] N = dN(b, c);
            Matrix Nx = N[0];
            Matrix Ny = N[1];

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        local[i][j] += (D1*Nx[i][k]*Nx[j][k] + D2*Ny[i][k]*Ny[j][k])*detJ*wk[k];
                        local[i][6 + j] += (D3*Nx[i][k]*Ny[j][k] + D2*Ny[i][k]*Nx[j][k])*detJ*wk[k];
                        local[6 + i][6 + j] += (D1*Ny[i][k]*Ny[j][k] + D2*Nx[i][k]*Nx[j][k])*detJ*wk[k];
                    }
                    local[6 + j][i] = local[i][6 + j];
                }
            return local;
        }

        public override Matrix linear(SbB.Geometry.Edge edge)
        {
            Matrix F = new Matrix(6, 2);
            F[0][0] = F[1][1] = F[2][0] = F[3][1] = edge.Length/6;
            F[4][0] = F[5][1] = 2*edge.Length/3;
            return F;
        }
    }
}
