using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1f;

    private float nextDamageTime;

    private void OnCollisionStay(Collision collision)
    {
        if (Time.time < nextDamageTime) return;

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null && collision.gameObject.CompareTag("Player"))
        {
            damageable.TakeDamage(damage);
            nextDamageTime = Time.time + damageCooldown;
        }
    }
}