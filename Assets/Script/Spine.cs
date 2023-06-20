using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spine : MonoBehaviour
{
    public Transform wheel;
    public Button spinButton;
    public float maxRotationSpeed = 1000f;
    public float minRotationSpeed = 100f;
    public float stoppingSpeed = 1f;
    public int numRewards = 8;

    private bool isSpinning = false;
    private float currentRotation;
    private float targetRotation;
    private float rotationSpeed;

    private void Start()
    {
        float angleBetweenRewards = 360f / numRewards;
        currentRotation = 0f;
        spinButton.onClick.AddListener(SpinWheel);
    }

    private void Update()
    {
        if (isSpinning)
        {
            wheel.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);

            if (rotationSpeed <= stoppingSpeed)
            {
                isSpinning = false;
                int selectedReward = Mathf.FloorToInt((currentRotation % 360) / (360f / numRewards));
                Debug.Log("Selected Reward: " + selectedReward);
            }

            rotationSpeed -= stoppingSpeed * Time.deltaTime;
        }
    }

    public void SpinWheel()
    {
        if (!isSpinning)
        {
            int numRotations = Random.Range(3, 5);
            float randomAngle = Random.Range(0f, 360f);
            targetRotation = numRotations * 360f + randomAngle;
            rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

            isSpinning = true;
        }
    }
}
