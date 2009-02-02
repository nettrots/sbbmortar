using System;

namespace SbB.Geometry
{
    public class Edge
    {
        protected Vertex a;
        protected Vertex b;

        public Edge(Vertex a, Vertex b)
        {
            this.a = a;
            this.b = b;
        }


        public Vertex A
        {
            get { return a; }
            set { a = value; }
        }
        public Vertex B
        {
            get { return b; }
            set { b = value; }
        }
        public double Length
        {
            get { return (a - b).Length; }
        }
        public virtual Vertex this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return a;
                    case 1: return b;
                    default: return null;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: a = value;
                        break;
                    case 1: b = value;
                        break;
                    default: 
                        break;
                }
            }
        }

        public static bool operator ==(Edge eLeft, Edge eRight)
        {
            if ((object)eLeft == null || (object)eRight == null) return (object)eLeft == (object)eRight;
            return ((eLeft.A == eRight.A) && (eLeft.B == eRight.B)) ||
                   ((eLeft.A == eRight.B) && (eLeft.B == eRight.A));
        }
        public static bool operator !=(Edge eLeft, Edge eRight)
        {
            return !(eLeft == eRight);
        }
        public static Edge operator &(Edge eLeft, Edge eRight)
        {
//            if (eLeft.classify(eRight.a) == VertexPos.LEFT ||
//                eLeft.classify(eRight.a) == VertexPos.RIGHT ||
//                eLeft.classify(eRight.b) == VertexPos.BEHIND ||
//                eLeft.classify(eRight.b) == VertexPos.BEYOND)
//                return null;
//            if (eRight.classify(eLeft.a) == VertexPos.LEFT ||
//               eRight.classify(eLeft.a) == VertexPos.RIGHT ||
//               eRight.classify(eLeft.b) == VertexPos.BEHIND ||
//               eRight.classify(eLeft.b) == VertexPos.BEYOND)
//                return null;
//            Vertex[] varray = new Vertex[] { eRight.a, eRight.b, eLeft.a, eLeft.b };
//            Array.Sort(varray);
//            if (varray[1] == varray[2]) return null;
//            return new Edge(varray[1], varray[2]);
            if (eLeft == eRight) return eLeft;

            if (eLeft.classify(eRight.a) == VertexPos.BETWEEN && eLeft.classify(eRight.b) == VertexPos.BETWEEN)
                return eRight;
            if (eRight.classify(eLeft.a) == VertexPos.BETWEEN && eRight.classify(eLeft.b) == VertexPos.BETWEEN)
                return eLeft;
            if(eRight.classify(eLeft.a)==VertexPos.BETWEEN&&eLeft.classify(eRight.b)==VertexPos.BETWEEN)
                return new Edge(eLeft.a, eRight.b);
            if (eRight.classify(eLeft.b) == VertexPos.BETWEEN && eLeft.classify(eRight.a) == VertexPos.BETWEEN)
                return new Edge(eRight.a, eLeft.b);

            if (eRight.classify(eLeft.a) == VertexPos.BETWEEN && eLeft.b==eRight.b)
                return eLeft;
            if (eRight.classify(eLeft.b) == VertexPos.BETWEEN && eLeft.a == eRight.a)
                return eLeft;

            if (eLeft.classify(eRight.a) == VertexPos.BETWEEN && eLeft.b == eRight.b)
                return eRight;
            if (eLeft.classify(eRight.b) == VertexPos.BETWEEN && eLeft.a == eRight.a)
                return eRight;
            return null;
        }
        public static Edge operator |(Edge eLeft, Edge eRight)
        {
            if (eLeft.classify(eRight.a) == VertexPos.LEFT ||
                eLeft.classify(eRight.a) == VertexPos.RIGHT ||
                eLeft.classify(eRight.b) == VertexPos.LEFT ||
                eLeft.classify(eRight.b) == VertexPos.RIGHT)
                return null;
            Vertex[] varray = new Vertex[] { eRight.a, eRight.b, eLeft.a, eLeft.b };
            Array.Sort(varray);
            if (varray[0] == varray[3]) return null;
            return new Edge(varray[0], varray[3]);
        }

        public VertexPos classify(Vertex p)
        {
            return p.classify(a, b);
        }
        public bool hasVertex(Vertex p)
        {
            if (classify(p) == VertexPos.ORIGIN ||
                classify(p) == VertexPos.BETWEEN ||
                classify(p) == VertexPos.DESTINATION) return true;
            return false;
        }
        public virtual int nodesCount()
        {
            return 2;
        }
        public virtual int rank()
        {
            return 0;
        }
        public virtual int[] indexes()
        {
            return new int[0];
        }
        public virtual double Ni(int i, Vertex v)
        {
            return 0;
        }

        public override string ToString()
        {
            string result = string.Format("A({0}; {1})", a.X, a.Y);
            result = string.Format("{0}\tB({1}; {2})", result, b.X, b.Y);
            return "{ " + result + " }";
        }
        public override bool Equals(object obj)
        {
            return this == (Edge)obj;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
