using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

    public GameObject interactPanel;
    public GameObject interactOriginPoint;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            // If no battle is happening -> Move normally
            if (BattleSystem.instance.state == BattleState.END)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    // Move to what we hit
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
            }
        }

        //TODO: make UI show up for interaction menu
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    //If no battle is happening -> Activate interaction panel
                    if (BattleSystem.instance.state == BattleState.END)
                    {
                        interactOriginPoint.transform.position = Input.mousePosition;
                        InteractPanel panel = interactPanel.GetComponent<InteractPanel>();
                        panel.setInteractions(interactable.interactions, interactable);
                        interactPanel.SetActive(true);
                    }
                }
            }
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
