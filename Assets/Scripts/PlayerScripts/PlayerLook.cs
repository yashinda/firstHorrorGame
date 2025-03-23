using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Camera playerCamera;
    public float sensitivity = 3.0f;
    public float maxYAngle = 70.0f;
    private float rotationMouseX;
    public Transform playerBody;
    public InventoryManager inventoryManager;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (inventoryManager.isOpen)
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationMouseX -= mouseY * sensitivity;
        rotationMouseX = Mathf.Clamp(rotationMouseX, -maxYAngle, maxYAngle);

        transform.localRotation = Quaternion.Euler(rotationMouseX, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX * sensitivity);
    }
}
