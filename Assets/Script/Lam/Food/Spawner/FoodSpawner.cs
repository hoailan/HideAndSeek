using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : Spawner
{
    protected override void LoadComponents()
    {
        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
    }

    private void Update()
    {
        Spawn();
    }
}
