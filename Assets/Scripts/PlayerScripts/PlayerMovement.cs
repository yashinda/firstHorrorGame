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

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CharMovement();
    }

    private void CharMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerOnGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayerMask);

        if (playerOnGround && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        Vector3 moveDirectional = transform.forward * vertical + transform.right * horizontal;
        controller.Move(moveDirectional * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && playerOnGround)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
