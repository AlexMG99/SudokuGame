using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private float _startTime = 0f;
    private float _waitTime = 0f;

    public void StartTimer(float waitTime)
    {
        _startTime = Time.time;
        _waitTime = waitTime;
    }

    public bool CheckTime()
    {
        return (Time.time - _startTime > _waitTime);
    }
}
