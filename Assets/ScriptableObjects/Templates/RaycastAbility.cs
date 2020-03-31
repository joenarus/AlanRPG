using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Raycast Ability")]
public class RaycastAbility : Ability
{
    public int damage = 1;
    public float range = 50f;
    public float hitForce = 0f;
    public Color rayCastColor = Color.white;

    private RaycastShootTriggerable rcShoot;
    public override void Initialize(GameObject obj)
    {
        rcShoot = obj.GetComponent<RaycastShootTriggerable>();
        rcShoot.Initialize();

        rcShoot.damage = damage;
        rcShoot.range = range;
        rcShoot.hitForce = hitForce;
        rcShoot.rayLine.material = new Material(Shader.Find("Unlit/Color"));
        rcShoot.rayLine.material.color = rayCastColor;
    }

    public override void TriggerAbility()
    {
        rcShoot.Fire();
    }
}
