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

    private int solutionNumber;
    private int currentNumber;

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
        SetSkin();
    }

    private void OnClicked()
    {
        if (isSolved)
        {
            // Deselect all other numbers
            GridController.Instance.DownlightAllCells();

            // Highlight same number in other cells
            GridController.Instance.HiglightNumberInCells(solutionNumber);
        }

        // Highlight rows and columns
    }

    private void ChangeColorImage(Color newColor)
    {
        tileImage.color = newColor;
    }



    #endregion

    #region PublicFunctions

    public void SetSkin()
    {
        tileImage.color = SkinController.Instance.CurrentTileSkin.TileIdleColor;
        borderImage.color = SkinController.Instance.CurrentTileSkin.TileBorderColor;
        if(isSolved)
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberLockColor;
        else
            numberTMP.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;
    }

    public void SetNumber(char solvedNum, char lockedNum)
    {
        solutionNumber = (int)char.GetNumericValue(solvedNum);

        if (lockedNum == '-')
        {
            solutionNumber = -1;
            isSolved = false;
        }
        else
        {
            solutionNumber = (int)char.GetNumericValue(lockedNum);
            isSolved = true;
        }

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

    public bool CheckSolvedNumber(int number)
    {
        return isSolved && number == solutionNumber;
    }

    #endregion
}
