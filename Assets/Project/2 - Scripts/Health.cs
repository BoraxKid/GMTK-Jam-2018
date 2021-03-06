﻿using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent _createHealthBar;
    [SerializeField] private UnityEvent _deathEvent;
    [SerializeField] private UnityEvent _hitEvent;
    [SerializeField] public Vector2 Offset; // Offset for Healthbar - Display in UI

    private HealthBar _healthBar;
    private int _startingHitPoints;
    private int _remainingHitPoints;

    private void OnDestroy()
    {
        this.DestroyHealthBar();
    }

    public void DestroyHealthBar()
    {
        if (this._healthBar)
        {
            GameObject.Destroy(this._healthBar.gameObject);
            this._healthBar = null;
        }
    }

    public void Hit(int damage)
    {
        this._remainingHitPoints -= damage;
        if (this._healthBar != null)
            this._healthBar.UpdateHealth(this._remainingHitPoints, this._startingHitPoints);
        this._hitEvent.Invoke();
        if (this._remainingHitPoints <= 0)
            this._deathEvent.Invoke();
    }

    public void SetStartingHitPoints(int hp)
    {
        this._startingHitPoints = hp;
        this._remainingHitPoints = this._startingHitPoints;
        this._createHealthBar.Invoke();
    }

    public void SetHealthBar(HealthBar healthBar)
    {
        this._healthBar = healthBar;
    }
}
