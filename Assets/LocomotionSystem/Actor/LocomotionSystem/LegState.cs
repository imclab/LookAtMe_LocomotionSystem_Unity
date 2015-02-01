
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class LegState
{
    // Past and future step
    public Vector3 stepFromPosition;
    public Vector3 stepToPosition;
    public Vector3 stepToPositionGoal;
    public Matrix4x4 stepFromMatrix;
    public Matrix4x4 stepToMatrix;
    public float stepFromTime;
    public float stepToTime;

    public int stepNr = 0;

    // Continiously changing foot state
    public float cycleTime = 1;
    public float designatedCycleTimePrev = 0.9f;
    public Vector3 hipReference;
    public Vector3 ankleReference;
    public Vector3 footBase;
    public Quaternion footBaseRotation;
    public Vector3 ankle;

    // Foot cycle event time stamps
    public float stanceTime = 0;
    public float liftTime = 0.1f;
    public float liftoffTime = 0.2f;
    public float postliftTime = 0.3f;
    public float prelandTime = 0.7f;
    public float strikeTime = 0.8f;
    public float landTime = 0.9f;

    public LegCyclePhase phase = LegCyclePhase.Stance;

    // Standing logic
    public bool parked;

    // Cycle properties
    public Vector3 stancePosition;
    public Vector3 heelToetipVector;

    public List<string> debugHistory = new List<string>();

    public float GetFootGrounding(float time)
    {
        if ((time <= liftTime) || (time >= landTime)) return 0;
        if ((time >= postliftTime) && (time <= prelandTime)) return 1;
        if (time < postliftTime)
        {
            return (time - liftTime) / (postliftTime - liftTime);
        }
        else
        {
            return 1 - (time - prelandTime) / (landTime - prelandTime);
        }
    }
}