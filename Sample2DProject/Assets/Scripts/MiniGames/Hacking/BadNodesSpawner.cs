using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hacking
{
    /// <summary>
    /// This spawns in a set amount of Bad Nodes.
    /// </summary>
    public class BadNodesSpawner : MonoBehaviour
    {
        [SerializeField] GameObject badNodePrefab;
        [SerializeField] float amountToSpawn = 5f;
        // Start is called before the first frame update
        void Start()
        {
            for(int i=0; i < amountToSpawn; i++)
            {
                GameObject newBadNode = Instantiate(badNodePrefab, transform);
            }
        }
    }
}
