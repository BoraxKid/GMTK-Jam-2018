using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    [SerializeField] private Transform _turretsContainer;
    [SerializeField] private int _playerTeamLayer;

    private TurretHelper _tmpTurret;
    private float _tmpBuildingTime;

    private void Awake()
    {
        if (this._turretsContainer == null)
            Debug.LogWarning("Turret container missing!!");
    }

    private void Update()
    {
        this._tmpTurret.transform.position = this.GetMouseScenePosition();

        if (Input.GetButtonDown("Place Turret") && !this._tmpTurret.IsTriggered())
        {
            // TODO: Construction time
            this._tmpTurret.gameObject.layer = this._playerTeamLayer;
            this._tmpTurret.enabled = false;
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
        if (this._tmpTurret != null)
            GameObject.Destroy(this._tmpTurret);

        this.enabled = true;
        this._tmpBuildingTime = turretSettings.BuildingTime;
        this._tmpTurret = GameObject.Instantiate(turretSettings.TurretPrefab, this._turretsContainer);
        this._tmpTurret.transform.position = this.GetMouseScenePosition();
    }
}
