using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetGraph
{
    public class Node
    {
        public int Id;
        public string Name;
        public List<Edge> Edges = new List<Edge>();

        public Node(string name)
        {
            Name = name;
        }

        public Node AddEdge(Node child, int w)
        {
            Edges.Add(new Edge
            {
                Parent = this,
                Child = child,
                Weigth = w
            });

            if (!child.Edges.Exists(a => a.Parent == child && a.Child == this))
            {
                child.AddEdge(this, w);
            }

            return this;
        }
    }
}
