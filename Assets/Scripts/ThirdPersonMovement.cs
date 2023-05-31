using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public float jumpingForce = 5f;

    float jumpForce = 5f;

    public float waterjumpForce = 1f;

    private Rigidbody rb;
    private bool isJumping = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    
        if (transform.position.y >= 10)
        {
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            isJumping = true;
        }




        // Character movement
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(Vector3.up * turnInput * turnSpeed * Time.deltaTime);

        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Character jumping

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            if (jumpForce == waterjumpForce)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            }
            else
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpForce = jumpingForce;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            jumpForce = waterjumpForce;
            isJumping = false;
        }
        else
        {
            isJumping = false;
        }
    }

}

