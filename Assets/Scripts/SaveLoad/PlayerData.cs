using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;

    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public float[] position;
    public PlayerData(Player player)
    {
        level = player.level.value;
        maxHealth = player.health.value;
        maxMana = player.mana;
        strength = player.strength;
        dexterity = player.dexterity;
        constitution = player.constitution;
        intelligence = player.intelligence;
        wisdom = player.wisdom;
        charisma = player.charisma;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
