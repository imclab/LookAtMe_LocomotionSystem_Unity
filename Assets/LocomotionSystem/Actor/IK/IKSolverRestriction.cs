using UnityEngine;
using System.Collections;

/// <summary>
///  Original source code :   http://www.darwin3d.com/gamedev/CCD3D.cpp
///  This class is ported on the original source code for Unity
/// </summary>
public class TrigoTools
{
	static public bool IsInRange(float val, float vmin, float vmax)
	{
		return (vmin<=val) && (val<=vmax);
	}
	static public float ConvertToTrigo180(float val)
	{
		return (val>180) ? 180-val : val;
	}
	static public float ConvertToTrigo360(float val)
	{
		return (val<0) ? (360+val) : val;
	}
}
public class IKSolverRestriction : MonoBehaviour
{
	[System.Serializable] 
	public class TrigoInterval
	{
		public TrigoInterval(float min, float max)
		{
			_Min = min;
			_Max = max;
		}
		public float GetRealMin360()
		{
			return Mathf.Min(_fNormalizedMin, _fNormalizedMax);
		}
		public float GetRealMax360()
		{
			return Mathf.Max(_fNormalizedMin, _fNormalizedMax);
		}
		
		public bool IsInInterval(float val)
		{
			bool  isInRange = TrigoTools.IsInRange(TrigoTools.ConvertToTrigo360(val), GetRealMin360(), GetRealMax360());
			return _bInvert ? !isInRange : isInRange;
		}
		public void Normalize_0_360()
		{
			_fNormalizedMin = TrigoTools.ConvertToTrigo360(_Min);
			_fNormalizedMax = TrigoTools.ConvertToTrigo360(_Max);
			_bInvert = _fNormalizedMin>_fNormalizedMax;
			if(_bInvert)
			{
				_fNormalizedCenter = (_fNormalizedMin + _fNormalizedMax) / 2.0f;
				_fNormalizedCenter += 180;
				_fNormalizedCenter = _fNormalizedCenter%360;
			}
			else
			{
				_fNormalizedCenter = (_fNormalizedMin + _fNormalizedMax) / 2.0f;
			}
		}
		public float GetMinimumLimitAngle(float angle)
		{
			angle = TrigoTools.ConvertToTrigo360(angle);
			if(angle>_fNormalizedCenter)
				return GetRealMax360();
			else
				return GetRealMin360();
		}
		
		public float _Min;
		public float _Max;
		
		public float _fNormalizedMin;
		public float _fNormalizedMax;
		public float _fNormalizedCenter;

		public bool _bInvert;
	}
		
	[System.Serializable]
	public class BoneEntity
	{
		public Transform bone;
		public bool restrictionEnabled;
		public Restriction restrictionRange;
	}
	
	[System.Serializable]
	public class Restriction
	{
		public TrigoInterval xRestriction = new TrigoInterval(0, 360);
		public TrigoInterval yRestriction = new TrigoInterval(0, 360);
		public TrigoInterval zRestriction = new TrigoInterval(0, 360);
		
		/*public float xMin = 0.0f;
		public float xMax = 360.0f;
		public float yMin = 0.0f;
		public float yMax = 360.0f;
		public float zMin = 0.0f;
		public float zMax = 360.0f;*/
		
		public float OldRotX = 0f;
		public float OldRotY = 0f;
		public float OldRotZ = 0f;
	}
	
	public Transform target;
	public BoneEntity[] boneEntity;
	public bool damping = false;
	public float dampingMax = 0.5f;
	public float limit = 0f;

	private float IK_POS_THRESH = 0.01f;
	private int MAX_IK_TRIES = 200;

	void Start ()
	{
		//if (target == null)
		//	target = transform;
	}

	public void LateUpdate ()
	{
		if(target)
			Solve ();
	}

	public void Solve ()
	{
		Transform endEffector = boneEntity[boneEntity.Length - 1].bone;
		Vector3 rootPos = Vector3.zero;
		Vector3 curEnd = Vector3.zero;
		
		Vector3 targetDirection = Vector3.zero;
		Vector3 currentDirection = Vector3.zero;
		Vector3 crossResult = Vector3.zero;
		
		float theDot = 0;
		float turnRadians = 0;
		float turnDeg = 0;
		
		int link = boneEntity.Length - 1;
		int tries = 0;
		
		// POSITION OF THE END EFFECTOR
		curEnd = endEffector.position;
		
		// QUIT IF I AM CLOSE ENOUGH OR BEEN RUNNING LONG ENOUGH
		// SEE IF I AM ALREADY CLOSE ENOUGH
		while (tries < MAX_IK_TRIES && (curEnd - target.position).magnitude > IK_POS_THRESH) {
			
			if (link < 0) {
				link = boneEntity.Length - 1;
			}
			
			rootPos = boneEntity[link].bone.position;
			curEnd = endEffector.position;
			
			// CREATE THE VECTOR TO THE CURRENT EFFECTOR POS
			currentDirection = curEnd - rootPos;
			// CREATE THE DESIRED EFFECTOR POSITION VECTOR
			targetDirection = target.position - rootPos;
			
			// NORMALIZE THE VECTORS (EXPENSIVE, REQUIRES A SQRT)
			currentDirection.Normalize ();
			targetDirection.Normalize ();
			
			// THE DOT PRODUCT GIVES ME THE COSINE OF THE DESIRED ANGLE
			theDot = Vector3.Dot (currentDirection, targetDirection);
			
			
			// IF THE DOT PRODUCT RETURNS 1.0, I DON'T NEED TO ROTATE AS IT IS 0 DEGREES
			if (theDot < 0.999999999999999999999999999999f) {
				
				// USE THE CROSS PRODUCT TO CHECK WHICH WAY TO ROTATE
				
				crossResult = Vector3.Cross (currentDirection, targetDirection);
				currentDirection.Normalize ();
				
				turnRadians = Mathf.Acos (theDot);
				turnDeg = turnRadians * Mathf.Rad2Deg;
				
				if (damping) {
					if (turnRadians > dampingMax) 
						turnRadians = dampingMax; 
					turnDeg = turnRadians * Mathf.Rad2Deg;
				}
				
				boneEntity[link].bone.rotation = Quaternion.AngleAxis (turnDeg, crossResult) * boneEntity[link].bone.rotation;
				
				if (boneEntity[link].restrictionEnabled)
					CheckRestrictions (boneEntity[link]);
			}
			
			tries++;
			link--;
		}
	}
	
	void CheckRestrictions (BoneEntity boneEntity)
	{
		
		// FIRST STEP IS TO CONVERT LINK QUATERNION BACK TO EULER ANGLES
        Vector3 euler = boneEntity.bone.localRotation.eulerAngles;

        //Debug.Log(euler);
		
        //float xAngle = TrigoTools.ConvertToTrigo360(euler.x);
        //float yAngle = TrigoTools.ConvertToTrigo360(euler.y);
        //float zAngle = TrigoTools.ConvertToTrigo360(euler.z);
		
		if(!boneEntity.restrictionRange.xRestriction.IsInInterval(euler.x))
		{
            //euler.x = boneEntity.restrictionRange.xRestriction.GetMinimumLimitAngle(euler.x);
			euler.x = boneEntity.restrictionRange.OldRotX;
		}
		else
		{
			boneEntity.restrictionRange.OldRotX = euler.x;
		}
		if(!boneEntity.restrictionRange.yRestriction.IsInInterval(euler.y))
		{
            //euler.y = boneEntity.restrictionRange.yRestriction.GetMinimumLimitAngle(euler.y);
			euler.y = boneEntity.restrictionRange.OldRotY;
		}
		else
		{
			boneEntity.restrictionRange.OldRotY = euler.y;
		}
		if(!boneEntity.restrictionRange.zRestriction.IsInInterval(euler.z))
		{
           	//euler.z = boneEntity.restrictionRange.zRestriction.GetMinimumLimitAngle(euler.z);
			euler.z = boneEntity.restrictionRange.OldRotZ;
		}
		else
		{
			boneEntity.restrictionRange.OldRotZ = euler.z;
		}
		
		/*double xMin = ConvertToTrigo180(boneEntity.restrictionRange.xMin);
		double xMax = ConvertToTrigo180(boneEntity.restrictionRange.xMax);
		
		double yMin = ConvertToTrigo180(boneEntity.restrictionRange.yMin);
		double yMax = ConvertToTrigo180(boneEntity.restrictionRange.yMax);
		
		double zMin = ConvertToTrigo180(boneEntity.restrictionRange.zMin);
		double zMax = ConvertToTrigo180(boneEntity.restrictionRange.zMax);
		
		bool xIsInRange = IsInRange(xAngle, Mathf.Min(xMin, xMax), Mathf.Max(xMin, xMax));

        /*double xCenter = (boneEntity.restrictionRange.xMin + boneEntity.restrictionRange.xMax) / 2.0;
        double yCenter = (boneEntity.restrictionRange.yMin + boneEntity.restrictionRange.yMax) / 2.0;
        double zCenter = (boneEntity.restrictionRange.zMin + boneEntity.restrictionRange.zMax) / 2.0;

        // CHECK THE DOF SETTINGS
        if (xAngle < boneEntity.restrictionRange.xMin)
            euler.x = boneEntity.restrictionRange.xMin;
        if (yAngle < boneEntity.restrictionRange.yMin)
            euler.y = boneEntity.restrictionRange.yMin;
        if (zAngle < boneEntity.restrictionRange.zMin)
            euler.z = boneEntity.restrictionRange.zMin;

        if (xAngle > boneEntity.restrictionRange.xMax
            && xAngle <= xCenter + 180.0)
            euler.x = boneEntity.restrictionRange.xMax;
        if (yAngle > boneEntity.restrictionRange.yMax
            && yAngle <= yCenter + 180.0)
            euler.y = boneEntity.restrictionRange.yMax;
        if (zAngle > boneEntity.restrictionRange.zMax
            && zAngle <= zCenter + 180.0)
            euler.z = boneEntity.restrictionRange.zMax;

        if (xAngle < 360.0 + boneEntity.restrictionRange.xMin
            && xAngle > xCenter + 180.0)
            euler.x = boneEntity.restrictionRange.xMin;
        if (yAngle < 360.0 + boneEntity.restrictionRange.yMin
            && yAngle > yCenter + 180.0)
            euler.y = boneEntity.restrictionRange.yMin;
        if (zAngle < 360.0 + boneEntity.restrictionRange.zMin
            && zAngle > zCenter + 180.0)
            euler.z = boneEntity.restrictionRange.zMin;*/

        //Vector3 euler = boneEntity.bone.localRotation.eulerAngles;

        //Debug.Log(euler);

        //// CHECK THE DOF SETTINGS
        //if (euler.x < boneEntity.restrictionRange.xMin)
        //    euler.x = boneEntity.restrictionRange.xMin;
        //if (euler.y < boneEntity.restrictionRange.yMin)
        //    euler.y = boneEntity.restrictionRange.yMin;
        //if (euler.z < boneEntity.restrictionRange.zMin)
        //    euler.z = boneEntity.restrictionRange.zMin;

        //if (euler.x > boneEntity.restrictionRange.xMax)
        //    euler.x = boneEntity.restrictionRange.xMax;
        //if (euler.y > boneEntity.restrictionRange.yMax)
        //    euler.y = boneEntity.restrictionRange.yMax;
        //if (euler.z > boneEntity.restrictionRange.zMax)
        //    euler.z = boneEntity.restrictionRange.zMax;*/

		// BACK TO QUATERNION
		boneEntity.bone.localRotation = Quaternion.Euler (euler);
		/*if(boneEntity.bone.name == "Bip01 L Clavicle")
		//if(boneEntity.bone.localRotation.eulerAngles.y == 287.8134f)
		{
            if(euler.y != boneEntity.restrictionRange.yMin)
			{
				int u=0;
			}
		}*/
	}
}
