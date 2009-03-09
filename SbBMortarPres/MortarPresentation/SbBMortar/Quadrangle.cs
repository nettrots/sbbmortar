using System;
using System.Collections.Generic;
using System.Text;

namespace SbBMortar.SbB
{
    public abstract class Quadrangle: Element
    {
        #region Methods
        public override bool hasVertex(Vertex v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
