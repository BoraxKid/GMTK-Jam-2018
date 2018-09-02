using UnityEngine;
using System.Collections.Generic;

public class BuildBarCreator : MonoBehaviour
{
    [SerializeField] private BuildBar _buildBarPrefab;

    private Dictionary<TurretHelper, BuildBar> _buildBars;

    private void Awake()
    {
        this._buildBars = new Dictionary<TurretHelper, BuildBar>();
    }

    public void CreateBuildBar(TurretHelper turret)
    {
        BuildBar tmp = GameObject.Instantiate(this._buildBarPrefab, this.transform);
        tmp.name = turret.name + " Build Bar";
        tmp.Turret = turret.GetComponent<TurretBuilder>();
        this._buildBars.Add(turret, tmp);
    }

    public void DestroyBuildBar(TurretHelper turret)
    {
        if (this._buildBars.ContainsKey(turret))
        {
            GameObject.Destroy(this._buildBars[turret].gameObject);
            this._buildBars.Remove(turret);
        }
    }
}
