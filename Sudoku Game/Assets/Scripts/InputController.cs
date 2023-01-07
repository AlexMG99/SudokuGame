using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviourSingleton<InputController>
{
    public int SelectedNumber => selectedNumber;
    private int selectedNumber = -1;

    [SerializeField]
    private Transform numberSelectionParent;
    private List<NumberSelection> numberSelections = new List<NumberSelection>();

    private Tile selectedTile;

    #region MonoBehaviourFunctions
    public void Start()
    {
        Init();
    }
    #endregion

    #region PrivateFunctions
    private void Init()
    {
        numberSelections.AddRange(numberSelectionParent.GetComponentsInChildren<NumberSelection>());

        foreach (NumberSelection numberSelection in numberSelections)
        {
            numberSelection.Init();
        }
    }

    
    #endregion

    #region PublicsFunctions
    public void ChangeSelectedNumber(int newNumber)
    {
        selectedNumber = newNumber;

        if(selectedTile)
            selectedTile.CheckNumber();
        //LowlightNumberSelectionNumbers();
    }

    public void LowlightNumberSelectionNumbers()
    {
        foreach (NumberSelection numberSelection in numberSelections)
        {
            numberSelection.LowlightNumber();
        }
    }

    public void SetSelectedTile(Tile newTile)
    {
        selectedTile = newTile;
    }
    #endregion
}
