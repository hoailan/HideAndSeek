using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPub : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.endPoint = transform;
    }

}
