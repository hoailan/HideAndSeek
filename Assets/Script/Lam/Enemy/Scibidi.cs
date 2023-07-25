using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UIElements;
using Spine;

public class Scibidi : CusMonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    public FieldOfView fov;
    public FieldOfView fov2;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    public float movementSpeed = 3f;
    public float targetHeight = 3f;
    public bool up = true;
    public bool check;
    private Vector3 back_po;
    public bool toiletChecked = false;

    public enum SpineAnimationEnum
    {
        idle,
        attack,
        attack_completer,
        down_head,
        joker,
        toilet,
        up_head
    }

    protected override void LoadComponents()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        fov = GetComponentInChildren<FieldOfView>();
        fov2 = transform.Find("FOV2").gameObject.GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.OnObTouch && !up)
        {
            up = true;
            targetPosition = transform.position + Vector3.up * targetHeight;
            startPosition = transform.position;
            StartCoroutine(moveToilet(startPosition, targetPosition));
            StartCoroutine(toiletUp());
        }
        if (Player.OnObTouch && up)
        {
            StartCoroutine(toiletCheck());
        }
        if (fov.viewChecked && fov2.viewChecked && up)
        {
            toiletChecked = true;
            Debug.Log("call");
            StartCoroutine(toiletDown());
            fov.viewChecked = false;
            fov2.viewChecked = false;
        }
        if (toiletChecked && up)
        {
            up = false;
            toiletChecked = false;
            
        }

        if (fov.seenPlayer || fov2.seenPlayer)
        {
            StartCoroutine(toiletAttack());
        }
    }

    private IEnumerator toiletUp()
    {
        PlayAnimation(SpineAnimationEnum.toilet);

        skeletonAnimation.timeScale = 0.6f;
        skeletonAnimation.loop = false;
        PlayAnimation(SpineAnimationEnum.up_head);
        Debug.Log(skeletonAnimation.AnimationState.GetCurrent(0).TrackTime);
        yield return new WaitForSeconds(skeletonAnimation.AnimationState.GetCurrent(0).Animation.Duration);
        
        PlayAnimation(SpineAnimationEnum.idle);
        skeletonAnimation.timeScale = 0.7f;
        skeletonAnimation.loop = true;

    }

    public IEnumerator moveToilet(Vector3 startPosition,Vector3 targetPosition )
    {
        float elapsedTime = 0f;
        float distance = Vector3.Distance(transform.position, targetPosition);
        float duration = distance / movementSpeed;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime * 2f;
            yield return null;
        }
    }

    public IEnumerator toiletCheck()
    {
        yield return new WaitForSeconds(3f);
        PlayAnimation(SpineAnimationEnum.attack);
        fov.isPeek = true;
        fov2.isPeek = true;
    }

    public IEnumerator toiletAttack()
    {
        skeletonAnimation.loop = false;
        skeletonAnimation.timeScale = 0.7f;
        fov.gameObject.GetComponent<MeshRenderer>().enabled = false;
        fov2.gameObject.GetComponent<MeshRenderer>().enabled = false;
        PlayAnimation(SpineAnimationEnum.attack_completer);
        yield return new WaitForSeconds(skeletonAnimation.AnimationState.GetCurrent(0).Animation.Duration + 0.5f);
        Gamemanager.Instance.GameOver();
    }

    public IEnumerator toiletDown()
    {
        // to do
        skeletonAnimation.loop = false;
        skeletonAnimation.timeScale = 0.7f;
        PlayAnimation(SpineAnimationEnum.joker);
        yield return new WaitForSeconds(skeletonAnimation.AnimationState.GetCurrent(0).Animation.Duration);
        PlayAnimation(SpineAnimationEnum.down_head);
        yield return new WaitForSeconds(skeletonAnimation.AnimationState.GetCurrent(0).Animation.Duration);
        targetPosition = transform.position - Vector3.up * targetHeight;
        startPosition = transform.position;
        StartCoroutine(moveToilet(startPosition, targetPosition));
    }


    public IEnumerator toiletJoke()
    {
        yield return null;
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

    private void OnAnimationCompleted(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "attack")
        {
            PlayAnimation(SpineAnimationEnum.joker);
        }
        else if (trackEntry.Animation.Name == "joker")
        {
            PlayAnimation(SpineAnimationEnum.down_head);
        }
        else if (trackEntry.Animation.Name == "down_head")
        {
            PlayAnimation(SpineAnimationEnum.toilet);
        }
    }

    protected override void OnEnable()
    {
        Gamemanager.OnStartGame += HandleStartGame;
    }

    protected override void OnDisable()
    {
        Gamemanager.OnStartGame -= HandleStartGame;
    }

    private void HandleStartGame()
    {
        StartCoroutine(toiletDown());
    }

}
