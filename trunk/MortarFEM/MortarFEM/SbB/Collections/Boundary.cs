using System.Collections;

namespace SbB.Geometry
{
    public class Boundary: IEnumerator, IEnumerable
    {
        private ArrayList edgeArray = new ArrayList();
        private int number;
        private int pos = -1;
        private BoundaryClass boundaryClass;

        public int Count
        {
            get { return edgeArray.Count; }
        }
        public Edge this[int index]
        {
            get { return (Edge)edgeArray[index]; }
            set { edgeArray[index] = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public BoundaryClass BoundaryClass
        {
            get { return boundaryClass; }
            set { boundaryClass = value; }
        }
     
        public Boundary(int number)
        {
            this.number = number;
        }

        public void Add(Edge edge)
        {
            edgeArray.Add(edge);
        }
        public void Remove(Edge edge)
        {
            edgeArray.Remove(edge);
        }
        public bool Contains(Edge edge)
        {
            return edgeArray.Contains(edge);
        }

        #region IEnumerator

        public bool MoveNext()
        {
            if (pos < edgeArray.Count - 1)
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
            pos = -1;// 0;
        }
        public object Current
        {
            get { return edgeArray[pos]; }
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
