using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance { get => instance;}
    public float durationSpeedChange;
    private SkeletonAnimation skeletonAnimation;
    public static event Action OnPlayerFinish;
    public static event Action OnPlayerSpeedUp;
    public static event Action OnPlayerSpeedDown;
    public static bool OnObTouch;
    public FieldOfView fov1;
    // public FieldOfView fov2;

    private Transform startPoint; 
    public float totalDistance; 
    public float percentage;
    public Transform endPoint;

    public enum SpineAnimationEnum
    {
        idle,
        lose,
        run_speed,
        run,
        scary,
        win,
        win2,
        win2_loop
    }

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        instance = this;
    }

    void Start()
    {
        PlayAnimation(SpineAnimationEnum.scary);
        OnObTouch = false;
        // temp
        startPoint = transform;
        totalDistance = MathF.Abs(endPoint.position.x - 0.8f - transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        caculateDi();
    }

    private void caculateDi()
    {
        float characterDistance =  MathF.Abs(endPoint.position.x - 0.7f - transform.position.x);
        percentage = 1f - (characterDistance / totalDistance);
    }

    private void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
        FieldOfView.OnSeenPlayer += HandleSeenPlayer;
        OnPlayerFinish += HandlePlayerFinish;
        OnPlayerSpeedUp += HandleOnSpeedUp;
        OnPlayerSpeedDown += HandleOnSpeedDown;
    }

    private void OnDisable()
    {
        Gamemanager.OnStartGame -= HandleStartGame;
        FieldOfView.OnSeenPlayer -= HandleSeenPlayer;
        OnPlayerFinish -= HandlePlayerFinish;
        OnPlayerSpeedUp -= HandleOnSpeedUp;
        OnPlayerSpeedDown -= HandleOnSpeedDown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // can change endpoint
        // reach endpoint
        GameObject gameObject = collision.gameObject;
        if (gameObject.name.Equals("endPoint"))
        {
            Debug.Log("end");
            OnPlayerFinish.Invoke();
            Gamemanager.Instance.countdownTimer.StopTimer();
            PlayAnimation(SpineAnimationEnum.win);
        }

        //reach speed up
        if (gameObject.name.Equals("SpeedUp"))
        {
            OnPlayerSpeedUp.Invoke();
        }

        // reach speed down
        if (gameObject.name.Equals("SpeedDown"))
        {
            OnPlayerSpeedDown.Invoke();
        }

        // reach ostacle
        if (gameObject.name.Equals("check"))
        {
            OnObTouch = true;
        }
        else
        {
            OnObTouch = false;
        }
    }

    private void HandleScreenTouchBegin()
    {
        // none
    }

    private void HandleScreenTouchHold()
    {
        PlayAnimation(SpineAnimationEnum.scary);
        skeletonAnimation.timeScale = 1f;
    }

    private void HandleScreenTouchEnd()
    {
        PlayAnimation(SpineAnimationEnum.run);
        skeletonAnimation.timeScale = 2f;
    }

    private void HandleStartGame()
    {
        TouchEventPublisher.OnScreenTouchBegin += HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold += HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd += HandleScreenTouchEnd;
        PlayAnimation(SpineAnimationEnum.run);
        skeletonAnimation.timeScale = 2f;
    }

    private void HandleSeenPlayer()
    {
        skeletonAnimation.timeScale = 1f;
        skeletonAnimation.loop = true;
        PlayAnimation(SpineAnimationEnum.lose);
    }

    private void HandlePlayerFinish()
    {
        TouchEventPublisher.OnScreenTouchBegin -= HandleScreenTouchBegin;
        TouchEventPublisher.OnScreenTouchHold -= HandleScreenTouchHold;
        TouchEventPublisher.OnScreenTouchEnd -= HandleScreenTouchEnd;
    }

    private void HandleOnSpeedUp()
    {
        Debug.Log("player speed up");
        // to do
        PlayAnimation(SpineAnimationEnum.run_speed);
        //back
        StartCoroutine(ResetSpeedAfterDuration(durationSpeedChange));
    }

    private void HandleOnSpeedDown()
    {
        Debug.Log("player speed down");
        // to do
        // khong co animation speed down

        //back
        StartCoroutine(ResetSpeedAfterDuration(durationSpeedChange));
    }

    // back to normail speed with "duration" second
    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("back in: " + duration );
        // to do
        PlayAnimation(SpineAnimationEnum.run);
    }

    public void PlayAnimation(SpineAnimationEnum animation)
    {
        string animationName = animation.ToString();

        if (skeletonAnimation.Skeleton.Data.FindAnimation(animationName) != null)
        {
            skeletonAnimation.AnimationName = animationName;
        }
        else
        {
            Debug.LogWarning("Animation '" + animationName + "' does not exist!");
        }
    }

}
