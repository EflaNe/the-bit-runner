using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform target; //Player


    [Header("Settings")]
    [SerializeField] private float yFollowThreshold; //Kamere takip çizgisi
    [SerializeField] private float xOffset; //Kamera karakterin içine girmesin diye ayarlamalar.
    [SerializeField] private float yOffset; 
    [SerializeField] private float backToRestLerp; //Karakterin sýnýrýn altýndaysa, yumuþak þekilde karaktere dönme
    [SerializeField] private float yDistanceFollowPower; // karakter  yukarý çýktýkça yetiþme
    [SerializeField] private float baseYLerp;

    [Header(" Motion Type Settings")]
    private bool isSpaceshipMode;
    private void Awake()
    {
        isSpaceshipMode = false;

        PlayerMovementDash.onSpaceshipModeStarted += SpaceshipModeStartedCallback;
        PlayerMovementDash.onSquareModeStarted += SquareModeStartedCallback;
    }

    private void SquareModeStartedCallback()
    {
        isSpaceshipMode = false;
    }

    private void OnDestroy()
    {
        PlayerMovementDash.onSpaceshipModeStarted -= SpaceshipModeStartedCallback;
    }

    private void SpaceshipModeStartedCallback() 
    {
        isSpaceshipMode = true;
    }

    private void LateUpdate()
    {
        float targetX = target.position.x + xOffset;
        float targetY = GetTargetY(isSpaceshipMode);

        Vector3 targetPosition = new Vector3(targetX, targetY, -10);
        transform.position = targetPosition;

    }
    private float GetTargetY(bool isSpaceshipMode)
    {

        if (isSpaceshipMode)
            return yOffset;

        float targetY = transform.position.y;


        if (target.position.y < yFollowThreshold)
        {
            targetY = Mathf.Lerp(transform.position.y, yOffset, Time.deltaTime * backToRestLerp);
        }
        else
        {
            float lerpFactor = Mathf.Abs(transform.position.y - target.position.y);
            float lerpMultiplier = Mathf.Pow(lerpFactor, yDistanceFollowPower); // yavaþça keçiþ

            targetY = Mathf.Lerp(transform.position.y, target.position.y + yOffset, Time.deltaTime * lerpMultiplier * baseYLerp); // (b , h , t)
        }
        return targetY;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 from = Vector3.up * yFollowThreshold;
        from += Vector3.left * 50;
        Vector3 to = from + Vector3.right * 100;
        Gizmos.DrawLine(from, to);
    }
}
