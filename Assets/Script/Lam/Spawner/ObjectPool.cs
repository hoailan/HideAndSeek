using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : CusMonoBehaviour
{
    public List<GameObject> pooledObjects;

    protected override void LoadComponents()
    {
        pooledObjects = new List<GameObject>();
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }

    public void PoolObject(GameObject prefab, int poolSize)
    {

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.parent = transform.Find("Holder"); ;
            pooledObjects.Add(obj);
        }
    }

}
