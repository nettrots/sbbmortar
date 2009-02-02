using SbB.Algebra;
using SbB.Geometry;

namespace SbB.FEM
{
    public class LinearMortar: Mortar
    {
        
        public LinearMortar(Boundary boundary)
        {
            foreach (Edge edge in boundary)
            {
               for(int i=0; i<edge.nodesCount(); i++)
                   if (!vertexes.contains(edge[i]))
                       vertexes.add(edge[i]);
            }
            vertexes.sort();
        }

        public override int rank()
        {
            return vertexes.Count-2;
        }

        public override Matrix bilinear(Edge edge)
        {
            Matrix matrix = new Matrix(2*edge.rank(), 2*rank());

            {
                Edge me = new LinearEdge(vertexes[0], vertexes[1]);
                Edge e = edge & me;
                e = null;
                if (e != null)
                {
                    //matrix[0][0] += (2*(N(0, edge, e.A) + N(0, edge, e.B)) + N(0, edge, e.A) + N(0, edge, e.B))/6;
                    //matrix[1][0] += (2*(N(0, edge, e.A) + N(0, edge, e.B)) + N(0, edge, e.A) + N(0, edge, e.B))/6;
                    //matrix[2][0] += (2*(N(1, edge, e.A) + N(1, edge, e.B)) + N(1, edge, e.A) + N(1, edge, e.B))/6;
                    //matrix[3][0] += (2 * (N(1, edge, e.A) + N(1, edge, e.B)) + N(0, edge, e.A) + N(0, edge, e.B)) / 6;
                    double d = edge.Ni(0, e.A);
                    double d1 = edge.Ni(0, e.B);
                     matrix[0][0] += (edge.Ni(0, e.A) + edge.Ni(0, e.B)) / 2 * e.Length;
                     matrix[1][1] += (edge.Ni(0, e.A) + edge.Ni(0, e.B)) / 2 * e.Length;
                     matrix[2][0] += (edge.Ni(1, e.A) + edge.Ni(1, e.B)) / 2 * e.Length;
                     matrix[3][1] += (edge.Ni(1, e.A) + edge.Ni(1, e.B)) / 2 * e.Length;

                }
            }

            for (int i=1; i<vertexes.Count-2; i++)
            {
                Edge me = new LinearEdge(vertexes[i], vertexes[i+1]);
                Edge e = edge & me;
                if (e != null)
                {
                    //TODO:
                    // Догадка №1!
                    //matrix[0][i - 1] += ((N(0, edge, e.A) + N(0, edge, e.B))*(N(0, me, e.A) + N(0, me, e.B)) +
                    //                     N(0, edge, e.A)*N(0, me, e.A) + N(0, edge, e.B)*N(0, me, e.B))/6;
                    //matrix[1][i - 1] += ((N(0, edge, e.A) + N(0, edge, e.B))*(N(0, me, e.A) + N(0, me, e.B)) +
                    //                     N(0, edge, e.A)*N(0, me, e.A) + N(0, edge, e.B)*N(0, me, e.B))/6;
                    //matrix[2][i - 1] += ((N(1, edge, e.A) + N(1, edge, e.B))*(N(0, me, e.A) + N(0, me, e.B)) +
                    //                     N(1, edge, e.A)*N(0, me, e.A) + N(1, edge, e.B)*N(0, me, e.B))/6;
                    //matrix[3][i - 1] += ((N(1, edge, e.A) + N(1, edge, e.B))*(N(0, me, e.A) + N(0, me, e.B)) +
                    //                     N(0, edge, e.A)*N(0, me, e.A) + N(0, edge, e.B)*N(0, me, e.B))/6;

                    //matrix[0][i] += ((N(0, edge, e.A) + N(0, edge, e.B))*(N(1, me, e.A) + N(1, me, e.B)) +
                    //                 N(0, edge, e.A)*N(1, me, e.A) + N(0, edge, e.B)*N(1, me, e.B))/6;
                    //matrix[1][i] += ((N(0, edge, e.A) + N(0, edge, e.B))*(N(1, me, e.A) + N(1, me, e.B)) +
                    //                 N(0, edge, e.A)*N(1, me, e.A) + N(0, edge, e.B)*N(1, me, e.B))/6;
                    //matrix[2][i] += ((N(1, edge, e.A) + N(1, edge, e.B))*(N(1, me, e.A) + N(1, me, e.B)) +
                    //                 N(1, edge, e.A)*N(1, me, e.A) + N(1, edge, e.B)*N(1, me, e.B))/6;
                    //matrix[3][i] += ((N(1, edge, e.A) + N(1, edge, e.B))*(N(1, me, e.A) + N(1, me, e.B)) +
                    //                 N(0, edge, e.A)*N(1, me, e.A) + N(0, edge, e.B)*N(1, me, e.B))/6;
                    // Догадка №2!
//                     matrix[0][2*i - 2] += ((edge.Ni(0, e.A) + edge.Ni(0, e.B))*(me.Ni(0, e.A) + me.Ni(0, e.B)) +
//                                            edge.Ni(0, e.A) * me.Ni(0, e.A) + edge.Ni(0, e.B) * me.Ni(0, e.B)) / 6 * e.Length;
//                     matrix[1][2*i - 1] += ((edge.Ni(0, e.A) + edge.Ni(0, e.B))*(me.Ni(0, e.A) + me.Ni(0, e.B)) +
//                                            edge.Ni(0, e.A) * me.Ni(0, e.A) + edge.Ni(0, e.B) * me.Ni(0, e.B)) / 6 * e.Length;
//                     matrix[2][2*i - 2] += ((edge.Ni(1, e.A) + edge.Ni(1, e.B))*(me.Ni(0, e.A) + me.Ni(0, e.B)) +
//                                            edge.Ni(1, e.A) * me.Ni(0, e.A) + edge.Ni(1, e.B) * me.Ni(0, e.B)) / 6 * e.Length;
//                     matrix[3][2*i - 1] += ((edge.Ni(1, e.A) + edge.Ni(1, e.B))*(me.Ni(0, e.A) + me.Ni(0, e.B)) +
//                                            edge.Ni(1, e.A) * me.Ni(0, e.A) + edge.Ni(1, e.B) * me.Ni(0, e.B)) / 6 * e.Length;
// 
//                     matrix[0][2*i] += ((edge.Ni(0, e.A) + edge.Ni(0, e.B))*(me.Ni(1, e.A) + me.Ni(1, e.B)) +
//                                        edge.Ni(0, e.A) * me.Ni(1, e.A) + edge.Ni(0, e.B) * me.Ni(1, e.B)) / 6 * e.Length;
//                     matrix[1][2*i + 1] += ((edge.Ni(0, e.A) + edge.Ni(0, e.B))*(me.Ni(1, e.A) + me.Ni(1, e.B)) +
//                                            edge.Ni(0, e.A) * me.Ni(1, e.A) + edge.Ni(0, e.B) * me.Ni(1, e.B)) / 6 * e.Length;
//                     matrix[2][2*i] += ((edge.Ni(1, e.A) + edge.Ni(1, e.B))*(me.Ni(1, e.A) + me.Ni(1, e.B)) +
//                                        edge.Ni(1, e.A) * me.Ni(1, e.A) + edge.Ni(1, e.B) * me.Ni(1, e.B)) / 6 * e.Length;
//                     matrix[3][2*i + 1] += ((edge.Ni(1, e.A) + edge.Ni(1, e.B))*(me.Ni(1, e.A) + me.Ni(1, e.B)) +
//                                            edge.Ni(1, e.A) * me.Ni(1, e.A) + edge.Ni(1, e.B) * me.Ni(1, e.B)) / 6 * e.Length;


                    matrix[0][2*i - 2] += (2*(edge.Ni(0, e.B) - edge.Ni(0, e.A))*(me.Ni(0, e.B) - me.Ni(0, e.A)) +
                                           3*(edge.Ni(0, e.B)*me.Ni(0, e.A) + edge.Ni(0, e.A)*me.Ni(0, e.B)))/6*e.Length;
                    matrix[1][2*i - 1] += (2*(edge.Ni(0, e.B) - edge.Ni(0, e.A))*(me.Ni(0, e.B) - me.Ni(0, e.A)) +
                                           3*(edge.Ni(0, e.B)*me.Ni(0, e.A) + edge.Ni(0, e.A)*me.Ni(0, e.B)))/6*e.Length;
                    matrix[2][2*i - 2] += (2*(edge.Ni(1, e.B) - edge.Ni(1, e.A))*(me.Ni(0, e.B) - me.Ni(0, e.A)) +
                                           3*(edge.Ni(1, e.B)*me.Ni(0, e.A) + edge.Ni(1, e.A)*me.Ni(0, e.B)))/6*e.Length;
                    matrix[3][2*i - 1] += (2*(edge.Ni(1, e.B) - edge.Ni(1, e.A))*(me.Ni(0, e.B) - me.Ni(0, e.A)) +
                                           3*(edge.Ni(1, e.B)*me.Ni(0, e.A) + edge.Ni(1, e.A)*me.Ni(0, e.B)))/6*e.Length;

                    matrix[0][2*i] += (2*(edge.Ni(0, e.B) - edge.Ni(0, e.A))*(me.Ni(1, e.B) - me.Ni(1, e.A)) +
                                       3*(edge.Ni(0, e.B)*me.Ni(1, e.A) + edge.Ni(0, e.A)*me.Ni(1, e.B)))/6*e.Length;
                    matrix[1][2*i + 1] += (2*(edge.Ni(0, e.B) - edge.Ni(0, e.A))*(me.Ni(1, e.B) - me.Ni(1, e.A)) +
                                           3*(edge.Ni(0, e.B)*me.Ni(1, e.A) + edge.Ni(0, e.A)*me.Ni(1, e.B)))/6*e.Length;
                    matrix[2][2*i] += (2*(edge.Ni(1, e.B) - edge.Ni(1, e.A))*(me.Ni(1, e.B) - me.Ni(1, e.A)) +
                                       3*(edge.Ni(1, e.B)*me.Ni(1, e.A) + edge.Ni(1, e.A)*me.Ni(1, e.B)))/6*e.Length;
                    matrix[3][2*i + 1] += (2*(edge.Ni(1, e.B) - edge.Ni(1, e.A))*(me.Ni(1, e.B) - me.Ni(1, e.A)) +
                                           3*(edge.Ni(1, e.B)*me.Ni(1, e.A) + edge.Ni(1, e.A)*me.Ni(1, e.B)))/6*e.Length;
                }

            }
            {
                Edge me = new LinearEdge(vertexes[vertexes.Count - 2], vertexes[vertexes.Count - 1]);
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

                     matrix[0][2 * (vertexes.Count - 3)] += (edge.Ni(0, e.A) + edge.Ni(0, e.B)) / 2 * e.Length;
                     matrix[1][2 * (vertexes.Count - 3) + 1] += (edge.Ni(0, e.A) + edge.Ni(0, e.B)) / 2 * e.Length;
                     matrix[2][2 * (vertexes.Count - 3)] += (edge.Ni(1, e.A) + edge.Ni(1, e.B)) / 2 * e.Length;
                     matrix[3][2 * (vertexes.Count - 3) + 1] += (edge.Ni(1, e.A) + edge.Ni(1, e.B)) / 2 * e.Length;

                }
            }
            return matrix;
        }
        private double V(int i, Edge edge, Vertex v)
        {
            if (!edge.hasVertex(v)) return 0;
            if (i != 0 && i != 1) return 0;
            return (v - edge[(i + 1) % 2]).Length / edge.Length;
        }
        private double N(int i, Edge edge, Vertex v)
        {
            if (!edge.hasVertex(v)) return 0;
            if (i != 0 && i != 1) return 0;
            return (v - edge[(i + 1)%2]).Length/edge.Length;
        }
    }
}
