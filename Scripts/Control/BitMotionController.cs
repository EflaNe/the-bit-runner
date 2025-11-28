using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitMotionController : MotionController
{
    [Header(" Data ")]
    private BitMotionData data;

    [Header(" Settings ")]
    private bool isJumping;
    private bool isGrounded;

    public BitMotionController(GameObject player, BitMotionData data) 
    {
        Awake(player);
        this.data = data;
    }
    public override void Update(bool isGrounded) 
    {
        this.isGrounded = isGrounded;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            Jump();

    }
    public override void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        velocity.x = data.MoveSpeed;


        if (isJumping)
        {
            velocity.y = data.JumpSpeed;
            isJumping = false; //Yerde deðil jump false demek.
        }

        rb.velocity = velocity;
    }

    private void Jump()
    {
        if (isGrounded)
            isJumping = true; //Yerdeyse jump true zýplayabilirsin demek.
    }
}
