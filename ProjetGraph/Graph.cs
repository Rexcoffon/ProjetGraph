using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetGraph
{
    public class Graph
    {
        public List<Node> Nodes { get; set; }

        public Node Root;
        public List<Node> AllNodes = new List<Node>();

        public int[,] Matrix;

        public Node CreateRoot(string name)
        {
            Root = CreateNode(name);
            return Root;
        }

        public Node CreateNode(string name)
        {
            var n = new Node(name);
            AllNodes.Add(n);
            return n;
        }

        public void CreateAdjMatrix()
        {
            int[,] adj = new int[AllNodes.Count, AllNodes.Count];

            for (int i = 0; i < AllNodes.Count; i++)
            {
                Node n1 = AllNodes[i];
                n1.Id = i;

                for (int j = 0; j < AllNodes.Count; j++)
                {
                    Node n2 = AllNodes[j];
                    n2.Id = j;

                    var edge = n1.Edges.FirstOrDefault(a => a.Child == n2);

                    if (edge != null)
                    {
                        adj[i, j] = edge.Weigth;
                    }
                    else
                    {
                        adj[i, j] = 0;
                    }
                }
            }
            Matrix = adj;
        }
        
    }
}
