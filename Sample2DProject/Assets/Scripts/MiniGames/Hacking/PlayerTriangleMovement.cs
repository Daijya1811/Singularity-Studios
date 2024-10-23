using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hacking
{
    public class PlayerTriangleMovement : MonoBehaviour
    {
        [SerializeField] float rotSpeed = 50f;
        [SerializeField] float radius = 10f;
        Vector2 movementInput = Vector2.zero;
        public Vector3 spawnPos;
        Vector3 center = Vector3.zero;
        float angle;
        bool buttonHeld = false;
        bool CCW = false;

        // Start is called before the first frame update
        void Start()
        {
            float startingRadius = radius;
            spawnPos = center + Vector3.left * startingRadius;
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.zero - this.transform.position);
            RotateObject(center, radius);
        }

        /// <summary>
        /// Rotates the object. 
        /// </summary>
        /// <param name="center"> Origin point of the rotation. </param>
        /// <param name="radius"> Radius of the rotation. </param>
        private void RotateObject(Vector3 center, float radius)
        {
            if (buttonHeld && CCW)
            {
                if (BlockerNodeDetected(transform.right, 0.5f))
                {
                    return;
                }
                angle += Time.deltaTime * rotSpeed;
            }
            if (buttonHeld && !CCW)
            {
                if (BlockerNodeDetected(-transform.right, 0.5f))
                {
                    buttonHeld = false;
                }
                angle -= Time.deltaTime * rotSpeed;
            }

            // Calculate direction from center and rotate the up vector by angle degrees
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.left;
            transform.position = center + direction * radius;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            // Read the value of the input
            movementInput = context.ReadValue<Vector2>();

            // Get held state of the button
            if(context.performed)
            {
                buttonHeld = true;
            }
            if(context.canceled)
            {
                buttonHeld = false;
            }

            // Get closer or further from the center based on UP/DOWN input
            if (movementInput == Vector2.up)
            {
                buttonHeld = false;
                if (BlockerNodeDetected(transform.up, 1f))
                {
                    return;
                }
                radius -= 0.5f;
            }
            if (movementInput == Vector2.down)
            {
                buttonHeld = false;
                if (BlockerNodeDetected(-transform.up, 1f))
                {
                    return;
                }
                radius += 0.5f;
            }

            // If the player holds down left or right, go CW or CCW
            if (movementInput == Vector2.left)
            {
                CCW = false;
            }
            if (movementInput == Vector2.right)
            {
                CCW = true;
            }
        }

        bool BlockerNodeDetected(Vector3 direction, float distance)
        {
            RaycastHit target;
            return Physics.Raycast(transform.position, direction, out target, distance, LayerMask.GetMask("BlockerNode"));
        }
        public void ResetSpawn()
        {
            transform.position = spawnPos;
            radius = Mathf.Abs(spawnPos.x);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Goal")
            {
                print("WIN");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, transform.up * 1f);
            Gizmos.DrawRay(transform.position, transform.right * 1f);
            Gizmos.DrawRay(transform.position, -transform.up * 1f);
            Gizmos.DrawRay(transform.position, -transform.right * 1f);
        }
    }
}