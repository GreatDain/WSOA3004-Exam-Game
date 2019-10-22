using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    protected int amountOfTimes;
    private int currentCount = 0;

    [SerializeField]
    protected float detectRotation = 30;
    [SerializeField]
    protected float speed = 1f;
    [SerializeField]
    private float currentTimeCheck = 0;
    private float currentGoal = 0;
    private float initialAngle;

    private bool isClockwise = false;

    private Quaternion initialRotation;
    private Quaternion currentRotationGoal;
    private Quaternion sourceRotation;

    private void Awake()
    {
        initialAngle = transform.eulerAngles.y;
        initialRotation = sourceRotation = transform.rotation;
        currentGoal = isClockwise ? detectRotation : -detectRotation;

        currentRotationGoal = transform.rotation * Quaternion.Euler(0, 0, currentGoal);

    }

    private void Update()
    {

        if(currentCount < 2) {
            currentTimeCheck += Time.deltaTime * speed;
            var newZ = Mathf.Lerp(initialAngle, currentGoal, currentTimeCheck);
            transform.rotation = Quaternion.Slerp(sourceRotation, currentRotationGoal, currentTimeCheck);
        }

        if ( currentTimeCheck >= 1 )
        {
            isClockwise = !isClockwise;
            currentGoal = isClockwise ? detectRotation : -detectRotation;
            currentTimeCheck = 0;
            initialAngle = transform.eulerAngles.y;
            sourceRotation = transform.rotation;
            currentRotationGoal = initialRotation * Quaternion.Euler(0, 0, currentGoal);

            currentCount++;
        }
    }
}
