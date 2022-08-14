using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = RNG.random.Next(n+ 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
