//Константи і перелічення алгебри

namespace SbB.Algebra
{
    public enum Norma
    {
        Linear = 1,
        Euclidean,
        Maximum
    }

    public struct PairInt
    {
        public int m;
        public int n;

        public PairInt(int m, int n)
        {
            this.m = m;
            this.n = n;
        }

        public override string ToString()
        {
            return string.Format("[ {0} x {1} ]", m, n);
        }
    }
}