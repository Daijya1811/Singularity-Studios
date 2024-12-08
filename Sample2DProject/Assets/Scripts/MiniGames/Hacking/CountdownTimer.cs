using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Hacking
{
    /// <summary>
    /// This class sets the countdown timer UI element in the Hacking minigame to countdown. If timer reaches zero, lose condition happens. 
    /// </summary>
    public class CountdownTimer : MonoBehaviour
    {
        [Tooltip("The amount of time given to the player for the Hacking.")]
        [SerializeField] float timeForTask = 10f;
        float timeLeft;
        [SerializeField] TextMeshProUGUI tmpObject;
        PlayerTriangleMovement player;

        private bool isHackingActive;

        public bool IsHackingActive { get { return isHackingActive; } set { isHackingActive = value; } }
        public float TimeForTask { get { return timeForTask; } set { timeForTask = value; } }
        private void OnEnable()
        {
            timeLeft = timeForTask;
        }
        private void Start()
        {
            player = FindObjectOfType<PlayerTriangleMovement>();
            timeLeft = timeForTask;
        }
        // Update is called once per frame
        void Update()
        {
            // Don't countdown while the player is reading the tutorial screen.
            if (player.FirstTimePlaying) return;

            // If the player has not won the minigame yet, then the timer should be counting down.
            if (!player.HasWon) Countdown();
            CheckForHackingActiveState();
        }
        /// <summary>
        /// Countsdown from timeForTask to 0 seconds, displaying to only 2 decimal places. If timeForTask reaches 0, then lose condition. 
        /// </summary>
        void Countdown()
        {
            if (timeLeft > TimeForTask) timeLeft = timeForTask;
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0f;
                //Lose condition
                if (!player.HasLost) player.HasLost = true;
            }
            tmpObject.text = timeLeft.ToString("F2");
        }
        private void CheckForHackingActiveState()
        {
            if (timeLeft > 0.05 && timeLeft < timeForTask) isHackingActive = true;
            else isHackingActive = false;
        }
    }
}