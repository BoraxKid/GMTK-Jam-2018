using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable] public class UnityEventInt : UnityEvent<int> {}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private UnityEventInt _readyToStartWaveEvent;
    [SerializeField] private UnityEventInt _clearedWaveEvent;
    [SerializeField] private UnityEventInt _clearedAllWavesEvent;
    [SerializeField] private Tilemap[] _spawnerTilemaps;
    [SerializeField] private Transform _enemyTurretContainer;
    [SerializeField] private int _playerTeamLayer;
    [SerializeField] private int _enemyTeamLayer;

    [System.Serializable]
    public class TileToTurretPrefab
    {
        public Tile key;
        public TurretSettings value;
    }

    [SerializeField] private List<TileToTurretPrefab> _tileToTurretPrefabList;
    private Dictionary<Tile, TurretSettings> _tileToTurretPrefab;
    private int _waveIndex;
    private List<TurretHelper> _remainingTurrets;
    private bool _check;

    private void Awake()
    {
        this._waveIndex = 0;
        this._check = false;
        this._remainingTurrets = new List<TurretHelper>();

        if (this._enemyTurretContainer == null)
            Debug.LogWarning("Enemy Turret Container missing!!");

        foreach (Tilemap tilemap in this._spawnerTilemaps)
        {
            TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
            tilemapRenderer.enabled = false;
        }

        this.InvokeRepeating("CheckWaveCleared", 10.0f, 0.5f);
        this._readyToStartWaveEvent.Invoke(this._waveIndex + 1);
    }

    private void OnDestroy()
    {
        this.CancelInvoke();
    }

    private void CheckWaveCleared()
    {
        if (this._check && this._remainingTurrets.Count == 0)
        {
            this._check = false;
            // Wave cleared
            if (this._waveIndex >= this._spawnerTilemaps.Length)
            {
                this._clearedAllWavesEvent.Invoke(this._waveIndex);
            }
            else
            {
                this._clearedWaveEvent.Invoke(this._waveIndex);
                this._readyToStartWaveEvent.Invoke(this._waveIndex + 1);
            }
        }
    }

    public void SpawnWave()
    {
        this._remainingTurrets.Clear();

        this._tileToTurretPrefab = new Dictionary<Tile, TurretSettings>();
        foreach (TileToTurretPrefab pair in this._tileToTurretPrefabList)
            this._tileToTurretPrefab.Add(pair.key, pair.value);

        BoundsInt tilemapBounds = this._spawnerTilemaps[this._waveIndex].cellBounds;
        TileBase[] tiles = this._spawnerTilemaps[this._waveIndex].GetTilesBlock(tilemapBounds);
        Tile tile;

        for (int i = 0; i < tilemapBounds.size.x; ++i)
        {
            for (int j = 0; j < tilemapBounds.size.y; ++j)
            {
                if (tiles[i + j * tilemapBounds.size.x] != null)
                {
                    tile = tiles[i + j * tilemapBounds.size.x] as Tile;
                    if (!this._tileToTurretPrefab.ContainsKey(tile))
                        Debug.LogWarning("Tile " + tile.name + " (sprite: " + tile.sprite.name + ") not found in dictionnary!!");
                    else
                    {
                        Vector3 tmp = this._spawnerTilemaps[this._waveIndex].GetCellCenterLocal(new Vector3Int(i, j, 0));
                        tmp.x += tilemapBounds.position.x;
                        tmp.y += tilemapBounds.position.y;
                        this.SpawnTurret(this._tileToTurretPrefab[tile], tmp);
                    }
                }
            }
        }
        ++this._waveIndex;
        this._check = true;
    }

    private void SpawnTurret(TurretSettings turretSettings, Vector3 position)
    {
        TurretHelper turret = GameObject.Instantiate(turretSettings.TurretPrefab, this._enemyTurretContainer);
        turret.transform.position = position;
        turret.enabled = false;
        turret.gameObject.layer = this._enemyTeamLayer;
        turret.GetComponent<AcquireTarget>().SetTargetLayer(this._playerTeamLayer);
        turret.Spawner = this;
        this._remainingTurrets.Add(turret);
    }

    public void NotifyDestroy(TurretHelper turret)
    {
        this._remainingTurrets.Remove(turret);
    }
}
