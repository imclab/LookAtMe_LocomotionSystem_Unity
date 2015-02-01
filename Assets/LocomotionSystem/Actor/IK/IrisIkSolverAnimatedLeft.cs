using UnityEngine;
using System.Collections;

public class IrisIkSolverAnimatedLeft : IrisIkSolverAnimated 
{
	protected IKSolverRestrictionLeft _IKSolverRestrictionLeft;
	
	new public void Start() 
	{		
		_IKSolverRestrictionLeft = GetComponent("IKSolverRestrictionLeft") as IKSolverRestrictionLeft;
		_ObjectToAnim = _IKSolverRestrictionLeft.InitLeftArm(this.transform);
		CurrentPos = null;
	}
	
	// Use this for initialization
	new public void launchAnimation(Vector3 target) 
	{
		_IKSolverRestrictionLeft.enabled = true;
		CurrentPos = _IKSolverRestrictionLeft.LHandTarget;//GameObject.CreatePrimitive(PrimitiveType.Sphere);
		CurrentPos.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

		_IKSolverRestrictionLeft.target = CurrentPos.transform;
		base.launchAnimation(target);
	}

	// Update is called once per frame
	new void Update()
	{
		base.Update();
		
		if(CurrentPos)
		{
			_IKSolverRestrictionLeft.target = CurrentPos.transform;
			_IKSolverRestrictionLeft.Solve();
		}
		if(isStopped())
		{
			_IKSolverRestrictionLeft.enabled = false;
		}
	}
}
