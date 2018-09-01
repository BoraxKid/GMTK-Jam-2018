using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class HealthUnityEvent : UnityEvent<Health> {}

public class HealthEventListener : MonoBehaviour
{
    [SerializeField] private HealthEvent _event;
    [SerializeField] private HealthUnityEvent _response;

    private void OnEnable()
    {
        this._event.RegisterListener(this);
    }

    private void OnDisable()
    {
        this._event.UnregisterListener(this);
    }

    public void OnEventRaised(Health health)
    {
        this._response.Invoke(health);
    }
}
