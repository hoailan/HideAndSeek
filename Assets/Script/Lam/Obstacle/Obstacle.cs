using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;

    public bool isMoving = false;

    private float distanceToDisable = 8f;
    private float initialPositionX;

    private void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
    public void StartMoving()
    {
        isMoving = true;
        initialPositionX = transform.position.x;
    }
    private void DisableObject()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
        FieldOfView.OnSeenPlayer += stopMoving;
    }

    private void OnDisable()
    {
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
        Player.OnPlayerFinish -= HandlePlayerFinish;
    }

    private void HandleStartGame()
    {
        TouchEventPublisher.OnScreenTouchBegin += HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold += HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd += HandleScreenTouchEnd;
        Player.OnPlayerFinish += HandlePlayerFinish;
        StartMoving();
    }

    private void HandleScreenTouchBegin()
    {
        stopMoving();
    }

    private void HandleScreenTouchHold()
    {
        stopMoving();
    }

    private void HandleScreenTouchEnd()
    {
        isMoving = true;
    }

    private void HandlePlayerFinish()
    {
        // to do
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void stopMoving()
    {
        isMoving = false;
    }
}
