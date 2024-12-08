using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hacking
{
    /// <summary>
    /// This class implements behavior and functionality for the Blocker Node used in the hacking minigame. 
    /// </summary>
    public class BlockerNodes : MonoBehaviour
    {
        float radius;
        float randomX;
        float randomY;
        float theta;
        Vector3 center;
        Vector3 startPos;
        PlayerTriangleMovement playerTriangle;
        MeshRenderer mesh;

        // Don't display during the tutorial popup. 
        private void OnEnable()
        {
            playerTriangle = FindObjectOfType<PlayerTriangleMovement>();
            mesh = GetComponent<MeshRenderer>();
            if (playerTriangle.FirstTimePlaying) mesh.enabled = false;
        }
        // Start is called before the first frame update
        void Start()
        {
            // Get a random radius
            radius = UnityEngine.Random.Range(2f, 9f);
            radius = Mathf.Floor(radius);

            // Get a random position on the Unit Circle
            theta = UnityEngine.Random.Range(0f, 359f);
            randomX = Mathf.Cos(theta);
            randomY = Mathf.Sin(theta);
            startPos = new Vector3(randomX, randomY, 0);

            // Apply the new position with the radius
            center = GameObject.FindGameObjectWithTag("Goal").transform.position;
            transform.position = center + startPos * radius;
            transform.LookAt(Vector3.zero);
        }

        void Update()
        {
            if (!playerTriangle.FirstTimePlaying) mesh.enabled = true;
        }
    }
}
