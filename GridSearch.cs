using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Source https://github.com/lordjesus/Packt-Introduction-to-graph-algorithms-for-game-developers
/// </summary>
public class GridSearch {

    public struct SearchResult
    {
        public List<Point> Path { get; set; }
    }

    public static List<Point> AStarSearch(Grid grid, Point startPosition, Point endPosition, bool isAgent = false)
    {
        List<Point> path = new List<Point>();

        List<Point> positionsTocheck = new List<Point>();
        Dictionary<Point, float> costDictionary = new Dictionary<Point, float>();
        Dictionary<Point, float> priorityDictionary = new Dictionary<Point, float>();
        Dictionary<Point, Point> parentsDictionary = new Dictionary<Point, Point>();

        positionsTocheck.Add(startPosition);
        priorityDictionary.Add(startPosition, 0);
        costDictionary.Add(startPosition, 0);
        parentsDictionary.Add(startPosition, null);

        while (positionsTocheck.Count > 0)
        {
            Point current = GetClosestVertex(positionsTocheck, priorityDictionary);
            positionsTocheck.Remove(current);
            if (current.Equals(endPosition))
            {
                path = GeneratePath(parentsDictionary, current);
                return path;
            }

            foreach (Point neighbour in grid.GetAdjacentCells(current, isAgent))
            {
                float newCost = costDictionary[current] + grid.GetCostOfEnteringCell(neighbour);
                if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                {
                    costDictionary[neighbour] = newCost;

                    float priority = newCost + ManhattanDiscance(endPosition, neighbour);
                    positionsTocheck.Add(neighbour);
                    priorityDictionary[neighbour] = priority;

                    parentsDictionary[neighbour] = current;
                }
            }
        }
        return path;
    }

    private static Point GetClosestVertex(List<Point> list, Dictionary<Point, float> distanceMap)
    {
        Point candidate = list[0];
        foreach (Point vertex in list)
        {
            if (distanceMap[vertex] < distanceMap[candidate])
            {
                candidate = vertex;
            }
        }
        return candidate;
    }

    private static float ManhattanDiscance(Point endPos, Point point)
    {
        return Math.Abs(endPos.X - point.X) + Math.Abs(endPos.Y - point.Y);
    }

    public static List<Point> GeneratePath(Dictionary<Point, Point> parentMap, Point endState)
    {
        List<Point> path = new List<Point>();
        Point parent = endState;
        while (parent != null && parentMap.ContainsKey(parent))
        {
            path.Add(parent);
            parent = parentMap[parent];
        }
        return path;
    }
}
