using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] float timeForTask = 10f;
    [SerializeField] TextMeshProUGUI tmpObject;

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }
    void Countdown()
    {
        timeForTask -= Time.deltaTime;
        if (timeForTask <= 0)
        {
            timeForTask = 0f;
            //TODO: Lose condition
        }
        tmpObject.text = timeForTask.ToString("F2");
    }
}
