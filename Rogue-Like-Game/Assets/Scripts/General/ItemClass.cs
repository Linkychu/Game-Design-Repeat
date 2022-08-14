using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Regular,
    Gold,
    Consumable,
    Map,
    BossKey
}




[CreateAssetMenu(fileName = "items1", menuName = "Data/Items", order = 0)]
    public class ItemClass : ScriptableObject
    {
        
        public int rateOfAppearing;
        public GameObject item;
        public ItemType type;
        public string description;
        public Sprite image;
        public int maxAmount;
        public int minAmount;
        public int HealAmount;
        public StatBoost Boost;



    }
