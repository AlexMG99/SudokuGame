using Helper.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Cell Skin Elements")]
    [SerializeField] private Image gridBorderImage;

    [SerializeField]
    private Transform tileContainer;

    private List<Tile> tiles = new List<Tile>();

    public int CellIdx => cellIdx;
    private int cellIdx = -1;

    private bool isCellSolved = false;

    #region MonoBehaviourFunctions
    private void Awake()
    {
        Init();
    }

    #endregion

    #region PrivateFunctions
    private void Init()
    {
        tiles.AddRange(tileContainer.GetComponentsInChildren<Tile>());

        foreach (Tile tile in tiles)
        {
            tile.SetCell(this);
        }
    }

    private void SetSkin()
    {
        gridBorderImage.color = SkinController.Instance.CurrentGridSkin.GridBorderColor;
    }

    private Tile GetTileByNumber(int number)
    {
        Tile numberTile = null;

        foreach (Tile tile in tiles)
        {
            if (tile.CheckNumber(number))
            {
                numberTile = tile;
                break;
            }
        }

        if (!numberTile)
            Debug.LogError($"There is no tile with number {number}!");
        else if (!numberTile.IsSolved())
            return null;

        return numberTile;
    }

    private Tile GetTileByCurrentNumber(int number)
    {
        Tile numberTile = null;

        foreach (Tile tile in tiles)
        {
            if (tile.CheckCurrentNumber(number))
            {
                numberTile = tile;
                break;
            }
        }

        return numberTile;
    }

    private bool CellIsInRow(int row)
    {
        if (row < 3)
            return cellIdx < 3;
        else if (row >= 3 && row < 6)
            return (cellIdx >= 3 && cellIdx <= 6);
        else if (row >= 6 && row < 9)
            return (cellIdx >= 6 && cellIdx <= 9);

        return false;
    }

    private bool CellIsInColumn(int column)
    {
        if (column < 3)
            return (cellIdx % 3 == 0);
        else if (column >= 3 && column < 6)
            return (cellIdx == 1 || cellIdx == 4 || cellIdx == 7);
        else if (column >= 6 && column < 9)
            return (cellIdx == 2 || cellIdx == 5 || cellIdx == 8);
        return false;
    }

    private void HighlightWrongNumberByRowOrColumn(int number, Vector2Int position)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.Position.x == position.x || tile.Position.y == position.y)
            {
                if(tile.CheckCurrentNumber(number) && !tile.IsWrong())
                    tile.HighlightWrongTile();
            }
        }
    }

    private void HighlightNumberByRowOrColumn(Vector2Int position)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.Position.x == position.x || tile.Position.y == position.y)
            {
                tile.HighlightTile();
            }
        }
    }

    private void HighlightCell()
    {
        foreach (Tile tile in tiles)
        {
            tile.HighlightTile();
        }
    }
    #endregion

    #region PublicFunctions
    public bool FindTileByPosition(Vector2Int position, out Tile tileOut)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.Position == position)
            {
                tileOut = tile;
                return true;
            }
        }

        tileOut = null;
        return false;
    }

    public void UpdateSkins()
    {
        SetSkin();
        foreach (Tile tile in tiles)
        {
            tile.SetSkin();
        }
    }

    public void SetTilesNumbers(string solutionNumbers, string lockedNumbers, int _cellIdx)
    {
        cellIdx = _cellIdx;
        for (int i = 0; i < tiles.Count; i++)
        {
            Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(i/3) + Mathf.FloorToInt(cellIdx/3) * 3
                ,i + 3 * (cellIdx - Mathf.FloorToInt(i / 3) - Mathf.FloorToInt(cellIdx / 3) * 3));
            tiles[i].SetNumber(solutionNumbers[i], lockedNumbers[i], tilePosition);
        }
    }

    public void CheckCellSolved()
    {
        foreach (Tile tile in tiles)
        {
            if (!tile.IsSolved())
            {
                isCellSolved = false;
                return;
            }
        }

        isCellSolved = true;
    }

    public bool IsCellSolved()
    {
        return isCellSolved;
    }

    public Action<int> UseHintOnTile()
    {
        int tryFindRandomCell = 0;

        while (tryFindRandomCell < 6)
        {
            int randomRange = Random.Range(0, tiles.Count);

            if (!tiles[randomRange].IsSolved())
            {
                return tiles[randomRange].SolveNumber();
            }

            tryFindRandomCell++;
        }

        foreach (Tile tile in tiles)
        {
            if (!tile.IsSolved())
            {
                return tile.SolveNumber();
            }
        }

        return new Action<int>(ActionType.None, -1, Vector2Int.zero);
    }

    public void HighlightNumberInCell(int number)
    {
        Tile highlightTile = GetTileByNumber(number);
        if (highlightTile)
            highlightTile.HighlightSameNumberTile();
    }

    public void HighlightWrongNumberInCell(int number)
    {
        Tile highlightTile = GetTileByCurrentNumber(number);
        if (highlightTile)
            highlightTile.HighlightWrongTile();
    }

    public void HighlightCellRowColumn(int _cellIdx, Vector2Int position)
    {
        // Check if is containing cell
        if (cellIdx == _cellIdx)
        {
            HighlightCell();
        }
        else if (CellIsInRow(position.x) || CellIsInColumn(position.y)) // Check if has row or column
        {
            HighlightNumberByRowOrColumn(position);
        }
    }

    public void HighlightWrongNumberRowColumn(int number, Vector2Int position)
    {
        // Check if is containing cell
        if (CellIsInRow(position.x) || CellIsInColumn(position.y)) // Check if has row or column
        {
            HighlightWrongNumberByRowOrColumn(number, position);
        }
    }

    public void DownlightCell()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.IsHighlighted())
                tile.DownlightTile();
        }
    }
    #endregion
}
