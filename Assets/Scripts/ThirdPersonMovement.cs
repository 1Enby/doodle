using UnityEngine;
using System.Collections.Generic;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        water_colliders = new List<Collider>();
    }

    private void Update()
    {

        //move the player back to the map if they fall off.
        if (transform.position.y <= -5)
        {
            transform.position = Playerdeath;
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

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            //if we are in water, don't set "isJumping" because we are allowed to jump a lot e.g. swimming
            bool inWater = (jumpForce == waterjumpForce);

            if (!inWater)
            {
                isJumping = true;
            }
        }
    }

    List<Collider> water_colliders;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Water"))
        {
            jumpForce = jumpingForce;
            isJumping = false;
            Playerdeath = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionExit(Collision collision)
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Enter");
        if (col.CompareTag("Water"))
        {
            water_colliders.Add(col);
            jumpForce = waterjumpForce;
        }
        isJumping = false;

    }
    void OnTriggerExit(Collider col)
    {
        foreach (Collider wc in water_colliders)
            Debug.Log(wc.gameObject.name);

        if (col.gameObject.CompareTag("Water"))
        {
            Playerdeath = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            water_colliders.Remove(col);
        }
        if (water_colliders.Count <= 0)
        {
            jumpForce = jumpingForce;
        }

    }



}

