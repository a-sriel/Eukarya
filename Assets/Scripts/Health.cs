using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Public method to take damage
    public void damage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log (gameObject.name + " took " + amount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Public method to heal
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health stays within 0 and maxHealth

        Debug.Log(gameObject.name + " healed for " + amount + ". Current Health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        // Add death functionality here (e.g., animations, destroying the object)
        Destroy(gameObject);
    }
}
