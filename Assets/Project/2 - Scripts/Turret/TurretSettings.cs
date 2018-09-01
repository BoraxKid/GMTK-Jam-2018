using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Turret", fileName = "New Turret Settings")]
public class TurretSettings : ScriptableObject
{
    public int HitPoints;
    public int Damage;
    public int FiringRate; // Shots fired per minute
    public float Range;
    public Bullet BulletPrefab;
    public float BulletVelocity;
}
