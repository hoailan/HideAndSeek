using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    public float scrollSpeed = 0.1f;

    private float scroll_pos = 0;
    private float[] pos;
    private float distance;

    void Start()
    {
        distance = 1f / (transform.childCount - 1f);
    }

    void Update()
    {
        pos = new float[transform.childCount];

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButtonDown(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else if (Input.GetMouseButton(0))
        {
            float delta = -Input.GetAxis("Mouse X") * scrollSpeed;
            scroll_pos += delta;
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
            }
        }
    }
}

