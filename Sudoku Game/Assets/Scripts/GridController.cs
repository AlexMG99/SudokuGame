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

    private void Update()
    {
        //if (Controls.Instance.TouchDown)
            //DownlightAllCells();
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
    public void HiglightNumberInCells(int number)
    {
        foreach(Cell cell in cells)
        {
            cell.HighlightNumberInCell(number);
        }
    }

    public void DownlightNumberInCells(int number)
    {
        foreach (Cell cell in cells)
        {
            cell.DownlightNumberInCell(number);
        }
    }

    public void UpdateSkins()
    {
        SetSkin();
        foreach (Cell cell in cells)
        {
            cell.UpdateSkins();
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
