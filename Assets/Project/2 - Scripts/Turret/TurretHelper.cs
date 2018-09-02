using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer), typeof(TurretShoot))]
public class TurretHelper : MonoBehaviour
{
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _notPlaceableColor;
    [SerializeField] private Color _placeableColor;

    public TurretPlacer _turretPlacer { get; set; }

    private SpriteRenderer _renderer;
    private TurretShoot _turretShoot;
    private List<Collider2D> _triggeredColliders = new List<Collider2D>();
    private bool _validDistance;

    private void Awake()
    {
        this._renderer = this.GetComponent<SpriteRenderer>();
        this._turretShoot = this.GetComponent<TurretShoot>();
        this._validDistance = false;
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
        this.CheckValidDistance();
        this.ModifyColor();
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
        if (Vector3.Distance(this.transform.position, this._turretPlacer.Player.position) <= this._turretPlacer.PlacableDistance)
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
}
