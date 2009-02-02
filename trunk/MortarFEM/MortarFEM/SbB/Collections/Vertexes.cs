using System.Collections;
using SbB.Geometry;

namespace SbB.Collections
{
    // Колекція вершин
    public class Vertexes: IEnumerable, IEnumerator
    {
        private ArrayList vertexArray = new ArrayList();
        private int pos = -1;

        public Vertex this[int index]
        {
            get { return (Vertex)vertexArray[index]; }
            set { vertexArray[index] = value; }
        }
        public int Count
        {
            get { return vertexArray.Count; }
        }

        public Vertexes() {}

        public void add(Vertex vertex)
        {
            vertexArray.Add(vertex);
        }
        public void remove(Vertex vertex)
        {
            vertexArray.Remove(vertex);
        }
        public void sort()
        {
            vertexArray.Sort();
            for (int i = 0; i < vertexArray.Count; i++)
                ((Vertex)vertexArray[i]).Number = i;
        }
        public bool contains(Vertex vertex)
        {
            return vertexArray.Contains(vertex);
        }
        public int indexOf(Vertex vertex)
        {
            return vertexArray.IndexOf(vertex);
        }

        #region IEnumerator

        public bool MoveNext()
        {
            if (pos < vertexArray.Count - 1)
            {
                pos++;
                return true;
            }
            else
            {
                pos = -1;
                return false;
            }
        }
        public void Reset()
        {
            pos = -1;
        }
        public object Current
        {
            get { return vertexArray[pos]; }
        }

        #endregion

        #region IEnumerable

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion
    }
}
