using System.Collections;
using SbB.Geometry;

namespace SbB.Collections
{
    public class Boundaries: IEnumerator, IEnumerable
    {
        private ArrayList boundaryArray = new ArrayList();
        private int pos = -1;

        public Boundary this[int index]
        {
            get { return (Boundary)boundaryArray[index];}
            set { boundaryArray[index] = value; }
        }

        public int Count
        {
            get { return boundaryArray.Count; }
        }

        public Boundaries() {}

        public void add()
        {
            boundaryArray.Add(new Boundary(boundaryArray.Count));
        }
        public void add(Boundary boundary)
        {
            boundaryArray.Add(boundary);
        }
        public void Remove(Boundary boundary)
        {
            boundaryArray.Remove(boundary);
        }
        public void remove(Edge edge)
        {
            foreach (Boundary boundary in boundaryArray)
                if (boundary.Contains(edge))
                {
                    boundary.Remove(edge);
                    return;
                }
        }
        public bool Contains(Edge edge)
        {
            foreach (Boundary boundary in boundaryArray)
                if (boundary.Contains(edge)) return true;
            return false;
        }

        #region IEnumerator

        public bool MoveNext()
        {
            if (pos < boundaryArray.Count - 1)
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
            get { return boundaryArray[pos]; }
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
