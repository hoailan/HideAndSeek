using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public float spawnInterval = 3.0f;
    private float spawnTimer = 0.0f;
    private ObjectPool objectPool;
    private bool isMoving;

    private void Start()
    {
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            objectPool = ObjectPool.Instance;
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                GameObject spawnedObject = objectPool.SpawnObject();

                if (spawnedObject != null)
                {
                    spawnedObject.transform.position = transform.position; // set position
                    spawnedObject.GetComponent<Obstacle>().StartMoving(); // moving
                }

                spawnTimer = 0.0f;
            }
        }
    }

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
        isMoving = false;
    }

    private void HandleScreenTouchHold()
    {
        isMoving = false;
    }

    private void HandleScreenTouchEnd()
    {
        isMoving = true;
    }
}
