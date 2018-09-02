using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Health))]
public class EnemyTurret : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        this.GetComponent<SpriteRenderer>().flipX = true;
        Vector2 tmp = this.GetComponent<Health>().Offset;
        tmp.x = - tmp.x;
        this.GetComponent<Health>().Offset = tmp;
    }
}
