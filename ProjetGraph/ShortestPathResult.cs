using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetGraph
{
    public class ShortestPathResult
    {
        public ShortestPathResult()
        {
            Path = new List<int>();
        }

        public int Distance { get; set; }

        public int FromNodeId { get; set; }

        public int ToNodeId { get; set; }

        public List<int> Path { get; set; }
    }
}
