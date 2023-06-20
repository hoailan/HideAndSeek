using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonscale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    public float scaleFactor = 1.1f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = button.transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button.transform.localScale = originalScale * scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button.transform.localScale = originalScale;
    }

    public void OnButtonClick()
    {
        
    }
}
