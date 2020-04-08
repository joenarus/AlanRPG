using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string abilityName = "New Ability";
    public int level;
    public IntRange damage;
    public IntRange heal;
    public bool AoE;
    public Sprite aSprite;
    public AudioClip aSound;

    public abstract void Initialize(GameObject obj);

    public abstract void ActivateSkill();

}
