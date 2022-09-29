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


    private void Awake()
    {
        GenerateBoard(tileCount_X, tileConut_Y);
    }

    private void GenerateBoard(int tileCountX, int tileCountY)
    {
        boardCenter = transform.position;
        bounds = new Vector3((tileCountX / 2) * tileSize, 0, (tileCountX / 2) * tileSize) + boardCenter;

        tiles = new GameObject[tileCountX, tileCountY];

        for (int i = 0; i < tileCountX; i++)
        {
            for (int j = 0; j < tileCountY; j++)
            {
                Vector3 pos = new Vector3(boardCenter.x + padding *j, 0, boardCenter.z + padding *i );
                tiles[i, j] = Instantiate(tileObject, pos, Quaternion.identity);
                tiles[i, j].transform.parent = gameObject.transform;
            }
        }
    }

}
