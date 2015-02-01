
using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AnimateTranslate
{
	AnimateCoordinate AnimX = null;
	AnimateCoordinate AnimY = null;
	AnimateCoordinate AnimZ = null;
	
	public float _Speed = 1.0f;
	protected float _DefaultDelta = 0.01f;
	protected float _MaxDelta;
	
	public Vector3 currentVector;
	public Vector3 finalpos;
	public Transform _TransformToAnim;
	
	protected bool _bLaunched;
	
	public AnimateTranslate(Transform obj_trans)
	{
		_TransformToAnim = obj_trans;
	}

	// Use this for initialization
	public void launchAnimation(Vector3 target, float speed) 
	{
		_Speed = speed;
		Vector3 initialpos = _TransformToAnim.position;
		currentVector = initialpos;
		finalpos = target;
		_bLaunched = true;
		/*GameObject init  = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		init.transform.Translate(initialpos);
		init.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		GameObject final  = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		final.transform.Translate(finalpos);
		final.transform.localScale = new Vector3(0.1f,0.1f,0.1f);*/
		
		AnimX = new AnimateCoordinate();
		AnimY = new AnimateCoordinate();
		AnimZ = new AnimateCoordinate();
		
		Vector3 Deltas = ComputeDeltas(
			Mathf.Sqrt(Mathf.Pow(initialpos.x-finalpos.x, 2)),
			Mathf.Sqrt(Mathf.Pow(initialpos.y-finalpos.y, 2)),
			Mathf.Sqrt(Mathf.Pow(initialpos.z-finalpos.z, 2)));
		
		AnimX.Start(initialpos.x, finalpos.x, Deltas.x);
		AnimY.Start(initialpos.y, finalpos.y, Deltas.y);
		AnimZ.Start(initialpos.z, finalpos.z, Deltas.z);
	}
	// Use this for initialization
	public void stopAnimation() 
	{	
		if(AnimX != null)
			AnimX.Stop();
		if(AnimY != null)
			AnimY.Stop();
		if(AnimZ != null)
			AnimZ.Stop();
	}
	public bool isLaunched()
	{
		return _bLaunched;
	}
	public bool isTargetReached()
	{
		if(_bLaunched)
			return (AnimX.IsTargetReached() && AnimY.IsTargetReached() && AnimZ.IsTargetReached());
		else
			return false;
	}
	public bool isStopped()
	{
		if(_bLaunched)
			return (AnimX.IsStopped() && AnimY.IsStopped() && AnimZ.IsStopped());
		else
			return false;
	}
	Vector3 ComputeDeltas(float distX, float distY, float distZ)
	{
		_MaxDelta = _DefaultDelta * _Speed;
		
		float distMax = Mathf.Max(Mathf.Max(distX, distY), distZ);
		Vector3 res = new Vector3();
		if(distMax == distX)
		{
			res.x = _MaxDelta;
			res.y = distY*_MaxDelta / distMax;
			res.z = distZ*_MaxDelta / distMax;
		}
		else if(distMax == distY)
		{
			res.x = distX*_MaxDelta / distMax;
			res.y = _MaxDelta;
			res.z = distZ*_MaxDelta / distMax;
		}
		else if(distMax == distZ)
		{
			res.x = distX*_MaxDelta / distMax;
			res.y = distY*_MaxDelta / distMax;
			res.z = _MaxDelta;
		}
		return res;
	}

	// Update is called once per frame
	public void Update()
	{
		if(isTargetReached())
			return;
		if(isStopped())
			return;
		if(!_bLaunched)
			return;

		float deltaX = AnimX.Update(_TransformToAnim.position.x);
		float deltaY = AnimY.Update(_TransformToAnim.position.y);
		float deltaZ = AnimZ.Update(_TransformToAnim.position.z);
		
		_TransformToAnim.position += new Vector3(deltaX, deltaY, deltaZ);
	}
}