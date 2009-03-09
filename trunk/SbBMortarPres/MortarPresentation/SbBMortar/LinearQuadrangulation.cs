using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace SbBMortar.SbB
{
    public class LinearQuadrangulation: Triangulation
    {
        #region Constructors
        public LinearQuadrangulation(Polygon polygon)
        {
            this.polygon = polygon;
        }
        #endregion

        #region Methods
        private void writeMainFile()
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
                writer.WriteLine("4 0 45.0 0.04 0.2 30.0 20.0 0.25 0.4 10 200 4 1 0.01 2 2 0.3");
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
                    s2 += "2 ";
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
                writer.WriteLine("0");
                writer.Close();
            }
            catch
            {
                throw new Exception("Disk error. Could not create file for triangulation.");
            }
        }
        private void writeFile()
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            writeMainFile();
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
            string currentCulture = CultureInfo.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            File.GetAccessControl("problem.mh2");
            StreamReader reader;

            reader = File.OpenText("problem.mh2");
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


            boundaries = new List<FEMEdge>[polygon.Count];
            List<Vertex>[] temp = new List<Vertex>[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
            {
                boundaries[i] = new List<FEMEdge>();
                temp[i] = new List<Vertex>();
            }

            reader.ReadLine();
            num = int.Parse(reader.ReadLine());
            for (int i = 0; i < num; i++)
                reader.ReadLine();

            reader.ReadLine();

            numbers = reader.ReadLine().Split(' ');
            num = int.Parse(numbers[1]);
            elements = new List<Element>();
            for (int i = 0; i < num; i++)
            {
                line = reader.ReadLine();
                numbers = line.Split(' ');
                Vertex[] v = new Vertex[4];
                v[0] = vertexes[int.Parse(numbers[1]) - 1];
                v[1] = vertexes[int.Parse(numbers[2]) - 1];
                v[2] = vertexes[int.Parse(numbers[3]) - 1];
                v[3] = vertexes[int.Parse(numbers[4]) - 1];
                elements.Add(new LinearQuadrangle(v));
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
            writeFile();
            executeTriangle(minAngle, maxArea);
            readFile();
        }
        #endregion
    }
}
