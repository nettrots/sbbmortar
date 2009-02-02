using SbB.Algebra;

namespace SbB.Geometry
{
    public enum BoundaryType
    {
        CINEMATIC,
        STATIC,
        MORTAR,
        NONMORTAR
    }

    public abstract class BoundaryClass
    {
        public static BoundaryClass[] initArray(BoundaryClass[] bcArray)
        {
            for (int i = 0; i < bcArray.Length; i++)
                bcArray[i] = new CinematicBoundary();
            return bcArray;
        }
        public static BoundaryClass[] initArray(BoundaryClass[] bcArray, int index)
        {
            for (int i = index; i < bcArray.Length; i++)
                bcArray[i] = new CinematicBoundary();
            return bcArray;
        }
        public static BoundaryClass[] initArray(BoundaryClass[] bcArray, int index, int length)
        {
            for (int i = index; (i < bcArray.Length && i-index < length); i++) 
                bcArray[i] = new CinematicBoundary();
            return bcArray;
        }
        public abstract BoundaryType type();
    }

    public class CinematicBoundary: BoundaryClass
    {
        public override BoundaryType type()
        {
            return BoundaryType.CINEMATIC;
        }
    }

    public class StaticBoundary: BoundaryClass
    {
        private Vector p = new Vector(2);

        public StaticBoundary() {}
        public StaticBoundary(double px, double py)
        {
            p[0] = px;
            p[1] = py;
        }
        public StaticBoundary(Vector p): this(p[0], p[1]) {}

        public Vector P
        {
            get { return p; }
            set { p = value; }
        }

        public override BoundaryType type()
        {
            return BoundaryType.STATIC;
        }
    }

    public class MortarBoundary : BoundaryClass
    {
        private int n;
        public MortarBoundary(int n)
        {
            this.n = n;
        }

        public int N
        {
            get { return n; }
        }

        public override BoundaryType type()
        {
            return BoundaryType.MORTAR;
        }
    }

    public class NonMortarBoundary : BoundaryClass
    {
        private int n;
        public NonMortarBoundary(int n)
        {
            this.n = n;
        }

        public int N
        {
            get { return n; }
        }
        public override BoundaryType type()
        {
            return BoundaryType.NONMORTAR;
        }
    }
}
