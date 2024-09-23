using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIPlayerSelect : MonoBehaviour
{
    [SerializeField] GameObject leftSelectPrefab;
    [SerializeField] GameObject rightSelectPrefab;
    int joinCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (joinCount == 2) this.gameObject.SetActive(false);
    }
    public void SpawnPlayer(int select)
    {
        if(select == 0)
        {
            Instantiate(leftSelectPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
            joinCount++;
        }
        else if(select == 1)
        {
            Instantiate(rightSelectPrefab, new Vector3(5, 0, 0), Quaternion.identity);
            joinCount++;
        }
    }
}
