using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 1f;  
    public float startPositionY = -10f;  
    public float endPositionY = 10f;  

    private void Update()
    {
        float newPositionY = Mathf.Repeat(Time.time * scrollSpeed, endPositionY - startPositionY);
        transform.position = new Vector3(transform.position.x, startPositionY + newPositionY, transform.position.z);
        
    }
}
