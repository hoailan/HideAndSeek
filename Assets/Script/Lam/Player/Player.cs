using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    public static event Action OnPlayerFinish;
    public static bool OnObTouch;
    public FieldOfView fov1;
   // public FieldOfView fov2;

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
    }

    void Start()
    {
        PlayAnimation(SpineAnimationEnum.scary);
        OnObTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void HandleScreenTouchBegin()
    {
        //
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.name.Equals("endPoint"))
        {
            // to do
            Debug.Log("end");
            OnPlayerFinish.Invoke();
            PlayAnimation(SpineAnimationEnum.win);
        }
        if (gameObject.name.Equals("check")) 
        {
            OnObTouch = true;
        } else
        {
            OnObTouch = false;
        }
    }


}
