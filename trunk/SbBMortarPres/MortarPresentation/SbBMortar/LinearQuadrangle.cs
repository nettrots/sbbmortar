using System;
using System.Collections.Generic;
using System.Text;

namespace SbBMortar.SbB
{
    public class LinearQuadrangle: Quadrangle
    {
        #region Constructors
        public LinearQuadrangle(Vertex a, Vertex b, Vertex c, Vertex d) : this(a, b, c, d, 0) {}
        public LinearQuadrangle(Vertex a, Vertex b, Vertex c, Vertex d, int number)
        {
            this.number = number;
            nodes = new Vertex[] {a,b,c,d};
        }
        public LinearQuadrangle(Vertex[] vertexes) : this(vertexes, 0) {}
        public LinearQuadrangle(Vertex[] vertexes, int number)
        {
            if (vertexes.Length>4) throw new Exception("Too much elements in 'vertexes'");
            if (vertexes.Length<4) throw new Exception("Not enought elements in 'vertexes'");
            nodes = new Vertex[] {vertexes[0], vertexes[1], vertexes[2], vertexes[3]};
            this.number = number;
        }
        #endregion

        #region Methods
        public override void FEM(Matrix K, Matrix D)
        {
            throw new NotImplementedException();
        }

        public override double phi(int i, Vertex v)
        {
            throw new NotImplementedException();
        }

        public override double[] dphi(int i, Vertex v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
