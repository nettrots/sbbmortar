using System;
using System.Diagnostics;
using System.IO;
using MortarFEM.SbB.FEM.TriangulationLib;
using SbB.Collections;
using SbB.Geometry;

namespace SbB.FEM.TriangulationLib
{
    internal class ExeTriangulation : Triangulation
    {
        //to be overriden
        public override void SetTriangulationParams(double minAngle, double maxArea)
        {
            _minAngle = minAngle;
            _maxArea = maxArea;
        }

        public override void SetPolygon(Polygon polygon)
        {
            this.polygon = polygon;
        }

        public override bool triangulate()
        {
            if (polygon == null) return false;
            writeFile();
            //  Console.WriteLine("Triangulating, please wait...");
            if (!executeTriangle(_minAngle, _maxArea))
            {
                Console.WriteLine("Triangulation failed");
                return false;
            }
            // Console.WriteLine("Triangulation completed");

            //  Console.WriteLine("Transforming triangulation results...");
            readFile();
            // Console.WriteLine("Triangulation results have been transformed");

            File.Delete("C.poly");
            File.Delete("c.1.ele");
            File.Delete("c.1.node");
            File.Delete("c.1.poly");


            return true;
        }

        //private methods
        private void writeFile()
        {
            try
            {
                FileInfo f = new FileInfo("C.poly");
                StreamWriter writer = f.CreateText();
                writer.WriteLine("{0} 2 0 0", polygon.N);
                for (int i = 0; i < polygon.N; i++)
                {
                    string str = string.Format("{0} {1} {2} 0", i + 1, polygon[i].X, polygon[i].Y);
                    str = str.Replace(",", ".");
                    writer.WriteLine(str);
                }
                writer.WriteLine("{0} 1", polygon.N);
                for (int i = 0; i < polygon.N; i++)
                {
                    int j = (i + 1)%polygon.N;
                    writer.WriteLine("{0} {1} {2} {0}", i + 1, i + 1, j + 1, i + 1);
                }
                writer.WriteLine("0");
                writer.Close();
            }
            catch
            {
                throw new Exception("Disk error. Could not create file for triangulation.");
            }
        }

        private bool executeTriangle(double minAngle, double maxArea)
        {
            String option = " -pqc" + minAngle + 'a' + maxArea + "C c.poly";
            option = option.Replace(",", ".");
            Process newp = new Process();

            try
            {
                File.WriteAllBytes("triangle.exe", TriangleExe.triangle);

                newp.StartInfo.FileName = "triangle.exe";
                newp.StartInfo.Arguments = option;
                newp.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                newp.Start();
                newp.WaitForExit();

                File.Delete("triangle.exe");
                return File.Exists("c.1.node") && File.Exists("c.1.ele");
            }
            catch
            {
                File.Delete("C.poly");
                return false;
            }
        }

        private void readFile()
        {
            File.GetAccessControl("c.1.node");
            StreamReader reader;

            {
                reader = File.OpenText("c.1.node");
                string line = reader.ReadLine();
                string[] number = line.Split(new char[] {' '});
                int vertexNum = int.Parse(number[0]);

                vertexes = new Vertexes();
                Vertexes[] boundVertexes = new Vertexes[polygon.N];
                for (int i = 0; i < polygon.N; i++)
                    boundVertexes[i] = new Vertexes();

                for (int i = 0; i < vertexNum; i++)
                {
                    line = reader.ReadLine();
                    while (line.Contains("  ")) line = line.Replace("  ", " ");
                    if (line[0] == ' ') line = line.Remove(0, 1);
                    line = line.Replace(".", ",");
                    number = line.Split(new char[] {' '});
                    Vertex vertex =
                        new Vertex(double.Parse(number[1]), double.Parse(number[2]), int.Parse(number[0]) - 1);
                    vertexes.add(vertex);
                    int boundNum = int.Parse(number[3]);
                    if (boundNum != 0)
                        boundVertexes[boundNum - 1].add(vertex);
                }
                reader.Close();
                reader = File.OpenText("c.1.ele");
                line = reader.ReadLine();
                number = line.Split(new char[] {' '});
                int triangleNum = int.Parse(number[0]);

                triangles = new Triangles();

                for (int i = 0; i < triangleNum; i++)
                {
                    line = reader.ReadLine();
                    while (line.Contains("  ")) line = line.Replace("  ", " ");
                    if (line[0] == ' ') line = line.Remove(0, 1);
                    number = line.Split(new char[] {' '});
                    Vertex vi = vertexes[int.Parse(number[1]) - 1];
                    Vertex vj = vertexes[int.Parse(number[2]) - 1];
                    Vertex vm = vertexes[int.Parse(number[3]) - 1];
                    Triangle triangle = new Triangle(vi, vj, vm, int.Parse(number[0]) - 1);
                    triangles.Add(triangle);
                }

                reader.Close();

                for (int i = 0; i < polygon.N; i++)
                {
                    int j = (i + 1)%polygon.N;
                    if (!boundVertexes[i].contains(vertexes[i]))
                        boundVertexes[i].add(vertexes[i]);
                    if (!boundVertexes[i].contains(vertexes[j]))
                        boundVertexes[i].add(vertexes[j]);
                }

                for (int i = 0; i < polygon.N; i++)
                    boundVertexes[i].sort();
                vertexes.sort();
                triangles.Sort();
                foreach (Triangle tri in triangles)
                    tri.Sort();

                boundaries = new Boundaries();
                for (int i = 0; i < polygon.N; i++)
                {
                    Boundary boundary = new Boundary(i);
                    for (int j = 0; j < boundVertexes[i].Count - 1; j++)
                        boundary.Add(new Edge(boundVertexes[i][j], boundVertexes[i][j + 1]));
                    boundaries.add(boundary);
                }
            }
        }
    }
}