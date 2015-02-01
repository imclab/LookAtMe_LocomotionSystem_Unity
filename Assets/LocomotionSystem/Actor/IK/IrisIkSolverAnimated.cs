using UnityEngine;
using System.Collections;


public class IrisIkSolverAnimated : MonoBehaviour 
{	
	AnimateCoordinate AnimX = null;
	AnimateCoordinate AnimY = null;
	AnimateCoordinate AnimZ = null;
	
	public float _Speed = 1.0f;
	protected float _DefaultDelta = 0.01f;
	protected float _MaxDelta;
	
	public Vector3 currentVector;
	public Vector3 initialpos;
	public Vector3 finalpos;
	public GameObject CurrentPos;
	public GameObject _ObjectToAnim;
	//protected IKSolverRestrictionRight _IKSolverRestrictionRight;
	//protected IKSolverRestrictionLeft _IKSolverRestrictionLeft;
	
	protected bool _bLaunched;
	
	public void Start() 
	{
		/*_IKSolverRestrictionRight = GetComponent("IKSolverRestrictionRight") as IKSolverRestrictionRight;
		_ObjectToAnim = _IKSolverRestrictionRight.InitRightArm(this.transform);
		
		_IKSolverRestrictionLeft = GetComponent("IKSolverRestrictionLeft") as IKSolverRestrictionLeft;
		_IKSolverRestrictionLeft.InitLeftArm(this.transform);

		CurrentPos = _IKSolverRestrictionRight.RHandTarget;//GameObject.CreatePrimitive(PrimitiveType.Sphere);
		CurrentPos.transform.localScale = new Vector3(0.1f,0.1f,0.1f);*/
	}
	
	// Use this for initialization
	public void launchAnimation(Vector3 target) 
	{
		//_IKSolverRestrictionRight.target = CurrentPos.transform;
		initialpos = _ObjectToAnim.transform.position;
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
		
		CurrentPos.transform.position = initialpos;
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
	protected void Update()
	{
		if(isTargetReached())
			return;
		if(isStopped())
			return;
		if(!_bLaunched)
			return;

		float deltaX = AnimX.Update(CurrentPos.transform.position.x);
		float deltaY = AnimY.Update(CurrentPos.transform.position.y);
		float deltaZ = AnimZ.Update(CurrentPos.transform.position.z);
		
		//currentVector += new Vector3(deltaX, deltaY, deltaZ);
		
		CurrentPos.transform.position += new Vector3(deltaX, deltaY, deltaZ);
		
		//_IKSolverRestrictionRight.target = CurrentPos.transform;
		
		//_IKSolverRestrictionRight.Solve();
	}
}
