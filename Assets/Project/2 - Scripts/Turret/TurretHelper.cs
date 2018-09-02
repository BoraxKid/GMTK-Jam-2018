using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer), typeof(TurretShoot), typeof(TurretBuilder))]
public class TurretHelper : MonoBehaviour
{
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _notPlaceableColor;
    [SerializeField] private Color _placeableColor;

    public TurretPlacer TurretPlacer { get; set; }
    public EnemySpawner Spawner { get; set; }

    private SpriteRenderer _renderer;
    private TurretShoot _turretShoot;
    public TurretBuilder TurretBuilder;
    private List<Collider2D> _triggeredColliders = new List<Collider2D>();
    private bool _validDistance;
    private bool _building;

    private void Awake()
    {
        this.Spawner = null;
        this._renderer = this.GetComponent<SpriteRenderer>();
        this._turretShoot = this.GetComponent<TurretShoot>();
        this.TurretBuilder = this.GetComponent<TurretBuilder>();
        this._validDistance = false;
        this._building = false;
    }

    private void OnDestroy()
    {
        if (this.Spawner)
        {
            this.Spawner.NotifyDestroy(this);
        }
    }

    private void OnEnable()
    {
        this._renderer.color = this._notPlaceableColor;
        this._turretShoot.enabled = false;
    }

    private void OnDisable()
    {
        this._renderer.color = this._normalColor;
        this._turretShoot.enabled = true;
    }

    private void Update()
    {
        if (!this._building)
        {
            this.CheckValidDistance();
            this.ModifyColor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.enabled)
            return;
        if (!this._triggeredColliders.Contains(collider))
            this._triggeredColliders.Add(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!this.enabled)
            return;
        if (this._triggeredColliders.Contains(collider))
            this._triggeredColliders.Remove(collider);
    }

    private void ModifyColor()
    {
        if (!this.CanPlace())
            this._renderer.color = this._notPlaceableColor;
        else
            this._renderer.color = this._placeableColor;
    }

    private void CheckValidDistance()
    {
        if (Vector3.Distance(this.transform.position, this.TurretPlacer.Player.position) <= this.TurretPlacer.PlacableDistance)
            this._validDistance = true;
        else
            this._validDistance = false;
    }

    public bool IsTriggered()
    {
        return (this._triggeredColliders.Count > 0);
    }

    public bool CanPlace()
    {
        return (!this.IsTriggered() && this._validDistance);
    }

    public void SetBuilding(bool building)
    {
        this._building = building;
    }
}
