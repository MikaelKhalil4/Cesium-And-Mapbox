using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Speed of movement
    public float boostMultiplier = 2f; // Speed multiplier when holding Shift

    [Header("Rotation Settings")]
    public float rotationSensitivity = 100f; // Sensitivity of mouse rotation

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Move camera using WASD and QE for up/down
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down arrow keys
        float moveY = 0;

        if (Input.GetKey(KeyCode.E)) moveY = 1;
        if (Input.GetKey(KeyCode.Q)) moveY = -1;

        Vector3 move = new Vector3(moveX, moveY, moveZ).normalized;
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f);
        
        transform.Translate(move * speed * Time.deltaTime, Space.Self);

        // Rotate camera using mouse
        rotationX += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Limit vertical rotation

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
        
        // Unlock and show the cursor when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}