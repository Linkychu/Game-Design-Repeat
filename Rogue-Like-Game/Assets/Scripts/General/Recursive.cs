using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursive : Maze
{
   private int finished;
   private int looping;
   private bool finishedLoading;
   
   public override void Generate()
   {
      RecursiveGenerate();
   }

   void RecursiveGenerate()
   {
      Vector2Int pos = new Vector2Int(RNG.RngCallRange(1, size.x - 1), RNG.RngCallRange(1, size.y - 1));
      Generate(pos);
   }

   void Generate(Vector2Int pos)
   {
      if (CountSquareNeighbours(pos.x, pos.y) >= 2 || CountDiagonalNeighbours(pos.x, pos.y) >= 2)
         return;


      map[pos.x, pos.y] = 0;


      //recursion
      
      directions.Shuffle();
      Generate(pos + directions[0].vector2);
      Generate(pos + directions[1].vector2);
      Generate(pos + directions[2].vector2);
      Generate(pos + directions[3].vector2);
      /*
      Generate(pos + directions[4].vector2);
      Generate(pos + directions[5].vector2);
      Generate(pos + directions[6].vector2);
      Generate(pos + directions[7].vector2);
      */



      
   }

   public override void AddRooms(int count, int minSize, int maxSize)
   {
      
         for (int c = 0; c < count; c++)
         {
            Vector2Int startPos = new Vector2Int(RNG.RngCallRange(3, size.x - 3), RNG.RngCallRange(3, size.y - 3));
            Vector2Int roomSize = new Vector2Int(RNG.RngCallRange(minSize, maxSize), RNG.RngCallRange(minSize, maxSize));

            for (int x = startPos.x; x < size.x - 3 && x < startPos.x + roomSize.x; x++)
            {
               for (int z = startPos.y; z < size.y - 3 && z < startPos.y + roomSize.y; z++)
               {
                  map[x, z] = 0;
                  Vector2Int roomPos = new Vector2Int(x, z);
                //  rooms.Add(new MapLocation(roomPos));
               }

            }
         }
   }
}
