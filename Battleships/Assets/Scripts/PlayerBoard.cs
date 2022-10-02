using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoard : MonoBehaviour
{
    [Header("Player board design")]
    [SerializeField] private GameObject tileObject;
    [SerializeField] private float padding = 11.2f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;

    [SerializeField] private GameObject hitTilePrefab;

    // constannts - board size
    private const int tileCount_X = 10;
    private const int tileConut_Y = 10;

    // grid
    private GameObject[,] tiles;

    public List<Vector2Int> placementTiles = new List<Vector2Int>(); // tiles on which ship can be placed
    private List<Vector2Int> resetTiles = new List<Vector2Int>(); // tiles for reseting placement tiles
    public List<Vector2Int> shipTiles = new List<Vector2Int>(); // tiles on which is ship
    public List<Vector2Int> tilesAffectedByShips = new List<Vector2Int>(); // all tiles that are affected by ships

    public List<Vector2Int> tilesAffectedByBattleship = new List<Vector2Int>(); // all tiles that are affected by battleship
    public List<Vector2Int> tilesAffectedByCruiser = new List<Vector2Int>(); // all tiles that are affected by Cruiser
    public List<Vector2Int> tilesAffectedByDestroyer = new List<Vector2Int>(); // all tiles that are affected by Destroyer
    public List<Vector2Int> tilesAffectedByFrigate = new List<Vector2Int>(); // all tiles that are affected by Frigate
    public List<Vector2Int> tilesAffectedByCorvette1 = new List<Vector2Int>(); // all tiles that are affected by first Corvette
    public List<Vector2Int> tilesAffectedByCorvette2 = new List<Vector2Int>(); // all tiles that are affected by second Corvette

    [Header("Ships")]
    [SerializeField] private GameObject[] shipPrefabs;
    public List<Ship> ship;

    // placeholder for tiles that my be used
    private List<Vector2Int> usedTiles = new List<Vector2Int>();
    private List<Vector2Int> affectedTiles = new List<Vector2Int>();

    private Vector3 rotation = Vector3.zero;
    private int whileLoopTurn = 0;

    [Header("Player")]
    public bool player; // true = player one, false = player two

    [Header("Other scripts")]
    [SerializeField] private UIManager UIManager;
    [SerializeField] private BattleBoard battleBoard;

    private void Awake()
    {
        GenerateBoard(tileCount_X, tileConut_Y);
    }

    private void Start()
    {
        SpawnShips(player);    
    }

    private void Update()
    {
        UIManager.SetPlayerName(player);
    }

    // method for generation tiles on a grid which represent a game board
    private void GenerateBoard(int tileCountX, int tileCountY)
    {
        //boardCenter = transform.position;

        tiles = new GameObject[tileCountX, tileCountY];

        for (int i = 0; i < tileCountX; i++)
        {
            for (int j = 0; j < tileCountY; j++)
            {
                Vector3 pos = new Vector3(j * padding, 0, i * padding);
                tiles[i,j] = Instantiate(tileObject,  pos, Quaternion.identity);
                tiles[i,j].transform.parent = gameObject.transform;
                placementTiles.Add(new Vector2Int(i,j));
                resetTiles.Add(new Vector2Int(i, j));
            }
        }
    }

    // method for spawning ships
    public void SpawnShips(bool player)
    {
        ship = new List<Ship>();

        ship.Add(SpawnSingleShip(ShipType.Battleship, player));
        ship.Add(SpawnSingleShip(ShipType.Cruiser, player));
        ship.Add(SpawnSingleShip(ShipType.Destroyer, player));
        ship.Add(SpawnSingleShip(ShipType.Frigate, player));;

        //// because there has to be two 1x1 ships
        for (int i = 0; i < 2; i++)
        {
            ship.Add(SpawnSingleShip(ShipType.Corvette, player));
        }

    }
    
    // create single ship
    private Ship SpawnSingleShip(ShipType type, bool player)
    {
        Ship shipPiece = Instantiate(shipPrefabs[(int)type - 1], transform).GetComponent<Ship>();

        shipPiece.ShipType = type;
        shipPiece.player = player;

        return shipPiece;
    }

    // method for valid ship placement
    public void PositionShips()
    {
        Debug.Log("POSTAVLJA");
        for (int i = 0; i < ship.Count; i++)
        {
            whileLoopTurn = 0;
            bool tileSearch = true;
            Debug.Log(ship[i].Name);

            while (tileSearch)
            {
                usedTiles = new List<Vector2Int>();

                if (whileLoopTurn == placementTiles.Count)
                {
                    tileSearch = false;
                    ResetLists();
                    PositionShips();
                }

                int startRow = placementTiles[Random.Range(0, placementTiles.Count)].x;
                int startColumn = placementTiles[Random.Range(0, placementTiles.Count)].y;
                int endRow = startRow;
                int endColumn = startColumn;
                int orientation = Random.Range(0, 3); // rotation of ship : 0 = 0; 1 = 90, 2 = 180, 3 = 270 

                Debug.Log(startRow + " " + startColumn + " " + orientation);

                int removeTile = 0;

                // foreach orientation get tiles that will be used
                //0
                if (orientation == 0)
                {
                    for (int w = 1; w <= ship[i].Width; w++)
                    {
                        usedTiles.Add(new Vector2Int(endRow, startColumn));
                        endRow--;
                    }

                    rotation = new Vector3(0, 0, 0);
                }
                //90
                else if (orientation == 1)
                {
                    for (int w = 1; w <= ship[i].Width; w++)
                    {
                        usedTiles.Add(new Vector2Int(startRow, endColumn));
                        endColumn--;

                    }
                    rotation = new Vector3(0, 90, 0);
                }
                //180
                else if (orientation == 2)
                {
                    for (int w = 1; w <= ship[i].Width; w++)
                    {
                        usedTiles.Add(new Vector2Int(endRow, startColumn));
                        endRow++;

                    }
                    rotation = new Vector3(0, 180, 0);
                }
                //270
                else if (orientation == 3)
                {
                    for (int w = 1; w <= ship[i].Width; w++)
                    {
                        usedTiles.Add(new Vector2Int(startRow, endColumn));
                        endColumn++;
                    }
                    rotation = new Vector3(0, 270, 0);
                }

                // cannot place ships beyond the boundaries of the board
                if (endRow > 9 || endColumn > 9 || endColumn < 0 || endRow < 0)
                {
                    Debug.Log("VANI");
                    whileLoopTurn++;
                    tileSearch = true;
                    continue;
                }

                // get all tiles that are affeceted by ship
                affectedTiles = ship[i].GetAffectedTiles(usedTiles[0].x, usedTiles[0].y, orientation);
                AddAfectedTilesToUsedTiles();

                // check if specified tiles are occupied
                for (int x = 0; x < usedTiles.Count; x++)
                {
                    for (int t = 0; t < placementTiles.Count; t++)
                    {
                        if (placementTiles[t].x == usedTiles[x].x && placementTiles[t].y == usedTiles[x].y)
                        {
                            removeTile++;
                        }
                    }
                }
                // if no tiles are occupied then remove them from placement tile list -- this represent occupation of tiles
                if (removeTile == usedTiles.Count)
                {
                    for (int x = 0; x < usedTiles.Count; x++)
                    {
                        for (int t = 0; t < placementTiles.Count; t++)
                        {
                            if (placementTiles[t].x == usedTiles[x].x && placementTiles[t].y == usedTiles[x].y)
                            {
                                // testing: tiles[placementTiles[t].x, placementTiles[t].y].gameObject.SetActive(false);
                                placementTiles.RemoveAt(t);
                            }
                        }
                    }

                    SetShipTilesToShare(ship[i]);
                    SetAffectedTilesToShare();
                    SetSpecificShipAffectedTiles(ship[i]);
                }
                else
                {
                    Debug.Log("ZAUZETO");
                    whileLoopTurn++;
                    tileSearch = true;
                    continue;
                }

                tileSearch = false;
                
            }

            // check which rotation does ship have for rightful placement
            PlaceShip(rotation, ship[i]);
        }
    }

    // method for adding affected tiles of ship to used tiles
    private void AddAfectedTilesToUsedTiles()
    {
        for (int i = 0; i < affectedTiles.Count; i++)
        {
            usedTiles.Add(affectedTiles[i]);
        }
    }

    // method for getting centar when ship has vertical orientation -- column center stays the same, but row center has to be calculated 
    private Vector3 GetPositionCenterForVerticalRotation(Vector2Int firstTile, Vector2Int lastTile)
    {
        return new Vector3(tiles[firstTile.x, firstTile.y].transform.position.x, 0, (tiles[firstTile.x, firstTile.y].transform.position.z + tiles[lastTile.x, lastTile.y].transform.position.z) / 2);
    }

    // method for getting centar when ship has horizontal orientation -- row center stays the same, but column center has to be calculated
    private Vector3 GetPositionCenterForHorizontalRotation(Vector2Int firstTile, Vector2Int lastTile)
    {
        return new Vector3((tiles[firstTile.x, firstTile.y].transform.position.x + tiles[lastTile.x, lastTile.y].transform.position.x) / 2, 0, tiles[firstTile.x, firstTile.y].transform.position.z);
    }

    // method for making a list of all tiles used by all ships
    private void SetShipTilesToShare(Ship ship)
    {
        for (int t = 0; t < ship.Width; t++)
        {
            shipTiles.Add(usedTiles[t]);
        }
    }

    // method for making a list of all tiles affected by all ships
    private void SetAffectedTilesToShare()
    {
        for (int t = 0; t < affectedTiles.Count; t++)
        {
            tilesAffectedByShips.Add(affectedTiles[t]);
        }
    }

    // method for making a lists of all tiles affected by certain ship
    private void SetSpecificShipAffectedTiles(Ship ship)
    {
        if (ship.Name == "Battleship")
        {
            tilesAffectedByBattleship = affectedTiles;
        }
        else if (ship.Name == "Cruiser")
        {
            tilesAffectedByCruiser = affectedTiles;
        }
        else if (ship.Name == "Destroyer")
        {
            tilesAffectedByDestroyer = affectedTiles;
        }
        else if (ship.Name == "Frigate")
        {
            tilesAffectedByFrigate = affectedTiles;
        }
        else if (ship.Name == "Corvette")
        {
            if (tilesAffectedByCorvette1.Count == 0)
            {
                tilesAffectedByCorvette1 = affectedTiles;
            }
            else
            {
                tilesAffectedByCorvette2 = affectedTiles;
            }
            
        }
    }

    // method for placing ship with rightful rotation
    private void PlaceShip(Vector3 rotation, Ship ship)
    {
        if (rotation.y == 0)
        {
            Vector3 tilePosition = GetPositionCenterForVerticalRotation(usedTiles[0], usedTiles[ship.Width - 1]);
            ship.transform.Rotate(rotation);
            ship.transform.position = tilePosition;
        }
        else if (rotation.y == 90)
        {
            Vector3 tilePosition = GetPositionCenterForHorizontalRotation(usedTiles[0], usedTiles[ship.Width - 1]);
            ship.transform.Rotate(rotation);
            ship.transform.position = tilePosition;
        }
        else if (rotation.y == 180)
        {
            Vector3 tilePosition = GetPositionCenterForVerticalRotation(usedTiles[0], usedTiles[ship.Width - 1]);
            ship.transform.Rotate(rotation);
            ship.transform.position = tilePosition;
        }
        else if (rotation.y == 270)
        {
            Vector3 tilePosition = GetPositionCenterForHorizontalRotation(usedTiles[0], usedTiles[ship.Width - 1]);
            ship.transform.Rotate(rotation);
            ship.transform.position = tilePosition;
        }
    }

    // method for reseting all lists conected to ship placement
    private void ResetLists()
    {
        usedTiles = new List<Vector2Int>();
        placementTiles = new List<Vector2Int>();
        placementTiles = resetTiles;
        affectedTiles = new List<Vector2Int>();
        shipTiles = new List<Vector2Int>();
        tilesAffectedByShips = new List<Vector2Int>();
        tilesAffectedByBattleship = new List<Vector2Int>();
        tilesAffectedByCruiser = new List<Vector2Int>();
        tilesAffectedByDestroyer = new List<Vector2Int>();
        tilesAffectedByFrigate = new List<Vector2Int>();
        tilesAffectedByCorvette1 = new List<Vector2Int>();
        tilesAffectedByCorvette2 = new List<Vector2Int>();
    }

    // method for showing where other player hit
    public void ShowHitTiles(int hitX, int hitY)
    {
        Vector3 position = tiles[hitX, hitY].transform.position;
        GameObject hitCube = Instantiate(hitTilePrefab, position, Quaternion.identity);
        hitCube.transform.parent = gameObject.transform;
    }



}
