using UnityEngine;

public class PlayerHitPoints : MonoBehaviour
{
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
            // TODO: Game over
        }
    }
}
