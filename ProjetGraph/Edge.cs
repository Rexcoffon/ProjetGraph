using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetGraph
{
    public class Edge
    {
        public int Weigth { get; set; }
        public Node Parent { get; set; }
        public Node Child { get; set; }
    }
}
