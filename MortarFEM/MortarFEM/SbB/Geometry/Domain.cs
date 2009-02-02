using System.Collections;

namespace SbB.Geometry
{
    public class Domain
    {
        private double youngModulus = 21000;
        private double poissonRatio = 0.3;
        private Polygon polygon;
        private ArrayList subDomains = new ArrayList();
        private BoundaryClass[] boundaryClasses;
        private ArrayList insides = new ArrayList();

        public Domain(Polygon polygon)
        {
            this.polygon = polygon;
            boundaryClasses = new BoundaryClass[polygon.N];
            BoundaryClass.initArray(boundaryClasses);
            subDomains.Add(new SubDomain(polygon, boundaryClasses));
        }
        public Domain(Polygon polygon, BoundaryClass[] bcArray)
        {
            this.polygon = polygon;
            boundaryClasses = new BoundaryClass[polygon.N];
            for (int i = 0; i < bcArray.Length && i < boundaryClasses.Length; i++)
                boundaryClasses[i] = bcArray[i];
            for (int i = bcArray.Length; i < boundaryClasses.Length; i++)
                boundaryClasses[i] = new CinematicBoundary();
            subDomains.Add(new SubDomain(polygon, boundaryClasses));
        }

        public SubDomain this[int index]
        {
            get 
            {
                if (index >= N) return null;
                return (SubDomain)subDomains[index];
            }
        }
        public int N
        {
            get { return subDomains.Count; }
        }
        public int IBN
        {
            get { return insides.Count; }
        }
        public Polygon P
        {
            get { return polygon; }
        }
        public double YoungModulus
        {
            get { return youngModulus; }
            set 
            { 
                youngModulus = value;
                for (int i = 0; i < N; i++)
                    this[i].YoungModulus = youngModulus;
            }
        }
        public double PoissonRatio
        {
            get { return poissonRatio; }
            set 
            { 
                poissonRatio = value;
                for (int i = 0; i < N; i++)
                    this[i].PoissonRatio = poissonRatio;

            }
        }

        public BoundaryClass boundary(int index)
        {
            if (index >= polygon.N) return null;
            return boundaryClasses[index];
        }
        public InsideBoundary insideBoundary(int index)
        {
            return (InsideBoundary)insides[index];
        }
        public void setBoundary(int index, BoundaryClass boundaryClass)
        {
            if (index < 0 || index >= polygon.N) return;
            boundaryClasses[index] = boundaryClass;
            int k;
            for (int i = 0; i < N; i++)
                if ( (k = this[i].P.isEdgeOnPolygon(polygon.edge(index))) > -1)
                    this[i].setBoundary(k, boundaryClass);
        }
        public void addInsideBoundary(Edge e)
        {
            int ind;
            for (ind = 0; ind < N; ind++)
                if (this[ind].P.hasEdge(e)) break;
            if (ind<N)
            {
                SubDomain subD = this[ind];
                Polygon[] ps = subD.P.divideByEdge(e);
                BoundaryClass[][] bcs = new BoundaryClass[2][] 
                    {
                        new BoundaryClass[ps[0].N],
                        new BoundaryClass[ps[1].N]
                    };
                BoundaryClass[] mortar = new BoundaryClass[2] { new MortarBoundary(2*IBN), new NonMortarBoundary(2*IBN+1) };
                int k;
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < ps[i].N; j++)
                        if ((k = subD.P.isEdgeOnPolygon(ps[i].edge(j))) > -1)
                            bcs[i][j] = subD.boundary(k);
                        else bcs[i][j] = mortar[i];
                SubDomain[] sd = new SubDomain[2];
                for (int i = 0; i < 2; i++)
                    sd[i] = new SubDomain(ps[i], bcs[i]);
                subDomains.RemoveAt(ind);
                subDomains.Insert(ind, sd[0]);
                subDomains.Insert(ind+1, sd[1]);
                insides.Add(new InsideBoundary(IBN, e, sd[0], sd[1]));
                subDomains.Sort();
            }
        }

        public override string ToString()
        {
            string s = "Domain:\n";
            for (int i = 0; i < polygon.N; i++)
                s += polygon.edge(i) + "\t" + boundary(i).type() + "\n";
            s += "\n\n";
            for (int i = 0; i < N; i++ )
                s+="SubDomain " +i+"\n" +this[i]+"\n";
            return s;
        }
    }
}
