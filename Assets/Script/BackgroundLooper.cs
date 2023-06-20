using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 1f;  // Tốc độ cuộn của background
    public float startPositionY = -10f;  // Vị trí ban đầu của background theo trục Y
    public float endPositionY = 10f;  // Vị trí kết thúc của background theo trục Y

    private void Update()
    {
        float newPositionY = Mathf.Repeat(Time.time * scrollSpeed, endPositionY - startPositionY);
        transform.position = new Vector3(transform.position.x, startPositionY + newPositionY, transform.position.z);
        
    }
}
