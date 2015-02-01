using UnityEngine;
using System.Collections;

public class IKSolverRestrictionLeft : IKSolverRestriction
{
	protected Transform LHandDummy;
	public GameObject LHandTarget;
	
	public GameObject InitLeftArm(Transform parent)
	{
		GameObject tmpLHandDummy = new GameObject();
		
		Transform Bip01 = parent.transform.FindChild("Bip01");
		Transform LClavicule = Bip01.FindChild("Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Neck/Bip01 L Clavicle");
		Transform LUpperArm = LClavicule.FindChild("Bip01 L UpperArm");
		Transform LForearm = LUpperArm.FindChild("Bip01 L Forearm");
		Transform LHand = LForearm.FindChild("Bip01 L Hand");
		Transform LHandDummy = LHand.FindChild("Bip01 L HandDummy");
		
		// si pas de dummy dans la main alors on en créée un
		if(LHandDummy == null)
		{			
			tmpLHandDummy.name = "Bip01 L HandDummy";
			tmpLHandDummy.transform.parent = LHand;
			tmpLHandDummy.transform.localPosition = new Vector3(-0.08637f, 0.0278f, 0.005734f);
			tmpLHandDummy.transform.localRotation = new Quaternion(0,0,0,0);
			tmpLHandDummy.transform.localScale = new Vector3(1,1,1);
			
			LHandDummy = LHand.FindChild("Bip01 L HandDummy");
			
			// on ajoute une target locale
			LHandTarget = new GameObject(); //GameObject.CreatePrimitive(PrimitiveType.Sphere);//
			LHandTarget.name = "Bip01 L HandTarget";
			LHandTarget.transform.parent = Bip01;
			LHandTarget.transform.localPosition = new Vector3(0f, 0f, 0f);
			LHandTarget.transform.localRotation = new Quaternion(0,0,0,0);
			LHandTarget.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		}
		
		boneEntity = new BoneEntity[5];
		
		//////////////////////////////////////////////////////
		// LClavicule
		//////////////////////////////////////////////////////
		int i=0;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = LClavicule;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = 19.61f;
		boneEntity[i].restrictionRange.xRestriction._Max = 19.61f;
		boneEntity[i].restrictionRange.yRestriction._Min = 253f;
		boneEntity[i].restrictionRange.yRestriction._Max = 290f;
		boneEntity[i].restrictionRange.zRestriction._Min = 175f;
		boneEntity[i].restrictionRange.zRestriction._Max = 180f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();		
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// LUpperArm
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = LUpperArm;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = -4f;
		boneEntity[i].restrictionRange.xRestriction._Max = 20f;
		boneEntity[i].restrictionRange.yRestriction._Min = -72f;
		boneEntity[i].restrictionRange.yRestriction._Max = -12f;
		boneEntity[i].restrictionRange.zRestriction._Min = -15f;
		boneEntity[i].restrictionRange.zRestriction._Max = 40f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// LForearm
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = LForearm;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = -15f;
		boneEntity[i].restrictionRange.xRestriction._Max = 15f;
		boneEntity[i].restrictionRange.yRestriction._Min = 0f;
		boneEntity[i].restrictionRange.yRestriction._Max = 0f;
		boneEntity[i].restrictionRange.zRestriction._Min = 18f;
		boneEntity[i].restrictionRange.zRestriction._Max = 144f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// LHand
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = LHand;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = -90f;
		boneEntity[i].restrictionRange.xRestriction._Max = -70f;
		boneEntity[i].restrictionRange.yRestriction._Min = -10f;
		boneEntity[i].restrictionRange.yRestriction._Max = 5f;
		boneEntity[i].restrictionRange.zRestriction._Min = 0f;
		boneEntity[i].restrictionRange.zRestriction._Max = 40f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// LHandDummy
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = LHandDummy;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = 0f;
		boneEntity[i].restrictionRange.xRestriction._Max = 0f;
		boneEntity[i].restrictionRange.yRestriction._Min = 0f;
		boneEntity[i].restrictionRange.yRestriction._Max = 0f;
		boneEntity[i].restrictionRange.zRestriction._Min = 0f;
		boneEntity[i].restrictionRange.zRestriction._Max = 0f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		return tmpLHandDummy;
	}
}
