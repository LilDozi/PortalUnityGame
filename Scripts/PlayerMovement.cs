using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; // Reference to the Rigidbody
    public float moveSpeed = 5f; // Speed of movement
    public float jumpForce = 5f; // Jump strength
    public float mouseSensitivity = 100f; // Mouse look speed
    public Transform playerCamera; // Reference to the camera

    private float xRotation = 0f; // To store camera's vertical rotation

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Lock the cursor to the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down
        Vector3 movement = transform.TransformDirection(new Vector3(moveX, 0f, moveZ).normalized * moveSpeed);
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player left/right (yaw)
        transform.Rotate(0f, mouseX, 0f);

        // Rotate camera up/down (pitch) and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit to straight up/down
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}