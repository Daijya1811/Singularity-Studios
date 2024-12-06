using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText; 

    [SerializeField]
    private float startTime = 198f; 

    private float currentTime;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        ResetTimer();
    }

    void Update()
    {
        UpdateTimer();
    }

    // Update the timer and display it
    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        
        DisplayTime(currentTime);
    }

    // Convert time into minutes and seconds
    private void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}"; // Display in "MM:SS" format
    }
    
    /// <summary>
    /// This resets the timer
    /// </summary>
    public void ResetTimer()
    {
        currentTime = startTime;
    }

    /// <summary>
    /// Adds or removes time. Pass in a - or + float. 
    /// </summary>
    /// <param name="time"></param>
    public void AddOrRemoveTime(float time)
    {
        currentTime += time;
    }
}
