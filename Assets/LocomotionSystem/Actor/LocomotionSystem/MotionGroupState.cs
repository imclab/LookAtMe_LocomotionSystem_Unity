
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class MotionGroupState
{
    public AnimationState controller;
    public float weight;
    public AnimationState[] motionStates;
    public float[] relativeWeights;
    public float[] relativeWeightsBlended;
    public int primaryMotionIndex;
}