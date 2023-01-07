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

    private bool isNotesMode = false;

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
        if (!selectedTile.IsSolved())
        {
            if(selectedTile.IsWrong())
                actionQueue.Add(new Action<int>(ActionType.RemoveValueWrong, selectedTile.CurrentNumber, selectedTile.Position));
            else
                actionQueue.Add(new Action<int>(ActionType.RemoveValue, selectedTile.CurrentNumber, selectedTile.Position));

            valueChanged = true;
        }

        if (selectedTile)
        {
            // Add action to queue
            if (selectedTile.CheckNumber())
                actionQueue.Add(new Action<int>(ActionType.AddValue, selectedNumber, selectedTile.Position));
            else
                actionQueue.Add(new Action<int>(ActionType.AddValueWrong, selectedNumber, selectedTile.Position));

            if (valueChanged)
                actionQueue.Add(new Action<int>(ActionType.ChangeValue, -1, selectedTile.Position));
        }

        
    }

    public void AddNoteNumber(int newNumber)
    {
        if (!selectedTile || selectedTile.IsSolved() || selectedTile.IsWrong())
            return;

        selectedNumber = newNumber - 1;

        if (selectedTile.SetNoteNumber(selectedNumber))
        {
            actionQueue.Add(new Action<int>(ActionType.AddNoteValue, selectedNumber, selectedTile.Position));
            
        }
        else
        {
            actionQueue.Add(new Action<int>(ActionType.RemoveNoteValue, selectedNumber, selectedTile.Position));
        }
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
                    actionTile.SetNumber(lastAction.value, false);
                    break;
                case ActionType.AddValueWrong:
                    actionTile.RemoveNumber(false);
                    break;
                case ActionType.RemoveValueWrong:
                    actionTile.SetNumber(lastAction.value, true);
                    break;
                case ActionType.AddNoteValue:
                case ActionType.RemoveNoteValue:
                    actionTile.SetNoteNumber(lastAction.value);
                    GridController.Instance.HiglightCellRowColumn(actionTile.CellParent.CellIdx, actionTile.Position);
                    actionTile.HighlightSelectedTile();
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

    public bool IsNotesMode()
    {
        return isNotesMode;
    }

    public void SetNotesMode(bool value)
    {
        isNotesMode = value;
    }
    #endregion
}
