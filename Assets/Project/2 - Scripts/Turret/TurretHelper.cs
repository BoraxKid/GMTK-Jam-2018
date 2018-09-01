using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer), typeof(TurretShoot))]
public class TurretHelper : MonoBehaviour
{
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _notPlaceableColor;
    [SerializeField] private Color _placeableColor;

    private SpriteRenderer _renderer;
    private TurretShoot _turretShoot;
    private List<Collider2D> _triggeredColliders = new List<Collider2D>();

    private void Awake()
    {
        this._renderer = this.GetComponent<SpriteRenderer>();
        this._turretShoot = this.GetComponent<TurretShoot>();
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.enabled)
            return;
        if (!this._triggeredColliders.Contains(collider))
            this._triggeredColliders.Add(collider);
        this.ModifyColor();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!this.enabled)
            return;
        if (this._triggeredColliders.Contains(collider))
            this._triggeredColliders.Remove(collider);
        this.ModifyColor();
    }

    private void ModifyColor()
    {
        if (this.IsTriggered())
            this._renderer.color = this._notPlaceableColor;
        else
            this._renderer.color = this._placeableColor;
    }

    public bool IsTriggered()
    {
        return (this._triggeredColliders.Count > 0);
    }
}
