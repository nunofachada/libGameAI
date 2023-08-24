/* Copyright (c) 2018-2023 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System.Linq;
using System.Collections.Generic;
using Xunit;
using LibGameAI.PathFinding;

namespace Tests.PathFinding
{
    public class DijkstraPathFinderTests
    {
        // Returns a collection of (graph, shortest path) pairs
        public static IEnumerable<object[]> GetGraphsAndShortestPaths()
        {
            // A graph represented by an adjacency matrix and its shortest path
            yield return new object[]
            {
                // Graph represented by an adjacency matrix
                new Graph(new float[,]
                {
                    { 0.0f, 0.2f, 0.0f, 1.2f, 0.0f, 0.0f, 9.5f, 0.0f},
                    { 0.1f, 0.0f, 0.0f, 0.0f, 3.1f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 2.3f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                    { 4.5f, 0.0f, 0.0f, 0.0f, 3.5f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 2.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.7f, 0.0f, 5.2f},
                    { 0.0f, 0.0f, 0.0f, 0.0f, 0.3f, 0.0f, 0.0f, 0.0f}
                }),
                // Shortest path
                new (int from, int to)[]
                { (0, 1), (1, 4), (4, 2), (2, 5), (5, 7) }
            };
            // A graph represented by an adjacency list and its shortest path
            yield return new object[]
            {
                // Graph represented by an adjacency matrix
                new Graph(
                    new IConnection[][]
                    {
                        new IConnection[]
                        {
                            new Connection(1.3f, 0, 1),
                            new Connection(1.6f, 0, 2),
                            new Connection(3.3f, 0, 3)
                        },
                        new IConnection[]
                        {
                            new Connection(1.5f, 1, 4),
                            new Connection(1.9f, 1, 5)
                        },
                        new IConnection[] { new Connection(1.3f, 2, 3) },
                        new IConnection[] { },
                        new IConnection[] { },
                        new IConnection[] { new Connection(1.4f, 5, 6) },
                        new IConnection[] { }
                    }
                ),
                // Shortest path
                new (int from, int to)[]
                { (0, 1), (1, 5), (5, 6) }
            };
        }

        // Returns a collection of (graph, start, end) tuples without a path
        // between start and end nodes
        public static IEnumerable<object[]> GetGraphsNoPath()
        {
            // A graph represented by an adjacency matrix
            yield return new object[]
            {
                // Graph represented by an adjacency matrix
                new Graph(new float[,]
                {
                    { 0.0f, 1.5f, 0.0f },
                    { 0.5f, 0.0f, 0.0f },
                    { 2.1f, 1.7f, 0.0f }
                }),
                0,
                2
            };
            // A graph represented by an adjacency list
            yield return new object[]
            {
                // Graph represented by an adjacency matrix
                new Graph(
                    new IConnection[][]
                    {
                        new IConnection[] { new Connection(1.5f, 0, 1) },
                        new IConnection[] { new Connection(0.5f, 1, 0) },
                        new IConnection[]
                        {
                            new Connection(2.1f, 2, 0),
                            new Connection(1.7f, 2, 1)
                        }
                    }
                ),
                0,
                2
            };
        }

        // Check if shortest path is found with Dijkstra algorithm
        [Theory]
        [MemberData(nameof(GetGraphsAndShortestPaths))]
        public void TestFindPath_Find_Yes(
            IGraph graph, (int from, int to)[] sPath)
        {
            // Instantiate a Dijkstra path finder
            IPathFinder pathFinder = new DijkstraPathFinder();

            // Get shortest path
            IEnumerable<IConnection> sPathToTest =
                pathFinder.FindPath(
                    graph, sPath[0].from, sPath[sPath.Length - 1].to);

            // Check if actual shortest path was found
            Assert.Equal(
                sPath, sPathToTest.Select((c) => (c.FromNode, c.ToNode)));
        }

        // Check if shortest path is found with Dijkstra algorithm
        [Theory]
        [MemberData(nameof(GetGraphsNoPath))]
        public void TestFindPath_Find_No(IGraph graph, int from, int to)
        {
            // Instantiate a Dijkstra path finder
            IPathFinder pathFinder = new DijkstraPathFinder();

            // Get shortest path
            IEnumerable<IConnection> sPathToTest =
                pathFinder.FindPath(graph, from, to);

            // Check if actual shortest path was found
            Assert.Null(sPathToTest);
        }
    }
}
