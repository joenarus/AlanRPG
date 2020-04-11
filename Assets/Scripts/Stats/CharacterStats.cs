using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public BaseStats baseStats;
    public Stat maxHealth;
    public Stat damage;
    public Stat armor;

    public HealthBar healthbar;
    public GameObject levelBox;
    public int currentHealth { get; private set; }

    void Awake()
    {
        ReadBaseStats();
    }

    void ReadBaseStats()
    {
        //Warrior
        damage.AddModifier(baseStats.strength.GetValue());

        // Caster
        damage.AddModifier(baseStats.intelligence.GetValue());

        // Armor
        armor.AddModifier(baseStats.dexterity.GetValue());

        maxHealth = baseStats.maxHealth;

        currentHealth = maxHealth.GetValue();
        if (healthbar != null)
            healthbar.SetMaxHealth(maxHealth.GetValue());
        
        if(levelBox != null)
        {
            levelBox.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(baseStats.level.GetValue().ToString());
        }
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
