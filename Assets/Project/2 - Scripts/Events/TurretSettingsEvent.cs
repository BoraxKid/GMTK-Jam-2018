using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Turret Settings event", fileName = "New turret settings event")]
public class TurretSettingsEvent : ScriptableObject
{
    private List<TurretSettingsEventListener> _listeners = new List<TurretSettingsEventListener>();

    public void RegisterListener(TurretSettingsEventListener listener)
    {
        if (!this._listeners.Contains(listener))
            this._listeners.Add(listener);
        else
            Debug.LogWarning("Attempting to register listener of " + listener.name + " but it is already present");
    }

    public void UnregisterListener(TurretSettingsEventListener listener)
    {
        if (this._listeners.Contains(listener))
            this._listeners.Remove(listener);
        else
            Debug.LogWarning("Attempting to unregister listener of " + listener.name + " but it is not present");
    }

    public void Raise(TurretSettings turretSettings)
    {
        for (int i = this._listeners.Count - 1; i >= 0; --i)
            this._listeners[i].OnEventRaised(turretSettings);
    }
}
