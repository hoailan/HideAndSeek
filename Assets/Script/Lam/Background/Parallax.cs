using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material material;
    private float distance;

    [Range(0f, 2f)]
    public float speed = 0.2f;
    private float dynamicSpeed;
    public bool start = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        dynamicSpeed = 0f;
    }

    void Update()
    {
        if (start)
        {
            move();
        }
    }

    private void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
        FieldOfView.OnSeenPlayer += HandleSeenPlayer;
    }

    private void OnDisable()
    {
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
        Player.OnPlayerFinish -= HandlePlayerFinish;
    }

    private void HandleScreenTouchBegin()
    {
        dynamicSpeed = 0f;
    }

    private void HandleScreenTouchHold()
    {
        dynamicSpeed = 0f;
    }

    private void HandleScreenTouchEnd()
    {
        dynamicSpeed = speed;
    }

    private void HandlePlayerFinish()
    {
        // to do
        dynamicSpeed = 0f;
    }

    private void HandleSeenPlayer()
    {
        dynamicSpeed = 0f;
    }

    private void HandleStartGame()
    {
        TouchEventPublisher.OnScreenTouchBegin += HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold += HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd += HandleScreenTouchEnd;
        Player.OnPlayerFinish += HandlePlayerFinish;
        dynamicSpeed = speed;
        start = true; 
    }

    private void move()
    {
        distance += Time.deltaTime * dynamicSpeed;
        material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
