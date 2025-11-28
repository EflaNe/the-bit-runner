using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMotionController : MotionController
{
    [Header(" Data ")]
    private SpaceshipMotionData data;

    [Header(" Settings ")]
    private bool isFlying;

    public SpaceshipMotionController(GameObject player, SpaceshipMotionData data)
    {
        Awake(player);
        this.data = data;
    }
    public override void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            Fly();

    }
    public override void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = data.MoveSpeed;


        if (isFlying)
        {
            velocity.y += data.SpaceshipAcceleretion;
            isFlying = false;   

        }
        else 
        {
            velocity.y -= data.SpaceshipAcceleretion;
        }

        rb.velocity = velocity;
    }

    private void Fly() => isFlying = true;
   
}
