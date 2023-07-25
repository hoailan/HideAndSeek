using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 2f;
    public float speedUp = 3.5f;
    public float speedDown = 0.5f;
    public float normalSpeed = 2f;
    public float durationSpeedChange = 2f;
    public bool isMoving = false;


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
    }
    private void DisableObject()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
    }

    private void OnDisable()
    {
        
        Player.OnPlayerFinish -= HandlePlayerFinish;
    }

    private void HandleStartGame()
    {
        TouchEventPublisher.OnScreenTouchBegin += HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold += HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd += HandleScreenTouchEnd;
        FieldOfView.OnSeenPlayer += stopMoving;
        Player.OnPlayerFinish += HandlePlayerFinish;
        Player.OnPlayerSpeedUp += HandleOnPlayerSpeedUp;
        Player.OnPlayerSpeedDown += HandleOnPlayerSpeedDown;
        StartMoving();
    }

    private void HandlePlayerFinish()
    {
        stopMoving();
        endEffect();
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
        Player.OnPlayerSpeedUp -= HandleOnPlayerSpeedUp;
        Player.OnPlayerSpeedDown -= HandleOnPlayerSpeedDown;
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

    private void HandleOnPlayerSpeedUp()
    {
        speed = speedUp;
        StartCoroutine(ResetSpeedAfterDuration(durationSpeedChange));
    }

    private void HandleOnPlayerSpeedDown()
    {
        speed = speedDown;
        StartCoroutine(ResetSpeedAfterDuration(durationSpeedChange));
    }

    // back to normail speed with "duration" second
    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = normalSpeed;
        // to do
    }

    private void stopMoving()
    {
        isMoving = false;
    }

    private void endEffect()
    {
        // to do
    }
}
