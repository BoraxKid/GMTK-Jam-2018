using UnityEngine;
using UnityEngine.Events;

public class ReactionDestroy : MonoBehaviour
{
    [SerializeField] private float _reactionTime;

    private GameObject _toDestroy;

    private void OnDestroy()
    {
        this.CancelInvoke();
    }

    public void React(GameObject destroy)
    {
        this._toDestroy = destroy;
        this.Invoke("ReactImmediate", this._reactionTime);
    }

    public void ReactImmediate()
    {
        GameObject.Destroy(this._toDestroy);
    }
}
