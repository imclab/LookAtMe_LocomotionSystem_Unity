using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class AFilter
{
	// variables
	bool _UseOnline;
	SkeletonReader _Reader;
	
	public SkeletonReader Reader
	{
		get
		{
			return _Reader;
		}
		
		set
		{
			_Reader = value;
		}
	}

	// methods
	abstract public void SmootRotation(float fCurrentTime, int currentIndex, out Vector3 LocalTargetInfOnTrajectory, out Vector3 LocalTargetSupOnTrajectory);

	abstract public void SmootPosition(float fCurrentTime, int currentIndex, out Vector3 LocalTargetInfOnTrajectory, out Vector3 LocalTargetSupOnTrajectory);

	public Vector3 GetPositionAtTime(float timerequested, bool increment, int currentIndex)
	{
		if(timerequested<_Reader.IndexList[0][1])
		{
			return new Vector3(_Reader.mSkeleton[0][1], 0.0f, _Reader.mSkeleton[0][2]);
		}
		else if(timerequested>_Reader.IndexList[_Reader.mSkeleton.Count-1][1])
		{
			return new Vector3(_Reader.mSkeleton[_Reader.mSkeleton.Count-1][1], 0.0f, _Reader.mSkeleton[_Reader.mSkeleton.Count-1][2]);
		}
		else
		{
			Vector4 vInfTemp = new Vector4();
			Vector4 vSupTemp = new Vector4();
			bool ok = FindIntervalFromTime(timerequested, ref vInfTemp, ref vSupTemp, increment, currentIndex);
			if(!ok)
			{
				return new Vector3();
			}
			float TVInf = vInfTemp[0];
			float TVSup = vSupTemp[0];
			float TReference = TVSup-TVInf;
			float TInInterval = timerequested-TVInf;
			float RatioTimeInInterval = TInInterval/TReference;
			
			Vector3 tmpvInfInWorld = new Vector3(vInfTemp[1], 0.0f, vInfTemp[2]);
			Vector3 tmpvSupInWorld = new Vector3(vSupTemp[1], 0.0f, vSupTemp[2]);
			Vector3 DirectionOfTheInterval = tmpvSupInWorld - tmpvInfInWorld;
			Vector3 CurrentPositionOnTheVector = tmpvInfInWorld+RatioTimeInInterval*DirectionOfTheInterval;
			return CurrentPositionOnTheVector;
		}
	}

	bool FindIntervalFromTime(float time, ref Vector4 vInfTemp, ref Vector4 vSupTemp, bool increment, int currentIndex)
	{
		if(currentIndex>=_Reader.IndexList.Count)
		{
			return false;
		}
		if(increment)
		{
			// test en avancant dans la liste 
			for(int iIndex = currentIndex ; iIndex<_Reader.IndexList.Count ; ++iIndex)
			{
				float fCurrentIndexTime = 0.0f;
				float fNextIndexTime = 0.0f;
				
				if(iIndex+1<_Reader.IndexList.Count)
				{
					fCurrentIndexTime = _Reader.IndexList[iIndex][1];
					fNextIndexTime = _Reader.IndexList[iIndex+1][1];
					
					if( (time>=fCurrentIndexTime) && (time<fNextIndexTime))
					{					
						vInfTemp = _Reader.mSkeleton[iIndex];
						vSupTemp = _Reader.mSkeleton[iIndex+1];
						return true;
					}
				}
			}
		}
		else
		{
			// si pas de solution trouvée, test en reculant dans la liste 
			for(int iIndex = currentIndex ; iIndex>=0 ; --iIndex)
			{
				float fCurrentIndexTime = 0.0f;
				float fNextIndexTime = 0.0f;
				
				if(iIndex+1<_Reader.IndexList.Count)
				{
					fCurrentIndexTime = _Reader.IndexList[iIndex][1];
					fNextIndexTime = _Reader.IndexList[iIndex+1][1];
					
					if( (time>=fCurrentIndexTime) && (time<fNextIndexTime))
					{					
						vInfTemp = _Reader.mSkeleton[iIndex];
						vSupTemp = _Reader.mSkeleton[iIndex+1];
						return true;
					}
				}
			}
		}
		return false;
	}

	public SkeletonReader getReader()
	{
		return _Reader;
	}
}