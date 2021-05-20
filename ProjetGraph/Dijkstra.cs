using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetGraph
{
    public static class Dijkstra
    {
        private static readonly int NO_PARENT = -1;

        public static List<ShortestPathResult> RunDijkstra(int[,] adjacencyMatrix, int startVertex)
        {
            int nVertices = adjacencyMatrix.GetLength(0);
            int[] shortestDistances = new int[nVertices];

            bool[] added = new bool[nVertices];


            for (int vertexIndex = 0; vertexIndex < nVertices;
                                                vertexIndex++)
            {
                shortestDistances[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            shortestDistances[startVertex] = 0;

            int[] parents = new int[nVertices];

            parents[startVertex] = NO_PARENT;

            for (int i = 1; i < nVertices; i++)
            {
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    if (!added[vertexIndex] &&
                        shortestDistances[vertexIndex] <
                        shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }
                added[nearestVertex] = true;

                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                    if (edgeDistance > 0
                        && ((shortestDistance + edgeDistance) <
                            shortestDistances[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        shortestDistances[vertexIndex] = shortestDistance +
                                                        edgeDistance;
                    }
                }
            }

            return FormatResult(startVertex, shortestDistances, parents);
        }

        private static List<ShortestPathResult> FormatResult(int startVertex, int[] distances, int[] parents)
        {
            List<ShortestPathResult> results = new List<ShortestPathResult>();
            int nVertices = distances.Length;


            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {

                if (vertexIndex != startVertex)
                {
                    var result = new ShortestPathResult()
                    {
                        FromNodeId = startVertex,
                        ToNodeId = vertexIndex,
                        Distance = distances[vertexIndex]
                    };

                    FormatPath(vertexIndex, parents, result.Path);

                    results.Add(result);
                }
            }
            return results;
        }

        private static void FormatPath(int currentVertex, int[] parents, List<int> path)
        {
            if (currentVertex == NO_PARENT)
            {
                return;
            }
            FormatPath(parents[currentVertex], parents, path);
            path.Add(currentVertex);
        }
    }
}
