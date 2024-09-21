using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircleExample : MonoBehaviour
{
    //this script moves a sprite 1 unit down
    public void move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
    }
}
