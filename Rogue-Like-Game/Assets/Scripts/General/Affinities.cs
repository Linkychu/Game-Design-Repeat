using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Affinity
{
    Fire,
    Water,
    Earth,
    Air,
    Fighting,
    Lightning

};









[CreateAssetMenu(fileName = "Affinity", menuName = "Data/Affinity")]
public class Affinities : ScriptableObject
{

    public Affinity affinity;

    public List<Affinity> Resistances = new List<Affinity>();
    public List<Affinity> Weaknesses = new List<Affinity>();

    public float DamageMultiplier(Affinities affinities)
    {
        if(Resistances.Contains(affinities.affinity))
        {
            return 0.5f;
        }

        else if(Weaknesses.Contains(affinities.affinity))
        {
            return 2f;
        }

        return 1;


    }
}
