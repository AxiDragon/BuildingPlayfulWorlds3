using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Transform sphereCheckPos;
    LayerMask groundMask;

    Vector3 moveVector;
    public float speed, jumpForce, groundDistance = 1f;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");

        foreach(Transform child in transform)
        {
            if (child.name == "GroundCheck")
            {
                sphereCheckPos = child;
                break;
            }
        }

        speed *= transform.lossyScale.x;
        jumpForce *= transform.lossyScale.x;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(sphereCheckPos.position, groundDistance, groundMask);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        else
            isGrounded = false;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speedMod = Input.GetKey(KeyCode.LeftShift) ? speed * 2f : speed;

        moveVector.Set(horizontal, 0f, vertical);
        moveVector.Normalize();
        moveVector = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * moveVector * speedMod * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector);
        rb.AddForce(moveVector);
        rb.angularVelocity = Vector3.zero;
    }
}
