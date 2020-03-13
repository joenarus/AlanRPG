using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGrounded = 0.5f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSettings = new MoveSettings();
    public PhysSettings physSettings = new PhysSettings();
    public InputSettings inputSettings = new InputSettings();

    Vector3 velocity = Vector3.zero;
    private Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput, jumpInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.distToGrounded, moveSettings.ground);
    }

    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        } 
        else
        {
            Debug.LogError("The character needs a rigidbody.");
        }

        forwardInput = turnInput = jumpInput = 0;

    }
    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
        jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS); //not interpolated
    }
    void Update()
    {
        GetInput();
        Turn();
    }
    void FixedUpdate()
    {
        Run();
        Jump();

        rBody.velocity = transform.TransformDirection(velocity);
    }

    void Run()
    {
        if(Mathf.Abs(forwardInput) > inputSettings.inputDelay)
        {
            velocity.z = moveSettings.forwardVel * forwardInput;
        }
        else
        {
            velocity.z = 0;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSettings.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput > 0 && Grounded())
        {
            velocity.y = moveSettings.jumpVel;
        }
        else if (jumpInput == 0 && Grounded())
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= physSettings.downAccel;
        }
    }

}
