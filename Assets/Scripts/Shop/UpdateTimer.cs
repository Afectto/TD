using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTimer : MonoBehaviour
{
    private Timer _time;
    [SerializeField]private Text _timeText;
    public event Action<bool> OnTimerEnd;
    private void Awake()
    {
        _time = new Timer();
        _time.Start();
    }

    private void Update()
    {
        var timeValue = 30 - _time.GetSeconds();
        _time.Update();
        if (timeValue < 0)
        {
            timeValue = 30;
            _time.Reset();
            OnTimerEnd.Invoke(false);
        }
        
        _timeText.text = timeValue.ToString() + " sec.";
    }
}
