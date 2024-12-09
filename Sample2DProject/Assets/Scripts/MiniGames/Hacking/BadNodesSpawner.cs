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
        List<GameObject> nodes;

        // Start is called before the first frame update
        void Awake()
        {
            nodes = new List<GameObject>();
        }

        public void SpawnNodes(int amountToSpawn)
        {
            foreach(GameObject node in nodes)
            {
                Destroy(node);
            }
            nodes.Clear();
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject newBadNode = Instantiate(badNodePrefab, transform);
                nodes.Add(newBadNode);
            }
        }
    }
}