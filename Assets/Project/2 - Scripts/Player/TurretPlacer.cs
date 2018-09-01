using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    [SerializeField] private TurretHelper _turretPrefab;
    [SerializeField] private Transform _turretsContainer;
    [SerializeField] private int _playerTeamLayer;

    private TurretHelper _tmpTurret;

    private void Awake()
    {
        if (this._turretPrefab == null)
            Debug.LogWarning("Turret prefab missing!!");
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

    public void StartPlacingTurret()
    {
        if (this._tmpTurret != null)
            GameObject.Destroy(this._tmpTurret);

        this.enabled = true;

        this._tmpTurret = GameObject.Instantiate(this._turretPrefab, this._turretsContainer);
        this._tmpTurret.transform.position = this.GetMouseScenePosition();
    }
}
