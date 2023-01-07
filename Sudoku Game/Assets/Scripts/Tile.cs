using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Helper.Actions;

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
    public int CurrentNumber => currentNumber;
    private int currentNumber;

    public Cell CellParent => cellParent;
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
        notes.AddRange(noteContainer.GetComponentsInChildren<TextMeshProUGUI>());

        foreach (TextMeshProUGUI textNote in notes)
        {
            textNote.enabled = false;
        }
    }

    private void OnClicked()
    {
        if (isLocked || isSolved)
        {
            // Highlight same number in other cells
            GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);

            InputController.Instance.SetSelectedTile(null);
        }
        else
        {
            // Set Selected Tile
            InputController.Instance.SetSelectedTile(this);

            // Highlight cells
            if (IsSolved())
                GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
            else
                GridController.Instance.HiglightCellRowColumn(cellParent.CellIdx, position);

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

    private void DisableNotes()
    {
        foreach (TextMeshProUGUI textNote in notes)
        {
            textNote.enabled = false;
        }
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

    public Action<int> SolveNumber()
    {
        UpdateNumberText(solutionNumber);
        isSolved = true;

        if (isWrong)
        {
            isWrong = false;
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;
        }

        GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
        HighlightSelectedTile();

        cellParent.CheckCellSolved();

        return new Action<int>(ActionType.AddValue, solutionNumber, position);
    }

    public bool CheckNumber()
    {
        DisableNotes();

        if (InputController.Instance.SelectedNumber == solutionNumber)
        {
            UpdateNumberText(InputController.Instance.SelectedNumber);
            isSolved = true;
            
            if(isWrong)
            {
                isWrong = false;
                numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;
            }

            GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
            HighlightSelectedTile();

            cellParent.CheckCellSolved();

            return true;
        }
        else
        {
            UpdateNumberText(InputController.Instance.SelectedNumber);
            isWrong = true;

            GridController.Instance.HiglightWrongNumberRowColumn(currentNumber, cellParent.CellIdx, position);
            HighlightWrongTile();

            Debug.Log($"The current number {InputController.Instance.SelectedNumber} it is not the same as {solutionNumber}");
            return false;
        }
    }

    public bool RemoveNumber()
    {
        if (IsSolved())
            return false;

        // Set number to blank
        currentNumber = -1;
        numberTMP.text = " ";

        // Refresh selected Tiles
        GridController.Instance.HiglightCellRowColumn(cellParent.CellIdx, position);
        HighlightSelectedTile();

        return true;
    }

    public bool RemoveNumber(bool isWrong)
    {
        if (IsSolved())
            return false;

        // Set number to blank
        currentNumber = -1;
        numberTMP.text = " ";

        // Refresh selected Tiles
        GridController.Instance.HiglightCellRowColumn(cellParent.CellIdx, position);
        HighlightSelectedTile();

        this.isWrong = isWrong;

        if (isWrong)
            HighlightWrongTile();
        else
            HighlightSelectedTile();

        return true;
    }
    public bool RemoveSolvedNumber()
    {
        // Set number to blank
        currentNumber = -1;
        numberTMP.text = " ";

        isSolved = false;
        cellParent.CheckCellSolved();

        // Refresh selected Tiles
        GridController.Instance.HiglightCellRowColumn(cellParent.CellIdx, position);
        HighlightSelectedTile();

        return true;
    }

    public void SetNumber(int number, bool isWrong)
    {
        // Set number to blank
        currentNumber = number;
        numberTMP.text = (currentNumber == -1) ? " " : currentNumber.ToString();

        // Refresh selected Tiles
        GridController.Instance.HiglightCellRowColumn(cellParent.CellIdx, position);
        HighlightSelectedTile();

        this.isWrong = isWrong;

        if (isWrong)
            HighlightWrongTile();
        else
            HighlightSelectedTile();
    }


    // Notes handler
    public bool SetNoteNumber(int noteNumber)
    {
        if (notes.Count == 0)
        {
            Debug.LogError($"Notes in tile {position} is empty!");
            return false;
        }

        TextMeshProUGUI currentNote = notes[noteNumber];
        currentNote.enabled = !currentNote.enabled;

        return currentNote.enabled;
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

    public bool IsWrong()
    {
        return isWrong;
    }

    public bool IsEmpty()
    {
        return currentNumber == -1;
    }

    #endregion
}
