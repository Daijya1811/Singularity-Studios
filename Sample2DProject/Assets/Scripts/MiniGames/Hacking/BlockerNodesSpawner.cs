using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hacking
{
    /// <summary>
    /// This spawns in a set amount of Blocker Nodes.
    /// </summary>
    public class BlockerNodesSpawner : MonoBehaviour
    {
        [SerializeField] GameObject blockerNodePrefab;
        [SerializeField] float amountToSpawn = 5f;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject newBlockerNode = Instantiate(blockerNodePrefab, transform);
            }
        }
    }
}
