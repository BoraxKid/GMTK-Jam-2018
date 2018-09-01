using UnityEngine;

public class HealthBarCreator : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBarPrefab;

    public void CreateHealthBar(Health health)
    {
        HealthBar healthBar = GameObject.Instantiate(this._healthBarPrefab, this.transform);
        healthBar.Health = health;
    }
}
