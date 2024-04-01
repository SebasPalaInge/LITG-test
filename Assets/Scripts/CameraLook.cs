using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private InputManager inputCamera;
    [SerializeField] private float lookSensitivity = 25f;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = inputCamera.input.CameraActions.MouseX.ReadValue<float>() * lookSensitivity * Time.deltaTime;
        float mouseY = inputCamera.input.CameraActions.MouseY.ReadValue<float>() * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.rotation *= Quaternion.Euler(0f, mouseX, 0f); 
    }
}
