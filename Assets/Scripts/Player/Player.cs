using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerManager
{
    public static Player instance;

    [Header("유저")]
    public Rigidbody rB;
    public float jP = 10f;
    private Vector2 mV;
    public float mS = 5f;

    [Header("시선")]
    private Vector2 cam;
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private bool canLook = true;

    public string itemName;
    public string itemInfo;



    private void Awake()
    {
        instance = this;
    }
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
        ItemCheck();
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 halfExtents = transform.lossyScale / 2f;
        Vector3 direction = transform.forward;
        float distance = 10f; // 박스 캐스트 거리
        Gizmos.matrix = Matrix4x4.TRS(start, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f); // 시작 위치의 박스
        Gizmos.matrix = Matrix4x4.TRS(start + direction * distance, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f); // 이동 후 위치의 박스
    }
    public void ItemCheck()
    {

        float maxDistance = 10;
        RaycastHit hit;
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, maxDistance);

        if (isHit)
        {
            if (hit.collider.gameObject.TryGetComponent<ItemData>(out ItemData what))
            {
                GameUI.instance.itemName.text = what.item.displayName;
            }
            else return;

        }
    }


}
