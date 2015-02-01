using UnityEngine;

public class ObjectInterface
{
	protected GameObject _MyObj;
	protected MeshRenderer _MeshRenderer = null;
	public void Init(GameObject obj)
	{
		_MyObj = obj;
		_MeshRenderer = _MyObj.GetComponent<MeshRenderer>();
	}
	public virtual void Show()
	{
		if(_MeshRenderer)
		{
			_MeshRenderer.enabled = true;
		}
	}
	public virtual void Hide()
	{
		if(_MeshRenderer)
		{
			_MeshRenderer.enabled = false;
		}
	}
	public virtual void OnLODChange(int current_lod_id)
	{
		// ras
	}
}
