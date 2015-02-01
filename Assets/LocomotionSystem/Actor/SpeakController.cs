/*using UnityEngine;
using System.Collections;
using LipSynch;

public class SpeakController 
{
	LSController 	_LipSynchController = null;
	GameObject 		_Actor;
	int SPEAK_CONTROLLER_NONE = 0;
	int SPEAK_CONTROLLER_SPEAKING = 1;
	int SPEAK_CONTROLLER_DONE = 2;
	int _iCurrentState;
	int _iSoundId;
	
	public SpeakController(GameObject go)
	{
		_Actor = go;
		_iCurrentState = SPEAK_CONTROLLER_NONE;
		
		_LipSynchController = new LSController(_Actor);
		
    	string[] lines = System.IO.File.ReadAllLines(@"ConfigLipSynch.cfg");

		LSSound._dPhonemeTime = float.Parse (lines[0]);
		LSSound._dPhonemeSpaceTime = float.Parse (lines[1]);
		LSSound._dPhonemePonctuationTime = float.Parse (lines[2]);
		LSSound._dPhonemeMaxWeight = float.Parse (lines[3]);
		LSSound._dDeltaOpenEnded = float.Parse (lines[4]);
		LSSound._dPhonemeOpenSpeed = float.Parse (lines[5]);
	}
	public void Update()
	{
		if(CheckState(SPEAK_CONTROLLER_SPEAKING))
		{
			_LipSynchController.Update();
			if(_LipSynchController.IsDone())
			{
				if(_iSoundId >=0)
				{
					if(SceneTools.GetSoundManager().IsDone(_iSoundId))
					{
						_LipSynchController.Reset();
						SceneTools.GetSubtitlesController().Stop();
						_iCurrentState = SPEAK_CONTROLLER_DONE;
					}
					else
					{
						SayFake("this is a text to be sure that the entire lip sync will be synchronized.");
					}
				}
				else
				{
					_LipSynchController.Reset();
					SceneTools.GetSubtitlesController().Stop();
					_iCurrentState = SPEAK_CONTROLLER_DONE;
				}
			}
			else if(_iSoundId >=0)
			{
				if(SceneTools.GetSoundManager().IsDone(_iSoundId))
				{
					_LipSynchController.ForceStop();
				}
			}
		}
	}
	
	public void SayFake(string sentence)
	{
		_LipSynchController.ComputePhonemeSequence(sentence);
		//_LipSynchController.DebugLS();
		_LipSynchController.Launch();
		
		_iCurrentState = SPEAK_CONTROLLER_SPEAKING;
	}
	public void Say(string sentence, string audio_path)
	{
		_LipSynchController.ComputePhonemeSequence(sentence);
		_LipSynchController.DebugLS();
		_LipSynchController.Launch();
		
		if(audio_path != "")
		{
			_iSoundId = SceneTools.GetSoundManager().Load(audio_path);
			SceneTools.GetSoundManager().Play(audio_path, false, 1.0f);
		}
		else
		{
			_iSoundId = -1;
		}
		
		SceneTools.GetSubtitlesController().Launch(sentence, _Actor);
		
		_iCurrentState = SPEAK_CONTROLLER_SPEAKING;
	}
	public LSController GetLSController()
	{
		return _LipSynchController;
	}
	public bool IsDone()
	{
		return CheckState(SPEAK_CONTROLLER_DONE);
	}
	public void Reset()
	{
		_iCurrentState = SPEAK_CONTROLLER_NONE;
	}
	
	protected bool CheckState(int val)
	{
		return (val == _iCurrentState);
	}

}*/
