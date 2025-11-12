using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerManager
{
    public static Player instance;

    [Header("유저")]
    private Rigidbody rB;
    public float jP = 10f;
    private Vector2 mV;
    public float mS = 5f;

    [Header("시선")]
    private Vector2 cam;
    public Transform cameraContainer;
    private float minXLook;
    private float maxXLook;
    private float camCurXRot;
    private float lookSensitivity;
    private bool canLook = true;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }
    void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void Move(InputAction.CallbackContext key)
    {
        if (key.phase == InputActionPhase.Performed)
        {
            mV = key.ReadValue<Vector2>();
        }
        else if (key.phase == InputActionPhase.Canceled)
        {
            mV = Vector2.zero;
        }
    }
    public void Jump(InputAction.CallbackContext jump)
    {
        if (jump.phase == InputActionPhase.Started)
        {
            rB.AddForce(Vector2.up * jP, ForceMode.Impulse);
        }
    }
    public void Look(InputAction.CallbackContext look)
    {
        cam = look.ReadValue<Vector2>();
    }
    private void MoveMent()
    {
        Vector3 dir = transform.forward * mV.y + transform.right * mV.x;
        dir *= mS;
        dir.y = rB.velocity.y;

        rB.velocity = dir;
    }
    void CameraLook()
    {
        camCurXRot += cam.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, cam.x * lookSensitivity, 0);
    }
}
