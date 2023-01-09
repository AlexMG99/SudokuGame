using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Helper.Actions;
using Audio.AudioSFX;

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

    private int tileScore = 30;
    public Cell CellParent => cellParent;
    private Cell cellParent;

    private bool isLocked = false;
    private bool isSolved = false;
    private bool isWrong = false;
    private TileStatus tileStatus = TileStatus.UNSELECTED;

    public enum TileStatus
    {
        UNSELECTED,
        HOVER,
        SELECTED,
        SAMENUMBER
    }

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
        if (GameManager.Instance.GameState != GameManager.GameStatus.PLAY)
            return;

        if (IsSolved())
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
        }

        HighlightSelectedTile();
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
        int countDeactivateNotes = 0;
        for(int i = 0; i < notes.Count; i++)
        {
            if(notes[i].enabled)
            {
                InputController.Instance.AddActionToQueue(new Action<int>(ActionType.RemoveNoteValue, i, Position));
                countDeactivateNotes++;

                notes[i].enabled = false;
            }
        }

        if(countDeactivateNotes > 0)
            InputController.Instance.AddActionToQueue(new Action<int>(ActionType.RemoveAllNotes, countDeactivateNotes, Position));
    }

    #endregion

    #region PublicFunctions

    public void ResetTile()
    {
        tileStatus = TileStatus.UNSELECTED;

        if (!isLocked)
        {
            isSolved = false;
            currentNumber = -1;
            numberTMP.text = " ";

            DisableNotes();
        }

        DownlightTile();
    }

    public void SetSkin()
    {
        switch (tileStatus)
        {
            case TileStatus.UNSELECTED:
                tileImage.color = SkinController.Instance.CurrentTileSkin.TileIdleColor;
                break;
            case TileStatus.SELECTED:
                tileImage.color = SkinController.Instance.CurrentTileSkin.TileSelectedColor;
                break;
            case TileStatus.HOVER:
                tileImage.color = SkinController.Instance.CurrentTileSkin.TileHoverColor;
                break;
            case TileStatus.SAMENUMBER:
                tileImage.color = SkinController.Instance.CurrentTileSkin.TileSameNumberColor;
                break;
            default:
                break;
        }

        borderImage.color = SkinController.Instance.CurrentTileSkin.TileBorderColor;

        if (isLocked)
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberLockColor;
        else if (IsWrong())
        {
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberWrongColor;
            tileImage.color = SkinController.Instance.CurrentTileSkin.TileWrongColor;
        }
        else
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;

        foreach (TextMeshProUGUI textNotes in notes)
        {
            textNotes.color = SkinController.Instance.CurrentTileSkin.NotesColor;
        }
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

        if (IsWrong())
            isWrong = false;

        numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;

        GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
        HighlightSelectedTile();

        cellParent.CheckCellSolved();

        return new Action<int>(ActionType.AddValue, solutionNumber, position);
    }

    public bool CheckNewNumber()
    {
        DisableNotes();

        if (InputController.Instance.SelectedNumber == solutionNumber)
        {
            UpdateNumberText(InputController.Instance.SelectedNumber);
            isSolved = true;
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;

            if (IsWrong())
                tileStatus = TileStatus.UNSELECTED;

            GridController.Instance.HiglightCellRowColumnNumber(solutionNumber, cellParent.CellIdx, position);
            HighlightSelectedTile();

            GridController.Instance.LevelController.AddScore(tileScore);
            tileScore = 0;
            cellParent.CheckCellSolved(true);

            AudioSFX.Instance.PlaySFX("Good");

            return true;
        }
        else
        {
            UpdateNumberText(InputController.Instance.SelectedNumber);
            isWrong = true;
            GridController.Instance.LevelController.AddMistake();

            GridController.Instance.HiglightWrongNumberRowColumn(currentNumber, cellParent.CellIdx, position);
            HighlightWrongTile();

            AudioSFX.Instance.PlaySFX("Wrong");

            Debug.Log($"The current number {InputController.Instance.SelectedNumber} it is not the same as {solutionNumber}");
            return false;
        }
    }

    public bool RemoveNumber()
    {
        if (isLocked)
            return false;

        // Set number to blank
        currentNumber = -1;
        numberTMP.text = " ";

        if (IsWrong())
            isWrong = false;

        if (isSolved)
            isSolved = false;

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

        if (IsWrong())
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

        if (IsWrong())
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
        tileStatus = TileStatus.HOVER;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileHoverColor;
    }

    public void HighlightSameNumberTile()
    {
        tileStatus = TileStatus.SAMENUMBER;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileSameNumberColor;
    }

    public void HighlightSelectedTile()
    {
        tileStatus = TileStatus.SELECTED;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileSelectedColor;
    }

    public void HighlightWrongTile()
    {
        tileImage.color = SkinController.Instance.CurrentTileSkin.TileWrongColor;
        tileStatus = TileStatus.SELECTED;

        if (IsWrong())
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberWrongColor;
    }

    public void DownlightTile()
    {
        tileStatus = TileStatus.UNSELECTED;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileIdleColor;
    }

    public bool IsHighlighted()
    {
        return tileStatus != TileStatus.UNSELECTED;
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
