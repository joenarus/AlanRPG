using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "New Ability";
    public float baseCastTime = 1f;
    public float baseCoolDown = 1f;
    public Sprite aSprite;
    public AudioClip aSound;

    public abstract void Initialize(GameObject obj);

    public abstract void TriggerAbility();
}