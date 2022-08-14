using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMarker
{
    public MapLocation Location;
    public float G;
    public float H;
    public float F;
    public PathMarker parent;

    
    public PathMarker(MapLocation l, float g, float h, float f,  PathMarker pathmarker)
    {
        Location = l;
        G = g;
        H = h;
        F = f;
        parent = pathmarker;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        else
        {
            return Location.Equals(((PathMarker) obj).Location);    
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

public class AStarAlgorithm : MonoBehaviour
{
   public Maze maze;

    private List<PathMarker> open = new List<PathMarker>();
    private List<PathMarker> closed = new List<PathMarker>();

    public PathMarker goalNode;
    public PathMarker startNode;

    private PathMarker lastPos;
    private bool isDone = false;

    public Vector3 startLocation;
    public Vector3 goalLocation;


    public bool validPath;

    void BeginSearch()
    {
        isDone = false;

        List<MapLocation> locations = new List<MapLocation>();
        for (int z = 1; z < maze.size.y - 1; z++)
        {
            for (int x = 1; x < maze.size.x - 1; x++)
            {
                if (maze.map[x, z] != 1)
                {
                    Vector2Int pos = new Vector2Int(x, z);
                    locations.Add(new MapLocation(pos));
                }
            }
        }
        
        locations.Shuffle();

        startLocation = new Vector3(locations[0].vector2.x * maze.scale, 0, locations[0].vector2.y * maze.scale);
        Vector2Int startPos = new Vector2Int(locations[0].vector2.x, locations[0].vector2.y);
        startNode = new PathMarker(new MapLocation(startPos), 0, 0, 0,  null);
        
        
        goalLocation =  new Vector3(locations[1].vector2.x * maze.scale, 0, locations[1].vector2.y * maze.scale);
        Vector2Int endPos = new Vector2Int(locations[1].vector2.x, locations[1].vector2.y);
        goalNode = new PathMarker(new MapLocation(endPos), 0, 0, 0, null);
        
        open.Clear();
        closed.Clear();
        
        open.Add(startNode);
        lastPos = startNode;
        
    }

    void Search(PathMarker currentNode)
    {
        if (currentNode.Equals(goalNode)) { isDone = true; return;}
        
            foreach (MapLocation direction in maze.directions)
            {
                MapLocation neighbour = direction + currentNode.Location;

                if (maze.map[neighbour.vector2.x, neighbour.vector2.y] == 1) continue;

                if (neighbour.vector2.x < 1 || neighbour.vector2.x >= maze.size.x || neighbour.vector2.y < 1 ||
                    neighbour.vector2.y >= maze.size.y) continue;

                if (isClosed(neighbour)) continue;

                float G = Vector2Int.Distance(currentNode.Location.vector2, neighbour.vector2) + currentNode.G;
                float H = Vector2Int.Distance(neighbour.vector2, goalNode.Location.vector2);
                float F = G + H;

           

                if (!UpdateMarker(neighbour, G, H, F, currentNode))
                {
                    open.Add(new PathMarker(neighbour, G, H, F, currentNode));
                }
                
            }
            

        open = open.OrderBy(pathmarker => pathmarker.F).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);
        closed.Add(pm);
        
        open.RemoveAt(0);
        lastPos = pm;
    }

    bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker parent)
    {
        foreach (PathMarker pathMarker in open)
        {
            if (pathMarker.Location.Equals(pos))
            {
                pathMarker.G = g;
                pathMarker.H = h;
                pathMarker.F = f;
                pathMarker.parent = parent;
                return true;
            }
        }

        return false;
    }

    bool isClosed(MapLocation location)
    {
        foreach (PathMarker path in closed)
        {
            if (path.Location.Equals(location)) return true;
        }

        return false;
    }

    void GetPath()
    {
        PathMarker begin = lastPos;

        while (!startNode.Equals(begin) && begin != null)
        {
            begin = begin.parent;
        }
    }
   

    public void Build(Vector3 end)
    {
        
        int count = 0;
        
        BeginSearch();
        while (!isDone && count < 5000)
        {
            Debug.Log("Searching");
            Search(lastPos);
            count++;
        }

        //  maze.InitialiseMap();
        MarkPath();
    }

    void MarkPath()
    {
       
        PathMarker begin = lastPos;

        while (!startNode.Equals(begin) && begin != null)
        {
            maze.map[begin.Location.vector2.x, begin.Location.vector2.y] = 0;
            begin = begin.parent;
            validPath = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
