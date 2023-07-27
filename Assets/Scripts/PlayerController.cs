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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (moveDirection.magnitude > 0)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, stopDamping * Time.deltaTime);
        }
    }
}