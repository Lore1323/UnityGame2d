using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Spawner s;
    public TextMeshProUGUI countdownText;
    public float countdownTime;
    public bool countdownActive = false;
    private float remainingTime;
    

    private void Start()
    {
        countdownTime = s.timeBeforeFirstWave;
        
    }

    void Update()
    {
        countdownTime = s.timeBeforeFirstWave;
        if (!countdownActive)
        {
            countdownText.text = "00";
            return;
        }
        
        float remainingTime = countdownTime -=1 * Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;

            StopCountdown();

        }

        int seconds = Mathf.CeilToInt(remainingTime);
        countdownText.text = seconds.ToString("00");
        
    }

    void StopCountdown()
    {
        countdownActive = false;
        countdownText.text = "00";
        s = null;
    }

    public void StartCountdown(float time, Spawner spawner)
    {
        countdownTime = time;
        remainingTime = time;
        s = spawner;
        countdownActive = true;
    }
}


