using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 600;
    public bool timerIsRunning = true;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    //Checks if Timer is running, and increments the time
    //Also calls ShowTime
    void Update()
    {
        if(timerIsRunning){
            if(timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
                ShowTime(timeRemaining);
            }
            else{
                Debug.Log("Time has run out");
                timeRemaining = 0;
                timerIsRunning = false;
                timeText.text = "Time Until Sundown: 00:00:000";
            }
        }
    }
    //Caluclates an displays the time
    void ShowTime(float timeToDisplay){
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 1000;
        timeText.text = "Time Until Sundown: ";
        timeText.text += string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

    }
}
