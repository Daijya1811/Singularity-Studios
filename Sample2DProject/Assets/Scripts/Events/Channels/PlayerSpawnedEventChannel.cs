using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "Player Spawned Event Channel", menuName = "Events/Player Spawned Event Channel")]
    public class PlayerSpawnedEventChannelSO : GenericEventChannelSO<PlayerInput>
    {
        /// <summary>
        /// Rather than make a separate system for void events, just make the event take an object
        /// and pass through null
        /// </summary>
        public void RaiseEvent(PlayerInput playerPrefab)
        {
            base.RaiseEvent(playerPrefab);
        }
    }


