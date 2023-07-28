using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Obstacle
{
    
    private SkeletonAnimation skeletonAnimation;
    public DoorStatus status;
    public Case checkCase;
    public float delayTime = 2f;
    public Sprite buttonOn, ButtonOff;

    public enum DoorStatus
    {
        open,
        close
    }

    public enum Case
    {
        CloseInRange,
        CloseByButton,
        Open
    }

    private void Start()
    {
        Player.OnPlayerReachDoor += ReachDoor;
        speed = this.normalSpeed;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.timeScale = 0.2f;
        PlayAnimation(status);
        base.direction = Vector3.left;
    }

    private void ReachDoor()
    {
        StartCoroutine(HandlePlayerReachDoor());
    }

    private IEnumerator HandlePlayerReachDoor()
    {
        
        if(status == DoorStatus.open)
        {
            if(checkCase == Case.CloseInRange)
            {
                yield return new WaitForSeconds(delayTime);
                CloseDoor();
            }
            if (checkCase == Case.CloseByButton)
            {
                if(GetComponentInChildren<SpriteRenderer>() != null)
                {
                    GetComponentInChildren<SpriteRenderer>().sprite = ButtonOff;
                }
                yield return new WaitForSeconds(1f);
                if (Player.Instance.spineAnimation == Player.SpineAnimationEnum.scary)
                {
                    CloseDoor();
                }
            }
        } 
        else if(status == DoorStatus.close && checkCase == Case.Open)
        {
            if (GetComponentInChildren<SpriteRenderer>() != null)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = ButtonOff;
            }
            yield return new WaitForSeconds(1f);
            if (Player.Instance.spineAnimation == Player.SpineAnimationEnum.scary)
            {
                OpenDoor();
            }

        }
    }

    private void CloseDoor()
    {
        PlayAnimation(DoorStatus.close);
        status = DoorStatus.close;
    }

    private void OpenDoor()
    {
        PlayAnimation(DoorStatus.open);
        status = DoorStatus.open;
    }

    public void PlayAnimation(DoorStatus animation)
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
