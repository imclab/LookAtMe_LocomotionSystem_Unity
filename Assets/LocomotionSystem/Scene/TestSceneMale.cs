using UnityEngine;
using System.Collections;
//using LipSynch;

	
public class TestSceneMale : MonoBehaviour {
	
	//Agent _ZeAgent;
	GameObject circle = null;
	//GotoController _GotoController;
	//SpeakController _SpeakController;
	//EmotionController _EmotionController;
	//MotionsController _MotionsController;
	//TestActorListener Listen = null;
	bool _bInit = false;
	
	
	// Use this for initialization
	void Start () 
	{
		circle = GameObject.Find("FakeCircle");
		if(circle == null)
		{
			circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		}
		Init();
	}
	public void Init()
	{
		if(GetComponent<ActorTools>().IsInitialized() && !_bInit)
		{
			//_GotoController = GetComponent<ActorTools>().GetGotoController();
			//_SpeakController = GetComponent<ActorTools>().GetSpeakController();
			//_EmotionController = GetComponent<ActorTools>().GetEmotionController();
			//_MotionsController = GetComponent<ActorTools>().GetMotionsController();
			//Listen = new TestActorListener(_MotionsController);
			//_MotionsController.RegisterListener(Listen);
			_bInit = true;
		}
	}
	
	
	// Update is called once per frame
	bool bStop = false;
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.H)) 
		{
			Init();
			//_GotoController.Launch(new Vector3(-118.8f, 0f, -117f));
			//_MotionsController.PlayMotion("Dying", false);
			/*if(bStop)
			{
				_MotionsController.StopMotion("Dying");
			}
			else
			{
				_MotionsController.PlayMotion("FullDie", true);
			}*/
			bStop = !bStop;
			//_SpeakController.Say("I will take you anywhere. But what will you do? Run away? Confront him?", "");
		}
		if (Input.GetKeyUp(KeyCode.R)) 
		{
			Init();
			//_MotionsController.PlayMotion("m_dancing_sexy", false);
			//_EmotionController.PlayEmotion("SMILE_03");
			//_SpeakController.Say("I will take you anywhere. But what will you do? Run away? Confront him?", "");
			//_SpeakController.Say("I will take you anywhere. But what will you do? Run away? Confront him?", "Assets/Resources/voices/dissatisfied_5.mp3");
		}
		if (Input.GetKeyUp(KeyCode.KeypadPlus)) 
		{
			Init();
			//_EmotionController.PlayEmotion("ANGRY_04");
		}
		if (Input.GetKeyUp(KeyCode.KeypadMinus)) 
		{
			Init();
			//_EmotionController.PlayEmotion("SMILE_03");
		}
		if (Input.GetButtonDown ("Fire1")) 
		{
			Init();
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit))
			{	
				circle.transform.position = hit.point;
			//	_GotoController.Launch(hit.point);
			}
		}
		//if(_GotoController!=null && _GotoController.IsDone())
		//{
		//	_GotoController.Reset();
		//	Debug.Log("TestGoto1 is done");
		//}
	}
}
