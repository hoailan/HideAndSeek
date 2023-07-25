using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPool : ObjectPool
{
    public List<GameObject> ListFood;
    public int foodNo;
    protected override void LoadComponents()
    {
        ListFood = new List<GameObject>();
        ListFood.Add(GameObject.Find("NormalFood"));
        ListFood.Add(GameObject.Find("PoisionFood"));
        ListFood.Add(GameObject.Find("DeadlyFood"));
    }

    protected override void Start()
    {
       
        foreach (var food in ListFood)
        {
            PoolObject(food, foodNo);
        }
    }
}
