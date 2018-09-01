using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent _deathEvent;

    [SerializeField] private int _startingHitPoints;

    private int _remainingHitPoints;

    private void Awake()
    {
        this._remainingHitPoints = this._startingHitPoints;
    }

    public void Hit(int damage)
    {
        this._remainingHitPoints -= damage;
        if (this._remainingHitPoints <= 0)
        {
            GameObject.Destroy(this.gameObject);
            this._deathEvent.Invoke();
        }
    }

    public void SetStartingHitPoints(int hp, bool setCurrentHP)
    {
        this._startingHitPoints = hp;
        if (setCurrentHP)
            this._remainingHitPoints = this._startingHitPoints;
    }
}
