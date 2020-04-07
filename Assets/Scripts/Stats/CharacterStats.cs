using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;
    public HealthBar healthbar;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthbar != null)
            healthbar.SetMaxHealth(maxHealth);
    }

    public bool TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        if (healthbar != null)
            healthbar.SetHealth(currentHealth);

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
