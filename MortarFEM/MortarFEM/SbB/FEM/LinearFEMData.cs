using SbB.FEM.TriangulationLib;
using SbB.Geometry;

namespace SbB.FEM
{
    public class LinearFEMData:FEMData
    {

        public LinearFEMData(SubDomain subdomain) : base(subdomain)
        {
            youngModulus = subdomain.YoungModulus;
            poissonRatio = subdomain.PoissonRatio;

            Triangulation.Triangulator.SetPolygon(subdomain.P);
            Triangulation.Triangulator.SetTriangulationParams(subdomain.MinAngle,subdomain.MaxArea);
            if (Triangulation.Triangulator.triangulate())
            {
                triangles = Triangulation.Triangulator.TriangleColection;
                for (int i = 0; i < triangles.Count; i++)
                {
                    triangles[i] = new LinearTriangle(triangles[i]);
                }

                vertexes = Triangulation.Triangulator.VertexColection;

                boundaries = Triangulation.Triangulator.BoundaryColection;
                for (int i = 0; i < boundaries.Count; i++)
                {
                    boundaries[i].BoundaryClass= subdomain.boundary(i);
                    for (int j = 0; j < boundaries[i].Count; j++)
                        boundaries[i][j] = new LinearEdge(boundaries[i][j]);
                }
            }
            
        }

    }
}
