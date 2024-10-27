using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hacking
{
    /// <summary>
    /// This class handles the movement of the player triangle during the Hacking minigame. 
    /// </summary>
    public class PlayerTriangleMovement : MonoBehaviour
    {
        [SerializeField] float rotSpeed = 50f;
        [SerializeField] float radius = 10f;
        float startingRadius;
        float angle;
        Vector2 movementInput = Vector2.zero;
        Vector3 spawnPos;
        Vector3 center;
        bool buttonHeld = false;
        bool CCW = false;
        bool hasWon = false;
        bool hasLost = false;

        public bool HasWon { get { return hasWon; } set { hasWon = value; } }
        public bool HasLost { get { return hasLost; } set { hasLost = value; } }

        DoorBehavior doorToUnlock;
        public DoorBehavior DoorToUnlock { set { doorToUnlock = value; } }


        private void OnDisable()
        {
            ResetSpawn();
        }
        // Start is called before the first frame update
        void Start()
        {
            center = GameObject.FindGameObjectWithTag("Goal").transform.position;
            startingRadius = radius;
            spawnPos = center + Vector3.left * startingRadius;
        }

        // Update is called once per frame
        void Update()
        {
            if (hasLost)
            {
                GetComponent<HackingFinished>().ToggleMiniGameOff();
                hasLost = false;

                return;
            }
            // This line has the triangle always facing the center of the circle
            transform.rotation = Quaternion.LookRotation(Vector3.forward, center - this.transform.position);
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
        /// <summary>
        /// Handles player input on whether the triangle goes up or down in radii, or rotates CW or CCW. 
        /// </summary>
        /// <param name="context"></param>
        public void OnMove(InputAction.CallbackContext context)
        {
            // Read the value of the input
            movementInput = context.ReadValue<Vector2>();

            // Normalizes movementInput when using a Gamepad.
            movementInput = new Vector2(Mathf.Round(movementInput.x), Mathf.Round(movementInput.y));

            // Get held state of the button
            if(context.performed)
            {
                buttonHeld = true;
            }
            if(context.canceled)
            {
                buttonHeld = false;
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
        /// <summary>
        /// Advances the triangle toward the goal if the Advance button is pressed.
        /// </summary>
        public void OnAdvance(InputAction.CallbackContext context)
        {
            if (BlockerNodeDetected(transform.up, 1f))
            {
                return;
            }
            else if (context.started) radius--;
        }
        /// <summary>
        /// Checks to see (via Raycast) if there is a blocker node in the direction that the triangle wants to move. If so, it's blocked and prevented from doing so in the RotateObject() method. 
        /// </summary>
        /// <param name="direction"> The direction that the triangle wants to move to (Up, Down, Left, or Right). </param>
        /// <param name="distance"> How far the ray should cast. </param>
        /// <returns></returns>
        bool BlockerNodeDetected(Vector3 direction, float distance)
        {
            RaycastHit target;
            return Physics.Raycast(transform.position, direction, out target, distance, LayerMask.GetMask("BlockerNode"));
        }
        /// <summary>
        /// Reset the spawn of the player triangle from back outside of the circle. 
        /// </summary>
        public void ResetSpawn()
        {
            transform.position = spawnPos;
            radius = startingRadius;
        }
        /// <summary>
        /// Checks to see if it enters the Goal object, in which case the minigame is won! 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Goal")
            {
                hasWon = true;
                GetComponent<HackingFinished>().ToggleMiniGameOff();
                doorToUnlock.Unlock();
            }
        }
    }
}