using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;

    public Transform PlayerBody;

    float xRotation = 0f;

    InputAction Look; 

    public PlayerController PlayerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Look = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController != null && PlayerController.LockControls)
            return; 

        var mouse = Look.ReadValue<Vector2>();

        float mouseX = mouse.x * MouseSensitivity * Time.deltaTime;
        float mouseY = mouse.y * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        PlayerBody.Rotate(Vector3.up * mouseX); 
    }
}
