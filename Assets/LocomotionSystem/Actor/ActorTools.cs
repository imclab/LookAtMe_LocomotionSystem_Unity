using UnityEngine;
using System.Collections;
using Constants;

//using LipSynch;

public class ActorTools : MonoBehaviour
{
	//protected SpeakController 		_SpeakController = null; 
	//protected GotoController 		_GotoController = null;
	//protected RotationController	_RotationController = null;
	//protected LookAtController 		_LookAtController = null;
	//protected Locomotion 			_Locomotion =  null;
	//protected EmotionController		_EmotionController = null;
	//protected MotionsController 	_MotionsController = null;
	protected bool				_bInitialized =  false;
	public bool					_DebugMode =  false;
	public int					_CurrentLOD;
	[HideInInspector] public bool _OptimizeShadows = true;
	[HideInInspector] public int _OptimizeShadowLOD = 2;
	
	protected LegAnimator 	_LegAnimator = null;
	[HideInInspector] public PlatformCharacterController 	_PlatformCharacterController = null;
	public bool _UseCharacterController;
	bool _bPlatformCharacterControllerInit = false;

	[HideInInspector] public AlignmentTracker	_AlignmentTracker = null;
	[HideInInspector] public LookAtController	_LookAtController = null;
	[HideInInspector] public NormalCharacterMotor	_NormalCharacterMotor = null;
	[HideInInspector] public FollowTrajectory _FollowTrajectory = null;
	[HideInInspector] public LODController _LODController = null;
	
	[HideInInspector] public bool _AutoInPlace = true;
	[HideInInspector] public bool _AutoMoving = true;
	[HideInInspector] public FootIKComputingType _FootIKComputingType = FootIKComputingType.Enabled;

	public bool _UseNormalCharacterMotor;
	bool _bUseNormalCharacterMotorInit = false;
	bool _bInitAlignmentTracker = false;
	bool _bInitLookAtController = false;
	bool _bInitLegAnimator = false;
	bool _bInitCharacterController = false;
	bool _bInitCharacterMotor = false;
	bool _bInitLODController = false;

	bool _UseFootIk = true;
	[HideInInspector] public bool _UseAlignmentTracker = true;
	[HideInInspector] public bool _FixeUpdateForAlignmentTracker = false;
	ActorInterface _MyInterface = null;
	
	// Use this for initialization
	public void Start()
	{
		if(_bInitialized)
			return;

		_LODController.Init(_MyInterface, GetComponent<LODGroup>());

		_bInitialized =  true;
	}

	void InitLODController()
	{
		if(!_bInitLODController)
		{
			_LODController = GetComponent<LODController>();
			if(_LODController == null)
				return;
			_bInitLODController = true;
			_LODController.hideFlags = HideFlags.HideInInspector;
			_LODController._OptimizeShadow = _OptimizeShadows;
		}
	}
	void InitAlignmentTracker()
	{
		if(!_bInitAlignmentTracker)
		{
			//this.gameObject.AddComponent("AlignmentTracker");
			_AlignmentTracker = GetComponent<AlignmentTracker>();
			if(_AlignmentTracker == null)
				return;
			
			_bInitAlignmentTracker = true;
			_AlignmentTracker.hideFlags = HideFlags.HideInInspector;
		}
	}
	void InitLookAtController()
	{
		if(!_bInitLookAtController)
		{
			//this.gameObject.AddComponent("LookAtController");
			_LookAtController = GetComponent<LookAtController>();
			if(_LookAtController == null)
				return;
			
			_bInitLookAtController = true;
			_LookAtController.hideFlags = HideFlags.HideInInspector;
			//_LookAtController.InitSegments();
		}
	}
	void InitLegAnimator()
	{
		if(!_bInitLegAnimator)
		{
			//this.gameObject.AddComponent("LegAnimator");
			_LegAnimator = GetComponent<LegAnimator>();
			if(_LegAnimator == null)
				return;
			
			_bInitLegAnimator = true;
			_LegAnimator.hideFlags = HideFlags.HideInInspector;
		}
	}
	void InitCharacterController()
	{
		if(_UseCharacterController && !_bInitCharacterController)
		{
			//this.gameObject.AddComponent("PlatformCharacterController");
			_PlatformCharacterController = GetComponent<PlatformCharacterController>();
			if(_PlatformCharacterController == null)
				return;
			
			_bInitCharacterController = true;
			_PlatformCharacterController.hideFlags = HideFlags.HideInInspector;
			_PlatformCharacterController.OnEnable();
			_bPlatformCharacterControllerInit = true;
		}
	}
	void InitCharacterMotor()
	{
		if(_UseNormalCharacterMotor && !_bInitCharacterMotor)
		{
			//this.gameObject.AddComponent("NormalCharacterMotor");
			_NormalCharacterMotor = GetComponent<NormalCharacterMotor>();
			if(_NormalCharacterMotor == null)
				return;
			_bInitCharacterMotor = true;
			_NormalCharacterMotor.hideFlags = HideFlags.HideInInspector;
			_NormalCharacterMotor.OnEnable();
			_bUseNormalCharacterMotorInit = true;
			/*_NormalCharacterMotor.maxForwardSpeed = 5.024946f;
			_NormalCharacterMotor.maxBackwardsSpeed = 3.280198f;
			_NormalCharacterMotor.maxSidewaysSpeed  = 3.280198f;
			_NormalCharacterMotor.maxVelocityChange = 0.2f;
			_NormalCharacterMotor.gravity = 10.0f;
			_NormalCharacterMotor.canJump = false;
			_NormalCharacterMotor.jumpHeight = 10f;
			_NormalCharacterMotor.maxRotationSpeed = 270.0f;*/
		}
	}
	// We have to deactivate the NavMeshAgent when using PlatformCharacterController.
	void OnEnable()
	{
		InitControllers();
	}
	public void InitControllers()
	{
		_MyInterface = new ActorInterface();
		_MyInterface.Init(this.gameObject, this);

		InitAlignmentTracker();
		InitCharacterController();
		InitCharacterMotor();
		InitLegAnimator();
		InitLookAtController();
		InitLODController();
	}

	public void AutoIK()
	{
		_FootIKComputingType = FootIKComputingType.Automatic;
	}
	public void ForceIK(bool force)
	{
		if(force)
			_FootIKComputingType = FootIKComputingType.Enabled;
		else
			_FootIKComputingType = FootIKComputingType.Disabled;
	}
	int nbIter = 0;
	private bool UpdateFootIkProperty()
	{
		// optim : si on est arrété alors on desactive l'ik si on est dans un LOD eloigné.
		// optim : si on ne bouge pas, on desactive la correction dans tous les cas
		bool UseIk = _UseFootIk;
		
		switch(_FootIKComputingType)
		{
		case FootIKComputingType.Disabled:
			UseIk = false;
			break;
			
		case FootIKComputingType.Enabled:
			UseIk = true;
			if(IsMoving())
			{
				_LegAnimator.CorrectionOn = true;
			}
			else
			{
				_LegAnimator.CorrectionOn = false;
			}
			break;
			
		case FootIKComputingType.Automatic:
			if(IsMoving())
			{
				UseIk = true;
				_LegAnimator.CorrectionOn = true;
				/*if(IsYMovingOn())
					UseIk = true;
				else
					UseIk = false;*/
			}
			else
			{
				_LegAnimator.CorrectionOn = false;
				//	_LegAnimator.DisableCorrection();
				/*if(LegsAreAtTheSameLevel())
				{
					if(nbIter<100)
					{
						++nbIter;
						UseIk = true;
					}
					else
						UseIk = false;
					
				}
				else*/
				{
					if(_LODController.GetCurrentLOD()<=2)
						UseIk = true;
					else
						UseIk = false;
				}
			}
			break;
			
		default:
			UseIk = true;
			break;
		}
		return UseIk;
	}
	// Update is called once per frame
	public void Update () 
	{
		if(!_bInitialized)
			return;
		
		if(_UseCharacterController && _bPlatformCharacterControllerInit)
			_PlatformCharacterController.Update();

		if(_UseNormalCharacterMotor && _bUseNormalCharacterMotorInit)
			_NormalCharacterMotor.Update();

		if(_FollowTrajectory == null)
		{
			_FollowTrajectory = GetComponent<FollowTrajectory>();
			if(_FollowTrajectory)
			{
				_FollowTrajectory.RegisterInterface(_MyInterface);
			}
		}

		_UseFootIk = UpdateFootIkProperty();
		_LegAnimator.useIK = _UseFootIk;
		_LODController._OptimizeShadow = _OptimizeShadows;
		_LODController._OptimizeShadowLOD = _OptimizeShadowLOD;
		//_SpeakController.Update();
		//_GotoController.Update();
		//_RotationController.Update();
		//_LookAtController.Update();
		//_EmotionController.Update();
		//_MotionsController.Update();
	}
	public bool LegsAreAtTheSameLevel()
	{
		float val;
		Vector3 Left = _LegAnimator.GetLeftAnklePosition();
		Vector3 Right = _LegAnimator.GetRightAnklePosition();
		val = Mathf.Abs(Left.y - Right.y);
		return val<0.15f;
	}
	public float val;
	public bool IsYMovingOn()
	{
		val = Mathf.Abs(_LegAnimator.CurrentVelocity.y);
		return val>0.00002f;
	}
	public bool IsMoving()
	{
		return _AlignmentTracker.IsMoving();
	}
	public void UpdateDebugMode()
	{
		HideFlags NewFlag;
		if(!_DebugMode)
			NewFlag = HideFlags.HideInInspector;
		else
			NewFlag = HideFlags.None;

		if(_AlignmentTracker)
			_AlignmentTracker.hideFlags = NewFlag;
		
		if(_LookAtController)
			_LookAtController.hideFlags = NewFlag;
		
		if(_LegAnimator)
			_LegAnimator.hideFlags = NewFlag;
		
		if(_PlatformCharacterController)
			_PlatformCharacterController.hideFlags = NewFlag;
		
		if(_LODController)
			_LODController.hideFlags = NewFlag;
		
		if(_NormalCharacterMotor)
			_NormalCharacterMotor.hideFlags = NewFlag;
	}
	public bool IsInitialized()
	{
		return _bInitialized;
	}
	// So if NavMeshAgent was enabled, we reactivate it.
	void OnDisable()
	{
		if(_UseCharacterController && _bPlatformCharacterControllerInit)
			_PlatformCharacterController.OnDisable();
	}
	public void OnLODChange(int id)
	{
		_CurrentLOD = id;
		_LODController.SetCurrentLOD(id);
		//Debug.Log("CurrentLOD is : "+id);
		/*bool HasChanged = _LODController.OnLODChange(id);
		if(HasChanged)
		{
			int iCurrentLOD = _LODController.GetCurrentLOD();
			nbIter = false;
			Debug.Log("CurrentLOD is : "+iCurrentLOD);
		}*/
	}
	
	//public Locomotion GetLocomotion()
	//{
	//	return _Locomotion;
	//}
	//public SpeakController GetSpeakController()
	//{
	//	//return _SpeakController;
	//}
	//public GotoController GetGotoController()
	//{
	//	return _GotoController;
	//}
	//public RotationController GetRotationController()
	//{
	//	return _RotationController;
	//}
	//public LookAtController GetLookAtController()
	//{
	//	return _LookAtController;
	//}
	//public EmotionController GetEmotionController()
	//{
	//	return _EmotionController;
	//}
	//public MotionsController GetMotionsController()
	//{
	//	return _MotionsController;
	//}
}
