using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasicFilter : AFilter
{
	public float _DeltaTimeForSmoothing;
	public bool _SmoothingRotation = false;
	public bool _SmoothingPosition = false;
	
	public BasicFilter()
	{
	}

	public override void SmootRotation(float fCurrentTime, int currentIndex, out Vector3 LocalTargetInfOnTrajectory, out Vector3 LocalTargetSupOnTrajectory)
	{
		LocalTargetInfOnTrajectory = GetPositionAtTime(fCurrentTime-_DeltaTimeForSmoothing, false, currentIndex);
		LocalTargetSupOnTrajectory = GetPositionAtTime(fCurrentTime+_DeltaTimeForSmoothing, true, currentIndex);
	}
	
	public override void SmootPosition(float fCurrentTime, int currentIndex, out Vector3 LocalTargetInfOnTrajectory, out Vector3 LocalTargetSupOnTrajectory)
	{
		LocalTargetInfOnTrajectory = GetPositionAtTime(fCurrentTime-_DeltaTimeForSmoothing, false, currentIndex);
		LocalTargetSupOnTrajectory = GetPositionAtTime(fCurrentTime+_DeltaTimeForSmoothing, true, currentIndex);
	}

}

