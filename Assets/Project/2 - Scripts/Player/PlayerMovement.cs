using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _startingHitPoints;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movement;

    private void Awake()
    {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
        this.GetComponent<Health>().SetStartingHitPoints(this._startingHitPoints);
    }

    private void Update()
    {
        this._movement.x = Input.GetAxis("Horizontal") * this._speed;
        this._movement.y = Input.GetAxis("Vertical") * this._speed;

        this._rigidbody.velocity = this._movement;
    }
}
