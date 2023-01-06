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

    private Vector2Int position;

    private int solutionNumber;
    private int currentNumber;

    private bool isLocked = false;
    private bool isSolved = false;
    private bool isHighlight = false;

    #region MonoBehaviourFunctions
    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            // Deselect all other numbers
            GridController.Instance.DownlightAllCells();

            // Highlight same number in other cells
            GridController.Instance.HiglightNumberInCells(solutionNumber);
        }
        else
        {
            if (InputController.Instance.SelectedNumber == solutionNumber)
            {
                currentNumber = InputController.Instance.SelectedNumber;
                numberTMP.text = currentNumber.ToString();
                isSolved = true;
            }
            else if(InputController.Instance.SelectedNumber == -1)
            {
                GridController.Instance.DownlightAllCells();
            }
            else
            {
                Debug.Log($"The current number {InputController.Instance.SelectedNumber} it is not the same as {solutionNumber}");
            }
            
        }

        // Highlight rows and columns and Cell

        // Check if number is correct
        
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

    public void SetNumber(char solvedNum, char lockedNum)
    {
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

    public void HighlightTile()
    {
        isHighlight = true;

        tileImage.color = SkinController.Instance.CurrentTileSkin.TileHoverColor;
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

    public bool CheckSolvedNumber(int number)
    {
        return isLocked && number == solutionNumber;
    }

    public bool CheckNumber(int number)
    {
        return number == solutionNumber;
    }

    #endregion
}
