using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material material;
    private float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;
    public float speedup;
    public float speeddown;
    public float durationSpeedChange;
    private float dynamicSpeed;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        dynamicSpeed = 0f;
    }

    void Update()
    {
        move();
    }

    private void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
        FieldOfView.OnSeenPlayer += HandleSeenPlayer;
    }

    private void OnDisable()
    {
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
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
        Player.OnPlayerSpeedUp -= HandleOnSpeedUp;
        Player.OnPlayerSpeedDown -= HandleOnSpeedDown;
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
        Player.OnPlayerSpeedUp += HandleOnSpeedUp;
        Player.OnPlayerSpeedDown += HandleOnSpeedDown;
        dynamicSpeed = speed;
    }

    private void move()
    {
        distance += Time.deltaTime * dynamicSpeed;
        material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }

    private void HandleOnSpeedUp()
    {
        dynamicSpeed = speedup;
        StartCoroutine(ResetSpeedAfterDuration(durationSpeedChange));
    }

    private void HandleOnSpeedDown()
    {
        dynamicSpeed = speeddown;
        StartCoroutine(ResetSpeedAfterDuration(durationSpeedChange));
    }

    // back to normail speed with "duration" second
    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        dynamicSpeed = speed;
    }
}
