using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private float _duration;
    private float _currentTime;
    private bool _isRunning = false;

    public event Action OnTimerFinished;

    private void Start()
    {
        _currentTime = _duration;
        UpdateCountdownText();
    }

    private void Update()
    {
        if (_isRunning)
        {
            _currentTime -= Time.deltaTime;
            UpdateCountdownText();

            if (_currentTime <= 0f)
            {
                _currentTime = 0f;
                _isRunning = false;

                OnTimerFinished?.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        _currentTime = _duration;
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _currentTime = _duration;
        UpdateCountdownText();
    }

    private void UpdateCountdownText()
    {
        _timeText.text = Mathf.CeilToInt(_currentTime).ToString();
    }
}
