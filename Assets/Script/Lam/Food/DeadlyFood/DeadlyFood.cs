using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyFood : BaseFood
{
    private void Update()
    {
        move();
        disableByTime();
    }
}
