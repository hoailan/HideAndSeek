using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObserver : MonoBehaviour
{
    private void OnEnable()
    {
        TouchEventPublisher.OnScreenTouchBegin += HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold += HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd += HandleScreenTouchEnd;
    }

    private void OnDisable()
    {
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
    }

    private void HandleScreenTouchBegin()
    {
        Debug.Log("Screen touch");
    }

    private void HandleScreenTouchHold()
    {
        Debug.Log("Screen being held ");
    }

    private void HandleScreenTouchEnd()
    {
        Debug.Log("Screen touch ended ");
    }
}
