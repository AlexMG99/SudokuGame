using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helper.Actions
{
    public enum ActionType
    {
        None = -1,
        AddValue,
        AddValueWrong,
        ChangeValue,
        RemoveValue,
        RemoveValueWrong,
        AddNoteValue,
        RemoveNoteValue,
        RemoveAllNotes
    }

    [System.Serializable]
    public struct Action<T>
    {
        [SerializeField]
        public ActionType actionType;
        [SerializeField]
        public Vector2Int position;
        [SerializeField]
        public T value;

        public Action(ActionType actionType, T value, Vector2Int position)
        {
            this.actionType = actionType;
            this.value = value;
            this.position = position;
        }
    }
}
