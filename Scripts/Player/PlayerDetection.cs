using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementDash))]
public class PlayerDetection : MonoBehaviour
{

    [Header(" Settings ")]
    [SerializeField] private LayerMask gameoverMask;

    [Header(" Components ")]
    [SerializeField] private Collider2D gameoverTrigger;
    private PlayerMovementDash playerController;
    private void Explode() => playerController.Explode();

    private void Awake()
    {
        playerController = GetComponent<PlayerMovementDash>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.IsTouching(gameoverTrigger) && IsInLayerMask(collision.gameObject.layer, gameoverMask))
            Explode();

        if (collision.TryGetComponent(out SpaceshipTrigger spaceshipTrigger))
            playerController.SetSpaceshipMotionType();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.collider.gameObject.layer) == "Obstacle") //Eðer Obstacle Collisonuna çarparsak....
            Explode();
            
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask) 
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
    
}
