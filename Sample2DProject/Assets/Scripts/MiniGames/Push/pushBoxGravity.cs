using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBoxGravity : MonoBehaviour
{
    bool hasHitGround;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHitGround) return;
        if(rb.transform.position.y < -0.45)
        {
            hasHitGround = true;
            rb.useGravity = false;
            rb.transform.position = new Vector3(rb.transform.position.x, -0.45f, rb.transform.position.z);
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }
}
