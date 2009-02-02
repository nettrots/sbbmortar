using System.Collections;
using SbB.Geometry;

namespace SbB.Collections
{
    // Колекція трикутників
    public class Triangles: IEnumerable, IEnumerator
    {
        private ArrayList triangleArray = new ArrayList();
        private int pos = -1;

        public Triangle this[int index]
        {
            get { return (Triangle)triangleArray[index]; }
            set { triangleArray[index] = value; }
        }
        public int Count
        {
            get { return triangleArray.Count; }
        }

        public Triangles() {}

        public void Add(Triangle triangle)
        {
            triangleArray.Add(triangle);
        }
        public void Remove(Triangle triangle)
        {
            triangleArray.Remove(triangle);
        }
        public void Sort()
        {
            triangleArray.Sort();
            for (int i=0; i<triangleArray.Count; i++)
                ((Triangle)triangleArray[i]).Number = i;
        }

        #region IEnumerator

        public bool MoveNext()
        {
            if (pos < triangleArray.Count - 1)
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
            get { return triangleArray[pos]; }
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
