/*using UnityEngine;
using System.Collections;

public class SceneTools : MonoBehaviour 
{
	static protected SoundManager _SoundManager = null;
	static protected SubtitlesController _SubtitlesController = null;
	bool _bInitialized = false;
	
	// Use this for initialization
	void Start () 
	{
		_bInitialized = false;
		Init();
	}
	void Init()
	{
		if(_bInitialized)
			return;
		
		_SoundManager = new SoundManager();
		_SubtitlesController = new SubtitlesController();
		_bInitialized = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Init();

		_SoundManager.Update();
		_SubtitlesController.Update();
	}
	static public SoundManager GetSoundManager()
	{
		return _SoundManager;
	}
	static public SubtitlesController GetSubtitlesController()
	{
		return _SubtitlesController;
	}
}
*/