using SbB.Algebra;
using SbB.Geometry;

namespace SbB.FEM
{
    public class LinearFEM: FEM
    {
        public LinearFEM(double youngModulus, double poissonRatio)
        {
            this.youngModulus = youngModulus;
            this.poissonRatio = poissonRatio;
        }

        public override Matrix bilinear(Triangle triangle)
        {
            
            double d = (1 - poissonRatio)*youngModulus/((1 + poissonRatio)*(1 - 2*poissonRatio));
            Matrix D = new Matrix(3,3);
            D[0][0] = 1;
            D[0][1] = D[1][0] =  poissonRatio/(1-poissonRatio);
            D[1][1] = 1;
            D[2][2] = (1-2*poissonRatio)/(2*(1-poissonRatio));
            D = d*D;

            Matrix B = new Matrix(3,6);
            for (int i = 0; i < 3; i++)
            {
                B[0][i] = B[2][i + 3] = (triangle.Point((i + 1)%3).Y - triangle.Point((i + 2)%3).Y)/2;
                B[1][i + 3] = B[2][i] = (triangle.Point((i + 2)%3).X - triangle.Point((i + 1)%3).X)/2;
            }
            Matrix m = 1 / (triangle.S) * B.transposed() * D *B;
            return m;
        }

        public override Matrix linear(Edge edge)
        {
            Matrix F = new Matrix(4,2);
            F[0][0] = F[1][1] = F[2][0] = F[3][1] = edge.Length/2;
            return F;
        }
    }
}
