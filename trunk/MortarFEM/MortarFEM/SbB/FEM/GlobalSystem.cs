using System.IO;
using SbB.Algebra;
using SbB.Collections;
using SbB.Geometry;

namespace SbB.FEM
{
    public class GlobalSystem
    {
        private FEMData[] femdatas;
        private FEM[] fems;
        private Mortar[] mortars;
        private Matrix matrix;
        private Vector vector;
        private Vector result;
        private Vertexes vertexes;

        public GlobalSystem(FEMData[] femdatas, FEM[] fems, Mortar[] mortars)
        {
            this.femdatas = femdatas;
            this.fems = fems;
            this.mortars = mortars;
            assemblyDatas();
        }

        public Vector Result
        {
            get { return result; }
        }

        public Vertexes Vertexes
        {
            get { return vertexes; }
        }

        public FEMData[] Femdatas
        {
            get { return femdatas; }
        }

        private void createMatrix()
        {
            matrix = createA();
            PairInt size = matrix.Size;
            Matrix D = createD();
            matrix.Size = new PairInt(matrix.Size.m + D.Size.n, matrix.Size.n + D.Size.n);
            for (int i=0; i<D.Size.m; i++)
                for (int j=0; j<D.Size.n; j++)
                {
                    matrix[i][size.n + j] = D[i][j];
                    matrix[size.m + j][i] = D[i][j];
                }
        }
        private void createVector()
        {
            int n = 2 * vertexes.Count;
            for (int i = 0; i < mortars.Length; i++) n += mortars[i].rank();
            vector = new Vector(n);
            for (int i = 0; i < fems.Length; i++)
                foreach (Boundary boundary in Femdatas[i].Boundaries)
                {
                    if (boundary.BoundaryClass.type() == BoundaryType.STATIC)
                    {
                        StaticBoundary stbd = (StaticBoundary)boundary.BoundaryClass;
                        foreach (Edge edge in boundary)
                        {
                            Vector local = fems[i].linear(edge) * stbd.P;
                            /*int[] indexes = edge.indexes();
                            for (int j = 0; j < indexes.Length; j++)
                                vector[indexes[j]] += local[j];*/
                            int[] ind = edge.indexes();
                            for (int j = 0; j < edge.rank(); j++)
                                for (int k = 0; k < 2; k++)
                                    vector[2*ind[j] + k] += local[2*j + k];
                        }
                    }
                }
            for (int i = 0; i < fems.Length; i++)
                foreach (Boundary boundary in Femdatas[i].Boundaries)
                {
                    if (boundary.BoundaryClass.type() == BoundaryType.CINEMATIC)
                    {
                        foreach (Edge edge in boundary)
                        {
                            /*int[] indexes = edge.indexes();
                            for (int j = 0; j < indexes.Length; j++)
                                vector[indexes[j]] = 0;*/
                            int[] ind = edge.indexes();
                            for (int j = 0; j < edge.rank(); j++)
                                for (int k = 0; k < 2; k++)
                                    vector[2 * ind[j] + k] = 0;
                        }
                    }
                }
        }
        private void assemblyDatas()
        {
            vertexes = new Vertexes();
            for (int i=0; i<Femdatas.Length; i++)
            {
                foreach (Triangle tr in Femdatas[i].Triangles)
                {
                    for (int j=0; j<tr.nodesCount(); j++)
                    {
                        if (Vertexes.contains(tr[j])) 
                            tr[j] = Vertexes[Vertexes.indexOf(tr[j])];
                        else Vertexes.add(tr[j]);
                    }
                }
                foreach (Boundary boundary in Femdatas[i].Boundaries)
                {
                    foreach (Edge edge in boundary)
                        for (int j = 0; j < edge.nodesCount(); j++)
                        {
                            if (Vertexes.contains(edge[j])) edge[j] = Vertexes[Vertexes.indexOf(edge[j])];
                            else Vertexes.add(edge[j]);
                        }
                }
                for (int j=0; j<Femdatas[i].Vertexes.Count; j++)
                {
                    if (Vertexes.contains(Femdatas[i].Vertexes[j]))
                        Femdatas[i].Vertexes[j] = Vertexes[Vertexes.indexOf(Femdatas[i].Vertexes[j])];
                    else Vertexes.add(Femdatas[i].Vertexes[j]);
                }
            }
            Vertexes.sort();
        }
        private Matrix createA()
        {
            Matrix a = new Matrix(2*vertexes.Count, 2*vertexes.Count);
            for (int f = 0; f<Femdatas.Length; f++ )
                foreach (Triangle tr in Femdatas[f].Triangles)
                {
                    Matrix local = fems[f].bilinear(tr);
                    int[] ind = tr.indexes();
                    for (int k = 0; k < 2; k++)
                        for (int l = 0; l < 2; l++)
                            for (int i = 0; i < tr.rank(); i++)
                                for (int j = 0; j < tr.rank(); j++)
                                    a[2*ind[i]+k][2*ind[j]+l] += local[k*tr.rank()+i][l*tr.rank()+j];
                }
            for (int i = 0; i < fems.Length; i++)
                foreach (Boundary boundary in Femdatas[i].Boundaries)
                {
                    if (boundary.BoundaryClass.type() == BoundaryType.CINEMATIC)
                    {
                        foreach (Edge edge in boundary)
                        {
                            /*int[] indexes = edge.indexes();
                            for (int j = 0; j < indexes.Length; j++)
                                a[indexes[j]][indexes[j]] = 1 / Constants.EPS;*/
                            int[] ind = edge.indexes();
                            for (int j = 0; j < edge.rank(); j++)
                                for (int k = 0; k < 2; k++)
                                    a[2*ind[j] + k][2*ind[j] + k] = 1/Constants.EPS;
                        }
                    }
                }
            return a;
        }
        private Matrix createD()
        {
            Matrix[] matrixes = new Matrix[mortars.Length/2];
            for (int i = 0; i < matrixes.Length; i++)
            {
                matrixes[i] = new Matrix(2*vertexes.Count, 2*mortars[2*i].rank());
                
            }
            for (int i = 0; i < Femdatas.Length; i++)
            {
                foreach (Boundary boundary in Femdatas[i].Boundaries)
                {
                  if (boundary.BoundaryClass.type() == BoundaryType.MORTAR)
                  {
                      int ind = ((MortarBoundary) boundary.BoundaryClass).N;
                      Matrix m = new Matrix(2*vertexes.Count, 2*mortars[ind].rank());
                      foreach (Edge edge in boundary)
                      {
                          Matrix local = mortars[ind].bilinear(edge);
                          int[] indexes = edge.indexes();
                          for (int k = 0; k < 2; k++)
                              for (int j = 0; j < indexes.Length; j++)
                                  for (int l = 0; l < 2*mortars[ind].rank(); l++)
                                      m[2*indexes[j] + k][l] += local[2*j + k][l];
                      }
                      matrixes[ind/2] = matrixes[ind/2] + m;
                  }
                  if (boundary.BoundaryClass.type() == BoundaryType.NONMORTAR)
                  {
                      int ind = ((NonMortarBoundary)boundary.BoundaryClass).N;
                      Matrix m = new Matrix(2 * vertexes.Count, 2*mortars[ind].rank());
                      foreach (Edge edge in boundary)
                      {
                          Matrix local = mortars[ind].bilinear(edge);
                          int[] indexes = edge.indexes();
                          for (int k = 0; k < 2; k++)
                              for (int j = 0; j < indexes.Length; j++)
                                  for (int l = 0; l < 2*mortars[ind].rank(); l++)
                                      m[2 * indexes[j] + k][l] += local[2 * j + k][l];
                      }
                      matrixes[ind / 2] = matrixes[ind / 2] - m;
                  }
                }
            }
            int count = 0;
            for (int i = 0; i < matrixes.Length; i++)
                count += matrixes[i].Size.n;
            Matrix rez = new Matrix(2*vertexes.Count, count);
            count = 0;
            for (int i = 0; i < matrixes.Length; i++)
            {
                for (int j = 0; j < matrixes[i].Size.m; j++)
                    for (int k = 0; k < matrixes[i].Size.n; k++)
                        rez[j][k + count] = matrixes[i][j][k];
                count += matrixes[i].Size.n;
            }
            return rez;
        }
        public bool solve()
        {
            createMatrix();
            createVector();
            for (int i = 2 * vertexes.Count; i < matrix.Size.m; i++ )
            {
                for (int j=0; j<matrix.Size.n; j++)
                {
                    if (matrix[i][j]!=0) break;
                    if (j==matrix.Size.n-1)
                    {
                        matrix.removeRow(j);
                        matrix.removeColumn(j);
                        vector.removeAt(j);
                        i--;
                        j--;
                    }
                }
            }
            result = LineGauss.Eval(matrix, vector);
            result.Length = 2*vertexes.Count;
            /*string s = "Mortar_8x8&4x8_";
            StreamWriter swu = File.CreateText(s+"U.txt");
            StreamWriter swv = File.CreateText(s+"V.txt");
            StreamWriter swk = File.CreateText(s+"K.txt");
            swk.WriteLine(matrix.ToString());
            foreach(Vertex v in vertexes)
                if (v.X == 1)
                {
                    swu.WriteLine("y = {0}\t\t{1}", v.Y, result[2*v.Number]);
                    swv.WriteLine("y = {0}\t\t{1}", v.Y, result[2*v.Number + 1]);
                }
            swu.Close();
            swv.Close();
            swk.Close();*/
            return true;
        }
    }
}
