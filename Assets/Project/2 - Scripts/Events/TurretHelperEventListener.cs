using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class TurretHelperUnityEvent : UnityEvent<TurretHelper> {}

public class TurretHelperEventListener : MonoBehaviour
{
    [SerializeField] private TurretHelperEvent _event;
    [SerializeField] private TurretHelperUnityEvent _response;

    private void OnEnable()
    {
        this._event.RegisterListener(this);
    }

    private void OnDisable()
    {
        this._event.UnregisterListener(this);
    }

    public void OnEventRaised(TurretHelper turretHelper)
    {
        this._response.Invoke(turretHelper);
    }
}
