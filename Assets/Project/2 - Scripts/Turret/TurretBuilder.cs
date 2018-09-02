using UnityEngine;
using UnityEngine.Events;

public class TurretBuilder : MonoBehaviour
{
    [SerializeField] private UnityEvent _startBuildingEvent;
    [SerializeField] private UnityEvent _updateBuildingEvent;
    [SerializeField] private UnityEvent _stopBuildingEvent;
    [SerializeField] private int _playerTeamLayer;
    [SerializeField] private TurretHelperEvent _createBuildBarEvent;
    [SerializeField] private TurretHelperEvent _destroyBuildBarEvent;

    public float ProgressAmount { get; private set; }

    private TurretSettings _turretSettings;
    private TurretHelper _turret;
    private float _elapsedTime;

    private void OnEnable()
    {
        this._elapsedTime = 0.0f;
        this._startBuildingEvent.Invoke();
        if (this.gameObject.layer == this._playerTeamLayer)
            this._createBuildBarEvent.Raise(this._turret);
    }

    private void OnDestroy()
    {
        if (this.gameObject.layer == this._playerTeamLayer)
            this._destroyBuildBarEvent.Raise(this._turret);
    }

    private void Update()
    {
        this._elapsedTime += Time.deltaTime;
        this.ProgressAmount = Mathf.Clamp01(this._elapsedTime / this._turretSettings.BuildingTime);
        if (this._elapsedTime >= this._turretSettings.BuildingTime)
        {
            this._stopBuildingEvent.Invoke();
            if (this.gameObject.layer == this._playerTeamLayer)
                this._destroyBuildBarEvent.Raise(this._turret);
            this._turret.enabled = false;
            this.enabled = false;
        }
        else
        {
            this._updateBuildingEvent.Invoke();
        }
    }

    public void SetTurretToBuild(TurretHelper turret, TurretSettings turretSettings)
    {
        this._turret = turret;
        this._turretSettings = turretSettings;
    }
}
