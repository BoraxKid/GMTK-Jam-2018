using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AcquireTarget))]
public class TurretShoot : MonoBehaviour
{
    [SerializeField] private TurretSettings _settings;
    [SerializeField] private UnityEvent _shotEvent;

    private Health _health;
    private AcquireTarget _acquireTarget;
    private Collider2D _target;
    private float _elapsedTime; // Time since last shot

    private void Awake()
    {
        this._health = this.GetComponent<Health>();
        this._acquireTarget = this.GetComponent<AcquireTarget>();
        if (this._settings == null)
            Debug.LogWarning("Turret Settings missing!!");
        this._elapsedTime = 0.0f;
        this.InvokeRepeating("UpdateTarget", 0.1f, 1.0f);
    }

    private void Start()
    {
        this._health.SetStartingHitPoints(this._settings.HitPoints);
    }

    private void OnDestroy()
    {
        this.CancelInvoke();
    }

    private void Update()
    {
        if (this._elapsedTime >= 60.0f / this._settings.FiringRate)
        {
            if (this._target != null)
            {
                Bullet tmp = GameObject.Instantiate(this._settings.BulletPrefab);
                tmp.Parent = this.GetComponent<Collider2D>();
                tmp.transform.position = this.transform.position;
                tmp.Damage = this._settings.Damage;
                tmp.Velocity = this._settings.BulletVelocity;
                Vector2 heading = this._target.transform.position - this.transform.position;
                tmp.Direction = heading / heading.magnitude;
                this._elapsedTime = 0.0f;
                this._shotEvent.Invoke();
            }
        }
        else
            this._elapsedTime += Time.deltaTime;
    }

    private void UpdateTarget()
    {
        Collider2D currentTarget = this._target;
        this._target = this._acquireTarget.GetTarget(this.transform, this._settings);
        if (this._target != null && this._target != currentTarget)
            this._elapsedTime = 0.0f;
    }
}
