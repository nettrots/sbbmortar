using System;
using System.Collections;

namespace SbB.Geometry
{
    public class Polygon
    {
        private ArrayList vertexArray = new ArrayList();

        public Polygon() { }
        public Polygon(Vertex[] points)
        {
            vertexArray.AddRange(points);
        }

        public int N
        {
            get { return vertexArray.Count; }
        }
        public Vertex this[int index]
        {
            get 
            {
                if (N == 0) return null;
                while (index >= N) index -= N;
                return (Vertex)vertexArray[index]; 
            }
            set 
            {
                if (N == 0) return;
                while (index >= N) index -= N;
                vertexArray[index] = value; 
            }
        }

        public Edge edge(int i)
        {
            if (N < 2) return null;
            return new Edge(this[i], this[i + 1]);
        }
        public void addVertex(Vertex vertex)
        {
            if (!vertexArray.Contains(vertex))
                vertexArray.Add(vertex);
        }
        private int insertVertex(Vertex v)
        {
            if (vertexArray.Contains(v)) return vertexArray.IndexOf(v);
            for (int i=0; i<N; i++)
                if (edge(i).classify(v) == VertexPos.BETWEEN)
                {
                    vertexArray.Insert(i+1, v);
                    return i+1;
                }
            return -1;
        }
        public bool isVertexInPolygon(Vertex v)
        {
            if (isVertexOnPolygon(v)) return false;
            bool result;
            int i=0;
            result = false;
            do
            {
                if (!((v.Y > this[i].Y) ^ (v.Y <= this[i+1].Y)))
                    if (v.X - this[i].X < (v.Y - this[i].Y) * (this[i+1].X - this[i].X) / (this[i+1].Y - this[i].Y)) 
                        result = !result;
                i++;
            }
            while(i<=N);
            return result;
        }
        public bool isVertexOnPolygon(Vertex v)
        {
            if (vertexArray.Contains(v)) return true;
            for (int i = 0; i < N; i++)
                if (edge(i).classify(v) == VertexPos.BETWEEN)
                    return true;
            return false;
        }
        public bool hasVertex(Vertex v)
        {
            return isVertexInPolygon(v) || isVertexOnPolygon(v);
        }
        public int isEdgeOnPolygon(Edge e)
        {
            for (int i = 0; i < N; i++)
                if (null != (Object)(edge(i) & e)) return i;
            return -1;
        }
        public bool hasEdge(Edge e)
        {
            return isVertexOnPolygon(e.A) && isVertexOnPolygon(e.B);
        }
        public Polygon[] divideByEdge(Edge e)
        {
            Polygon p1 = new Polygon(), p2 = new Polygon();
            if (!hasEdge(e)) return new Polygon[] { p1, p2 };
            Vertex[] vs = new Vertex[N];
            vertexArray.CopyTo(vs);
            insertVertex(e.A);
            int ind = insertVertex(e.B);
            do p1.addVertex(this[ind]); while (this[ind++] != e.A);
            ind--;
            do p2.addVertex(this[ind]); while (this[ind++] != e.B);
            vertexArray = new ArrayList(vs);
            return new Polygon[] { p1, p2 };
        }

        public override string ToString()
        {
            String s = "";
            for (int i=0; i<N; i++)
                s += (Vertex)vertexArray[i] + "\n";
            return s;
        }
    }
}
