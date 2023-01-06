using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEvent", menuName = "ScriptableObjects/Events/VoidEvent", order = 1)]
public class VoidEventSO : ScriptableObject
{
    private readonly List<VoidEventListenerSO> eventListeners = new List<VoidEventListenerSO>();

    virtual public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(VoidEventListenerSO listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(VoidEventListenerSO listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
