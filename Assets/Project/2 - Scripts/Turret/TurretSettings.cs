using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Turret", fileName = "New Turret Settings")]
public class TurretSettings : ScriptableObject
{
    public TurretHelper TurretPrefab;
    public int HitPoints;
    public int Damage;
    public int FiringRate; // Shots fired per minute
    public float Range;
    public float BuildingTime;
    public Bullet BulletPrefab;
    public float BulletVelocity;
}
