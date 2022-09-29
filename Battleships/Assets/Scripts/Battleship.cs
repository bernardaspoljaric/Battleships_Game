using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleship : Ship
{
    public Battleship()
    {
        Name = "Battleship";
        Width = 5;
        ShipType = ShipType.Battleship;
    }

    public override List<Vector2Int> GetAffectedTiles(ref Ship[,] ship, int selectedx, int selectedy, int orientation)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        // row 0
        if (selectedx == 0 && selectedy == 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 6));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy + i));
            }
        }

        if (selectedx == 0 && selectedy == 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 6));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy - i));
            }
        }
        if (selectedx == 0 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy - 6));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy - i));
            }
        }

        if (selectedx == 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy + 6));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy + i));
            }
        }

        // row 9
        if (selectedx == 9 && selectedy == 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 6));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy + i));
            }
        }

        if (selectedx == 9 && selectedy == 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 6));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy - i));
            }
        }
        if (selectedx == 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 1 ));
            returnValue.Add(new Vector2Int(selectedx, selectedy - 6));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy - i));
            }
        }

        if (selectedx == 9 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy + 6));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy - 1));
            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy + i));
            }
        }

        // column 0
        if (selectedx == 0 && selectedy == 0 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 6, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy + 1));
            }
        }

        if (selectedx == 9 && selectedy == 0 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx - 6, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy + 1));
            }
        }
        if (selectedy == 0 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 6, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy));
            }
        }

        if (selectedy == 0 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 6, selectedy));
            returnValue.Add(new Vector2Int(selectedx -1 , selectedy + 1));

            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy));
            }
        }

        // column 9
        if (selectedx == 0 && selectedy == 9 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 6, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy - 1));
            }
        }

        if (selectedx == 9 && selectedy == 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx - 6, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
        }
        if (selectedy == 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 6, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy - 1));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
        }

        if (selectedy == 9 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 6, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));

            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
        }

        if (orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 6, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));

            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
        }

        return returnValue;
    }
}
