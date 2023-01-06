using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviourSingleton<GridController>
{
    [Header("Grid Skin Elements")]
    [SerializeField] private Image backgroundImage;

    private List<Cell> cells = new List<Cell>();

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

        SetSkin();
    }

    private void SetSkin()
    {
        backgroundImage.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;
        // UI Text
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
