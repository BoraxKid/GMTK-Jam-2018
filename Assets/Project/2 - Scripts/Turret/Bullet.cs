using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { private get; set; }
    public Vector2 Direction { private get; set; }
    public float Velocity { private get; set; }

    private void FixedUpdate()
    {
        this.transform.Translate(this.Direction * this.Velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("Trigger with " + collider.name);
        GameObject.Destroy(this.gameObject);
        // TODO: Particle effects and sound
        this.ApplyDamage(collider);
    }

    private void ApplyDamage(Collider2D collider)
    {
        Health health = null;
        if ((health = collider.GetComponent<Health>()) != null)
        {
            health.Hit(this.Damage);
            return;
        }
    }
}
