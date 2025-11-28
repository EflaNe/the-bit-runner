using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionController
{
    [Header("Componets")]
    protected Rigidbody2D rb;

    protected void Awake(GameObject player)
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    public  void SetGravity(float gravityScale) 
    {
        rb.gravityScale = gravityScale;
    }
    public void Explode() 
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    public void Revive() 
    {
        rb.isKinematic = false;
    }

    public float GetYVelocity() => rb.velocity.y;
    public abstract void FixedUpdate();
    public virtual void Update(bool isGrounded) 
    {
        Update();

    }
    public virtual void Update() 
    {

    }
}
