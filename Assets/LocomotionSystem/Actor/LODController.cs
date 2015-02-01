using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LODGroup))]
public class LODController : MonoBehaviour
{
	MyLODGroup _MyLODGroup;
	bool UseMyLodGroup = false;
	Dictionary<string, int> _LodLevelMap;
	SkinnedMeshRenderer [] _SMRVector;
	public int _iCurrentLOD;

	// please UseMyLodGroup = true if you want to set head and hip. it's used for the calculation of the LOD rended to the screen
	/*public */Transform _Head = null;
	/*public */Transform _Hip = null;

	bool [] _SavedShadowRendering;
	public bool _OptimizeShadow = false;
	public int _OptimizeShadowLOD = 2;
	void Update()
	{
		if(UseMyLodGroup)
			_MyLODGroup.Update();
		
		/*Transform lodTransform = GetComponent<LODGroup>().transform;
		foreach (Transform child in lodTransform)
		{
			if (child.renderer != null && child.renderer.isVisible)
			{ 
				Debug.Log("This LODstep is used: " + child.name); 
			}
		}*/
	}
	public void Init(ObjectInterface inter, LODGroup LOD_Group)
	{
		if(UseMyLodGroup)
			_MyLODGroup = new MyLODGroup(inter, _Head, _Hip);

		_LodLevelMap = new Dictionary<string, int>();
		_SMRVector = new SkinnedMeshRenderer[LOD_Group.lodCount];
		_SavedShadowRendering = new bool[LOD_Group.lodCount];

//		SerializedObject obj = new SerializedObject(LOD_Group);
		float step = 0f;
		for(int i=0 ; i<LOD_Group.lodCount ; ++i)
		{
//			SerializedProperty SPheight = obj.FindProperty("m_LODs.Array.data["+i+"].screenRelativeHeight");
//			{
//				step = SPheight.floatValue * 100.0f;
//			}
//			SkinnedMeshRenderer CurrentSMR = obj.FindProperty("m_LODs.Array.data["+i+"].renderers").GetArrayElementAtIndex(0).FindPropertyRelative("renderer").objectReferenceValue as SkinnedMeshRenderer;
//			CurrentSMR.gameObject.AddComponent<LODManagement>();
//			LODManagement currentLODManageemt = CurrentSMR.gameObject.GetComponent<LODManagement>();
//			_LodLevelMap.Add(CurrentSMR.name, i);
//			_SMRVector.SetValue(CurrentSMR, i);

//			_SavedShadowRendering.SetValue(CurrentSMR.castShadows, i);
//			currentLODManageemt.Init(inter, i );
			/*if(_OptimizeShadow)
			{
				CurrentSMR.castShadows = false;
				CurrentSMR.receiveShadows = false;
			}*/

			if(UseMyLodGroup)
			{
				float min = (i==0) ? 100 : _MyLODGroup.LODVector[i-1]._Percentage;
//				_MyLODGroup.LODVector.Add(new MyLOD(i, CurrentSMR, min, step));
			}
		}
		if(UseMyLodGroup)
			_MyLODGroup.LODVector.Add(new MyLOD(LOD_Group.lodCount+1, null, step,0.0f));
		//_MyLODGroup.enabled = false;
	}
	public void SetCurrentLOD(int lod)
	{
		_iCurrentLOD = lod;
		
		for(int i=0 ; i<_SMRVector.Length ; ++i)
		{
			if(_iCurrentLOD == i)
			{
				if(_OptimizeShadow)
				{
					if(_iCurrentLOD>_OptimizeShadowLOD)
					{
						_SMRVector[i].castShadows = false;
						_SMRVector[i].receiveShadows = false;
					}
					else
					{
						_SMRVector[i].castShadows = true;
						_SMRVector[i].receiveShadows = true;
					}
				}
				else
				{
					_SMRVector[i].castShadows = _SavedShadowRendering[i];
					_SMRVector[i].receiveShadows = _SavedShadowRendering[i];
				}
			}
			else
			{
				if(_OptimizeShadow)
				{
					_SMRVector[i].castShadows = false;
					_SMRVector[i].receiveShadows = false;
				}
				else
				{
					_SMRVector[i].castShadows = _SavedShadowRendering[i];
					_SMRVector[i].receiveShadows = _SavedShadowRendering[i];
				}
			}
		}
	}
	public int GetCurrentLOD()
	{
		return _iCurrentLOD;
	}
}
