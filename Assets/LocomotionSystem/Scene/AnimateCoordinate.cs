using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AnimateCoordinate
{
	protected int ANIMATE_COORDINATE_STATE_NONE = 0;
	protected int ANIMATE_COORDINATE_STATE_STARTING = 1;
	protected int ANIMATE_COORDINATE_STATE_IsTargetReached = 2;
	protected int ANIMATE_COORDINATE_STATE_STOPPING = 4;
	protected int ANIMATE_COORDINATE_STATE_STOPPED = 8;
	
	public AnimateCoordinate()
	{		
		_iCurrentState = ANIMATE_COORDINATE_STATE_NONE;
	}
	public void Start(float initial_val, float final_val, float delta)
	{
		if(CheckState(ANIMATE_COORDINATE_STATE_NONE) || CheckState(ANIMATE_COORDINATE_STATE_STOPPED))
		{
			_fInitialVal = initial_val;
			_fFinalVal = final_val;
			_fDelta = delta;
			_bIncrement = (_fInitialVal<_fFinalVal);
			_iCurrentState = ANIMATE_COORDINATE_STATE_STARTING;
		}
	}
	public void Stop()
	{
		if(CheckState(ANIMATE_COORDINATE_STATE_IsTargetReached))
		{
			_iCurrentState = ANIMATE_COORDINATE_STATE_STOPPING;
			_bIncrement = (_fFinalVal<_fInitialVal);
		}
	}
	
	protected bool CheckState(int val)
	{
		return (val == _iCurrentState);
	}
	
	public float Update(float current_pos)
	{
		if(CheckState(ANIMATE_COORDINATE_STATE_STARTING))
		{
			if(_bIncrement)
			{
				_fCurrentVal = current_pos + _fDelta;
				if(_fCurrentVal >= _fFinalVal)
					_iCurrentState = ANIMATE_COORDINATE_STATE_IsTargetReached;

				return _fDelta;
			}
			else
			{
				_fCurrentVal = current_pos - _fDelta;
				if(_fCurrentVal <= _fFinalVal)
					_iCurrentState = ANIMATE_COORDINATE_STATE_IsTargetReached;
				
				return -_fDelta;
			}
		}
		else if(CheckState(ANIMATE_COORDINATE_STATE_STOPPING))
		{
			if(_bIncrement)
			{
				_fCurrentVal = current_pos + _fDelta;
				if(_fCurrentVal >= _fInitialVal)
					_iCurrentState = ANIMATE_COORDINATE_STATE_STOPPED;

				return _fDelta;
			}
			else
			{
				_fCurrentVal = current_pos - _fDelta;
				if(_fCurrentVal <= _fInitialVal)
					_iCurrentState = ANIMATE_COORDINATE_STATE_STOPPED;
				
				return -_fDelta;
			}
		}
		return 0;
	}
	
	public bool IsTargetReached()
	{
		return CheckState(ANIMATE_COORDINATE_STATE_IsTargetReached);
	}
	public bool IsStopped()
	{
		return CheckState(ANIMATE_COORDINATE_STATE_STOPPED);
	}
	
	protected float _fInitialVal;
	protected float _fFinalVal;
	protected float _fCurrentVal;
	protected int _iCurrentState;
	protected bool  _bIncrement;
	protected float _fDelta;
}