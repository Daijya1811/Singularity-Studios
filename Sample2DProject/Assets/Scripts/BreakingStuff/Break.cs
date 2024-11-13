using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField] float force;
    Rigidbody[] rbs;

    private void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    public void Destroy()
    {
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(force, transform.position, 10f, 1f, ForceMode.Impulse);
        }
    }
}
