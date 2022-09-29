using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    None = 0,
    Battleship = 1,
    Cruiser = 2,
    Destroyer = 3,
    Frigate = 4,
    Corvette = 5
}
public class Ship : MonoBehaviour
{
    public string Name { get; set; }
    public int Width { get; set; }
    public ShipType ShipType { get; set; }

    public bool player;


    public virtual List<Vector2Int> GetAffectedTiles(int selectedx, int selectedy, int orientation)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        return returnValue;
    }

}
