using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using MortarPresentation.SbBMortar;

namespace SbBMortar.SbB
{

    public class LinialTriangleTriangulation: Triangulation
    {
        private int elementsCount;

        #region Constructors
        public LinialTriangleTriangulation(Polygon polygon)
        {
            this.polygon = polygon;
        }
        #endregion

        #region Methods
        private void writeMainFile(double minAngle, double maxArea)
        {
            try
            {
                FileInfo f = new FileInfo("problem.m2");
                StreamWriter writer = f.CreateText();
                writer.WriteLine("202 1 1 0 0 0 1");
                writer.WriteLine("problem.rg2");
                writer.WriteLine("problem.cs2");
                writer.WriteLine("problem.mh2");
                writer.WriteLine("1.0e-10 0 0");
                // options
                writer.WriteLine("4 0 45.0 0.04 0.2 30.0 20.0 0.25 0.4 10 {0} 3 1 0.01 2 2 0.3", (int)(polygon.S/maxArea));
                writer.Close();
            }
            catch
            {
                throw new Exception("Disk error. Could not create file for triangulation.");
            }
        }
        private void writeRegionFile()
        {
            try
            {
                FileInfo f = new FileInfo("problem.rg2");
                StreamWriter writer = f.CreateText();
                writer.WriteLine("{0}", polygon.Count);
                for (int i = 0; i < polygon.Count; i++)
                {
                    writer.WriteLine("{0} {1} 2", polygon[i].X, polygon[i].Y);
                }
                writer.WriteLine();
                writer.WriteLine("0");
                writer.WriteLine();
                writer.WriteLine("1");
                writer.WriteLine();
                writer.WriteLine("{0} 1 0", polygon.Count);

                string s1="", s2="";
                for (int i = 0; i < polygon.Count; i++)
                {
                    s1 += (i + 1) + " ";
                    s2 += (20*(i+1)+2)+" ";
                }
                writer.WriteLine(s1);
                writer.WriteLine(s2);
                writer.Close();
            }
            catch
            {
                throw new Exception("Disk error. Could not create file for triangulation.");
            }
        }
        private void writeSurfaceFile()
        {
            try
            {
                FileInfo f = new FileInfo("problem.cs2");
                StreamWriter writer = f.CreateText();
                /*writer.WriteLine("{0}", polygon.Count);
                for (int i = 0; i < polygon.Count; i++)
                {
                    writer.WriteLine();
                    writer.WriteLine("1 0");
                    writer.WriteLine("{0} {1}", i+1, (i+2)%polygon.Count);
                }*/
                writer.WriteLine(polygon.Count);
                writer.WriteLine();
                for (int i = 0; i < polygon.Count; i++)
                {
                    writer.WriteLine("1 0");
                    writer.WriteLine("{0} {1}", (i + 1), (i + 1)%polygon.Count + 1);
                    writer.WriteLine();
                }
                writer.Close();
            }
            catch
            {
                throw new Exception("Disk error. Could not create file for triangulation.");
            }
        }
        private void writeFile(double minAngle, double maxArea)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            writeMainFile(minAngle, maxArea);
            writeRegionFile();
            writeSurfaceFile();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
        }
        private void executeTriangle(double minAngle, double maxArea)
        {
            String option = " problem.m2 problem.err";
            Process newp = new Process();

            try
            {
                newp.StartInfo.FileName = "zgp.exe";
                newp.StartInfo.Arguments = option;
                newp.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                newp.Start();
                newp.WaitForExit();

                if (!File.Exists("problem.mh2")) throw new Exception("Quadrangulation failed");
            }
            catch
            {
                throw ;
            }
        }
        private void readFile()
        {
            // Open file
            string currentCulture = CultureInfo.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            File.GetAccessControl("problem.mh2");
            StreamReader reader;

            reader = File.OpenText("problem.mh2");

            // read verteces's information
            string line = reader.ReadLine();
            int num = int.Parse(line);

            vertexes = new List<Vertex>();
            string[] numbers;

            for (int i = 0; i < num; i++)
            {
                line = reader.ReadLine();
                numbers = line.Split(' ');
                vertexes.Add(new Vertex(double.Parse(numbers[0]), double.Parse( numbers[1]), i));
            }

            // read verteces's extra-info
            reader.ReadLine();
            num = int.Parse(reader.ReadLine());
            for (int i = 0; i < num; i++)
                reader.ReadLine();

            // read elements's information
            reader.ReadLine();

            numbers = reader.ReadLine().Split(' ');
            num = int.Parse(numbers[1]);
            elements = new List<Element>();
            for (int i = 0; i < num; i++)
            {
                line = reader.ReadLine();
                numbers = line.Split(' ');
                Vertex[] v = new Vertex[3];
                v[0] = vertexes[int.Parse(numbers[1]) - 1];
                v[1] = vertexes[int.Parse(numbers[2]) - 1];
                v[2] = vertexes[int.Parse(numbers[3]) - 1];
                //v[3] = vertexes[int.Parse(numbers[4]) - 1];
                elements.Add(new LinearTriangle(v));
            }

            // read boundaries's information
            reader.ReadLine();

            boundaries = new List<FEMEdge>[polygon.Count];
            //List<Vertex>[] temp = new List<Vertex>[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
            {
                boundaries[i] = new List<FEMEdge>();
                //temp[i] = new List<Vertex>();
            }

            for (int i = 0; i < num; i++)
            {
                line = reader.ReadLine();
                numbers = line.Split(' ');

                for (int j = 2; j < 5; j++)
                {
                    int edgeinfo = int.Parse(numbers[j]);
                    if (edgeinfo%20 == 2)
                    {
                        edgeinfo = edgeinfo/20 - 1;
                        boundaries[edgeinfo].Add(new LinearEdge(elements[i][j-2], elements[i][j-1])); 
                    }
                }
            }

            reader.Close();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);

            /*for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1) % polygon.Count;
                if (!temp[i].Contains(vertexes[i]))
                    temp[i].Add(vertexes[i]);
                if (!temp[i].Contains(vertexes[j]))
                    temp[i].Add(vertexes[j]);
                temp[i].Sort();
            }
            for (int i = 0; i < boundaries.Length; i++)
            {
                for (int j = 0; j < temp[i].Count - 1; j++)
                    boundaries[i].Add(new LinearEdge(temp[i][j], temp[i][j + 1]));
            }

            for (int i = 0; i < boundaries.Length; i++)
                for (int j = 0; j < boundaries[i].Count; j++)
                    if (!polygon.hasVertex(boundaries[i][j].A) || !polygon.hasVertex(boundaries[i][j].B))
                        boundaries[i].RemoveAt(j--);
            */
        }


        public override void triangulate(double minAngle, double maxArea)
        {
            writeFile(minAngle, maxArea);
            executeTriangle(minAngle, maxArea);
            readFile();

            File.Delete("problem.cs2");
            File.Delete("problem.err");
            File.Delete("problem.m2");
            File.Delete("problem.mh2");
            File.Delete("problem.rg2");
        }
        #endregion
    }

    public class LinialTriangleTriangulation_old: Triangulation
    {
        #region Constructors
        public LinialTriangleTriangulation_old(Polygon polygon)
        {
            this.polygon = polygon;
        }
        #endregion

        #region Methods
        private void writeFile()
        {
            try
            {
                FileInfo f = new FileInfo("C.poly");
                StreamWriter writer = f.CreateText();
                writer.WriteLine("{0} 2 0 0", polygon.Count);
                for (int i = 0; i < polygon.Count; i++)
                {
                    string str = string.Format("{0} {1} {2} 0", i + 1, polygon[i].X, polygon[i].Y);
                    str = str.Replace(",", ".");
                    writer.WriteLine(str);
                }
                writer.WriteLine("{0} 1", polygon.Count);
                for (int i = 0; i < polygon.Count; i++)
                {
                    int j = (i + 1)%polygon.Count;
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
        private void executeTriangle(double minAngle, double maxArea)
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
                if (!File.Exists("c.1.node") && File.Exists("c.1.ele")) throw new Exception("Triangulation failed");
            }
            catch
            {
                throw new Exception("Triangulation failed");
            }
        }
        private void readFile()
        {
            File.GetAccessControl("c.1.node");
            StreamReader reader;

            reader = File.OpenText("c.1.node");
            string line = reader.ReadLine();
            string[] number = line.Split(new char[] {' '});
            int vertexNum = int.Parse(number[0]);

            vertexes = new List<Vertex>();
            boundaries = new List<FEMEdge>[polygon.Count];
            List<Vertex>[] temp = new List<Vertex>[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
            {
                boundaries[i] = new List<FEMEdge>();
                temp[i] = new List<Vertex>();
            }

            for (int i = 0; i < vertexNum; i++)
            {
                line = reader.ReadLine();
                while (line.Contains("  ")) line = line.Replace("  ", " ");
                if (line[0] == ' ') line = line.Remove(0, 1);
                line = line.Replace(".", ",");
                number = line.Split(new char[] {' '});
                Vertex vertex =
                    new Vertex(double.Parse(number[1]), double.Parse(number[2]), int.Parse(number[0]) - 1);
                vertexes.Add(vertex);
                int boundNum = int.Parse(number[3]);
                if (boundNum != 0)
                    temp[boundNum - 1].Add(vertex);
            }
            reader.Close();

            reader = File.OpenText("c.1.ele");
            line = reader.ReadLine();
            number = line.Split(new char[] {' '});
            int triangleNum = int.Parse(number[0]);
            elements = new List<Element>();
            for (int i = 0; i < triangleNum; i++)
            {
                line = reader.ReadLine();
                while (line.Contains("  ")) line = line.Replace("  ", " ");
                if (line[0] == ' ') line = line.Remove(0, 1);
                number = line.Split(new char[] {' '});
                Vertex vi = vertexes[int.Parse(number[1]) - 1];
                Vertex vj = vertexes[int.Parse(number[2]) - 1];
                Vertex vm = vertexes[int.Parse(number[3]) - 1];
                elements.Add(new LinearTriangle(vi, vj, vm, int.Parse(number[0]) - 1));
            }
            reader.Close();

            for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1)%polygon.Count;
                if (!temp[i].Contains(vertexes[i]))
                    temp[i].Add(vertexes[i]);
                if (!temp[i].Contains(vertexes[j]))
                    temp[i].Add(vertexes[j]);
                temp[i].Sort();
            }
            for (int i = 0; i < boundaries.Length; i++)
            {
                for (int j = 0; j < temp[i].Count - 1; j++)
                    boundaries[i].Add(new LinearEdge(temp[i][j], temp[i][j + 1]));
            }

            for (int i = 0; i < boundaries.Length; i++)
                for (int j = 0; j < boundaries[i].Count; j++)
                    if (!polygon.hasVertex(boundaries[i][j].A) || !polygon.hasVertex(boundaries[i][j].B))
                        boundaries[i].RemoveAt(j--);

            for (int i = 0; i < elements.Count; i++)
                if (!polygon.hasElement(elements[i]))
                    elements.RemoveAt(i--);

            for (int i = 0; i < vertexes.Count; i++)
                if (!polygon.hasVertex(vertexes[i]))
                    vertexes.RemoveAt(i--);
        }

        public override void triangulate(double minAngle, double maxArea)
        {
            writeFile();
            executeTriangle(minAngle, maxArea);
            readFile();

            File.Delete("C.poly");
            File.Delete("c.1.ele");
            File.Delete("c.1.node");
            File.Delete("c.1.poly");
        }
        #endregion
    }
}