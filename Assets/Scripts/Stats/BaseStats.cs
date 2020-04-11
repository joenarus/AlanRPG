using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity/Entity Base Stats")]
public class BaseStats : ScriptableObject
{
    public Stat level;
    public Stat maxHealth;
    public Stat maxMana;

    public Stat strength;
    public Stat dexterity;
    public Stat constitution;
    
    public Stat intelligence;
    public Stat wisdom;
    
    public Stat charisma;
}
