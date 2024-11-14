using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField] float force;
    Rigidbody[] rbs;
    bool hasBroken = false;

    private void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    public void Destroy(Material mat)
    {
        if (hasBroken) return;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers )
        {
            renderer.material = mat;
        }
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(force, transform.position, 10f, 1f, ForceMode.Impulse);
        }
        hasBroken = true;
    }
}
