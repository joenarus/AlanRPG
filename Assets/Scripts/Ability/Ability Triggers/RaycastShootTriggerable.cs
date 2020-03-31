using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShootTriggerable : MonoBehaviour
{
    [HideInInspector] public int damage = 1;
    [HideInInspector] public float range = 10f;
    [HideInInspector] public float hitForce = 0f;
    [HideInInspector] public LineRenderer rayLine;
    public Transform spawnPoint;
    private Camera tpCam;
    private GameObject player;
    private WaitForSeconds duration = new WaitForSeconds(.07f);
    private new AudioSource audio;

    public void Initialize()
    {
        rayLine = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
        //audio = GetComponent<AudioSource>();
        tpCam = GameObject.Find("TPCamera").GetComponent<Camera>();
    }

    //TODO: FIGURE OUT how to shoot from player forward
    public void Fire()
    {
        
    }

    private IEnumerator ShotEffect()
    {
        rayLine.enabled = true;
        yield return duration;
        rayLine.enabled = false;
    }
}
