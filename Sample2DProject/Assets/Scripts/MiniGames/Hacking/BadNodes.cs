using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hacking
{
    /// <summary>
    /// This class implements behavior and functionality for the Bad Node used in the hacking minigame. 
    /// </summary>
    public class BadNodes : MonoBehaviour
    {
        float rotSpeed = 50f;
        float radius = 0f;
        bool counterClockwise = false;
        float angle;
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
        /// <summary>
        /// Assigns random values to the radius, speed, the startPos, and whether or not its CW or CCW.
        /// </summary>
        private void Start()
        {
            center = GameObject.FindGameObjectWithTag("Goal").transform.position;

            radius = UnityEngine.Random.Range(2f, 9f);
            radius = Mathf.Floor(radius);

            rotSpeed = UnityEngine.Random.Range(45f, 80f);

            if (UnityEngine.Random.Range(0f, 1f) < 0.5f) counterClockwise = false;
            else counterClockwise = true;

            if (UnityEngine.Random.Range(0f, 1f) < 0.25f) startPos = Vector3.up;
            else if (UnityEngine.Random.Range(0f, 1f) < 0.50f) startPos = Vector3.down;
            else if (UnityEngine.Random.Range(0f, 1f) < 0.75f) startPos = Vector3.right;
            else startPos = Vector3.left;
        }
        // Update is called once per frame
        void Update()
        {
            if(!playerTriangle.FirstTimePlaying) mesh.enabled = true;
            RotateObject(center, radius, rotSpeed);
        }

        /// <summary>
        /// Rotates the object. 
        /// </summary>
        /// <param name="center"> Origin point of the rotation. </param>
        /// <param name="radius"> Radius of the rotation. </param>
        /// <param name="rotationSpeed"> How fast it rotates. </param>
        private void RotateObject(Vector3 center, float radius, float rotationSpeed)
        {
            // Angle in degrees that changes based on the rotationSpeed. += rotates CCW, -= rotates CW. 
            if(counterClockwise) angle += Time.deltaTime * rotationSpeed;
            else angle -= Time.deltaTime * rotationSpeed;

            // Calculate direction from center and rotate the up vector by angle degrees
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * startPos;
            transform.position = center + direction * radius;

            // Always look at the center
            transform.LookAt(center);
        }

        /// <summary>
        /// If the player collides with the bad node, make the player start from the beginning. 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "PlayerTriangle")
            {
                other.GetComponent<PlayerTriangleMovement>().ResetSpawn();
            }
        }
    }
}