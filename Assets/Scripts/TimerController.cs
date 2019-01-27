﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public const float GAME_LENGTH = 1.2f; // in minutes

    private Text timeText;
    public Timer masterTimer = new Timer(GAME_LENGTH * 60);
    private int minutes, seconds, milliseconds;

	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
        masterTimer.Set(GAME_LENGTH * 60);
}
	
	// Update is called once per frame
	void Update () {
        masterTimer.Update();

        float timeRemaining = GAME_LENGTH * 60 - masterTimer.time; // in seconds

        minutes = Mathf.FloorToInt(timeRemaining / 60);
        seconds = Mathf.FloorToInt(timeRemaining % 60);
        milliseconds = (int)((timeRemaining - Mathf.FloorToInt(timeRemaining)) * 60);

        timeText.text = TimeString(minutes) + ":" + TimeString(seconds) + ":" +TimeString(milliseconds);

        if (timeRemaining < 60)
        {
            timeText.color = Color.red;
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