using System;
using System.Collections;

namespace SbBMortar.SbB
{
    public class Matrix
    {
        private ArrayList vectors = new ArrayList();

        public Vector this[int index]
        {
            get { return (Vector)vectors[index]; }
            set
            {
                if (((Vector)vectors[index]).Length != value.Length)
                    throw new Exception("Can't add change this row");
                vectors[index] = value;
            }
        }
        public PairInt Size
        {
            get 
            {
                if (vectors.Count == 0)
                    return new PairInt(0, 0);
                return new PairInt(vectors.Count, ((Vector)vectors[0]).Length);
            }
            set 
            {
                if (value.m < 0 || value.n < 0)
                    throw new ArgumentOutOfRangeException();
                for (int i = 0; i < vectors.Count; i++)
                    ((Vector)vectors[i]).Length = value.n;
                for (int i = vectors.Count; i <= value.m; i++)
                    vectors.Add(new Vector(value.n));
                for (int i = vectors.Count; i > value.m; i--)
                    vectors.RemoveAt(i - 1);
            }
        }

        public Matrix() {}
        public Matrix(int m, int n): this(m, n, 0) {}
        public Matrix(int m, int n, double element)
        {
            for (int i = 0; i < m; i++)
                vectors.Add(new Vector(n, element));
        }
        public Matrix(PairInt dim): this(dim, 0) {}
        public Matrix(PairInt dim, double element): this(dim.m, dim.n, element) {}
        public Matrix(Vector[] array)
        {
            int m = array.Length;
            if (m == 0) return;
            int n = array[0].Length;
            for (int i = 0; i < m; i++)
                if (array[i].Length != n)
                    throw new Exception("Can't create this matrix");
                else vectors.Add(array[i]);
        }
        public Matrix(Vector vector): this(new Vector[] { vector }) {}
        public Matrix(double[][] elements)
        {
            int m = elements.Length;
            if (m == 0) return;
            int n = elements[0].Length;
            for (int i = 0; i < m; i++)
                if (elements[i].Length != n)
                    throw new Exception("Can't create this matrix");
                else vectors.Add(new Vector(elements[i]));
        }

        public static Matrix operator +(Matrix mLeft, Matrix mRight)
        {
            if ((mLeft.Size.m != mRight.Size.m) || (mLeft.Size.n != mRight.Size.n))
                throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(mLeft.Size);
            for (int i = 0; i < mLeft.vectors.Count; i++)
                m[i] = mLeft[i] + mRight[i];
            return m;
        }
        public static Matrix operator -(Matrix mLeft, Matrix mRight)
        {
            if ((mLeft.Size.m != mRight.Size.m) || (mLeft.Size.n != mRight.Size.n))
                throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(mLeft.Size);
            for (int i = 0; i < mLeft.vectors.Count; i++)
                m[i] = mLeft[i] - mRight[i];
            return m;
        }
        public static Matrix operator *(Matrix mLeft, Matrix mRight)
        {
            if (mLeft.Size.n != mRight.Size.m)
                throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(new PairInt(mLeft.Size.m, mRight.Size.n));
            for (int i = 0; i < m.Size.m; i++)
                for (int j = 0; j < m.Size.n; j++)
                    m[i][j] = mLeft[i] * mRight.column(j);
            return m;
        }
        public static Vector operator *(Vector vLeft, Matrix mRight)
        {
            if (vLeft.Length != mRight.Size.m)
                throw new ArgumentOutOfRangeException();
            Vector v = new Vector(mRight.Size.n);
            for (int i = 0; i < v.Length; i++)
                v[i] = vLeft * mRight.column(i);
            return v;
        }
        public static Vector operator *(Matrix mRight, Vector vLeft)
        {
            if (vLeft.Length != mRight.Size.n)
                throw new ArgumentOutOfRangeException();
            Vector v = new Vector(mRight.Size.m);
            for (int i = 0; i < v.Length; i++)
                v[i] = vLeft * mRight[i];
            return v;
        }
        public static Matrix operator *(double k, Matrix mRight)
        {
            Vector[] array = new Vector[mRight.vectors.Count];
            for (int i = 0; i < mRight.vectors.Count; i++)
                array[i] = k * ((Vector)mRight.vectors[i]);
            return new Matrix(array);
        }
        public static Matrix operator *(Matrix mRight, double k)
        {
            Vector[] array = new Vector[mRight.vectors.Count];
            for (int i = 0; i < mRight.vectors.Count; i++)
                array[i] = k * ((Vector)mRight.vectors[i]);
            return new Matrix(array);
        }
        public static Matrix operator /(Matrix mRight, double k)
        {
            Vector[] array = new Vector[mRight.vectors.Count];
            for (int i = 0; i < mRight.vectors.Count; i++)
                array[i] = ((Vector)mRight.vectors[i]) / k;
            return new Matrix(array);
        }

        public Matrix transposed()
        {
            Matrix m = new Matrix(this.Size.n, this.Size.m);
            for (int i = 0; i < this.Size.n; i++)
                m[i] = this.column(i);
            return m;
        }
        public Vector column(int index)
        {
            Vector v = new Vector(vectors.Count);
            for (int i = 0; i < v.Length; i++)
                v[i] = ((Vector)vectors[i])[index];
            return v;
        }
        public void clear()
        {
            for (int i = 0; i < vectors.Count; i++)
                ((Vector)vectors[i]).clear();
        }
        public void erase()
        {
            for (int i = 0; i < vectors.Count; i++)
                ((Vector)vectors[i]).erase();
            vectors.Clear();
        }
        public void removeColumn(int index)
        {
            if (index < 0 || index > Size.n) throw new ArgumentOutOfRangeException();
            for(int i=0; i<Size.m; i++)
                ((Vector)vectors[i]).removeAt(index);
        }
        public void removeRow(int index)
        {
            if (index < 0 || index > Size.m) throw new ArgumentOutOfRangeException();
            vectors.RemoveAt(index);
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < vectors.Count; i++)
                str += ((Vector)vectors[i]).ToString() + "\n";
            return str;
        }
    }
}
