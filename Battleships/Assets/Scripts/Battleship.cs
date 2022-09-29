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

    public override List<Vector2Int> GetAffectedTiles(int selectedx, int selectedy, int orientation)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        // row 0
        if (selectedx == 0 && selectedy == 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 5));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy + i));         
            }
            Debug.Log("selectedx == 0 && selectedy == 0 && orientation == 3");
        }

        if (selectedx == 0 && selectedy == 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 5));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy - i));
            }
            Debug.Log("selectedx == 0 && selectedy == 9 && orientation == 1");
        }
        if (selectedx == 0 && selectedy != 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy - 5));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy - i));
                Debug.Log("selectedx == 0 && orientation == 1");
            }
        }

        if (selectedx == 0 && selectedy != 0 && selectedy != 9 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy + 1));
                returnValue.Add(new Vector2Int(selectedx + i, selectedy - 1));
            }
            Debug.Log("selectedx == 0 && selectedy != 0 && orientation == 2");
        }

        if (selectedx == 0 && selectedy != 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy + 5));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy + i));
            }
            Debug.Log("selectedx == 0 && selectedy != 0 && orientation == 3");
        }

        // row 9
        if (selectedx == 9 && selectedy == 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 5));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy + i));
            }
            Debug.Log("selectedx == 9 && selectedy == 0 && orientation == 3");
        }

        if (selectedx == 9 && selectedy == 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 5));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy - i));
            }
            Debug.Log("selectedx == 9 && selectedy == 9 && orientation == 1");
        }
        if (selectedx == 9 && selectedy != 0 && selectedy != 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 1 ));
            returnValue.Add(new Vector2Int(selectedx, selectedy - 5));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy - i));
            }
            Debug.Log("selectedx == 9 && selectedy != 0 && selectedy != 9 && orientation == 1");
        }

        if (selectedx == 9 && selectedy != 0 && selectedy != 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy + 1));
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
            Debug.Log("selectedx == 9 && selectedy != 0 && orientation == 0");
        }

        if (selectedx == 9 && selectedy != 0 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy + 5));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy - 1));
            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy + i));
            }
            Debug.Log("selectedx == 9 && selectedy != 0 && orientation == 3");
        }

        // column 0
        if (selectedx == 0 && selectedy == 0 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy + 1));
            }
            Debug.Log("selectedx == 0 && selectedy == 0 && orientation == 2");
        }

        if (selectedx == 9 && selectedy == 0 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy + 1));
            }
            Debug.Log("selectedx == 9 && selectedy == 0 && orientation == 0");
        }
        if (selectedy == 0 && selectedx != 0 && selectedx != 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy));
            }
            Debug.Log("selectedy == 0 && selectedx != 0 && selectedx != 9 && orientation == 0");
        }

        if (selectedy == 0 && selectedx != 0 && selectedx != 9 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 6));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy + i));
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy + i));
            }
            Debug.Log("selectedy == 0 && selectedx != 0 && selectedx != 0 && orientation == 3");
        }

        if (selectedy == 0 && selectedx != 0 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 5, selectedy));
            returnValue.Add(new Vector2Int(selectedx -1 , selectedy + 1));

            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy + 1));
            }
            Debug.Log("selectedy == 0 && selectedx != 0 && orientation == 2");
        }

        // column 9
        if (selectedx == 0 && selectedy == 9 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy - 1));
            }
            Debug.Log("selectedx == 0 && selectedy == 9 && orientation == 2");
        }

        if (selectedx == 9 && selectedy == 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
            Debug.Log("selectedx == 9 && selectedy == 9 && orientation == 0");
        }
        if (selectedy == 9 && selectedx != 0 && selectedx != 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy - 1));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
            Debug.Log("selectedy == 9 && selectedx != 0 && selectedx != 9 && orientation == 0");
        }

        if (selectedy == 9 && selectedx != 0 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));

            for (int i = 0; i < 5; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
            Debug.Log("selectedy == 9 && selectedx != 0 && orientation == 2");
        }

        if (selectedy == 9 && selectedx != 0 && selectedx != 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));
            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy - 1));
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
            }
            Debug.Log("selectedy == 9 && selectedx != 0 && selectedx != 9 && orientation == 1");
        }

        // just orientation

        if (selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 0)
        {
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx - 5, selectedy));

            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx - i, selectedy - 1));
                returnValue.Add(new Vector2Int(selectedx - i, selectedy + 1));
            }
            Debug.Log("selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 0");
        }

        if (selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 1)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy - 5));

            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy - i));
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy - i));
            }
            Debug.Log("selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 1");
        }

        if (selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 2)
        {
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy + 1));
            returnValue.Add(new Vector2Int(selectedx + 5, selectedy));

            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + i, selectedy - 1));
                returnValue.Add(new Vector2Int(selectedx + i, selectedy + 1));
            }
            Debug.Log("selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 2");
        }

        if (selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 3)
        {
            returnValue.Add(new Vector2Int(selectedx, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx + 1, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx - 1, selectedy - 1));
            returnValue.Add(new Vector2Int(selectedx, selectedy + 5));

            for (int i = 0; i < 6; i++)
            {
                returnValue.Add(new Vector2Int(selectedx + 1, selectedy + i));
                returnValue.Add(new Vector2Int(selectedx - 1, selectedy + i));
            }
            Debug.Log("selectedx != 0 && selectedy != 0 && selectedx != 9 && selectedy != 9 && orientation == 3");
        }

        return returnValue;
    }
}
