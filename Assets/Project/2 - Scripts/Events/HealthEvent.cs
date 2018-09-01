using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Health event", fileName = "New health event")]
public class HealthEvent : ScriptableObject
{
    private List<HealthEventListener> _listeners = new List<HealthEventListener>();

    public void RegisterListener(HealthEventListener listener)
    {
        if (!this._listeners.Contains(listener))
            this._listeners.Add(listener);
        else
            Debug.LogWarning("Attempting to register listener of " + listener.name + " but it is already present");
    }

    public void UnregisterListener(HealthEventListener listener)
    {
        if (this._listeners.Contains(listener))
            this._listeners.Remove(listener);
        else
            Debug.LogWarning("Attempting to unregister listener of " + listener.name + " but it is not present");
    }

    public void Raise(Health health)
    {
        for (int i = this._listeners.Count - 1; i >= 0; --i)
            this._listeners[i].OnEventRaised(health);
    }
}
