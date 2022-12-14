using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float airSpeed = 8f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Vector3 move;

    void Update()
    {
        grounded();
        doMove();
        doJump();
        doGravity();
    }

    void doMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed;

        if (isGrounded)
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = airSpeed;
        }

        move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * Time.deltaTime * currentSpeed);
    }
    
    void grounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            velocity.y = -2f;
        }
    }

    void doJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100.0f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    void doGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
