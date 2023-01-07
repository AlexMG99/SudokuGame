using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (!selectedTile)
            return;

        selectedNumber = newNumber;

        // Add removed number to actionQueue
        bool valueChanged = false;
        if (!selectedTile.IsSolved() && selectedTile.IsWrong())
        {
            actionQueue.Add(new Action<int>(ActionType.RemoveValue, selectedTile.CurrentNumber, selectedTile.Position));
            valueChanged = true;
        }

        if(selectedTile)
            selectedTile.CheckNumber();

        // Add action to queue
        actionQueue.Add(new Action<int>(ActionType.AddValue, selectedNumber, selectedTile.Position));

        if(valueChanged)
            actionQueue.Add(new Action<int>(ActionType.ChangeValue, -1, selectedTile.Position));
    }

    public void RemoveNumberOnTile()
    {
        if (selectedTile && !selectedTile.IsEmpty())
        {
            int tileNumber = selectedTile.CurrentNumber;
            if (selectedTile.RemoveNumber())
                actionQueue.Add(new Action<int>(ActionType.RemoveValue, tileNumber, selectedTile.Position));
        }    
        
    }

    public void UndoMovement()
    {
        if(actionQueue.Count > 0)
        {
            Action<int> lastAction = actionQueue.Last();
            actionQueue.Remove(lastAction);

            Tile actionTile = GridController.Instance.FindTileByPosition(lastAction.position);
            selectedTile = actionTile;
            switch (lastAction.actionType)
            {
                case ActionType.AddValue:
                    if (actionTile.IsSolved())
                        actionTile.RemoveSolvedNumber();
                    else
                        actionTile.RemoveNumber();
                    break;
                case ActionType.ChangeValue:
                    UndoMovement();
                    UndoMovement();
                    break;
                case ActionType.RemoveValue:
                    actionTile.SetNumber(lastAction.value);
                    break;
                case ActionType.AddNoteValue:
                    break;
                case ActionType.RemoveNoteValue:
                    break;
                case ActionType.None:
                default:
                    Debug.LogError("Action has no type!");
                    break;
            }

        }
        else
        {
            Debug.Log("The queue is empty, there is no more elements in list!");
        }
    }

    public void SetSelectedTile(Tile newTile)
    {
        selectedTile = newTile;
    }
    #endregion
}
