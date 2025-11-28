using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Renderer[] tilemapRenderers;
    [Header("Settings")]
    [SerializeField] private float maxDistance;


    private void Start()
    {
        Ýnitialize();
    }

    void Ýnitialize() 
    {
        foreach (Renderer renderer in tilemapRenderers)
            renderer.material.SetFloat("_Max_Distance",maxDistance);
    }
    private void Update()
    {
        foreach (Renderer renderer in tilemapRenderers)
            renderer.material.SetVector("_CameraPosition", cameraTransform.position);
    }
}
