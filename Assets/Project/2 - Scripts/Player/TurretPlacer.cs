using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    [SerializeField] private Transform _turretsContainer;
    [SerializeField] private int _playerTeamLayer;
    [SerializeField] public Transform Player;
    [SerializeField] public float PlacableDistance;
    [SerializeField] private TurretSettingsEvent _stopPlacingEvent;

    public bool Building { get; set; }

    private TurretHelper _tmpTurret;
    private TurretSettings _tmpTurretSettings;

    private void Awake()
    {
        if (this._turretsContainer == null)
            Debug.LogWarning("Turret container missing!!");
    }

    private void Update()
    {
        this._tmpTurret.transform.position = this.GetMouseScenePosition();

        if (Input.GetButtonDown("Place Turret") && this._tmpTurret.CanPlace())
        {
            this._tmpTurret.gameObject.layer = this._playerTeamLayer;
            this._tmpTurret.SetBuilding(true);
            this._stopPlacingEvent.Raise(this._tmpTurretSettings);
            this._tmpTurret.TurretBuilder.SetTurretToBuild(this._tmpTurret, this._tmpTurretSettings);
            this._tmpTurret.TurretBuilder.enabled = true;
            this._tmpTurret = null;
            this.enabled = false; // No updates necessary so we disable the component
        }
    }

    private Vector2 GetMouseScenePosition()
    {
        return (Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void StartPlacingTurret(TurretSettings turretSettings)
    {
        if (this.Building)
            return;
        if (this._tmpTurret != null)
            GameObject.Destroy(this._tmpTurret);

        this.enabled = true;
        this._tmpTurretSettings = turretSettings;
        this._tmpTurret = GameObject.Instantiate(turretSettings.TurretPrefab, this._turretsContainer);
        this._tmpTurret._turretPlacer = this;
        this._tmpTurret.transform.position = this.GetMouseScenePosition();
    }
}
