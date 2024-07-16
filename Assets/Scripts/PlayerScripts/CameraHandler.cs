using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class CameraHandler : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float mouseSens = 5f;

    [SerializeField] private SanityStatusEffect sanityScript;

    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] private float bobbingSpeed = 14f;
    [SerializeField] private float verticalBobbingAmount = 0.05f;
    [SerializeField] private float horizontalBobbingAmount = 0.05f;
    private Vector3 defaultPos;
    private float timer = 0;

    public bool isCanvasEnabled;

    private void OnEnable()
    {
        CanvasManager.OnCanvasEnabled += isAnyCanvasOn;
    }
    private void OnDisable()
    {
        CanvasManager.OnCanvasEnabled -= isAnyCanvasOn;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sanityScript.playerFainted.AddListener(CameraControl);
        defaultPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCanvasEnabled)
        {
            CameraControl();
            ApplyHeadBob();
        }
    }

    private void CameraControl()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //yRotation += mouseX;
        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
    private void ApplyHeadBob()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            // Calculate bobbing
            float verticalBob = Mathf.Sin(timer) * verticalBobbingAmount;
            float horizontalBob = Mathf.Cos(timer / 2f) * horizontalBobbingAmount;

            timer += Time.deltaTime * bobbingSpeed;

            // Apply bobbing
            Vector3 newPosition = defaultPos + new Vector3(horizontalBob, verticalBob, 0);
            transform.localPosition = newPosition;
        }
        else
        {
            // Reset timer and smoothly return to default position
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPos, Time.deltaTime * bobbingSpeed);
        }
    }
    public void isAnyCanvasOn(bool isOn)
    {
        isCanvasEnabled = isOn;
    }
}
