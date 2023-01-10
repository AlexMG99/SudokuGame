using Helper.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelController))]
public class GridController : MonoBehaviourSingleton<GridController>
{
    [Header("Grid Skin Elements")]
    [SerializeField] private Image backgroundImage;

    private List<Cell> cells = new List<Cell>();

    public LevelController LevelController => levelController;
    private LevelController levelController;

    #region MonoBehaviourFunctions
    public override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        // Create Level
        SetGridLevel();
        UpdateSkins();
    }

    #endregion

    #region PrivateFunctions
    private void Init()
    {
        cells.AddRange(transform.GetComponentsInChildren<Cell>());

        // Level Generation
        levelController = GetComponent<LevelController>();
        levelController.LoadLevel();
    }

    private void SetSkin()
    {
        backgroundImage.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;
        // UI Text
    }

    private void SetGridLevel()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            string cellLockedNumbers = GetCellNumber(i, levelController.CurrentLevel.NumberSolution);
            string cellHideNumbers = GetCellNumber(i, levelController.CurrentLevel.NumberHide);
            cells[i].SetTilesNumbers(cellLockedNumbers, cellHideNumbers, i);
        }
    }

    private string GetCellNumber(int cellIdx, string[] numberArray)
    {
        string cellNumbers = "";
        int rowCellIdx = Mathf.FloorToInt(cellIdx / 3);

        for (int stringIdx = rowCellIdx * 3; stringIdx < (rowCellIdx + 1) * 3 && stringIdx >= rowCellIdx * 3; stringIdx++)
        {
            for (int chardIdx = 0; chardIdx < 3; chardIdx++)
            {
                cellNumbers += numberArray[stringIdx][chardIdx + (cellIdx - (rowCellIdx) * 3) * 3];
            }

        }

        return cellNumbers;
    }
    #endregion

    #region PublicFunctions
    public void ResetLevel()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].ResetCell();
        }
    }
    
    public void ResetSameLevel()
    {
        LevelController.SetMistakes(0);
        DownlightAllCells();
    }

    public void SetNextLevel()
    {
        ResetLevel();

        SetGridLevel();
        UpdateSkins();
    }

    public void CheckIfGameIsWin()
    {
        foreach (Cell cell in cells)
        {
            if (!cell.IsCellSolved())
                return;
        }

        GameManager.Instance.WinGame();
    }

    public bool CheckNumberSolvedInAllCells(int number)
    {
        foreach (Cell cell in cells)
        {
            if (!cell.CheckNumberSolvedInCell(number))
                return false;
        }

        return true;
    }

    public Tile FindTileByPosition(Vector2Int position)
    {
        Tile tileInPosition = null;

        foreach (Cell cell in cells)
        {
            if (cell.FindTileByPosition(position, out tileInPosition))
                return tileInPosition;
        }

        return tileInPosition;
    }

    public Action<int> UseHintOnCell()
    {
        int tryFindRandomCell = 0;

        while (tryFindRandomCell < 3)
        {
            int randomRange = Random.Range(0, cells.Count);

            if (!cells[randomRange].IsCellSolved())
            {
                return cells[randomRange].UseHintOnTile();
            }

            tryFindRandomCell++;
        }

        foreach (Cell cell in cells)
        {
            if (!cell.IsCellSolved())
            {
                return cell.UseHintOnTile();
            }
        }

        return new Action<int>(ActionType.None, -1, Vector2Int.zero);
    }

    public void UpdateSkins()
    {
        SetSkin();
        foreach (Cell cell in cells)
        {
            cell.UpdateSkins();
        }
    }

    public void HiglightNumberInCells(int number)
    {
        foreach(Cell cell in cells)
        {
            cell.HighlightNumberInCell(number);
        }
    }

    public void HiglightWrongNumberRowColumn(int number, int cellIdx, Vector2Int position)
    {
        DownlightAllCells();

        foreach (Cell cell in cells)
        {
            cell.HighlightCellRowColumn(cellIdx, position);
            cell.HighlightWrongNumberInCell(number);
            cell.HighlightWrongNumberRowColumn(number, position);
        }
    }

    public void HiglightCellRowColumnNumber(int number, int cellIdx, Vector2Int position)
    {
        DownlightAllCells();

        foreach (Cell cell in cells)
        {
            cell.HighlightCellRowColumn(cellIdx, position);
            cell.HighlightNumberInCell(number);
        }
    }

    public void HiglightCellRowColumn(int cellIdx, Vector2Int position)
    {
        DownlightAllCells();

        foreach (Cell cell in cells)
        {
            cell.HighlightCellRowColumn(cellIdx, position);
        }
    }

    public void DownlightAllCells()
    {
        foreach (Cell cell in cells)
        {
            cell.DownlightCell();
        }
    }

    #endregion
}
