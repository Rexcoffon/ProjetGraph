using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjetGraph
{
    class Program
    {
        static long dijkstraConstTime;
        static long DFSCalcTime;

        static void Main(string[] args)
        {
            var graph = InitializeGraph();
            var matrix = graph.Matrix;

            Console.WriteLine("Saisissez la ville de départ (Voir liste des villes vill.txt) :");
            string startCity = string.Empty;
            Node startNode = null;
            do
            {
                startCity = Console.ReadLine();
                startNode = graph.AllNodes.Find(n => n.Name == startCity);
                if (startCity == null)
                {
                    Console.WriteLine("Ville incorrecte");
                }

            } while (startCity == string.Empty && startNode == null);

            Console.WriteLine("Saisissez la ville d'arriver :");
            string endCity = string.Empty;
            Node endNode = null;
            do
            {
                endCity = Console.ReadLine();
                endNode = graph.AllNodes.Find(n => n.Name == endCity);
                if (endNode == null)
                {
                    Console.WriteLine("Ville incorrecte");
                }

            } while (startCity == string.Empty || endNode == null);


            Stopwatch stopwatch = new Stopwatch();


            // Dijkstra
            stopwatch.Start();
            var dijkstra = Dijkstra.RunDijkstra(matrix, startNode.Id);
            var dijkstraRes = dijkstra.Find(r => r.ToNodeId == endNode.Id);
            stopwatch.Stop();
            dijkstraConstTime = stopwatch.ElapsedTicks;


            //DFS
            var dfsResult = new ShortestPathResult()
            {
                FromNodeId = startNode.Id,
                ToNodeId = endNode.Id
            };

            stopwatch.Reset();
            stopwatch.Start();
            DFS.RunDFS(graph.AllNodes, dfsResult);
            var edges = graph.AllNodes.Select(n => n.Edges).SelectMany(i => i).Distinct().ToList();
            for (int i = 0; i < dfsResult.Path.Count - 1; i++)
            {
                var n1 = dfsResult.Path[i];
                var n2 = dfsResult.Path[i + 1];

                dfsResult.Distance += edges.Find(e => e.Parent.Id == n1 && e.Child.Id == n2)?.Weigth ?? 0;
            }
            stopwatch.Stop();
            DFSCalcTime = stopwatch.ElapsedTicks;


            // Result
            Console.WriteLine("Dijkstra :");
            PrintInfo(dijkstraRes, graph.AllNodes);
            Console.WriteLine();
            Console.WriteLine("Time : " + dijkstraConstTime);

            Console.WriteLine("=========================================");

            Console.WriteLine("DFS :");
            PrintInfo(dfsResult, graph.AllNodes);
            Console.WriteLine();
            Console.WriteLine("Time : " + DFSCalcTime);
        }

        static Graph InitializeGraph()
        {
            var graph = new Graph();

            var a = graph.CreateRoot("Annecy");
            var b = graph.CreateNode("Lyon");
            var c = graph.CreateNode("Grenoble");
            var d = graph.CreateNode("Aix-les-Bains");
            var e = graph.CreateNode("Chamonix-Mont-Blanc");
            var f = graph.CreateNode("Villeurbanne");
            var g = graph.CreateNode("Chambery");
            var h = graph.CreateNode("Valence");
            var i = graph.CreateNode("Bourg-En-Bresse");
            var j = graph.CreateNode("Saint-Etienne");
            var k = graph.CreateNode("Auriac");
            var l = graph.CreateNode("Vichy");
            var m = graph.CreateNode("Saint-Julien-En-Genevois");
            var n = graph.CreateNode("Albertville");
            var o = graph.CreateNode("Saint-Jean-De-Maurienne");
            var p = graph.CreateNode("Vienne");

            // Annecy
            a.AddEdge(d, 35)
             .AddEdge(e, 94);

            // Lyon
            b.AddEdge(j, 81)
             .AddEdge(p, 47)
             .AddEdge(h, 123);

            //Grenoble
            c.AddEdge(b, 113)
             .AddEdge(g, 58);

            //Aix-les-Bains
            d.AddEdge(g, 19);

            //Villeurbanne
            f.AddEdge(b, 8);

            //Chambery
            g.AddEdge(o, 74)
             .AddEdge(n, 52);

            //Valence
            h.AddEdge(p, 73);

            //Bourg-En-Bresse
            i.AddEdge(k, 80);

            //Saint-Etienne
            j.AddEdge(o, 239)
             .AddEdge(l, 143);

            //Saint-Julien-En-Genevois
            m.AddEdge(a, 34)
             .AddEdge(i, 99);

            graph.CreateAdjMatrix();

            return graph;
        }

        private static void PrintInfo(ShortestPathResult pathResult, List<Node> nodes)
        {
            Console.WriteLine(nodes.Find(n => n.Id == pathResult.FromNodeId)?.Name + "->" + nodes.Find(n => n.Id == pathResult.ToNodeId)?.Name);
            Console.WriteLine("Distance : " + pathResult.Distance + "km");
            PrintPath(pathResult.Path, nodes);
        }

        public static void PrintPath(List<int> path, List<Node> nodes)
        {
            foreach (var item in path)
            {
                Console.Write(nodes.First(n => n.Id == item).Name + " ");
            }

        }
    }
}
