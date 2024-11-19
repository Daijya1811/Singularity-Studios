using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{

    [SerializeField] private Transform transformToFollow;
    // Update is called once per frame
    void Update()
    {
        transform.position = transformToFollow.position;
    }
}
