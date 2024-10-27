using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldCollision : MonoBehaviour
{
    [Header("Collision Detection")]
    [SerializeField] private int layer  = 6;
    [SerializeField] private float collisionHoldTime = 0.5f; // Time to wait before registering the collision
    [SerializeField] private UnityEvent collisionEvent;
    
    private Coroutine collisionCoroutine;
    
    //check the layer and start delayed collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layer) return;
        
        // Start the collision coroutine
        if (collisionCoroutine != null) return;
            
        collisionCoroutine = StartCoroutine(CollisionHold(other));
    }

    /// <summary>
    /// On the collision exit, check the layer and stop the collision coroutine
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layer) return;
        
        // Stop the collision coroutine if the collider leaves
        if (collisionCoroutine == null) return;
            
        StopCoroutine(collisionCoroutine);
        collisionCoroutine = null;
    }

    /// <summary>
    /// Waits a sec before establishing a collision
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private IEnumerator CollisionHold(Collider other)
    {
        // Wait for the specified hold time
        yield return new WaitForSeconds(collisionHoldTime);

        // Register the collision if still valid after the hold time
        if (other.gameObject.layer == layer)
        {
            collisionEvent.Invoke();
        }

        // Reset the coroutine reference
        collisionCoroutine = null;
    }
}
