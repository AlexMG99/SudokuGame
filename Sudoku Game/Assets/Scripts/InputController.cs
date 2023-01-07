using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper.Actions;

public class InputController : MonoBehaviourSingleton<InputController>
{
    public int SelectedNumber => selectedNumber;
    private int selectedNumber = -1;

    [SerializeField]
    private List<Action<int>> actionQueue = new List<Action<int>>();

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
    }

    
    #endregion

    #region PublicsFunctions
    public void ChangeSelectedNumber(int newNumber)
    {
        selectedNumber = newNumber;

        // Add removed number to actionQueue
        if (!selectedTile.IsSolved() && selectedTile.IsWrong())
           actionQueue.Add(new Action<int>(ActionType.RemoveValue, selectedTile.CurrentNumber));

        if(selectedTile)
            selectedTile.CheckNumber();

        // Add action to queue
        actionQueue.Add(new Action<int>(ActionType.AddValue, selectedNumber));
    }

    public void RemoveNumberOnTile()
    {
        if (selectedTile)
        {
            int tileNumber = selectedTile.CurrentNumber;
            if (selectedTile.RemoveNumber())
                actionQueue.Add(new Action<int>(ActionType.RemoveValue, tileNumber));
        }
    }

    public void SetSelectedTile(Tile newTile)
    {
        selectedTile = newTile;
    }
    #endregion
}
