using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : CusMonoBehaviour
{
    
    public float spawnInterval = 3.0f;
    private float spawnTimer = 0.0f;
    protected ObjectPool objectPool;
    private bool isStart;

    protected override void Start()
    {
        isStart = true;
    }

    void Update()
    {
        if (isStart)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            GameObject spawnedObject = objectPool.GetObject();

            if (spawnedObject != null)
            {
                spawnedObject.transform.position = transform.position; // set position
                spawnedObject.SetActive(true); // spawn
            }

            spawnTimer = 0.0f;
        }
    }
}
