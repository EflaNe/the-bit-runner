using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRendererManager : MonoBehaviour
{
    [Header(" Renderers ")]
    [SerializeField] private GameObject bitRenderer;
    [SerializeField] private GameObject spaceshipRenderer;
    private void Awake()
    {
        PlayerMovementDash.onSpaceshipModeStarted += SpaceshipModeStartedCallback;
        PlayerMovementDash.onSquareModeStarted += SquareModeStartedCallback;
    }

    private void OnDestroy()
    {
        PlayerMovementDash.onSpaceshipModeStarted -= SpaceshipModeStartedCallback;
        PlayerMovementDash.onSquareModeStarted -= SquareModeStartedCallback;
    }

    private void SpaceshipModeStartedCallback()
    {
        bitRenderer.SetActive(false);
        spaceshipRenderer.SetActive(true);
    }


    private void SquareModeStartedCallback()
    {
        bitRenderer.SetActive(true);
        spaceshipRenderer.SetActive(false);
    }


}
