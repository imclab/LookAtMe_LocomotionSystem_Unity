/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LipSynch;

public class SubtitlesController 
{
	int 	SUB_STATE_NONE = 0;
	int 	SUB_STATE_SHOWING = 1;
	int 	SUB_STATE_DONE = 2;
	
	GameObject 		_ActorSpeaking = null;
	int 			_iState;
	string			_sCurrentSentence;
	string			_sLastSentence;
	bool			_bShowSubtitles;
	LSController 	_LSController = null;
	SpeakController	_SpeakController = null;
	int 			_iFrameWidth;
	int			 	_iCharactersPerLine;
	
	protected GUIText _Subtitleline0 = null;
	protected GUIText _Subtitleline1 = null;
	protected GUIText _Subtitleline2 = null;

	public SubtitlesController()
	{
		_iState = SUB_STATE_NONE;
		_iFrameWidth = Screen.width; // we can put 35 characters for a width of 336
		_iCharactersPerLine = 35*_iFrameWidth/336;
		
		// SUBTITILES
		_Subtitleline0 = GameObject.Find("SubtitlesLine0").GetComponent<GUIText>() as GUIText;
		_Subtitleline1 = GameObject.Find("SubtitlesLine1").GetComponent<GUIText>() as GUIText;
		_Subtitleline2 = GameObject.Find("SubtitlesLine2").GetComponent<GUIText>() as GUIText;
		_Subtitleline0.enabled = true;
		_Subtitleline1.enabled = true;
		_Subtitleline2.enabled = true;
	}
	public void Launch(string sentence, GameObject actor)
	{
		if( CheckState(SUB_STATE_SHOWING) )
		{
			return;
		}
		_ActorSpeaking = actor;
		_SpeakController = _ActorSpeaking.GetComponent<ActorTools>().GetSpeakController();
		_LSController = _SpeakController.GetLSController();
		
		ShowSubtitles( sentence, true);
		
		_iState = SUB_STATE_SHOWING;
	}
	
	// Update is called once per frame
	public void Update() 
	{
		// RAS
	}
	public void Stop()
	{
		if( !CheckState(SUB_STATE_SHOWING) )
		{
			return;
		}
		
		Hide();
		_iState = SUB_STATE_DONE;
	}
	public bool IsDone()
	{
		return CheckState(SUB_STATE_DONE);
	}
	protected bool CheckState(int val)
	{
		return (val == _iState);
	}
	
	public void HideSubtitles()
	{
		Launch("", null);
	}
	public void AddSubtitles( string sentence) 
	{
		_sCurrentSentence += " ";
		_sCurrentSentence += sentence;
		Launch(_sCurrentSentence, _ActorSpeaking);
	}
	public void UpdateAutoSubtitles()
	{
		if( (_sCurrentSentence == _sLastSentence) || _sLastSentence != "")
			return;

		string sCurrentSentence = _sLastSentence;
		string sCurrentWord = _LSController.GetCurrentWord();
		if( (sCurrentWord=="") || (sCurrentWord==" ") )
			return;

		int iPos = sCurrentSentence.IndexOf(sCurrentWord);
		int TailleUneLigne = _iFrameWidth / _iCharactersPerLine;
		if( (0<iPos) && (iPos<TailleUneLigne) )
		{
			// RAS
		}
		else if( (TailleUneLigne<iPos) && (iPos<2*TailleUneLigne) )
		{
			// RAS
		}
		else if( (2*TailleUneLigne<iPos) && ((int)(sCurrentSentence.Length)>3*TailleUneLigne) ) // si on est en train de lire au dela de la premiere ligne
		{
			while(2*TailleUneLigne<iPos)
			{
				sCurrentSentence = sCurrentSentence.Substring(TailleUneLigne, sCurrentSentence.Length-TailleUneLigne);
				iPos = sCurrentSentence.IndexOf(sCurrentWord);
			}
		}
		ShowSubtitles(sCurrentSentence, true);
		_sLastSentence = sCurrentSentence;
	}
	protected void Show(string l0, string l1, string l2)
	{
		_Subtitleline0.text = l0;
		_Subtitleline1.text = l1;
		_Subtitleline2.text = l2;
		
		//_Subtitleline0.enabled = true;
		//_Subtitleline1.enabled = true;
		//_Subtitleline2.enabled = true;
	}
	protected void Hide()
	{
		_Subtitleline0.text = "";
		_Subtitleline1.text = "";
		_Subtitleline2.text = "";
		
		//_Subtitleline0.enabled = false;
		//_Subtitleline1.enabled = false;
		//_Subtitleline2.enabled = false;
	}
	public void ShowSubtitles( string sentence, bool show_name) 
	{
		string Subtitle;
		if(show_name)
		{
			Subtitle = _ActorSpeaking.name;
			Subtitle += " : ";
			Subtitle += sentence;
		}
		else
			Subtitle = sentence;

		// first line
		if(Subtitle == "")
		{
			Hide();
		}
		else
		{
			List<string> lines = LSTools.SeperateInLines(Subtitle, _iCharactersPerLine);

			if( (lines.Count>2) && (lines[1] ==  "") && (lines[2] ==  ""))
			{
				Show(lines[2], lines[1], lines[0]);
			}
			else if ( (lines.Count>2) && (lines[2] ==  "") )
			{
				Show(lines[2], lines[0], lines[1]);
			}
			else
			{
				if (lines.Count>2)
				{
					Show(lines[0], lines[1], lines[2]);
				}
				else if (lines.Count>1)
				{
					Show(lines[0], lines[1], "");
				}
				else
				{
					Show(lines[0], "", "");
				}
			}
			
		}
	}
}
*/