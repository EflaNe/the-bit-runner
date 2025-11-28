using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Rigidbody2D))] //BU SCRÝPTÝN ÇALIÞMASI ÝÇÝN RÝGÝDBODY2D GEREKLÝ
public class PlayerMovementDash : MonoBehaviour
{  
    private State PlayerState;
    private MotionType motionType;

    [Header(" Components")]
    private MotionController motionController;
    private BitMotionController bmc;
    private SpaceshipMotionController spmc;

    [Header(" Data ")]
    [SerializeField] private BitMotionData bitMotionData;
    [SerializeField] private SpaceshipMotionData spaceshipMotionData;

    [Header("Elements")]
    [SerializeField] private Transform groundDetector;

    [Header("Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public float groundDetectionRadius;

    [Header("Actions")]
    public Action onExploded;
    public Action onRevived;

    [Header(" Effects ")]
    [SerializeField] private ParticleSystem explodeParticles;

    [Header(" Spaceship Settings ")]
    [SerializeField] private float spaceshipAcceleration;


    public static Action onSpaceshipModeStarted;
    public static Action onSquareModeStarted;
    public void Awake()
    {
        PlayerState = State.Alive;
        motionType = MotionType.Square;

        bmc = new BitMotionController(gameObject, bitMotionData);
        spmc = new SpaceshipMotionController(gameObject, spaceshipMotionData);
    }
    public void Start()
    {
        motionController = bmc;
    }

    public void Update()
    {
        if (IsDead())
            return;

        motionController.Update(IsGrounded());
    }
    public void FixedUpdate()
    {
        if (IsDead())
            return;

        motionController.FixedUpdate();
     
    }
    private void SetBitMotionType() 
    {
        if (motionType == MotionType.Square)
            return;

        motionType = MotionType.Square;

        motionController.SetGravity(1);

        motionController = bmc;

        onSquareModeStarted?.Invoke();
    }
    public void SetSpaceshipMotionType() 
    {
        if (motionType == MotionType.SpaceShip)
            return;

        motionType = MotionType.SpaceShip;

        motionController.SetGravity(0);

        motionController = spmc;

        onSpaceshipModeStarted?.Invoke();

        Debug.Log("Hit a Spaceship trigger");
    }
    public void Explode() 
    {
        PlayerState = State.Dead;

        motionController.Explode();

        explodeParticles.Play();

        LeanTween.delayedCall(2, Revive);

        onExploded?.Invoke();
    }

    private void Revive() 
    {
        PlayerState = State.Alive;

        transform.position = Vector3.up * .5f;

        motionController.Revive();

        onRevived?.Invoke();

        SetBitMotionType();
    }

    public bool IsDead() => PlayerState == State.Dead;

    public bool IsGrounded()
    {
        Collider2D ground = Physics2D.OverlapCircle(groundDetector.position, groundDetectionRadius, groundLayer); //Zemindemiyiz deðilmiyizin asýl sorumlusu bu baþkan.
        return ground != null;
    }
    public bool IsPressing() => Input.GetMouseButton(0);
    public bool IsSquareMode() => motionType == MotionType.Square;
    public float GetYVelocity() => motionController.GetYVelocity();
    public MotionType GetMotionType() => motionType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundDetector.position, groundDetectionRadius);
    }
}
