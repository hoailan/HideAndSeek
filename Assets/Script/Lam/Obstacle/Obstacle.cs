using DigitalRuby.AdvancedPolygonCollider;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public float speedUp = 3.5f;
    public float speedDown = 0.5f;
    public float normalSpeed = 2f;
    public float durationSpeedChange = 2f;
    protected bool isMoving = false;
    protected Vector3 direction;

    public void Awake()
    {
        
    }

    private void Start()
    {
        isMoving = false;
        direction = Vector3.left;
    }

    void Update()
    {
        if (isMoving)
        {
            moving(direction);
        }
    }

    public void moving(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void StartMoving()
    {
        isMoving = true;
    }
    

    private void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
    }

    protected virtual void HandleStartGame()
    {
        SubTouchPub();
        SubPlayer();
        SubFOV();
        StartMoving();
    }

    private void HandlePlayerFinish()
    {
        stopMoving();
        endEffect();
        UnSubTouchPub();
        UnSubPlayer();
    }

    protected void SubTouchPub()
    {
        TouchEventPublisher.OnScreenTouchBegin += HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold += HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd += HandleScreenTouchEnd;
    }

    protected void UnSubTouchPub()
    {
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
    }

    protected void SubPlayer()
    {
        Player.OnPlayerFinish += HandlePlayerFinish;
        Player.OnPlayerSpeedUp += HandleOnPlayerSpeedUp;
        Player.OnPlayerSpeedDown += HandleOnPlayerSpeedDown;
    }

    protected void UnSubPlayer()
    {
        Player.OnPlayerSpeedUp -= HandleOnPlayerSpeedUp;
        Player.OnPlayerSpeedDown -= HandleOnPlayerSpeedDown;
        Player.OnPlayerFinish -= HandlePlayerFinish;
    }

    protected void SubFOV()
    {
        FieldOfView.OnSeenPlayer += stopMoving;
    }

    protected void UnSubFOV()
    {
        FieldOfView.OnSeenPlayer -= stopMoving;
    }


    protected virtual void HandleScreenTouchBegin()
    {
        stopMoving();
    }

    protected virtual void HandleScreenTouchHold()
    {
        stopMoving();
    }

    protected virtual void HandleScreenTouchEnd()
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
