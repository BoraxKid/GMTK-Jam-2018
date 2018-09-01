using UnityEngine;

[RequireComponent(typeof(AcquireTarget))]
public class TurretShoot : MonoBehaviour
{
    [SerializeField] private TurretSettings _settings;

    private AcquireTarget _acquireTarget;
    private Collider2D _target;
    private float _elapsedTime; // Time since last shot
    private int _remainingHitPoints;

    private void Awake()
    {
        this._acquireTarget = this.GetComponent<AcquireTarget>();
        if (this._settings == null)
            Debug.LogWarning("Turret Settings missing!!");
        this._elapsedTime = 0.0f;
        this._remainingHitPoints = this._settings.HitPoints;
        this.InvokeRepeating("UpdateTarget", 0.1f, 1.0f);
    }

    private void Update()
    {
        if (this._elapsedTime >= 60.0f / this._settings.FiringRate)
        {
            if (this._target != null)
            {
                Bullet tmp = GameObject.Instantiate(this._settings.BulletPrefab, this.transform);
                tmp.transform.position = this.transform.position;
                tmp.Damage = this._settings.Damage;
                tmp.Velocity = this._settings.BulletVelocity;
                Vector2 heading = this._target.transform.position - this.transform.position;
                tmp.Direction = heading / heading.magnitude;
                this._elapsedTime = 0.0f;
            }
        }
        else
            this._elapsedTime += Time.deltaTime;
    }

    private void UpdateTarget()
    {
        this._target = this._acquireTarget.GetTarget(this.transform, this._settings);
    }

    // Called when the turret is shot
    public void Hit(int damage)
    {
        this._remainingHitPoints -= damage;
        if (this._remainingHitPoints <= 0)
        {
            GameObject.Destroy(this.gameObject);
            // TODO: Particle effects and sound
        }
    }

    public void SetTarget(Collider2D target)
    {
        this._target = target;
    }
}
