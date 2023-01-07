using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper.Actions
{
    public enum ActionType
    {
        None = -1,
        AddValue,
        RemoveValue,
        AddNoteValue,
        RemoveNoteValue
    }

    [System.Serializable]
    public struct Action<T>
    {
        [SerializeField]
        public ActionType actionType;
        [SerializeField]
        public T value;

        public Action(ActionType actionType, T value)
        {
            this.actionType = actionType;
            this.value = value;
        }
    }
}
