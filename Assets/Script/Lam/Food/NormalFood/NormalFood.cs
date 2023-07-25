using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFood : BaseFood
{
    private void Update()
    {
        move();
        disableByTime();
    }
}
