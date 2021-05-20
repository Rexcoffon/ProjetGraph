using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetGraph
{
    public static class DFS
    {
        static List<Node> n = new List<Node>();
        static bool find = false;
        public static void RunDFS(List<Node> nodes, ShortestPathResult shortestPath)
        {
            bool[] vis = new bool[nodes.Count + 1];
            Array.Fill(vis, false);

            n = nodes;

            DFSReq(vis, shortestPath.FromNodeId, shortestPath.ToNodeId, shortestPath.Path, shortestPath.Distance);
        }

        static void DFSReq(bool[] vis, int x, int y, List<int> stack, int distance)
        {
            stack.Add(x);
            if (x == y)
            {
                find = true;
                return;
            }
            vis[x] = true;
  
            if (n[x].Edges.Count > 0)
            {
                foreach (var edge in n[x].Edges)
                {
                    if (vis[edge.Child.Id] == false && !find)
                    {                        
                        DFSReq(vis, edge.Child.Id, y, stack, distance) ;
                    }                    
                }
            }
            
        }
    }
}
