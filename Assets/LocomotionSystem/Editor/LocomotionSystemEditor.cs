using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Constants;

[CustomEditor(typeof(FollowTrajectory))]
class LocomotionSystemEditor : Editor 
{	
	public FollowTrajectory _target;
	private SerializedObject LocoSysInspector;

	private SerializedProperty _TrajectroryFilenameProperty;
	private SerializedProperty _SamplingInputProperty;
	private SerializedProperty _SamplingFactorProperty;
	private SerializedProperty _MergeClosedPointsProperty;
	private SerializedProperty _ClosedPointsMinimumDistanceProperty;
	private SerializedProperty _AnimateYAxesProperty;
	private SerializedProperty _DefaultAngularSpeedProperty;
	private SerializedProperty _TargetToWatchForTheCameraProperty;
	private SerializedProperty _AnimateYAxeProperty;
	private SerializedProperty _AvatarDeltaYCorrectionProperty;
	private SerializedProperty _DisplayLineRendererProperty;
	private SerializedProperty _NbPointsOnTheTrajectoryProperty;
	private SerializedProperty _SyncLaunchWithTrajectoryProperty;

	private FilteringMethod _SelectedFilter;

	private bool _SelectOrientation;
	private bool _ShowInputFilters = true;
	private bool _ShowOnlineFilters = true;
	private bool _ShowRenderingParameters = true;
	private int _firstTabSize = 7;
	private int _secondTabSize = 14;
	private int _thirdTabSize = 21;

	void OnEnable () 
	{
		_target = (FollowTrajectory) target;
		LocoSysInspector = new SerializedObject(target);

		_TrajectroryFilenameProperty = LocoSysInspector.FindProperty("_TrajectroryFilename");
		_SyncLaunchWithTrajectoryProperty = LocoSysInspector.FindProperty("_SyncLaunchWithTrajectory");

		_SamplingInputProperty = LocoSysInspector.FindProperty("_SamplingInput");
		_SamplingFactorProperty = LocoSysInspector.FindProperty("_SamplingFactor");
		_AnimateYAxesProperty = LocoSysInspector.FindProperty("_AnimateYAxes");
		_DefaultAngularSpeedProperty = LocoSysInspector.FindProperty("_DefaultAngularSpeed");
		_TargetToWatchForTheCameraProperty = LocoSysInspector.FindProperty("_TargetToWatchForTheCamera");
		_AnimateYAxeProperty = LocoSysInspector.FindProperty("_AnimateYAxe");
		_AvatarDeltaYCorrectionProperty = LocoSysInspector.FindProperty("_AvatarDeltaYCorrection");
		_DisplayLineRendererProperty = LocoSysInspector.FindProperty("_DisplayLineRenderer");
		_MergeClosedPointsProperty = LocoSysInspector.FindProperty("_MergeClosedPoints");
		_ClosedPointsMinimumDistanceProperty = LocoSysInspector.FindProperty("_ClosedPointsMinimumDistance");
		_NbPointsOnTheTrajectoryProperty = LocoSysInspector.FindProperty("_NbPointsOnTheTrajectory");

		_SelectedFilter = _target._FilterType;
	}
	
	public override void OnInspectorGUI () 
	{
		// Draw the default inspector
		DrawDefaultInspector();
		LocoSysInspector.Update(); // TODO: ???
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		//// Trajectory parameters
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(_TrajectroryFilenameProperty, GUILayout.Width(350));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(_SyncLaunchWithTrajectoryProperty, GUILayout.Width(350));
		EditorGUILayout.EndHorizontal();
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		//// Preprocessing filters
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("", GUILayout.Width(_firstTabSize));
		_ShowInputFilters = EditorGUILayout.Foldout(_ShowInputFilters, "Preprocessing filters");
		if(_ShowInputFilters)
		{
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			EditorGUILayout.PropertyField(_SamplingInputProperty, false);
			EditorGUILayout.EndHorizontal();

			if(_target._SamplingInput)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				EditorGUILayout.PropertyField(_SamplingFactorProperty, GUILayout.Width(350));
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			EditorGUILayout.PropertyField(_MergeClosedPointsProperty, false);
			EditorGUILayout.EndHorizontal();
			
			if(_target._MergeClosedPoints)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				EditorGUILayout.PropertyField(_ClosedPointsMinimumDistanceProperty, GUILayout.Width(350));
				EditorGUILayout.EndHorizontal();
			}
		}
		else
		{
			EditorGUILayout.EndHorizontal();
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		//// Online filters
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("", GUILayout.Width(_firstTabSize));
		_ShowOnlineFilters = EditorGUILayout.Foldout(_ShowOnlineFilters, "Online filters");
		if(_ShowOnlineFilters)
		{
			EditorGUILayout.EndHorizontal();
			
			/*EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			GUILayout.Label(" Filtering options ");
			EditorGUILayout.EndHorizontal();*/
			
			// filter selection
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			_SelectedFilter = (FilteringMethod) EditorGUILayout.EnumPopup("Filter type", _SelectedFilter);
			EditorGUILayout.EndHorizontal();
			
			_target._FilterType = _SelectedFilter;
			
			switch(_SelectedFilter)
			{
			case FilteringMethod.None:
				_target.SetApplyFilter(false);
				break;
				
			case FilteringMethod.Basic:
				_target.SetApplyFilter(true);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				_target._DeltaTimeForSmoothing = EditorGUILayout.FloatField("DeltaTimeForSmoothing", _target._DeltaTimeForSmoothing);
				EditorGUILayout.EndHorizontal();
				break;
				
			case FilteringMethod.LinearRegression:
				_target.SetApplyFilter(true);
				break;
			}
			_target.InitFilter();
		}
		else
		{
			EditorGUILayout.EndHorizontal();
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		//// Rendering parameters
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("", GUILayout.Width(_firstTabSize));
		_ShowRenderingParameters = EditorGUILayout.Foldout(_ShowRenderingParameters, "Rendering parameters");
		if(_ShowRenderingParameters)
		{
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			EditorGUILayout.PropertyField(_DefaultAngularSpeedProperty, GUILayout.Width(350));
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			EditorGUILayout.PropertyField(_AnimateYAxeProperty, GUILayout.Width(350));
			EditorGUILayout.EndHorizontal();
			
			Camera tto = _target.gameObject.camera; // test si camera
			if(tto)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				EditorGUILayout.PropertyField(_TargetToWatchForTheCameraProperty, GUILayout.MinWidth(150),GUILayout.MaxWidth(2000), GUILayout.ExpandWidth(false));
				EditorGUILayout.EndHorizontal();
			}
			//Camera tto = _target.gameObject.camera; // test si camera
			if(_target.gameObject.tag == "Player")
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				EditorGUILayout.PropertyField(_AvatarDeltaYCorrectionProperty, GUILayout.Width(350));
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				_AvatarDeltaYCorrectionProperty.floatValue = 0.0f;
			}
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			EditorGUILayout.PropertyField(_NbPointsOnTheTrajectoryProperty, GUILayout.Width(350));
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			EditorGUILayout.PropertyField(_DisplayLineRendererProperty, GUILayout.Width(350));
			EditorGUILayout.EndHorizontal();

			_target.SetDisplayLineRendererProperty(_DisplayLineRendererProperty.boolValue);
			_target.ChangeAngularSpeed(_DefaultAngularSpeedProperty.floatValue);
		}
		else
		{
			EditorGUILayout.EndHorizontal();
		}

		if(GUI.changed)
		{
			Debug.Log ("GUI.changed");
			EditorUtility.SetDirty(_target);
		}

		LocoSysInspector.ApplyModifiedProperties();
	}
}
