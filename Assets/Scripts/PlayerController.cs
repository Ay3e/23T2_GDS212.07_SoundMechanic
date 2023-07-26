using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stopDamping = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player using Rigidbody
        if (moveDirection.magnitude > 0)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // Apply stopping force to the Rigidbody to come to an immediate stop
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, stopDamping * Time.deltaTime);
        }
    }
}