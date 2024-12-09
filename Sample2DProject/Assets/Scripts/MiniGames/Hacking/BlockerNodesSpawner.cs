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
        List<GameObject> nodes;
        // Start is called before the first frame update
        void Awake()
        {
            nodes = new List<GameObject>();
        }

        public void SpawnNodes(int amountToSpawn)
        {
            foreach (GameObject node in nodes)
            {
                Destroy(node);
            }
            nodes.Clear();
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject newBlockerNode = Instantiate(blockerNodePrefab, transform);
                nodes.Add(newBlockerNode);
            }
        }
    }
}