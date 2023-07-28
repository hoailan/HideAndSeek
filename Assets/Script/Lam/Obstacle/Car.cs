using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacle
{
    protected override void HandleStartGame()
    {
        base.HandleStartGame();
        Player.OnPlayerReachCarCheck += HandleOnPlayerReachCar;
        base.UnSubTouchPub();
    }

    protected override void HandleScreenTouchBegin()
    {
        // none
    }

    protected override void HandleScreenTouchHold()
    {
        base.direction = -Vector3.left;
    }

    protected override void HandleScreenTouchEnd()
    {
        base.direction = Vector3.left;
    }

    protected void HandleOnPlayerReachCar()
    {
        base.speed = this.normalSpeed;
        base.SubTouchPub();
    }
}
