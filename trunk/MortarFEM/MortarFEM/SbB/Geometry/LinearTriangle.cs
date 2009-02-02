using System;

namespace SbB.Geometry
{
    public class LinearTriangle : Triangle, ICloneable
    {
        public LinearTriangle(Vertex i, Vertex j, Vertex m, int number)
            : base(i, j, m, number)
        {
        }
        public LinearTriangle(Vertex i, Vertex j, Vertex m)
            : base(i, j, m)
        {
        }
        public LinearTriangle(Triangle triangle) : base(triangle.A, triangle.B, triangle.C, triangle.Number) { }

        public override int[] indexes()
        {
            return new int[] { a.Number, b.Number, c.Number };
        }
        public override double Ni(int index, double x, double y)
        {
            return Ni(index, new Vertex(x, y));
        }
        public override double Ni(int index, Vertex v)
        {
            if (!hasVertex(v)) return 0;
            //double a = this[(index + 1)%3].X*this[(index + 2)%3].Y - this[(index + 2)%3].X*this[(index + 1)%3].Y;
            //double b = this[(index + 1)%3].Y - this[(index + 2)%3].Y;
            //double c = this[(index + 1)%3].X - this[(index + 2)%3].X;
            Triangle tr = new Triangle(v, this[(index + 1) % 3], this[(index + 2) % 3]);
            return tr.S / S;
        }
        public override double dNxi(int index, double x, double y)
        {
            return (Point((index + 1)%3).Y - Point((index + 2)%3).Y)/(2*S);
        }
        public override double dNyi(int index, double x, double y)
        {
            return (Point((index + 2)%3).X - Point((index + 1)%3).X)/(2*S);
        }

        #region ICloneable

        public override object Clone()
        {
            return new LinearTriangle(a, b, c, number);
        }

        #endregion
        #region IComparable

        public override int CompareTo(object o)
        {
            LinearTriangle temp = (LinearTriangle)o;
            Vertex[] leftArray = new Vertex[] { a, b, c };
            Vertex[] rightArray = new Vertex[] { temp.a, temp.b, temp.c };
            Array.Sort(leftArray);
            Array.Sort(rightArray);
            if (leftArray[0] > rightArray[0])
                return 1;
            if (leftArray[0] < rightArray[0])
                return -1;
            double leftY = leftArray[1].Y < leftArray[2].Y ? leftArray[1].Y : leftArray[2].Y;
            double rightY = rightArray[1].Y < rightArray[2].Y ? rightArray[1].Y : rightArray[2].Y;
            if (leftY > rightY)
                return 1;
            if (leftY < rightY)
                return -1;
            return 0;
        }

        #endregion

    }
}
