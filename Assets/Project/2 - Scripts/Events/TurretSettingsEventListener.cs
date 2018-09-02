using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class TurretSettingsUnityEvent : UnityEvent<TurretSettings> {}

public class TurretSettingsEventListener : MonoBehaviour
{
    [SerializeField] private TurretSettingsEvent _event;
    [SerializeField] private TurretSettingsUnityEvent _response;

    private void OnEnable()
    {
        this._event.RegisterListener(this);
    }

    private void OnDisable()
    {
        this._event.UnregisterListener(this);
    }

    public void OnEventRaised(TurretSettings turretSettings)
    {
        this._response.Invoke(turretSettings);
    }
}
