using System;
using System.Collections.Generic;
using System.Text;
using SbB.Algebra;
using SbB.FEM;
using SbB.Geometry;

namespace SbB.FEM
{
    class QuadraticMortar:Mortar
    {
        public override int rank()
        {
            return vertexes.Count - 2;
        }

        public QuadraticMortar(Boundary boundary)
        {
            foreach (Edge edge in boundary)
            {
                for (int i = 0; i < edge.nodesCount(); i++)
                    if (!vertexes.contains(edge[i]))
                        vertexes.add(edge[i]);
            }
            vertexes.sort();
        }

        public override Matrix bilinear(Edge edge)
        {
            Matrix matrix = new Matrix(2*edge.rank(), 2*rank());
                      

            {
                Edge me = new Edge(vertexes[0], vertexes[1]);
                Edge e = edge & me;
                e = null;
                if (e != null)
                {
                    
                    double d = edge.Ni(0, e.A);
                    double d1 = edge.Ni(0, e.B);
                    matrix[0][0] += (edge.Ni(0, e.A) + edge.Ni(0, e.B))/2*e.Length;
                    matrix[1][1] += (edge.Ni(0, e.A) + edge.Ni(0, e.B))/2*e.Length;
                    matrix[2][0] += (edge.Ni(1, e.A) + edge.Ni(1, e.B))/2*e.Length;
                    matrix[3][1] += (edge.Ni(1, e.A) + edge.Ni(1, e.B))/2*e.Length;
                }
            }

            for (int i = 1; i < vertexes.Count - 2; i++)
            {
                Edge me = new LinearEdge(vertexes[i], vertexes[i + 1]);
                Edge e = edge & me;
                if (e != null)
                {
                    double[] k, l, m, a, b;
                    k = new double[3];
                    m = new double[3];
                    l = new double[3];
                    for (int j = 0; j < 3; j++)
                    {
                        k[j] = edge.Ni(j, e.B);
                        m[j] = edge.Ni(j, e.A);
                        l[j] = 4 * edge.Ni(j, 0.5 * (e.A + e.B)) - k[j]-m[j];
                    }
                    a = new double[2];
                    b = new double[2];
                    for (int j = 0; j < 2; j++)
                    {
                        a[j] = me.Ni(j, e.A);
                        b[j] = me.Ni(j, e.B);
                    }
                    double[,] I = new double[3, 2];
                    for (int ii = 0; ii < 3; ii++)
                    {
                        for (int jj = 0; jj < 2; jj++)
                        {
                            I[ii, jj] = (0.25*(b[jj]-a[jj])*(k[ii]-l[ii]+m[ii]) +
                                         (1.0/3.0)*(a[jj]*(k[ii] - 2*l[ii] + 3*m[ii]) + b[jj]*(l[ii]-2*m[ii])) +
                                         0.5*(b[jj]*m[ii]-a[jj]*(-l[ii]+3*m[ii])) +
                                         a[jj]*m[ii])*e.Length;
                        }
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        matrix[2*j][2*i - 2] += I[j, 0];
                        matrix[2*j + 1][2*i - 1] += I[j, 0];

                        matrix[2*j][2*i] += I[j, 1];
                        matrix[2*j + 1][2*i + 1] += I[j, 1];
                    }
                    
                }
            }
            {
                Edge me = new Edge(vertexes[vertexes.Count - 2], vertexes[vertexes.Count - 1]);
                Edge e = edge & me;
                e = null;
                if (e != null)
                {
                    //matrix[0][vertexes.Count - 3] += (2*(N(0, edge, e.A) + N(0, edge, e.B)) + N(0, edge, e.A) +
                    //                                  N(0, edge, e.B))/6;
                    //matrix[1][vertexes.Count - 3] += (2*(N(0, edge, e.A) + N(0, edge, e.B)) + N(0, edge, e.A) +
                    //                                  N(0, edge, e.B))/6;
                    //matrix[2][vertexes.Count - 3] += (2*(N(1, edge, e.A) + N(1, edge, e.B)) + N(1, edge, e.A) +
                    //                                  N(1, edge, e.B))/6;
                    //matrix[3][vertexes.Count - 3] += (2*(N(1, edge, e.A) + N(1, edge, e.B)) + N(0, edge, e.A) +
                    //                                  N(0, edge, e.B))/6;

                    matrix[0][2*(vertexes.Count - 3)] += (edge.Ni(0, e.A) + edge.Ni(0, e.B))/2*e.Length;
                    matrix[1][2*(vertexes.Count - 3) + 1] += (edge.Ni(0, e.A) + edge.Ni(0, e.B))/2*e.Length;
                    matrix[2][2*(vertexes.Count - 3)] += (edge.Ni(1, e.A) + edge.Ni(1, e.B))/2*e.Length;
                    matrix[3][2*(vertexes.Count - 3) + 1] += (edge.Ni(1, e.A) + edge.Ni(1, e.B))/2*e.Length;
                }
            }
            return matrix;
        }
    }
}
