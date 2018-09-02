using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _spawnerTilemap;
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

    private void Awake()
    {
        if (this._enemyTurretContainer == null)
            Debug.LogWarning("Enemy Turret Container missing!!");

        TilemapRenderer tilemapRenderer = this._spawnerTilemap.GetComponent<TilemapRenderer>();
        tilemapRenderer.enabled = false;

        this._tileToTurretPrefab = new Dictionary<Tile, TurretSettings>();
        foreach (TileToTurretPrefab pair in this._tileToTurretPrefabList)
            this._tileToTurretPrefab.Add(pair.key, pair.value);

        BoundsInt tilemapBounds = this._spawnerTilemap.cellBounds;
        TileBase[] tiles = this._spawnerTilemap.GetTilesBlock(tilemapBounds);
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
                        Vector3 tmp = this._spawnerTilemap.GetCellCenterLocal(new Vector3Int(i, j, 0));
                        tmp.x += tilemapBounds.position.x;
                        tmp.y += tilemapBounds.position.y;
                        this.SpawnTurret(this._tileToTurretPrefab[tile], tmp);
                    }
                }
            }
        }
    }

    private void SpawnTurret(TurretSettings turretSettings, Vector3 position)
    {
        TurretHelper turretHelper = GameObject.Instantiate(turretSettings.TurretPrefab, this._enemyTurretContainer);
        turretHelper.transform.position = position;
        turretHelper.enabled = false;
        turretHelper.gameObject.layer = this._enemyTeamLayer;
        turretHelper.GetComponent<AcquireTarget>().SetTargetLayer(this._playerTeamLayer);
    }
}
