using System;
using System.IO;
using SbB.Geometry;

namespace SbB.FEM
{
    public delegate double Fx(double x);

    public delegate double Fxy(double x, double y);

    public delegate double[] Fmas(double x, double y);

    public enum ParamNum
    {
        FIRST,
        SEDOND
    }

    public class FunctionParams
    {
        public double[] interval;
        public double parameter;
        public ParamNum paramNum;
        public int pointsNum;

        public FunctionParams(double parameter, ParamNum paramNum, double[] interval, int pointsNum)
        {
            this.parameter = parameter;
            this.pointsNum = pointsNum;
            this.interval = interval;
            this.paramNum = paramNum;
        }

        public FunctionParams()
        {
            interval = new double[] {-1, 1};
            parameter = 0;
            paramNum = ParamNum.FIRST;
            pointsNum = 50;
        }

        public bool isInitialized()
        {
            return interval == null;
        }

        public void reInit(double parameter, ParamNum paramNum, double[] interval, int pointsNum)
        {
            this.parameter = parameter;
            this.pointsNum = pointsNum;
            this.interval = interval;
            this.paramNum = paramNum;
        }
    }

    public class FxyToFxConvertor
    {
        private Fxy fxy;
        private double param;
        private ParamNum paramnum;

        public FxyToFxConvertor(Fxy fxy, ParamNum paramnum, double param)
        {
            this.fxy = fxy;
            this.paramnum = paramnum;
            this.param = param;
        }

        public void reInit(Fxy fxy, ParamNum paramnum, double param)
        {
            this.fxy = fxy;
            this.paramnum = paramnum;
            this.param = param;
        }

        public double f(double x)
        {
            switch (paramnum)
            {
                case ParamNum.FIRST:
                    return fxy(param, x);
                case ParamNum.SEDOND:
                    return fxy(x, param);
                default:
                    return 0;
            }
        }
    }

    public class FmasToMasFConvector
    {
        private Fmas fmas;
        private int i;
        public FmasToMasFConvector(Fmas fmas)
        {
            this.fmas = fmas;
            i = 0;
        }

        public Fxy this[int index]
        {
            get
            {
                i = index;
                return fi;
            }
        }
        public double fi(double x,double y)
        {
            return fmas(x, y)[i];
        }
    }

    public class Manager
    {
        private Domain domain;

        private GlobalSystem gs;
        private TextWriter sw;

        public Manager(Domain domain, TextWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");
            this.domain = domain;
            this.sw = sw;
        }

        public Manager(Domain domain)
        {
            this.domain = domain;
            sw = Console.Out;
        }

        public GlobalSystem Gs
        {
            get { return gs; }
        }

        public void collectData()
        {
            // Creating FEMData
            FEMData[] femdatas = new FEMData[domain.N];
            for (int i = 0; i < domain.N; i++)
                switch (domain[i].Order)
                {
                    case ApproximationOrder.LINEAR:
                        femdatas[i] = new LinearFEMData(domain[i]);
                        break;
                    case ApproximationOrder.QUADRATIC:
                        femdatas[i] = new QuadraticFEMData(domain[i]);
                        break;
                    default:
                        break;
                }
            // Creating FEMs
            FEM[] fems = new FEM[domain.N];
            for (int i = 0; i < fems.Length; i++)
                switch (domain[i].Order)
                {
                    case ApproximationOrder.LINEAR:
                        fems[i] = new LinearFEM(domain[i].YoungModulus, domain[i].PoissonRatio);
                        break;
                    case ApproximationOrder.QUADRATIC:
                        fems[i] = new QuadraticFEM(domain[i].YoungModulus, domain[i].PoissonRatio);
                        break;
                    default:
                        break;
                }
            // Creating Mortars

            #region Mortars

            Boundary[] mortarBoundary = new Boundary[domain.IBN];
            for (int i = 0; i < femdatas.Length; i++)
                foreach (Boundary boundary in femdatas[i].Boundaries)
                    if (boundary.BoundaryClass.type() == BoundaryType.MORTAR)
                        mortarBoundary[((MortarBoundary) boundary.BoundaryClass).N/2] = boundary;
            Mortar[] mortars = new Mortar[2*domain.IBN];
            for (int i = 0; i < domain.N; i++)
                for (int j = 0; j < domain[i].P.N; j++)
                    switch (domain[i].boundary(j).type())
                    {
                        case BoundaryType.MORTAR:
                            switch (domain[i].Order)
                            {
                                case ApproximationOrder.LINEAR:
                                    int n = ((MortarBoundary) domain[i].boundary(j)).N;
                                    mortars[n] = new LinearMortar(mortarBoundary[n/2]);
                                    break;
                                case ApproximationOrder.QUADRATIC:
                                    n = ((MortarBoundary)domain[i].boundary(j)).N;
                                    mortars[n] = new QuadraticMortar(mortarBoundary[n / 2]);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case BoundaryType.NONMORTAR:
                            switch (domain[i].Order)
                            {
                                case ApproximationOrder.LINEAR:
                                    int n = ((NonMortarBoundary) domain[i].boundary(j)).N;
                                    mortars[n] = new LinearMortar(mortarBoundary[n/2]);
                                    break;
                                case ApproximationOrder.QUADRATIC:
                                    n = ((NonMortarBoundary)domain[i].boundary(j)).N;
                                    mortars[n] = new QuadraticMortar(mortarBoundary[n / 2]);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }

            #endregion

            gs = new GlobalSystem(femdatas, fems, mortars);
        }

        public void solve()
        {
            Gs.solve();
        }

        public double u(Vertex vertex)
        {
            for (int i = 0; i < Gs.Femdatas.Length; i++)
                for (int j = 0; j < Gs.Femdatas[i].Triangles.Count; j++)
                    if (Gs.Femdatas[i].Triangles[j].hasVertex(vertex))
                    {
                        Triangle tr = Gs.Femdatas[i].Triangles[j];
                        double rez = 0.0;
                        for (int k = 0; k < tr.nodesCount(); k++)
                            rez += Gs.Result[2*tr[k].Number]*tr.Ni(k, vertex);
                        return rez;
                    }
            return 0;
        }

        public double u(double x, double y)
        {
            return u(new Vertex(x, y));
        }

        public double v(Vertex vertex)
        {
            for (int i = 0; i < Gs.Femdatas.Length; i++)
                for (int j = 0; j < Gs.Femdatas[i].Triangles.Count; j++)
                    if (Gs.Femdatas[i].Triangles[j].hasVertex(vertex))
                    {
                        Triangle tr = Gs.Femdatas[i].Triangles[j];
                        double rez = 0.0;
                        for (int k = 0; k < tr.nodesCount(); k++)
                            rez += Gs.Result[2*tr[k].Number + 1]*tr.Ni(k, vertex);
                        return rez;
                    }
            return 0;
        }

        public double v(double x, double y)
        {
            return v(new Vertex(x, y));
        }

        private double[] ES(double x, double y, int num)
        {
            double[] rez = new double[domain.N];
            for (int i = 0; i < domain.N; i++)
            {
                if (!domain[i].P.hasVertex(new Vertex(x, y)))
                {
                    rez[i] = double.NaN;
                    continue;
                }
                double pr = domain[i].PoissonRatio;
                double ym = domain[i].YoungModulus;
                int count = 0;
                foreach (Triangle tr in gs.Femdatas[i].Triangles)
                    if (tr.hasVertex(new Vertex(x,y)))
                    {
                        count++;
                        double[] uv = new double[2*tr.rank()];
                        int[] ind = tr.indexes();
                        for (int j = 0; j < ind.Length; j++)
                        {
                            uv[2*j] = gs.Result[2*ind[j]];
                            uv[2*j + 1] = gs.Result[2*ind[j] + 1];
                        }
                        rez[i] += tr.strainStres(x, y, ym, pr, uv)[num];
                        //gs.Femdatas[i].Triangles.Reset();
                        //break;
                    }
                rez[i] /= count;
            }
            return rez;
        }

        public double[] Exx(double x, double y)
        {
            return ES(x, y, 0);
        }

        public double[] Eyy(double x, double y)
        {
            return ES(x, y, 1);

        }
        public double[] Exy(double x, double y)
        {
            return ES(x, y, 2);

        }
        public double[] Sxx(double x, double y)
        {
            return ES(x, y, 3);

        }
        public double[] Syy(double x, double y)
        {
            return ES(x, y, 4);

        }
        public double[] Sxy(double x, double y)
        {
            return ES(x, y, 5);

        }
     
        public void uvTable()
        {
            sw.WriteLine("U: ");
            for (int i = 0; i < Gs.Result.Length/2; i++)
                sw.WriteLine("{0}:\t{2}{1:E}", Gs.Vertexes[i], Gs.Result[2*i], (Gs.Result[2*i] >= 0) ? " " : "");
            sw.WriteLine("\nV: ");
            for (int i = 0; i < Gs.Result.Length/2; i++)
                sw.WriteLine("{0}:\t{2}{1:E}", Gs.Vertexes[i], Gs.Result[2*i + 1], (Gs.Result[2*i + 1] >= 0) ? " " : "");
        }
    }
}