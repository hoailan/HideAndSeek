using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEventPublisher : MonoBehaviour
{
    public static event Action OnScreenTouchBegin; 
    public static event Action OnScreenTouchHold; 
    public static event Action OnScreenTouchEnd;
    public static bool OnTouchOb = false;

    public bool isTouching = false;
    public float touchDuration = 0f;
    private float touchHoldThreshold = 0.5f; 

    private void Update()
    {
        if (isTouching)
        {
            touchDuration += Time.deltaTime;

            if (OnScreenTouchHold != null)
            {
                OnScreenTouchHold.Invoke();
            }

            if (touchDuration >= touchHoldThreshold)
            {
                if (OnScreenTouchHold != null)
                {
                    OnScreenTouchHold.Invoke();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        isTouching = true;
        touchDuration = 0f;

        if (OnScreenTouchBegin != null)
        {
            OnScreenTouchBegin.Invoke();
        }
    }

    private void OnMouseUp()
    {
        isTouching = false;
        touchDuration = 0f;

        if (OnScreenTouchEnd != null)
        {
            OnScreenTouchEnd.Invoke();
        }
    }

}
