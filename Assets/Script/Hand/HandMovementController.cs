using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovementController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private GameObject handObject;
    [Header("Movement")]
    [SerializeField]
    private float maxZDistance;
    [SerializeField]
    private float maxXDistance;
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float movementSensibility = 0.01f;
    [Header("Rotation")]
    [SerializeField]
    private KeyCode rotationKey;
    [SerializeField]
    private float maxXRotation;
    [SerializeField]
    private float maxZRotation;

    //Movement
    private float maxZPosition;
    private float minZPosition;
    private float maxXPosition;
    private float minXPosition;
    private float maxYPosition;
    private float minYPosition;
    private Vector2 mousePosition;
    private Vector2 mouseWheel;

    //Rotation
    private float maxXAngle;
    private float minXAngle;
    private float maxZAngle;
    private float minZAngle;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        //Movement
        maxZPosition = transform.position.z + maxZDistance;
        maxXPosition = transform.position.x + maxXDistance;
        minZPosition = transform.position.z;
        minXPosition = transform.position.x - maxXDistance;
        maxYPosition = transform.position.y + maxHeight;
        minYPosition = transform.position.y;

        //Rotation
        maxXAngle = QuaternionUtility.GetInspectorAngleByEuler(handObject.transform.eulerAngles.x) + maxXRotation;
        maxZAngle = QuaternionUtility.GetInspectorAngleByEuler(handObject.transform.eulerAngles.z) + maxZRotation;
        minXAngle = QuaternionUtility.GetInspectorAngleByEuler(handObject.transform.eulerAngles.x) - maxXRotation;
        minZAngle = QuaternionUtility.GetInspectorAngleByEuler(handObject.transform.eulerAngles.z) - maxZRotation;
    }

    void Update()
    {
        mousePosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseWheel = Input.mouseScrollDelta;

        if (Input.GetKey(rotationKey))
            RotateHand();
        else
            MoveHand();
    }

    private void MoveHand()
    {
        mousePosition *= movementSensibility;
        mouseWheel *= movementSensibility;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + mousePosition.x, minXPosition, maxXPosition), Mathf.Clamp(transform.position.y + mouseWheel.y, minYPosition, maxYPosition), Mathf.Clamp(transform.position.z + mousePosition.y, minZPosition, maxZPosition));
    }

    private void RotateHand()
    {
        handObject.transform.Rotate(new Vector3(mousePosition.y, 0, mousePosition.x));

        Vector3 clampedEulers = handObject.transform.eulerAngles;
        clampedEulers.x = QuaternionUtility.GetEulerByInspectorAngle(Mathf.Clamp(QuaternionUtility.GetInspectorAngleByEuler(handObject.transform.eulerAngles.x + mousePosition.y), minXAngle, maxXAngle));
        clampedEulers.y = 0f;
        clampedEulers.z = QuaternionUtility.GetEulerByInspectorAngle(Mathf.Clamp(QuaternionUtility.GetInspectorAngleByEuler(handObject.transform.eulerAngles.z + mousePosition.x), minZAngle, maxZAngle));
        handObject.transform.eulerAngles = clampedEulers;
    }
}
