using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Crawler : Maze
{
    // Start is called before the first frame update
    
    
    public override void Generate()
    {
        for (int i = 0; i < 3; i++)
        {
            Horizontal();
        }

        for (int i = 0; i < 2; i++)
        {
            Vertical();
        }
      
    }

    void Horizontal()
    {
        bool done = false;
        Vector2Int dimensions = new Vector2Int(1, RNG.RngCallRange(1, size.y -1));
      



      while (!done)
        {
            map[dimensions.x, dimensions.y] = 0;
            
            
            
            if (RNG.RngCallRange(0, 10) < 5)
            {
                dimensions.x += RNG.RngCallRange(0, 2);
            }

            else
            {
                
                dimensions.y += RNG.RngCallRange(-1, 2);
            }
            
            done |= (dimensions.x < 1 || dimensions.x >= size.x -1 || dimensions.y < 1 || dimensions.y >= size.y -1);
        }
    }

    void Vertical()
    {
        bool done = false;
        Vector2Int dimensions = new Vector2Int(RNG.RngCallRange(1, size.x - 1), 1);



        while (!done)
        {
            map[dimensions.x, dimensions.y] = 0;
           
            if (RNG.RngCallRange(0, 10) < 5)
            {
                dimensions.x += RNG.RngCallRange(-1, 2);
            }

            else
            {
                dimensions.y += RNG.RngCallRange(0, 2);
            }
            
            done |= (dimensions.x < 1 || dimensions.x >= size.x - 1 || dimensions.y < 1 || dimensions.y >= size.y -1);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
