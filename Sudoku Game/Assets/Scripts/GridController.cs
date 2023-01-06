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

    #endregion

    #region PrivateFunctions
    private void Init()
    {
        cells.AddRange(transform.GetComponentsInChildren<Cell>());

        // Level Generation
        levelController = GetComponent<LevelController>();
        levelController.LoadLevel();

        GenerateLevel();

        // Skin
        SetSkin();
    }

    private void SetSkin()
    {
        backgroundImage.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;
        // UI Text
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            string cellLockedNumbers = GetCellNumber(i, levelController.CurrentLevel.NumberSolution);
            string cellHideNumbers = GetCellNumber(i, levelController.CurrentLevel.NumberHide);
            cells[i].SetTilesNumbers(cellLockedNumbers, cellHideNumbers);
        }
    }

    private string GetCellNumber(int cellIdx, string[] numberArray)
    {
        string cellNumbers = "";

        if(cellIdx < 3)
        {
            for (int stringIdx = 0; stringIdx < 3; stringIdx++)
            {
                for (int idxChar = 0; idxChar < 3; idxChar++)
                {
                    cellNumbers += numberArray[stringIdx][idxChar * cellIdx];
                }
                
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
