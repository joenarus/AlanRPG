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
    private WaitForSeconds duration = new WaitForSeconds(.07f);
    private new AudioSource audio;

    public void Initialize()
    {
        rayLine = GetComponent<LineRenderer>();

        //audio = GetComponent<AudioSource>();

        tpCam = GetComponentInParent<Camera>();
    }

    public void Fire()
    {
        Vector3 rayOrigin = tpCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
        Debug.DrawRay(rayOrigin, tpCam.transform.forward * range, Color.green);
        RaycastHit hit;

        StartCoroutine(ShotEffect());

        rayLine.SetPosition(0, spawnPoint.position);

        if (Physics.Raycast(rayOrigin, tpCam.transform.forward, out hit, range))
        {
            rayLine.SetPosition(1, hit.point);

            // Get Reference to script associated with hit
            // Entity health = hit.collider.GetComponent<Entity>();
            // if (health != null)
            // {
            //     health.Damage(damage);
            // }
            // if (hit.rigidbody != null)
            // {
            //     hit.rigidbody.AddForce(-hit.normal * hitForce);
            // }
        }
        else {
            rayLine.SetPosition(1, tpCam.transform.forward * range);
        }
    }

    private IEnumerator ShotEffect() {
        rayLine.enabled = true;
        yield return duration;
        rayLine.enabled = false;
    }
}
