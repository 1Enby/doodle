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

    Vector3 Playerdeath;

    float PlayerFlyX;
    float PlayerFlyY;
    float PlayerFlyZ;

    float Fly = 1;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (transform.position.y <= -5)
            {
                transform.position = Playerdeath;
            }
        if (PlayerFlyY + 4 < transform.position.y)
            {
                transform.position = Playerdeath;
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
            isJumping = false;

            Playerdeath = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

            Fly = 1;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            jumpForce = waterjumpForce;
            isJumping = false;

            Playerdeath = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

            PlayerFlyX = transform.position.x;
            PlayerFlyY = transform.position.y;
            PlayerFlyZ = transform.position.z;
        }
        else
        {
            isJumping = false;

            Fly = 1;
        }
    }

      private void OnCollisionExit(Collision collision)
     {
          if (collision.gameObject.CompareTag("Water"))
        {
            jumpForce = 0;
        }
     }

    

}

