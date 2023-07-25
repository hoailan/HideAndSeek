using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFood : CusMonoBehaviour
{
    public float speed;
    public float disableTime;
    protected override void LoadComponents()
    {
        speed = 2f;
    }

    public void move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void disableByTime()
    {
        disableTime += Time.deltaTime * 1f;
        if (disableTime >= 3.5f)
        {
            gameObject.SetActive(false);
            disableTime = 0f;
        }
    }
}
