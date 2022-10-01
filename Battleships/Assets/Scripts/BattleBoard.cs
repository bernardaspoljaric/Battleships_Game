using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoard : MonoBehaviour
{
    [Header("Battle board design")]
    [SerializeField] private GameObject tileObject;
    [SerializeField] private float tileSize = 10;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;

    // constannts - board size
    private const int tileCount_X = 10;
    private const int tileConut_Y = 10;

    private Vector3 bounds;

    // grid
    private GameObject[,] tiles;
    private float padding = 11.2f;


    private Camera currentCamera;
    private Vector2Int currentHover;

    [Header("Tile materials")]
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material tileMaterial;
    [SerializeField] private Material affectedMaterial;
    [SerializeField] private Material shipMaterial;
    [SerializeField] private Material missMaterial;
    [SerializeField] private Material noShipMaterial;

    [Header("Player")]
    public bool player;
    [SerializeField] private GameObject ownBoard;
    [SerializeField] private GameObject enemyBoard;
    [SerializeField] private GameObject battleBoard;

    [Header("Score")]
    public int score = 0;
    private int battleship = 0;
    private int cruiser = 0;
    private int destroyer = 0;
    private int frigate = 0;
    private int corvette1 = 0;
    private int corvette2 = 0;

    [Header("Other scripts")]
    [SerializeField] private PlayerBoard playerBoard;
    [SerializeField] private Ship ship;
    [SerializeField] private UIManager UIManager;

    private void Awake()
    {
        GenerateBoard(tileCount_X, tileConut_Y);
    }

    private void Start()
    {
        GetShipTiles();
        GetAffectedTiles();
    }

    private void Update()
    {
        if (!currentCamera)
        {
            currentCamera = Camera.main;
            return;
        }

        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out info, 200))
        {
            Vector2Int hitPosition = GetTileIndex(info.transform.gameObject);

            // if we hover a tile after not hovering any tile
            if (currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].gameObject.GetComponent<Renderer>().material = highlightMaterial;
            }
            // if we were already on another tile
            if (currentHover != hitPosition)
            {
                CheckMaterial(currentHover.x, currentHover.y);
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].gameObject.GetComponent<Renderer>().material = highlightMaterial;
            }
            // if we click on the tile
            if (Input.GetMouseButtonDown(0))
            {
                //check if tile is occupied by ship
                if (tiles[hitPosition.x, hitPosition.y].gameObject.layer == LayerMask.NameToLayer("Ship"))
                {
                    if (tiles[hitPosition.x, hitPosition.y].gameObject.tag == "Battleship")
                    {
                        battleship++;
                        score++;
                    }
                    else if (tiles[hitPosition.x, hitPosition.y].gameObject.tag == "Cruiser")
                    {
                        cruiser++;
                        score++;
                    }
                    else if (tiles[hitPosition.x, hitPosition.y].gameObject.tag == "Destroyer")
                    {
                        destroyer++;
                        score++;
                    }
                    else if (tiles[hitPosition.x, hitPosition.y].gameObject.tag == "Frigate")
                    {
                        frigate++;
                        score++;
                    }
                    else if (tiles[hitPosition.x, hitPosition.y].gameObject.tag == "Corvette1")
                    {
                        corvette1++;
                        score++;
                    }
                    else if (tiles[hitPosition.x, hitPosition.y].gameObject.tag == "Corvette2")
                    {
                        corvette2++;
                        score++;
                    }

                    tiles[hitPosition.x, hitPosition.y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[hitPosition.x, hitPosition.y].gameObject.tag = "HitShip";
                    tiles[hitPosition.x, hitPosition.y].gameObject.GetComponent<Renderer>().material = shipMaterial;
                    playerBoard.ShowHitTiles(hitPosition.x, hitPosition.y);
                    
                    CheckIfWholeShipHasBeenHit();
                }
                else
                {
                    tiles[hitPosition.x, hitPosition.y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[hitPosition.x, hitPosition.y].gameObject.tag = "Miss";
                    tiles[hitPosition.x, hitPosition.y].gameObject.GetComponent<Renderer>().material = missMaterial;
                    playerBoard.ShowHitTiles(hitPosition.x, hitPosition.y);

                    ChangeToAnotherPlayer();
                }

                CheckWin();
            }
        }
    }

    // method for generating tiles with certain padding
    private void GenerateBoard(int tileCountX, int tileCountY)
    {
        boardCenter = transform.position;
        bounds = new Vector3((tileCountX / 2) * tileSize, 0, (tileCountX / 2) * tileSize) + boardCenter;

        tiles = new GameObject[tileCountX, tileCountY];

        for (int i = 0; i < tileCountX; i++)
        {
            for (int j = 0; j < tileCountY; j++)
            {
                Vector3 pos = new Vector3(boardCenter.x + padding * j, 0, boardCenter.z + padding * i);
                tiles[i, j] = Instantiate(tileObject, pos, Quaternion.identity);
                tiles[i, j].transform.parent = gameObject.transform;
            }
        }
    }

    // method for getting tile's indexes in relation where raycast hit
    private Vector2Int GetTileIndex(GameObject hitInfo)
    {
        for (int i = 0; i < tileCount_X; i++)
        {
            for (int j = 0; j < tileConut_Y; j++)
            {
                if (tiles[i, j] == hitInfo)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        Debug.Log("Out of bounds.");
        return -Vector2Int.one;
    }

    // method for getting all tiles that ships take and separating them to ships they belong to
    private void GetShipTiles()
    {
        for (int i = 0; i < tileCount_X; i++)
        {
            for (int j = 0; j < tileConut_Y; j++)
            {
                for (int k = 0; k < playerBoard.shipTiles.Count; k++)
                {
                    if(k == 0 || k <= 4)
                    {
                        if (i == playerBoard.shipTiles[k].x && j == playerBoard.shipTiles[k].y)
                        {
                            tiles[i, j].gameObject.tag = "Battleship";
                            tiles[i, j].gameObject.layer = LayerMask.NameToLayer("Ship");
                        }
                    }
                    else if (k == 5 || k <= 8)
                    {
                        if (i == playerBoard.shipTiles[k].x && j == playerBoard.shipTiles[k].y)
                        {
                            tiles[i, j].gameObject.tag = "Cruiser";
                            tiles[i, j].gameObject.layer = LayerMask.NameToLayer("Ship");
                        }
                    }
                    else if (k == 9 || k <= 11)
                    {
                        if (i == playerBoard.shipTiles[k].x && j == playerBoard.shipTiles[k].y)
                        {
                            tiles[i, j].gameObject.tag = "Destroyer";
                            tiles[i, j].gameObject.layer = LayerMask.NameToLayer("Ship");
                        }
                    }
                    else if (k == 12 || k == 13)
                    {
                        if (i == playerBoard.shipTiles[k].x && j == playerBoard.shipTiles[k].y)
                        {
                            tiles[i, j].gameObject.tag = "Frigate";
                            tiles[i, j].gameObject.layer = LayerMask.NameToLayer("Ship");
                        }
                    }
                    else if(k == 14)
                    {
                        if (i == playerBoard.shipTiles[k].x && j == playerBoard.shipTiles[k].y)
                        {
                            tiles[i, j].gameObject.tag = "Corvette1";
                            tiles[i, j].gameObject.layer = LayerMask.NameToLayer("Ship");
                        }
                    }
                    else if (k == 15)
                    {
                        if (i == playerBoard.shipTiles[k].x && j == playerBoard.shipTiles[k].y)
                        {
                            tiles[i, j].gameObject.tag = "Corvette2";
                            tiles[i, j].gameObject.layer = LayerMask.NameToLayer("Ship");
                        }
                    }

                }
            }
        }
    }

    // method for getting all afected tiles by ships
    private void GetAffectedTiles()
    {
        for (int i = 0; i < tileCount_X; i++)
        {
            for (int j = 0; j < tileConut_Y; j++)
            {
                for (int k = 0; k < playerBoard.tilesAffectedByShips.Count; k++)
                {
                    if (i == playerBoard.tilesAffectedByShips[k].x && j == playerBoard.tilesAffectedByShips[k].y)
                    {
                        tiles[i, j].gameObject.tag = "Affected";
                    }
                }
            }
        }
    }

    // method for checking which material should tile use
    private void CheckMaterial(int currentHoverX, int currentHoverY)
    {
        if (tiles[currentHover.x, currentHover.y].gameObject.tag == "HitShip")
        {
            tiles[currentHover.x, currentHover.y].gameObject.GetComponent<Renderer>().material = shipMaterial;
        }
        else if (tiles[currentHover.x, currentHover.y].gameObject.tag == "Miss")
        {
            tiles[currentHover.x, currentHover.y].gameObject.GetComponent<Renderer>().material = missMaterial;
        }
        else
        {
            tiles[currentHover.x, currentHover.y].gameObject.GetComponent<Renderer>().material = tileMaterial;
        }
    }

    // method for changing to another player
    private void ChangeToAnotherPlayer()
    {
        ownBoard.SetActive(false);
        this.gameObject.SetActive(false);
        enemyBoard.SetActive(true);
        battleBoard.SetActive(true);

    }

    // method for checking if player won
    private void CheckWin()
    {
        // if score is 16, last player who clicked won
        if (score == playerBoard.shipTiles.Count)
        {
            UIManager.ShowWinMenu(player);
        }
    }

    // method for checking if whole ship has been hit so tiles affected by it can change material
    private void CheckIfWholeShipHasBeenHit()
    {
        if (battleship == playerBoard.ship[0].Width)
        {
            for (int i = 0; i < playerBoard.tilesAffectedByBattleship.Count; i++)
            {
                if (tiles[playerBoard.tilesAffectedByBattleship[i].x, playerBoard.tilesAffectedByBattleship[i].y].gameObject.tag != "Miss")
                {
                    tiles[playerBoard.tilesAffectedByBattleship[i].x, playerBoard.tilesAffectedByBattleship[i].y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[playerBoard.tilesAffectedByBattleship[i].x, playerBoard.tilesAffectedByBattleship[i].y].gameObject.GetComponent<Renderer>().material = noShipMaterial;
                }
            }
        }
        if (cruiser == playerBoard.ship[1].Width)
        {
            for (int i = 0; i < playerBoard.tilesAffectedByCruiser.Count; i++)
            {
                if (tiles[playerBoard.tilesAffectedByCruiser[i].x, playerBoard.tilesAffectedByCruiser[i].y].gameObject.tag != "Miss")
                {
                    tiles[playerBoard.tilesAffectedByCruiser[i].x, playerBoard.tilesAffectedByCruiser[i].y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[playerBoard.tilesAffectedByCruiser[i].x, playerBoard.tilesAffectedByCruiser[i].y].gameObject.GetComponent<Renderer>().material = noShipMaterial;
                }
            }
        }
        if (destroyer == playerBoard.ship[2].Width)
        {
            for (int i = 0; i < playerBoard.tilesAffectedByDestroyer.Count; i++)
            {
                if (tiles[playerBoard.tilesAffectedByDestroyer[i].x, playerBoard.tilesAffectedByDestroyer[i].y].gameObject.tag != "Miss")
                {
                    tiles[playerBoard.tilesAffectedByDestroyer[i].x, playerBoard.tilesAffectedByDestroyer[i].y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[playerBoard.tilesAffectedByDestroyer[i].x, playerBoard.tilesAffectedByDestroyer[i].y].gameObject.GetComponent<Renderer>().material = noShipMaterial;
                }
            }
        }
        if (frigate == playerBoard.ship[3].Width)
        {
            for (int i = 0; i < playerBoard.tilesAffectedByFrigate.Count; i++)
            {
                if (tiles[playerBoard.tilesAffectedByFrigate[i].x, playerBoard.tilesAffectedByFrigate[i].y].gameObject.tag != "Miss")
                {
                    tiles[playerBoard.tilesAffectedByFrigate[i].x, playerBoard.tilesAffectedByFrigate[i].y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[playerBoard.tilesAffectedByFrigate[i].x, playerBoard.tilesAffectedByFrigate[i].y].gameObject.GetComponent<Renderer>().material = noShipMaterial;
                }
            }
        }
        if (corvette1 == playerBoard.ship[4].Width)
        {
            for (int i = 0; i < playerBoard.tilesAffectedByCorvette1.Count; i++)
            {
                if (tiles[playerBoard.tilesAffectedByCorvette1[i].x, playerBoard.tilesAffectedByCorvette1[i].y].gameObject.tag != "Miss")
                {
                    tiles[playerBoard.tilesAffectedByCorvette1[i].x, playerBoard.tilesAffectedByCorvette1[i].y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[playerBoard.tilesAffectedByCorvette1[i].x, playerBoard.tilesAffectedByCorvette1[i].y].gameObject.GetComponent<Renderer>().material = noShipMaterial;
                }
            }
        }
        if (corvette2 == playerBoard.ship[5].Width)
        {
            for (int i = 0; i < playerBoard.tilesAffectedByCorvette2.Count; i++)
            {
                if (tiles[playerBoard.tilesAffectedByCorvette2[i].x, playerBoard.tilesAffectedByCorvette2[i].y].gameObject.tag != "Miss")
                {
                    tiles[playerBoard.tilesAffectedByCorvette2[i].x, playerBoard.tilesAffectedByCorvette2[i].y].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    tiles[playerBoard.tilesAffectedByCorvette2[i].x, playerBoard.tilesAffectedByCorvette2[i].y].gameObject.GetComponent<Renderer>().material = noShipMaterial;
                }
            }
        }
    }

}
