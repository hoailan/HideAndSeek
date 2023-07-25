using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisionFood : BaseFood
{
    private void Update()
    {
        move();
        disableByTime();
    }
}
