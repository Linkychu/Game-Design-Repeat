using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsAlgorithm : Maze
{
    public override void Generate()
    {
        Vector2Int position = new Vector2Int(RNG.RngCallRange(0, size.x), RNG.RngCallRange(0, size.y));

        map[position.x, position.y] = 0;

        List<MapLocation> walls = new List<MapLocation>();
        
        walls.Add(new MapLocation(new Vector2Int(position.x + 1, position.y)));
        walls.Add(new MapLocation(new Vector2Int(position.x -1, position.y)));
        walls.Add(new MapLocation(new Vector2Int(position.x , position.y + 1)));
        walls.Add(new MapLocation(new Vector2Int(position.x, position.y - 1)));

        int countLoops = 0;
        while (walls.Count > 0 && countLoops < 5000)
        {
            int rWall = RNG.RngCallRange(0, walls.Count);
            position.x = walls[rWall].vector2.x;
            position.y = walls[rWall].vector2.y;
            walls.RemoveAt(rWall);
            if (CountSquareNeighbours(position.x, position.y) == 1)
            {
                map[position.x, position.y] = 0;
                walls.Add(new MapLocation( new Vector2Int(position.x + 1, position.y)));
                walls.Add(new MapLocation(new Vector2Int(position.x -1, position.y)));
                walls.Add(new MapLocation(new Vector2Int(position.x , position.y + 1)));
                walls.Add(new MapLocation(new Vector2Int(position.x, position.y - 1)));
            }
            countLoops++;
        }
    }
}
