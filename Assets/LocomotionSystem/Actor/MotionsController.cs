using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotionsController
{
	/*Animator _Animator;
	List<MotionController> 	_MotionsList = null;
	List<ActorListener> 	_ListenerList = null;
	
	public MotionsController(Animator animator)
	{
		_Animator = animator;
		_MotionsList = new List<MotionController>();
		_ListenerList = new List<ActorListener>();
	}
	
	public bool PlayMotion(string motion_id, bool loop)
	{		
		MotionController currentMotion = new MotionController(_Animator);
		currentMotion.Launch(motion_id, loop);
		_MotionsList.Add(currentMotion);
		return true;
	}
	
	public bool StopMotion(string motion_id)
	{
		MotionController currentMotion = FindMotion(motion_id);
		if(currentMotion != null)
		{
			currentMotion.Stop();
			_MotionsList.Remove(currentMotion);
			return true;
		}
		else
		{
			return false;
		}
	}
	public bool DeleteMotion(string motion_id)
	{
		MotionController currentMotion = FindMotion(motion_id);
		if(currentMotion != null)
		{
			_MotionsList.Remove(currentMotion);
			return true;
		}
		else
		{
			return false;
		}
	}
	
	// return -1 : not found
	// return 0 : motion is not done
	// return 1 : motion is done
	public int IsMotionDone(string motion_id)
	{
		MotionController currentMotion = FindMotion(motion_id);
		if(currentMotion != null)
		{
			return currentMotion.IsDone() ? 1 : 0;
		}
		else
		{
			return -1;
		}
	}
	
	public MotionController FindMotion(string motion_id)
	{
		foreach(MotionController currentmotion in _MotionsList)
		{
			if(currentmotion.GetMotionName() == motion_id)
			{
				return currentmotion;
			}
		}
		return null;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		List<MotionController> MotionToDelete = new List<MotionController>();
		foreach(MotionController currentmotion in _MotionsList)
		{
			currentmotion.Update();
			
			if(!currentmotion.IsLooping() && currentmotion.IsDone())
			{
				MotionToDelete.Add(currentmotion);
			}
		}
		foreach(MotionController currentmotion in MotionToDelete)
		{
			foreach(ActorListener listener in _ListenerList)
			{
				listener.MotionIsDone(currentmotion.GetMotionName());
			}
		}
	}
	public void RegisterListener(ActorListener listener)
	{
		_ListenerList.Add(listener);
	}*/
	
}
