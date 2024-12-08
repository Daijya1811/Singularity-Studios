using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    [SerializeField] GameObject[] fallingObjectPrefabs;
    float boundX = 18f;
    float boundY = 10f;
    float boundZ = 18f;
    Vector3 boundingArea;

    private void Start()
    {
        boundingArea = new Vector3(boundX, boundY, boundZ) + this.transform.position;
    }
    public void DropRandomItem()
    {
        int rngIndex = Mathf.RoundToInt(Random.Range(0f, fallingObjectPrefabs.Length - 1));
        float rngSpawnPosX = Random.Range(-boundingArea.x / 2f, boundingArea.x / 2f);
        float rngSpawnPosZ = Random.Range(-boundingArea.z / 2f, boundingArea.z / 2f);
        Vector3 rngSpawnPos = new Vector3(rngSpawnPosX, boundingArea.y, rngSpawnPosZ);
        Instantiate(fallingObjectPrefabs[rngIndex], rngSpawnPos, Random.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        // Potential bounding area for our falling object instantiations
        Gizmos.DrawWireCube(transform.position, new Vector3(boundX, boundY, boundZ));
        //Gizmos.DrawWireSphere(transform.position, fallingBoundsRadius);
    }
}
