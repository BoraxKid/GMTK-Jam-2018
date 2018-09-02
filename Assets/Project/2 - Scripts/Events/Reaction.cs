using UnityEngine;
using UnityEngine.Events;

public class Reaction : MonoBehaviour
{
    [SerializeField] private UnityEvent _reactionEvent;
    [SerializeField] private float _reactionTime;

    private void OnDestroy()
    {
        this.CancelInvoke();
    }

    public void React()
    {
        this.Invoke("ReactImmediate", this._reactionTime);
    }

    public void ReactImmediate()
    {
        this._reactionEvent.Invoke();
    }
}
