using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovementDash))]
public class PlayerRotator : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Transform rotatorParent;
    private PlayerMovementDash playerController;

    [Header(" Settings ")]
    [SerializeField] private float angeleIncrement;
    [SerializeField] private float leanAngleTime;
    private bool isTweening;

    [Header(" Spaceship Settings")]
    [SerializeField] private float spaceshipRotationMultiplier;

    private void Awake()
    {
        playerController = GetComponent<PlayerMovementDash>();
        playerController.onExploded += Explode;
        playerController.onRevived += Revive;

        PlayerMovementDash.onSpaceshipModeStarted += SpaceshipModeStarted;
    }

    private void OnDestroy()
    {
        playerController.onRevived -= Explode;
        playerController.onExploded -= Revive;

        PlayerMovementDash.onSpaceshipModeStarted -= SpaceshipModeStarted;
    }
    private void Update()
    {
        if (playerController.IsDead())
            return;
        switch (playerController.GetMotionType()) 
        {
            case MotionType.Square:
                ManagerPlayerRotation();
                break;
            case MotionType.SpaceShip:
                ManagerSpaceshipRotation();
                break;
        }
    }

    private void ManagerSpaceshipRotation()
    {
        float yVelocity = playerController.GetYVelocity();
        yVelocity *= spaceshipRotationMultiplier;

        rotatorParent.right = Quaternion.Euler(Vector3.forward * yVelocity) * Vector3.right;
    }

    private void SpaceshipModeStarted()
    {
        LeanTween.cancel(rotatorParent.gameObject);
        rotatorParent.rotation = Quaternion.identity;
    }

    private void ManagerPlayerRotation() 
    {
        if (!playerController.IsGrounded())
            PlayerNotGroundedState();
        else
            PlayerGroundedState();
    }

    private void PlayerGroundedState()
    {
        if (playerController.IsPressing()) 
        {
            if (isTweening) 
            {
                isTweening = false;
                LeanTween.cancel(rotatorParent.gameObject);
            }
            return;
        }
        if (isTweening) 
            return;

        float closestAngle = Mathf.FloorToInt(rotatorParent.transform.localEulerAngles.z / 90) * 90;
        float angleDifference = rotatorParent.transform.localEulerAngles.z - closestAngle;

        if(angleDifference > 45) 
            angleDifference = (90 - angleDifference) * -1;

        LeanTween.cancel(rotatorParent.gameObject);
        LeanTween.rotateAroundLocal(rotatorParent.gameObject,Vector3.forward, -angleDifference,leanAngleTime);

        isTweening = true;
    }

    private void PlayerNotGroundedState() 
    {
        if (isTweening) 
        {
            LeanTween.cancel(rotatorParent.gameObject);
            isTweening = false;
        }
        rotatorParent.transform.localEulerAngles += Time.deltaTime * Vector3.forward * angeleIncrement; //localEulerAngles global dünya eksinine göre değil, kendi ekseni etrafında döner.
    }

    private void Explode() 
    {
        rotatorParent.gameObject.SetActive(false);
    }

    private void Revive() 
    {
        rotatorParent.gameObject.SetActive(true);

        rotatorParent.transform.rotation = Quaternion.identity;
    }
}
