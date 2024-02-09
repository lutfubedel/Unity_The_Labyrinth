using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovement : MonoBehaviour
{

    CharacterController controller;

    public float speed = 12f;
    public float jumpHeight;
    private float gravity = -9.18f;
    private float groundDistance = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;

    public bool isGrounded;
    public bool canJump;

    Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
