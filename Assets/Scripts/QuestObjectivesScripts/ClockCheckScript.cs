using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClockChecker : MonoBehaviour
{
    public HourhandRotation hourHand;
    public MinutehandRotation minuteHand;

    [SerializeField, Range(0, 12)]
    private int hourHandTargetTime;

    [SerializeField, Range(0, 59)]
    private int minuteHandTargetTime;

    [SerializeField]
    private float minuteHandAngleTolerance;
    [SerializeField]
    private float hourHandAngleTolerance;

    [SerializeField]
    public float howOftenCheck;

    [SerializeField] private InterfaceOnClose closeInterfaceScript;


    private float checkTimer;

    public UnityEvent onPuzzleCompleted;
    private float hourHandTargetAngle => NormalizeAngle(-hourHandTargetTime * 30f);
    private float minuteHandTargetAngle => NormalizeAngle(-minuteHandTargetTime * 6f);
    void Start()
    {
        checkTimer = howOftenCheck;
        closeInterfaceScript.interfaceClosed.AddListener(CheckHandsPosition);
        Debug.Log("Hour Hand Target Time: " + hourHandTargetTime);
        Debug.Log("Minute Hand Target Time: " + minuteHandTargetTime);
        Debug.Log("Hour Hand Target Angle: " + hourHandTargetAngle);
        Debug.Log("Minute Hand Target Angle: " + minuteHandTargetAngle);

    }

    //void Update()
    //{
    //    checkTimer -= Time.deltaTime;

    //    if (checkTimer <= 0f)
    //    {
    //        CheckHandsPosition();
    //        checkTimer = howOftenCheck;
    //    }
    //}

    private void CheckHandsPosition()
    {

        if (IsMinuteHandInCorrectPosition() && IsHourHandInCorrectPosition())
        {
            onPuzzleCompleted.Invoke();
            Debug.Log("Both the hour and minute hands are in the correct position!");

        }
    }


    private bool IsMinuteHandInCorrectPosition()
    {
        float currentAngle = minuteHand.GetCurrentAngle();
        if (currentAngle < 0)
        {
            currentAngle += 360;
        }
        return Mathf.Abs(currentAngle - minuteHandTargetAngle) <= minuteHandAngleTolerance;
    }

    private bool IsHourHandInCorrectPosition()
    {
        float currentAngle = hourHand.GetCurrentAngle();
        if (currentAngle < 0)
        {
            currentAngle += 360;
        }
        return Mathf.Abs(currentAngle - hourHandTargetAngle) <= hourHandAngleTolerance;
    }
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
        {
            angle += 360;
        }
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }
}