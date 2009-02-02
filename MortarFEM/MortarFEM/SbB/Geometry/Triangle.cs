using System;

namespace SbB.Geometry
{
    public class Triangle: ICloneable, IComparable
    {
        protected Vertex a, b, c;
        protected int number = 0;

        // Вершини трикутника
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
        public Vertex C
        {
            get { return c; }
            set { c = value; }
        }
        // Ребра трикутника
        public Edge Ei
        {
            get { return new Edge(c, b); }
        }
        public Edge Ej
        {
            get { return new Edge(a, c); }
        }
        public Edge Em
        {
            get { return new Edge(b, a); }
        }
        // Номер трикутника
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        // Площа
        public double S
        {
            get
            {
                return 0.5 * Math.Abs( (b.X * c.Y - c.X * b.Y) - (a.X * c.Y - c.X * a.Y) + (a.X * b.Y - b.X * a.Y) );
            }
        }

        public Triangle(Vertex i, Vertex j, Vertex m): this(i,j,m,0) {}
        public Triangle(Vertex i, Vertex j, Vertex m, int number)
        {
            this.a = i;
            this.b = j;
            this.c = m;
            this.Number = number;
            if ((new Edge(a,b)).classify(c)==VertexPos.RIGHT)
            {
                Vertex change = b;
                b = c;
                c = change;
            }
        }

        public Vertex Point(int index)
        {
            return index == 0 ? a
                : index == 1 ? b
                : index == 2 ? c
                : new Vertex();

        }
        public void Sort()
        {
            Vertex[] array = new Vertex[] { a, b, c };
            Array.Sort(array);
            a = array[0];
            b = array[1];
            c = array[2];
        }

        public override string ToString()
        {
            string[] strPoint = new string[3];
            for (int i = 0; i < 3; i++)
                strPoint[i] = string.Format("{0}({1}; {2})", (char)('A'+i), Point(i).X, Point(i).Y);
            string result =  string.Format("{0}\t{1}\t{2}", strPoint[0], strPoint[1], strPoint[2]);
            return "{ " + result + "\t n = " + number.ToString() + " }";
        }

        #region ICloneable

        public virtual object Clone()
        {
            return new Triangle(a, b, c, number);
        }

        #endregion

        #region IComparable

        public virtual int CompareTo(object o)
        {
            Triangle temp = (Triangle)o;
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


        //New
        public virtual Vertex this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return a;
                    case 1: return b;
                    case 2: return c;
                    default: return null;
                }
                
            }
            set
            {
                switch (i)
                {
                    case 0: a = value;
                        break;
                    case 1: b = value;
                        break;
                    case 2: c = value;
                        break;
                    default:
                        break;
                }
            }
        }
        public virtual int rank() {return 3; }
        public virtual int nodesCount() {return 3; }
        public virtual int[] indexes()
        {
            return new int[0];
        }
        public virtual double Ni(int index, double x, double y)
        {
            return 0;
        }
        public virtual double Ni(int index, Vertex v)
        {
            return 0;
        }
        public virtual double dNxi(int index, double x, double y)
        {
            return 0;
        }
        public virtual double dNyi(int index, double x, double y)
        {
            return 0;
        }
    
        public double[] strainStres(double x, double y, double youngModulus, double poissonRatio, double[] uv)
        {
            double[] result = new double[6];
            double[] Nx = new double[rank()];
            double[] Ny = new double[rank()];
            for (int i = 0; i < rank(); i++)
            {
                Nx[i] = dNxi(i, x, y);
                Ny[i] = dNyi(i, x, y);
            }

            for (int i = 0; i < rank(); i++)
            {
                result[0] += uv[2*i]*Nx[i];
                result[1] += uv[2*i + 1]*Ny[i];
                result[2] += uv[2*i]*Ny[i] + uv[2*i + 1]*Nx[i];
            }
            
            double D1 = (1 - poissonRatio)*youngModulus/((1 + poissonRatio)*(1 - 2*poissonRatio));
            double D2 = D1*(1 - 2*poissonRatio)/(2 - 2*poissonRatio);
            double D3 = D1*poissonRatio/(1 - poissonRatio);

            result[3] = D1*result[0] + D3*result[1];
            result[4] = D3*result[0] + D1*result[1];
            result[5] = D2*result[2];

            return result;
        }

        //Vertex and triangle
        public bool isVertexInTriangle(Vertex v)
        {
            if(isVertexOnTriangle(v)) return false;
            Triangle a1, a2, a3;
            a1=new Triangle(a,b,v);
            a2 = new Triangle(a, c, v);
            a3 = new Triangle(c, b, v);
            
           // Vertex[] tr = {a, b, c};
           // Array.Sort(tr);
           // bool c = false;
           // for(int i=0,j=2;i<3;j=i++)
           // {
           //     if ((((tr[i].Y<=v.Y)&&(v.Y<tr[j].Y))||((tr[i].Y<=v.Y)&&(v.Y<tr[j].Y)))&&
           //        (v.X>(tr[j].X-tr[i].X)*(v.Y-tr[i].Y)/(tr[j].Y-tr[i].Y+tr[i].X))) 
           //         c = !c;
           // }

            return (S==(a1.S+a2.S+a3.S));
        }
        public bool isVertexOnTriangle(Vertex v)
        {
            
            Edge e1=new Edge(a,v),e2=new Edge(v,b);
            if(e1.Length+e2.Length==Em.Length) return true;

            e1 = new Edge(c, v); e2 = new Edge(v, b);
            if (e1.Length + e2.Length == Ei.Length) return true;

            e1 = new Edge(a, v); e2 = new Edge(v, c);
            if (e1.Length + e2.Length == Ej.Length) return true;

            return false;
        }
        public bool hasVertex(Vertex v)
        {
            return isVertexInTriangle(v) || isVertexOnTriangle(v);
        }
       

    }
}