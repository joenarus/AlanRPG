using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 3.4f, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -8;
        public float zoomSmooth = 100;
        public float maxZoom = -2;
        public float minZoom = -15;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "OrbitHorizontal";
        public string ORBIT_VERTICAL = "OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
    }

    public Transform target;

    public PositionSettings positionSettings = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings inputSettings = new InputSettings();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    CharacterController charController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;



    // Start is called before the first frame update
    void Start()
    {
        SetCameraTarget(target);

        targetPos = target.position + positionSettings.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * positionSettings.distanceFromTarget;
        destination += targetPos;
        transform.position = destination;

    }

    public void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<CharacterController>())
            {
                charController = target.GetComponent<CharacterController>();
            }
            else
            {
                Debug.LogError("Camera's target needs a character controller");
            }
        }
        else
        {
            Debug.LogError("Your camera needs a target.");
        }
    }
    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(inputSettings.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(inputSettings.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(inputSettings.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(inputSettings.ZOOM);
    }

    void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }

    void Update()
    {
        GetInput();
        OrbitTarget();
        ZoomOnTarget();
    }

    void MoveToTarget()
    {
        targetPos = target.position + positionSettings.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * positionSettings.distanceFromTarget;
        destination += targetPos;
        transform.position = destination;
    }
    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, positionSettings.lookSmooth * Time.deltaTime);
    }

    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = -180;
        }

        orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

        if (orbit.xRotation > orbit.maxXRotation)
        {
            orbit.xRotation = orbit.maxXRotation;
        }
        if (orbit.xRotation < orbit.minXRotation)
        {
            orbit.xRotation = orbit.minXRotation;
        }
    }
    void ZoomOnTarget()
    {
        positionSettings.distanceFromTarget += zoomInput * positionSettings.zoomSmooth * Time.deltaTime;
        if (positionSettings.distanceFromTarget > positionSettings.maxZoom)
        {
            positionSettings.distanceFromTarget = positionSettings.maxZoom;
        }
        if (positionSettings.distanceFromTarget < positionSettings.minZoom)
        {
            positionSettings.distanceFromTarget = positionSettings.minZoom;
        }
    }
}
