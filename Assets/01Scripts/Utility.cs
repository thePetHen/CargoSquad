using UnityEngine;

public static class Utility 
{
    public static LayerMask StandableMask => LayerMask.GetMask("Ground", "Obstacle", "Default");
    public static float BoatDrag => .98f;
}
