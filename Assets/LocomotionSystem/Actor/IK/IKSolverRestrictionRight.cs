using UnityEngine;
using System.Collections;

public class IKSolverRestrictionRight : IKSolverRestriction
{
	protected Transform RHandDummy;
	public GameObject RHandTarget;
	
	public GameObject InitRightArm(Transform parent)
	{
		GameObject tmpRHandDummy = new GameObject();
		
		Transform Bip01 = parent.transform.FindChild("Bip01");
		Transform RClavicule = Bip01.FindChild("Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Neck/Bip01 R Clavicle");
		Transform RUpperArm = RClavicule.FindChild("Bip01 R UpperArm");
		Transform RForearm = RUpperArm.FindChild("Bip01 R Forearm");
		Transform RHand = RForearm.FindChild("Bip01 R Hand");
		Transform RHandDummy = RHand.FindChild("Bip01 R HandDummy");
		
		// si pas de dummy dans la main alors on en créée un
		if(RHandDummy == null)
		{
			tmpRHandDummy.name = "Bip01 R HandDummy";
			tmpRHandDummy.transform.parent = RHand;
			tmpRHandDummy.transform.localPosition = new Vector3(-0.08637f, 0.0278f, 0.005734f);
			tmpRHandDummy.transform.localRotation = new Quaternion(0,0,0,0);
			tmpRHandDummy.transform.localScale = new Vector3(1,1,1);
			
			RHandDummy = RHand.FindChild("Bip01 R HandDummy");
			
			// on ajoute une target locale
			RHandTarget = new GameObject(); //GameObject.CreatePrimitive(PrimitiveType.Sphere);//
			RHandTarget.name = "Bip01 R HandTarget";
			RHandTarget.transform.parent = Bip01;
			RHandTarget.transform.localPosition = new Vector3(0f, 0f, 0f);
			RHandTarget.transform.localRotation = new Quaternion(0,0,0,0);
			RHandTarget.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
			
			// on configure l'animation de l'IK avec les elements créés
			// 1-- ajout du script d'animation sur le dummy de la main
			//IrisIkSolverAnimated IkAnimatedComponent = RHandDummy.GetComponent("IrisIkSolverAnimated") as IrisIkSolverAnimated;
			//if(IkAnimatedComponent == null)
			//{
			//	IkAnimatedComponent = tmpRHandDummy.AddComponent(typeof(IrisIkSolverAnimated)) as IrisIkSolverAnimated;
			//}
		}
		
		boneEntity = new BoneEntity[5];
		
		//////////////////////////////////////////////////////
		// RClavicule
		//////////////////////////////////////////////////////
		int i=0;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = RClavicule;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = 341f;
		boneEntity[i].restrictionRange.xRestriction._Max = 341f;
		boneEntity[i].restrictionRange.yRestriction._Min = 73.2f;
		boneEntity[i].restrictionRange.yRestriction._Max = 103f;
		boneEntity[i].restrictionRange.zRestriction._Min = 175f;
		boneEntity[i].restrictionRange.zRestriction._Max = 184f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();		
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// RUpperArm
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = RUpperArm;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = -20f;
		boneEntity[i].restrictionRange.xRestriction._Max = 4f;
		boneEntity[i].restrictionRange.yRestriction._Min = -20f;
		boneEntity[i].restrictionRange.yRestriction._Max = 69f;
		boneEntity[i].restrictionRange.zRestriction._Min = -15f;
		boneEntity[i].restrictionRange.zRestriction._Max = 70f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();		
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// RForearm
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = RForearm;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = -15f;
		boneEntity[i].restrictionRange.xRestriction._Max = 15f;
		boneEntity[i].restrictionRange.yRestriction._Min = -10f;
		boneEntity[i].restrictionRange.yRestriction._Max = -10f;
		boneEntity[i].restrictionRange.zRestriction._Min = 5f;
		boneEntity[i].restrictionRange.zRestriction._Max = 145f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();		
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// RHand
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = RHand;
		boneEntity[i].restrictionEnabled = true;
		boneEntity[i].restrictionRange = new Restriction();
		boneEntity[i].restrictionRange.xRestriction._Min = 40f;
		boneEntity[i].restrictionRange.xRestriction._Max = 60f;
		boneEntity[i].restrictionRange.yRestriction._Min = 5f;
		boneEntity[i].restrictionRange.yRestriction._Max = 10f;
		boneEntity[i].restrictionRange.zRestriction._Min = 0f;
		boneEntity[i].restrictionRange.zRestriction._Max = 40f;
		
		boneEntity[i].restrictionRange.OldRotX = boneEntity[i].bone.localEulerAngles.x;
		boneEntity[i].restrictionRange.OldRotY = boneEntity[i].bone.localEulerAngles.y;
		boneEntity[i].restrictionRange.OldRotZ = boneEntity[i].bone.localEulerAngles.z;
		
		boneEntity[i].restrictionRange.xRestriction.Normalize_0_360();		
		boneEntity[i].restrictionRange.yRestriction.Normalize_0_360();
		boneEntity[i].restrictionRange.zRestriction.Normalize_0_360();
		
		//////////////////////////////////////////////////////
		// RHandDummy
		//////////////////////////////////////////////////////
		++i;
		boneEntity[i] = new BoneEntity();
		boneEntity[i].bone = RHandDummy;
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
		
		return tmpRHandDummy;
	}
}
