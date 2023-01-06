using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler 
{
    [Header("Tile Components")]
    [SerializeField] private Image tileImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private TextMeshProUGUI numberTMP;


    [Space()]
    [SerializeField] private Transform noteContainer;
    private List<TextMeshProUGUI> notes = new List<TextMeshProUGUI>();

    public Vector2Int Position => position;
    [SerializeField]
    private Vector2Int position;

    private int solutionNumber;
    private int currentNumber;

    private Cell cellParent;

    private bool isLocked = false;
    private bool isSolved = false;
    private bool isHighlight = false;
    private bool isWrong = false;

    #region MonoBehaviourFunctions
    private void Awake()
    {
        Init();
    }
    #endregion

    #region IPointerEvent

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked();
        Debug.Log("Tile was clicked");
    }

    #endregion


    #region PrivateFunctions
    private void Init()
    {
        notes.AddRange(transform.GetComponentsInChildren<TextMeshProUGUI>());
    }

    private void OnClicked()
    {
        if (isLocked || isSolved)
        {
            // Highlight same number in other cells
            GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
        }
        else
        {
            if (InputController.Instance.SelectedNumber == solutionNumber)
            {
                UpdateNumberText(InputController.Instance.SelectedNumber);
                isSolved = true;

                GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
            }
            else if(InputController.Instance.SelectedNumber == -1)
            {
                GridController.Instance.HiglightCellRowColumn(cellParent.CellIdx, position);
            }
            else
            {
                UpdateNumberText(InputController.Instance.SelectedNumber);
                isWrong = true;

                GridController.Instance.HiglightWrongNumberRowColumn(currentNumber, cellParent.CellIdx, position);
                HighlightWrongTile();

                Debug.Log($"The current number {InputController.Instance.SelectedNumber} it is not the same as {solutionNumber}");
                return;
            }

            HighlightSelectedTile();

        }

        
    }

    private void UpdateNumberText(int number)
    {
        currentNumber = number;
        numberTMP.text = currentNumber.ToString();
    }

    private void SetNumberToText()
    {
        if (isLocked)
            numberTMP.text = solutionNumber.ToString();
        else
            numberTMP.text = (currentNumber == -1) ? " " : currentNumber.ToString();
    }



    #endregion

    #region PublicFunctions

    public void SetSkin()
    {
        tileImage.color = SkinController.Instance.CurrentTileSkin.TileIdleColor;
        borderImage.color = SkinController.Instance.CurrentTileSkin.TileBorderColor;
        if(isLocked)
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberLockColor;
        else
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;
    }

    public void SetNumber(char solvedNum, char lockedNum, Vector2Int pos)
    {
        position = pos;

        solutionNumber = (int)char.GetNumericValue(solvedNum);

        if (lockedNum == '-')
        {
            currentNumber = -1;
            isLocked = false;
        }
        else
        {
            currentNumber = (int)char.GetNumericValue(lockedNum);
            isLocked = true;
        }

        SetNumberToText();
    }

    public void SetCell(Cell cell)
    {
        cellParent = cell;
    }

    public void HighlightTile()
    {
        isHighlight = true;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileHoverColor;
    }

    public void HighlightSameNumberTile()
    {
        isHighlight = true;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileSameNumberColor;
    }

    public void HighlightSelectedTile()
    {
        isHighlight = true;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileSelectedColor;
    }

    public void HighlightWrongTile()
    {
        isHighlight = true;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileWrongColor;
        
        if(isWrong)
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberWrongColor;
    }

    public void DownlightTile()
    {
        isHighlight = false;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileIdleColor;
    }

    public bool IsHighlighted()
    {
        return isHighlight;
    }

    public bool IsSolved()
    {
        return isLocked || isSolved;
    }

    public bool CheckCurrentNumber(int number)
    {
        return number == currentNumber;
    }

    public bool CheckNumber(int number)
    {
        return number == solutionNumber;
    }

    #endregion
}
