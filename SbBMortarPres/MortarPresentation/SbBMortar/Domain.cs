using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace SbBMortar.SbB
{
    public class Domain
    {
        #region Events
        public event EmptyDelegate onTriangComplete;
        public event EmptyDelegate onDomainLoaded;
        public event EmptyDelegate onSolveComplete;
        public event EmptyDelegate onCreateK_Complete;
        public event EmptyDelegate onCreateD_Complete;
        public event EmptyDelegate onCreateGlobalVector_Complete;
        public event EmptyDelegate onCreateGlobalMatrix_Complete;
        static void empty(){}
        #endregion

        #region Fields
        private Polygon polygon;
        private SubDomain[] subDomains;
        private BoundaryClass[] boundaryClasses;
        private MortarSide[] mortarSides;
        private List<Vertex> vertexes;
        private List<Element> elements;
        private List<FEMEdge>[] boundaries;
        private Vector result;
       private string name;
        #endregion

        #region Constructors
        public Domain(string filename)
        {
            name = Path.GetFileName(filename);
            name = name.Replace(".txt", "");
            onTriangComplete += empty;
            onDomainLoaded += empty;
            onCreateK_Complete += empty;
            onCreateD_Complete += empty;
            onCreateGlobalVector_Complete += empty;
            onCreateGlobalMatrix_Complete += empty;

            StreamReader file = File.OpenText(filename);
            string currentCulture = CultureInfo.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // Read polygon
            int n = int.Parse(file.ReadLine());
            Vertex[] vertexes = new Vertex[n];
            string line = file.ReadLine();
            string[] lines = line.Split(' ');
            for (int i = 0; i < n; i++)
                vertexes[i] = new Vertex(double.Parse(lines[2*i]), double.Parse(lines[2*i+1]));
            polygon = new Polygon(vertexes);

            // Read boundary settings
            BoundaryClasses = new BoundaryClass[n];
            for (int i = 0; i < n; i++)
            {
                line = file.ReadLine();
                lines = line.Split(' ');
                switch(lines[0])
                {
                    case "Static":
                        BoundaryClasses[i] = new StaticBoundary(double.Parse(lines[1]), double.Parse(lines[2]));
                        break;
                    case "Kinematic":
                        BoundaryClasses[i] = new KinematicBoundary();
                        break;
                    default:
                        BoundaryClasses[i] = new KinematicBoundary();
                        break;
                }
            }

            // Read Subdomains
            int nsd = int.Parse(file.ReadLine());
            subDomains = new SubDomain[nsd];
            for (int i = 0; i < nsd; i++)
            {
                n = int.Parse(file.ReadLine());
                vertexes = new Vertex[n];
                line = file.ReadLine();
                lines = line.Split(' ');
                for (int j = 0; j < n; j++)
                    vertexes[j] = new Vertex(double.Parse(lines[2*j]), double.Parse(lines[2*j+1]));
                Polygon p = new Polygon(vertexes);
                subDomains[i] = new SubDomain(p);

                line = file.ReadLine();
                lines = line.Split(' ');
                subDomains[i].YoungModulus = double.Parse(lines[0]);
                subDomains[i].PoissonRatio = double.Parse(lines[1]);

                line = file.ReadLine();
                lines = line.Split(' ');
                switch(lines[0])
                {
                    case "LinearTriangle":
                        subDomains[i].Triangulation = new LinialTriangleTriangulation(p);
                        break;
                    case "QuadraticTriangle":
                        subDomains[i].Triangulation = new QuadraticTriangleTriangulation(p);
                        break;
                    case "LinearQuadrangle":
                        subDomains[i].Triangulation = new LinearQuadrangulation(p);
                        break;
                    default: subDomains[i].Triangulation = new LinialTriangleTriangulation(p);
                        break;
                }
                subDomains[i].MaxArea = double.Parse(lines[1]);
                subDomains[i].MinAngle = double.Parse(lines[2]);
                subDomains[i].N = i;
            }

            // Read MortarSides
            n = int.Parse(file.ReadLine());
            MortarSides = new MortarSide[n];
            for (int i = 0; i < n; i++)
            {
                nsd = int.Parse(file.ReadLine());
                vertexes = new Vertex[nsd];
                lines = file.ReadLine().Split(' ');
                for (int j = 0; j < nsd; j++)
                    vertexes[j] = new Vertex(double.Parse(lines[2*j]), double.Parse(lines[2*j + 1]));
                lines = file.ReadLine().Split(' ');
                SubDomain[] msd = new SubDomain[int.Parse(lines[0])];
                for (int j = 0; j < msd.Length; j++)
                    msd[j] = subDomains[int.Parse(lines[j + 1])];
                lines = file.ReadLine().Split(' ');
                SubDomain[] nmsd = new SubDomain[int.Parse(lines[0])];
                for (int j = 0; j < nmsd.Length; j++)
                    nmsd[j] = subDomains[int.Parse(lines[j + 1])];
                MortarSides[i] = new MortarSide(vertexes, msd, nmsd);
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
            file.Close();
           // onDomainLoaded();
        }
        #endregion

        #region Properties
        public Polygon Polygon
        {
            get { return polygon; }
        }
        public SubDomain this[int i]
        {
            get { return subDomains[i]; }
        }
        public int SubDomainCount
        {
            get { return subDomains.Length; }
        }
        public List<Vertex> Vertexes
        {
            get
            {
                if (vertexes == null) throw new Exception("Triangulation did not complete");
                return vertexes;
            }
        }
        public List<Element> Elements
        {
            get
            {
                //if (elements == null) r
                return elements;
            }
            set
            {
                if(value==null)
                elements = value;
            }

        }
        public List<FEMEdge>[] Boundaries
        {
            get
            {
                if (boundaries == null) throw new Exception("Triangulation did not complete");
                return boundaries;
            }
        }

        public BoundaryClass[] BoundaryClasses
        {
            get { return boundaryClasses; }
            set { boundaryClasses = value; }
        }

        public MortarSide[] MortarSides
        {
            get { return mortarSides; }
            set { mortarSides = value; }
        }

        public Vector Result
        {
            get { return result; }
        }

        #endregion

        #region Methods
        #region Private
        private void merge()
        {
            vertexes = new List<Vertex>();
            elements = new List<Element>();
            boundaries = new List<FEMEdge>[polygon.Count];
            for (int i = 0; i < boundaries.Length; i++)
                boundaries[i] = new List<FEMEdge>();

            foreach (SubDomain subDomain in subDomains)
            {
                for (int i = 0; i < subDomain.Vertexes.Count; i++)
                {
                    int k = vertexes.IndexOf(subDomain.Vertexes[i]);
                    if (k >= 0) subDomain.Vertexes[i] = vertexes[k];
                    else vertexes.Add(subDomain.Vertexes[i]);
                }
                foreach (Element element in subDomain.Elements)
                {
                    for (int i = 0; i < element.NodesCount; i++)
                        element[i] = vertexes[vertexes.IndexOf(element[i])];
                    elements.Add(element);
                }
                for (int i = 0; i < subDomain.Polygon.Count; i++)
                {
                    int k = polygon.isEdgeOnPolygon(subDomain.Polygon.edge(i));
                    if (k >= 0)
                        foreach (FEMEdge edge in subDomain.Boundaries[i])
                        {
                            for (int j = 0; j < edge.NodesCount; j++)
                                edge[j] = vertexes[vertexes.IndexOf(edge[j])];
                            boundaries[k].Add(edge);
                        }
                    else
                    {
                        foreach (FEMEdge edge in subDomain.Boundaries[i])
                            for (int j = 0; j < edge.NodesCount; j++)
                                edge[j] = vertexes[vertexes.IndexOf(edge[j])];
                    }
                }
            }

            for (int i = 0; i < MortarSides.Length; i++)
                MortarSides[i].createMortarNodes();

            vertexes.Sort();
            elements.Sort();
            for (int i = 0; i < boundaries.Length; i++)
                boundaries[i].Sort();
            for (int i = 0; i < vertexes.Count; i++)
                vertexes[i].Number = i;
        }
        private Matrix createK()
        {
            Matrix K = new Matrix(2*vertexes.Count, 2*vertexes.Count);
            foreach (SubDomain subDomain in subDomains)
                foreach (Element element in subDomain.Elements)
                    element.FEM(K,subDomain.D);
            for (int i = 0; i < BoundaryClasses.Length; i++)
                if (BoundaryClasses[i].type() == BoundaryType.KINEMATIC)
                {
                    foreach (FEMEdge edge in boundaries[i])
                        for (int j = 0; j < edge.NodesCount; j++)
                        {
                            K[2 * edge[j].Number][2 * edge[j].Number] = 1.0 / Constants.EPS;
                            K[2 * edge[j].Number+1][2 * edge[j].Number+1] = 1.0 / Constants.EPS;
                        }
                }
            onCreateK_Complete();
            Thread.Sleep(100);
            return K;
        }
        private Matrix[] createD()
        {
            int m = vertexes.Count;
            Matrix[] D =new Matrix[MortarSides.Length];
            for (int i = 0; i < MortarSides.Length; i++)
            {
                Mortar mortar = MortarSides[i].createMortar(m);
                mortar.Sign = true;
                foreach (SubDomain subDomain in MortarSides[i].Mortars)
                    for (int j = 0; j < MortarSides[i].NodesCount-1; j++)
                    {
                        int k = subDomain.Polygon.isEdgeOnPolygon(MortarSides[i].edge(j));
                        if (k >= 0)
                            foreach (FEMEdge femEdge in subDomain.Boundaries[k])
                                femEdge.mortar(mortar);
                    }
                mortar.Sign = false;
                foreach (SubDomain subDomain in MortarSides[i].Nonmortars)
                    for (int j = 0; j < MortarSides[i].NodesCount - 1; j++)
                    {
                        int k = subDomain.Polygon.isEdgeOnPolygon(MortarSides[i].edge(j));
                        if (k >= 0)
                            foreach (FEMEdge femEdge in subDomain.Boundaries[k])
                                femEdge.mortar(mortar);
                    } 
                D[i] = mortar.D;
            }
            onCreateD_Complete();
            Thread.Sleep(100);
            return D;
        }
        private Matrix createGlobalMatrix()
        {
            
            Matrix K = createK();
            
            Matrix[] D = createD();
        
            int m = K.Size.m, n = m, k = m;
            for (int i = 0; i < D.Length; i++)
                m += D[i].Size.m;
            Matrix matrix = new Matrix(m,m);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i][j] = K[i][j];
            for (int i = 0; i < D.Length; i++)
            {
                for (int l = 0; l < D[i].Size.n; l++)
                {
                    for (int j = 0; j < n; j++)
                        matrix[j][k] = matrix[k][j] = D[i][j][l];
                    k++;
                }
            }
            onCreateGlobalMatrix_Complete();
            Thread.Sleep(100);
            return matrix;
            
        }
        private Vector createGlobalVector(int m)
        {
            Vector F = new Vector(m);
            for (int i = 0; i < BoundaryClasses.Length; i++)
                if (BoundaryClasses[i].type()==BoundaryType.STATIC)
                {
                    Vertex p = ((StaticBoundary) BoundaryClasses[i]).P;
                    foreach (FEMEdge edge in boundaries[i])
                        edge.FEM(F,p);
                }
            for (int i = 0; i < BoundaryClasses.Length; i++)
                if (BoundaryClasses[i].type()==BoundaryType.KINEMATIC)
                    foreach (FEMEdge edge in boundaries[i])
                        for (int j = 0; j < edge.NodesCount; j++)
                        {
                            F[2*edge[j].Number] = 0.0;
                            F[2*edge[j].Number + 1] = 0.0;
                        }
            onCreateGlobalVector_Complete();
            Thread.Sleep(100);
            return F;
        }
       
        #endregion

        #region Public
        public void mesh()
        {
            for (int i = 0; i < subDomains.Length; i++)
                subDomains[i].triangulate();
            merge();
            onTriangComplete();
            Thread.Sleep(100);
        }
        public void solve()
        {
            
            Matrix matrix = createGlobalMatrix();
            Thread.Sleep(100);
            for (int i = 2 * vertexes.Count; i < matrix.Size.m; i++)
            {
                for (int j = 0; j < matrix.Size.n; j++)
                {
                    if (matrix[i][j] != 0) break;
                    if (j == matrix.Size.n - 1)
                    {
                        matrix.RemoveRow(j);
                        matrix.RemoveColumn(j);
                        i--;
                        j--;
                    }
                }
            }
            
            Vector vector = createGlobalVector(matrix.Size.m);
            Thread.Sleep(100);
            result = Solver.Eval(matrix, vector);
            Result.Length = 2 * vertexes.Count;
            
            onSolveComplete();
            Thread.Sleep(100);
        }

        public double U(int i)
        {
            if (Result==null) return 0;
            return Result[2*i];
        }
        public double U(double x, double y)
        {
            return U(new Vertex(x, y));
        }
        public double U(Vertex v)
        {
            double rez = 0.0;
            foreach (Element element in elements)
            {
                if (element.hasVertex(v))
                {
                    for (int i = 0; i < element.NodesCount; i++)
                        rez += U(element[i].Number)*element.phi(i, v);
                    return rez;
                }
            }
            return rez;

        }

        public double V(int i)
        {
            if (Result == null) return 0;
            return Result[2*i + 1];
        }
        public double V(double x, double y)
        {
            return V(new Vertex(x, y));
        }
        public double V(Vertex v)
        {
            foreach (Element element in elements)
                if (element.hasVertex(v))
                {
                    double rez = 0.0;
                    for (int i = 0; i < element.NodesCount; i++)
                        rez += V(element[i].Number)*element.phi(i, v);
                    return rez;
                }
            return 0.0;
        }

        public double Exx(int i)
        {
            return Exx(vertexes[i]);
        }
        public double Exx(double x, double y)
        {
            return Exx(new Vertex(x, y));
        }
        public double Exx(Vertex vertex)
        {
            double exx = 0.0;
            int count = 0;
            foreach (Element element in elements)
                if (element.hasVertex(vertex))
                {
                    Vector u = new Vector(element.NodesCount);
                    for (int i = 0; i < element.NodesCount; i++)
                        u[i] = U(element[i].Number);
                    exx += element.Exx(vertex, u);
                    //return exx;
                    count++;
                }
            return exx/count;
        }

        public double Eyy(int i)
        {
            return Eyy(vertexes[i]);
        }
        public double Eyy(double x, double y)
        {
            return Eyy(new Vertex(x, y));
        }
        public double Eyy(Vertex vertex)
        {
            double eyy = 0.0;
            int count = 0;
            foreach (Element element in elements)
                if (element.hasVertex(vertex))
                {
                    Vector v = new Vector(element.NodesCount);
                    for (int i = 0; i < element.NodesCount; i++)
                        v[i] = V(element[i].Number);
                    eyy += element.Eyy(vertex, v);
                    count++;
                }
            return eyy/count;
        }

        public double Exy(int i)
        {
            return Exy(vertexes[i]);
        }
        public double Exy(double x, double y)
        {
            return Exy(new Vertex(x, y));
        }
        public double Exy(Vertex vertex)
        {
            double exy = 0.0;
            int count = 0;
            foreach (Element element in elements)
                if (element.hasVertex(vertex))
                {
                    Vector u = new Vector(element.NodesCount),
                           v = new Vector(element.NodesCount);
                    for (int i = 0; i < element.NodesCount; i++)
                    {
                        u[i] = U(element[i].Number);
                        v[i] = V(element[i].Number);
                    }
                    exy += element.Exy(vertex, u, v);
                    count++;
                }
            return exy/count;
        }

        public double Sxx(int i)
        {
            return Sxx(vertexes[i]);
        }
        public double Sxx(double x, double y)
        {
            return Sxx(new Vertex(x, y));
        }
        public double Sxx(Vertex vertex)
        {
            double sxx = 0.0;
            int count = 0;
            foreach (SubDomain subDomain in subDomains)
                foreach (Element element in elements)
                    if (element.hasVertex(vertex))
                    {
                        Vector u = new Vector(element.NodesCount),
                               v = new Vector(element.NodesCount);
                        for (int i = 0; i < element.NodesCount; i++)
                        {
                            u[i] = U(element[i].Number);
                            v[i] = V(element[i].Number);
                        }
                        sxx += element.Sxx(vertex, u, v, subDomain.D);
                        //return sxx;
                        count++;
                    }
            return sxx/count;
        }

        public double Syy(int i)
        {
            return Syy(vertexes[i]);
        }
        public double Syy(double x, double y)
        {
            return Syy(new Vertex(x, y));
        }
        public double Syy(Vertex vertex)
        {
            double syy = 0.0;
            int count = 0;
            foreach (SubDomain subDomain in subDomains)
                foreach (Element element in elements)
                    if (element.hasVertex(vertex))
                    {
                        Vector u = new Vector(element.NodesCount),
                               v = new Vector(element.NodesCount);
                        for (int i = 0; i < element.NodesCount; i++)
                        {
                            u[i] = U(element[i].Number);
                            v[i] = V(element[i].Number);
                        }
                        syy += element.Syy(vertex, u, v, subDomain.D);
                        count++;
                    }
            return syy / count;
        }

        public double Sxy(int i)
        {
            return Sxy(vertexes[i]);
        }
        public double Sxy(double x, double y)
        {
            return Sxy(new Vertex(x, y));
        }
        public double Sxy(Vertex vertex)
        {
            double sxy = 0.0;
            int count = 0;
            foreach (SubDomain subDomain in subDomains)
                foreach (Element element in elements)
                    if (element.hasVertex(vertex))
                    {
                        Vector u = new Vector(element.NodesCount),
                               v = new Vector(element.NodesCount);
                        for (int i = 0; i < element.NodesCount; i++)
                        {
                            u[i] = U(element[i].Number);
                            v[i] = V(element[i].Number);
                        }
                        sxy += element.Sxy(vertex, u, v, subDomain.D);
                        count++;
                    }
            return sxy / count;
        }

        public List<Vertex>[] getFuncArrX(Functions func, double x, int count)
        {
            FunctionXY f;
            switch(func)
            {
                case Functions.U: f = U;
                                  break;
                case Functions.V: f = V;
                                  break;
                case Functions.Exx: f = Exx;
                                  break;
                case Functions.Eyy: f = Eyy;
                                  break;
                case Functions.Exy: f = Exy;
                                  break;
                case Functions.Sxx: f = Sxx;
                                  break;
                case Functions.Syy: f = Syy;
                                  break;
                case Functions.Sxy: f = Sxy;
                                  break;
                default: f = U;
                                  break;
            }

            List<Vertex>[] array = new List<Vertex>[1];
            array[0] = new List<Vertex>();
            double minY = polygon.MinVertex.Y;
            double maxY = polygon.MaxVertex.Y;
            double h = (maxY - minY)/(count-1);
            for (int i = 0; i < count-1; i++)
            {
                double y = minY + i * h;
                if (!polygon.hasVertex(new Vertex(x, y))) continue;
                array[0].Add(new Vertex(y,f(x,y)));
            }
            if (polygon.hasVertex(new Vertex(x, maxY)))
                array[0].Add(new Vertex(maxY, f(x,maxY)));
            return array;
        }
        public List<Vertex>[] getFuncArrY(Functions func, double y, int count)
        {
            FunctionXY f;
            switch (func)
            {
                case Functions.U: f = U;
                    break;
                case Functions.V: f = V;
                    break;
                case Functions.Exx: f = Exx;
                    break;
                case Functions.Eyy: f = Eyy;
                    break;
                case Functions.Exy: f = Exy;
                    break;
                case Functions.Sxx: f = Sxx;
                    break;
                case Functions.Syy: f = Syy;
                    break;
                case Functions.Sxy: f = Sxy;
                    break;
                default: f = U;
                    break;
            }

            List<Vertex>[] array = new List<Vertex>[1];
            array[0] = new List<Vertex>();
            double minX = polygon.MinVertex.X;
            double maxX = polygon.MaxVertex.X;
            double h = (maxX - minX)/(count - 1);
            for (int i = 0; i < count - 1; i++)
            {
                double x = minX + i*h;
                if (!polygon.hasVertex(new Vertex(x, y))) continue;
                array[0].Add(new Vertex(x, f(x, y)));
            }
            if (polygon.hasVertex(new Vertex(maxX, y)))
                array[0].Add(new Vertex(maxX, f(maxX, y)));
            return array;
        }
        public override string ToString()
        {
            return name;
        }
        #endregion
        #endregion
    }
}