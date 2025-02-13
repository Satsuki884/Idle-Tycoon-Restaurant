using System;
using System.Collections;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private Light _sun;
    private bool _isDay;

    // private void Start()
    // {
    //     StartCoroutine(CheckTimeRoutine());
    // }

    // private bool IsNightTime()
    // {
    //     int hour = DateTime.Now.Hour;
    //     return hour >= 18 || hour < 6;
    // }

    // private IEnumerator CheckTimeRoutine()
    // {
    //     while (true)
    //     {
    //         bool newDayState = !IsNightTime();
    //         if (_isDay != newDayState)
    //         {
    //             _isDay = newDayState;
    //             UpdateLighting();
    //         }
    //         yield return new WaitForSeconds(1f);
    //     }
    // }
    // private void UpdateLighting()
    // {
    //     if (_sun == null || Camera == null) return;

    //     if (_isDay)
    //     {
    //         _sun.intensity = 1f;
    //         Camera.backgroundColor = Color.cyan;
    //     }
    //     else
    //     {
    //         _sun.intensity = 0.1f;
    //         Camera.backgroundColor = new Color(0.1f, 0.1f, 0.3f);
    //     }
    // }

    private void Start()
    {
        _isDay = !IsNightTime();
        UpdateLighting();
    }

    private bool IsNightTime()
    {
        int hour = DateTime.Now.Hour;
        return hour >= 20 || hour < 6; // Ночь с 19:00 до 06:00
    }

    private void Update()
    {
         if (_isDay)
        {
            _sun.intensity = 1f;
            Camera.backgroundColor = Color.cyan;
        }
        else
        {
            _sun.intensity = 0.1f;
            Camera.backgroundColor = new Color(0.1f, 0.1f, 0.3f); // Темно-синий
        }
    }

    private void UpdateLighting()
    {
        if (_isDay)
        {
            _sun.intensity = 1f;
            Camera.backgroundColor = Color.cyan;
        }
        else
        {
            _sun.intensity = 0.1f;
            Camera.backgroundColor = new Color(0.1f, 0.1f, 0.3f); // Темно-синий
        }
    }
}
