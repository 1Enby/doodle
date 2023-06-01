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
    Vector3 Flying;

   
    float PlayerFlyY;
    

    



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
                transform.position = Flying;
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

            PlayerFlyY = 100;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            jumpForce = waterjumpForce;
            isJumping = false;
            Flying = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
            Playerdeath = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
            PlayerFlyY = transform.position.y;

        }
        else
        {
            isJumping = false;
            PlayerFlyY = 100;
        }
    }

    

}

