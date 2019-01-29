using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    [Tooltip("The length of the game in minutes.")]
    public float gameLength = 2.0f; // in minutes


    private Text timeText;
    public Timer masterTimer = new Timer(0);
    private int minutes, seconds, milliseconds;

    public static bool GameOver { get; private set; }

    public GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
        masterTimer.Set(gameLength * 60);
        gameOverPanel.SetActive(false);
        GameOver = false;
}
	
	// Update is called once per frame
	void Update () {
        if (masterTimer.Done)
        {
            Time.timeScale = 0;

            GameOver = true;

            gameOverPanel.SetActive(true);

            timeText.text = "00:00:00";
        }
        else
        {
            masterTimer.Update();


            float timeRemaining = gameLength * 60 - masterTimer.time; // in seconds

            minutes = Mathf.FloorToInt(timeRemaining / 60);
            seconds = Mathf.FloorToInt(timeRemaining % 60);
            milliseconds = (int)((timeRemaining - Mathf.FloorToInt(timeRemaining)) * 60);

            timeText.text = TimeString(minutes) + ":" + TimeString(seconds) + ":" + TimeString(milliseconds);

            if (timeRemaining < 10)
            {
                timeText.color = Color.red;
            }
        }
	}

    public string TimeString(int time)
    {
        string timeStr = time.ToString();

        if (time < 10)
            timeStr = "0" + timeStr;

        return timeStr;
    }
}
