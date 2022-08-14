using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MazePieces
{
    H_Straight,
    V_Straight,
    Right_Up_Corner,
    Right_Down_Corner,
    Left_Up_Corner,
    Left_Down_Corner,
    TJunction,
    T_Junction_Upside_Down,
    T_Junction_Left,
    T_Junction_Right,
    CRoad,
    DeadEnd,
    DeadEnd_UpisdeDown,
    DeadEnd_Right,
    DeadEnd_Left,
    Wall,
    Room

};

[CreateAssetMenu(fileName = "DungeonTemplate", menuName = "Data/DungeonTemplate")]
public class DungeonTemplate : ScriptableObject
{
    
    
    
    [System.Serializable]
    public struct Module
    {
        public GameObject prefab;
        public Vector3 rotation;
        
    }
    
    
    public Module VerticalStraight;
    public Module HorizontalStraight;
    public Module CrossRoad;
    public Module RightUpCorner;
    public Module RightDownCorner;
    public Module LeftUpCorner;
    public Module LeftDownCorner;
    public Module T_intersection;
    public Module T_intersectionUpsideDown;
    public Module T_intersectionLeft;
    public Module T_intersectionRight;
    public Module Endpiece;
    public Module EndpieceUpsideDown;
    public Module EndpieceRight;
    public Module EndpieceLeft;
    public Module WallPieceTop; 
    public Module WallPieceBottom;
    public Module WallPieceRight;
    public Module WallPieceLeft;
    public Module FloorPiece;
    public Module CeilingPiece;
    public Module Pillar;
    public Module DoorWayUp;
    public Module DoorWayDown;
    public Module DoorWayRight;
    public Module DoorWayLeft;

    public LayerMask WallLayer;

    public GameObject BossStairRoom;
    public GameObject BossRoom;

    public List<EnemyData> enemies = new List<EnemyData>();

}
