using System;
using System.Collections.Generic;
using System.Text;
using SbB.Algebra;

namespace SbB.Geometry
{
    class QuadraticTriangle: Triangle
    {
        private Vertex a_, b_, c_;
        public QuadraticTriangle(Vertex i, Vertex j, Vertex m, int number) : base(i, j, m, number)
        {
            a_ = 0.5*(b + c);
            b_ = 0.5 * (a + c);
            c_ = 0.5 * (b + a);
        }
        public QuadraticTriangle(Vertex i, Vertex j, Vertex m) : base(i, j, m)
        {
            a_ = 0.5 * (b + c);
            b_ = 0.5 * (a + c);
            c_ = 0.5 * (b + a);
        }
        public QuadraticTriangle(Triangle triangle) : base(triangle.A, triangle.B, triangle.C, triangle.Number)
        {
            a_ = 0.5 * (b + c);
            b_ = 0.5 * (a + c);
            c_ = 0.5 * (b + a);
        }

        public override int nodesCount()
        {
            return 6;
        }
        public override int rank()
        {
            return 6;
        }
        public override int[] indexes()
        {
            return new int[] { a.Number, b.Number, c.Number, a_.Number, b_.Number, c_.Number };
        }
        public override Vertex this[int i]
        {
            get
            {
                if (0<=i && i<=2) return base[i];
                switch(i)
                {
                    case 3:
                        return a_;
                    case 4:
                        return b_;
                    case 5:
                        return c_;
                    default: return null;
                }
            }
            set
            {
                if (0 <= i && i <= 2) base[i] = value;
                switch (i)
                {
                    case 3:
                        a_=value;
                        break;
                    case 4:
                         b_ = value;
                         break;
                    case 5:
                        c_ = value;
                        break;
                    default: break;
                }
            }
        }
        public override double Ni(int index, Vertex v)
        {
            LinearTriangle lt = new LinearTriangle(this);
            double phii = lt.Ni(0, v);
            double phij = lt.Ni(1, v);
            double phim = lt.Ni(2, v);
            switch (index)
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
        public override double Ni(int index, double x, double y)
        {
            return Ni(index, new Vertex(x, y));
        }
        private Vector[] dNL(double L1, double L2)
        {
            Vector[] rez = new Vector[] {new Vector(6), new Vector(6, 7)};

            rez[0][0] = 4*L1 - 1;
            rez[0][1] = 0;
            rez[0][2] = 4*(L1 + L2) - 3;
            rez[0][3] = -4*L2;
            rez[0][4] = 4*(1 - 2*L1 - L2);
            rez[0][5] = 4*L2;

            rez[1][0] = 0;
            rez[1][1] = 4*L2 - 1;
            rez[1][2] = 4*(L1 + L2) - 3;
            rez[1][3] = 4*(1 - L1 - 2*L2);
            rez[1][4] = -4*L1;
            rez[1][5] = 4*L1;

            return rez;
        }
        private Vector[] dN(double[] b, double[] c, double L1, double L2)
        {
            Vector[] rez = new Vector[2];
            Vector[] Nl = dNL(L1, L2);
            rez[0] = b[0] * Nl[0] + b[1] * Nl[1];
            rez[1] = c[0] * Nl[0] + c[1] * Nl[1];
            return rez;
        }

        public override double dNxi(int index, double x, double y)
        {
            double S1 = (new Triangle(new Vertex(x, y), Point(1), Point(2))).S;
            double S2 = (new Triangle(new Vertex(x, y), Point(2), Point(0))).S;
            double L1 = S1/S;
            double L2 = S2/S;
            
            double detJ = 2 * this.S;

            double[] b = new double[] { (this[1].Y - this[2].Y) / detJ, -(this[0].Y - this[2].Y) / detJ };
            double[] c = new double[] { -(this[1].X - this[2].X) / detJ, (this[0].X - this[2].X) / detJ };
            Vector[] dNxy = dN(b, c, L1, L2);
            return dNxy[0][index];
        }
        public override double dNyi(int index, double x, double y)
        {
            double S1 = (new Triangle(new Vertex(x, y), Point(1), Point(2))).S;
            double S2 = (new Triangle(new Vertex(x, y), Point(2), Point(0))).S;
            double L1 = S1 / S;
            double L2 = S2 / S;

            double detJ = 2 * this.S;

            double[] b = new double[] { (this[1].Y - this[2].Y) / detJ, -(this[0].Y - this[2].Y) / detJ };
            double[] c = new double[] { -(this[1].X - this[2].X) / detJ, (this[0].X - this[2].X) / detJ };
            Vector[] dNxy = dN(b, c, L1, L2);
            return dNxy[1][index];
        }
    }
}
