﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Timer object class
public class Timer
{
    public float time { get; private set; }
    private float presetTime;
    public bool Done { get; private set; }

    public Timer(float duration)
    {
        time = 0;
        presetTime = duration;
        Done = false;
    }

    public void Reset()
    {
        time = 0;
        Done = false;
    }

    // Call during Start()
    public void Set(float duration)
    {
        presetTime = duration;
        Reset();
    }

    // Call during Update()
    public void Update()
    {
        time += Time.deltaTime;

        if (time >= presetTime)
        {
            Done = true;
        }
    }

    public int GetPercentDone()
    {
        return (int)(100 * time / presetTime);
    }
}
