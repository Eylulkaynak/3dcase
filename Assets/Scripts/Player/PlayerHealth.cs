using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        //Debug.Log("Player Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateUI();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }

        gameObject.SetActive(false);
    }
}