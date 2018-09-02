using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Turret Helper event", fileName = "New turret helper event")]
public class TurretHelperEvent : ScriptableObject
{
    private List<TurretHelperEventListener> _listeners = new List<TurretHelperEventListener>();

    public void RegisterListener(TurretHelperEventListener listener)
    {
        if (!this._listeners.Contains(listener))
            this._listeners.Add(listener);
        else
            Debug.LogWarning("Attempting to register listener of " + listener.name + " but it is already present");
    }

    public void UnregisterListener(TurretHelperEventListener listener)
    {
        if (this._listeners.Contains(listener))
            this._listeners.Remove(listener);
        else
            Debug.LogWarning("Attempting to unregister listener of " + listener.name + " but it is not present");
    }

    public void Raise(TurretHelper turretHelper)
    {
        for (int i = this._listeners.Count - 1; i >= 0; --i)
            this._listeners[i].OnEventRaised(turretHelper);
    }
}
