using UnityEngine;
using System.Collections;

public class LODManagement : MonoBehaviour 
{
	ObjectInterface _ObjectInterface = null;
	bool _bCurrentVisibility;
	SkinnedMeshRenderer CurrentSMR;
	int _iIndex;
	// Use this for initialization
	public void Init(ObjectInterface inter, int index)
	{
		_iIndex = index;
		_ObjectInterface = inter;
		CurrentSMR = GetComponent<SkinnedMeshRenderer>();
		_bCurrentVisibility = CurrentSMR.renderer.isVisible;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(renderer.isVisible != _bCurrentVisibility)
		{
			_bCurrentVisibility = CurrentSMR.renderer.isVisible;
			if(_ObjectInterface!=null && _bCurrentVisibility)
			{
				_ObjectInterface.OnLODChange(_iIndex);
			}
		}
	}
}
