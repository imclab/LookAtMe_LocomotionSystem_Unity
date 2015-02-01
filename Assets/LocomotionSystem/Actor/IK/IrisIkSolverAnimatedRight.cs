using UnityEngine;
using System.Collections;

public class IrisIkSolverAnimatedRight : IrisIkSolverAnimated 
{	
	protected IKSolverRestrictionRight _IKSolverRestrictionRight;
	
	new public void Start()
	{
		_IKSolverRestrictionRight = GetComponent("IKSolverRestrictionRight") as IKSolverRestrictionRight;
		_ObjectToAnim = _IKSolverRestrictionRight.InitRightArm(this.transform);
		CurrentPos = null;
	}
	
	// Use this for initialization
	new public void launchAnimation(Vector3 target) 
	{
		_IKSolverRestrictionRight.enabled = true;
		CurrentPos = _IKSolverRestrictionRight.RHandTarget;//GameObject.CreatePrimitive(PrimitiveType.Sphere);
		CurrentPos.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		
		_IKSolverRestrictionRight.target = CurrentPos.transform;
		base.launchAnimation(target);
	}

	// Update is called once per frame
	new void Update()
	{
		base.Update();
		if(CurrentPos)
		{
			_IKSolverRestrictionRight.target = CurrentPos.transform;
			_IKSolverRestrictionRight.Solve();
		}
		if(isStopped())
		{
			_IKSolverRestrictionRight.enabled = false;
		}
	}
}
