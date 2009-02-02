using SbB.Collections;
using SbB.Geometry;

namespace SbB.FEM.TriangulationLib
{
    public class Triangulation
    {
        
        protected Polygon polygon;
        protected double _minAngle;
        protected double _maxArea;

        protected Vertexes vertexes;
        protected Triangles triangles;
        protected Boundaries boundaries;

        //Public Properties(result)
        public Vertexes VertexColection
        {
            get { return vertexes; }
        }
        public Triangles TriangleColection
        {
            get { return triangles; }
        }
        public Boundaries BoundaryColection
        {
            get { return boundaries; }
        }

        //Singleton Pattern
        public static Triangulation Triangulator
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExeTriangulation();
                }
                return instance;
            }
        }                 //Default Triangulator
        protected Triangulation() {}
        private static Triangulation instance;                      //Triangulator

        public static Triangulation advTriangulator(string _id)
        {
            if ((_id != triangulatorName) || (instance == null))
            {
                if (_id == "triangle.exe")
                {
                    instance = new ExeTriangulation();
                    triangulatorName = _id;
                }
                //...
                //...
                if (instance == null)
                {
                    instance = new Triangulation();
                    triangulatorName = "";
                }
            }

            return instance;
        }
        private static string triangulatorName;

        //Virtual methods
        public virtual void SetPolygon(Polygon polygon){}
        public virtual void SetTriangulationParams(double minAngle, double maxArea){}
        public virtual bool triangulate()
        {
            return false;
        }
        
        



#region ForFuture 
        /*
        static void Register(String _id,Triangulation tr){}
        public static Triangulation advTriangulator(string _id)
        {
            if(instance==null)
            {
                instance = Lookup(_id);
                
            }
            return instance;
        }
        protected Triangulation Lookup(string _id) {
            return null;
        }

        private static Pair _idTringul;*/ 
#endregion
    }
}
