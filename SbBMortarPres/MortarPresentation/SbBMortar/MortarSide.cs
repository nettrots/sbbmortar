using System.Collections.Generic;

namespace SbBMortar.SbB
{
    public class MortarSide
    {
        #region Fields
        private Vertex[] nodes;
        private SubDomain[] mortars;
        private SubDomain[] nonmortars;
        private List<Vertex> vertexes;
        #endregion

        #region Constructors
        public MortarSide(Vertex[] nodes, SubDomain[] mortars, SubDomain[] nonmortars)
        {
            this.nodes = nodes;
            this.mortars = mortars;
            this.nonmortars = nonmortars;
        }
        #endregion

        #region Properties
        public SubDomain[] Mortars
        {
            get { return mortars; }
            set { mortars = value; }

        }
        public SubDomain[] Nonmortars
        {
            get { return nonmortars; }
            set { nonmortars = value; }
        }
        public int NodesCount
        {
            get { return nodes.Length; }
        }
        #endregion

        #region Methods
        public Edge edge(int i)
        { 
            return new Edge(nodes[i], nodes[i+1]);
        }
        public void createMortarNodes()
        {
            vertexes = new List<Vertex>();
            for (int i = 0; i < nodes.Length-1; i++)
            {
                Edge e = new Edge(nodes[i], nodes[i+1]);
                for (int j = 0; j < Mortars.Length; j++)
                {
                    int k = Mortars[j].Polygon.isEdgeOnPolygon(e);
                    if (k >= 0)
                    {
                        List<Vertex> localVertexes = new List<Vertex>();
                        foreach (FEMEdge femedge in Mortars[j].Boundaries[k])
                        {
                            for (int m = 0; m < femedge.NodesCount; m++)
                                if (!localVertexes.Contains(femedge[m])) localVertexes.Add(femedge[m]);
                        }
                        localVertexes.Sort();
                        if (localVertexes[0]!=e.A) localVertexes.Reverse();
                        for (int m = 0; m < localVertexes.Count-1; m++)
                            vertexes.Add(localVertexes[m]);
                        break;
                    }
                }
            }
            vertexes.Add(nodes[nodes.Length-1]);
            
        }
        public Mortar createMortar(int femnodescount)
        {
            return new LinearMortar(femnodescount, vertexes);
        }
        #endregion
    }
}