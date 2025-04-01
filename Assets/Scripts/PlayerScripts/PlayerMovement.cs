using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool playerOnGround;
    public Transform groundCheck;
    public LayerMask groundLayerMask;
    public float groundCheckRadius;
    public float moveSpeed = 3.0f;
    public float jumpHeight = 2.0f;
    public float gravityValue = -9.81f;
    public InventoryManager inventoryManager;
    public bool isRunning = false;
    [SerializeField] private float maxStamina = 100.0f;
    public float currentStamina;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        CharMovement();
    }

    private void CharMovement()
    {
        if ((currentStamina < maxStamina && !isRunning) || inventoryManager.isOpen)
            currentStamina += 7.0f * Time.deltaTime;

        if (currentStamina >= maxStamina)
            currentStamina = maxStamina;

        if (inventoryManager.isOpen)
        {
            isRunning = false;
            return;
        }
            
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerOnGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayerMask);

        if (playerOnGround && playerVelocity.y < 0)
            playerVelocity.y = 0;

        Vector3 moveDirectional = transform.forward * vertical + transform.right * horizontal;
        controller.Move(moveDirectional * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && playerOnGround)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

        if (Input.GetKeyDown(KeyCode.LeftShift) && playerOnGround)
        {
            isRunning = true;
            moveSpeed = 8.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && playerOnGround)
        {
            isRunning = false;
            moveSpeed = 4.0f;
        }

        if (isRunning)
            currentStamina -= 10.0f * Time.deltaTime;

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
