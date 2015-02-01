using UnityEngine;

public class ActorInterface : ObjectInterface
{
	GameObject _MyObj;
	LODGroup _LodGroup = null;
	ActorTools _ActorTools;
	public void Init(GameObject obj, ActorTools ActorTools)
	{
		_MyObj = obj;
		_MeshRenderer = null;
		_LodGroup = _MyObj.GetComponent<LODGroup>();
		_ActorTools = ActorTools;
	}
	public override void Show()
	{
		if(_LodGroup)
		{
			_LodGroup.enabled = true;
		}
	}
	public override void Hide()
	{
		if(_LodGroup)
		{
			_LodGroup.enabled = false;
		}
	}
	public override void OnLODChange(int current_lod_id)
	{
		if(_ActorTools)
			_ActorTools.OnLODChange(current_lod_id);
	}
}
