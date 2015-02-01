using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Constants;

[CustomEditor(typeof(ActorTools))]
class ActorToolsEditor : Editor 
{	
	public ActorTools _target;
	private SerializedObject _ActorToolsInspector;
	private SerializedProperty _UseAlignmentTrackerProperty;
	private SerializedProperty _OptimizeShadowProperty;
	private SerializedProperty _OptimizeShadowLODProperty;
	private SerializedProperty _FixeUpdateForAlignmentTrackerProperty;
	private SerializedProperty _AutoMovingProperty;
	private SerializedProperty _AutoInplaceProperty;
	private bool _ShowIKType = true;
	private FootIKComputingType _IKType;
	private int _rootTabSize = 7;
	private int _firstTabSize = 7;
	private int _secondTabSize = 14;
	private int _thirdTabSize = 21;

	void OnEnable () 
	{
		_target = (ActorTools) target;
		_IKType = _target._FootIKComputingType;
		_ActorToolsInspector = new SerializedObject(target);

		_UseAlignmentTrackerProperty = _ActorToolsInspector.FindProperty("_UseAlignmentTracker");
		_OptimizeShadowProperty = _ActorToolsInspector.FindProperty("_OptimizeShadows");
		_AutoMovingProperty = _ActorToolsInspector.FindProperty("_AutoMoving");
		_AutoInplaceProperty = _ActorToolsInspector.FindProperty("_AutoInPlace");
		_FixeUpdateForAlignmentTrackerProperty = _ActorToolsInspector.FindProperty("_FixeUpdateForAlignmentTracker");
		_OptimizeShadowLODProperty = _ActorToolsInspector.FindProperty("_OptimizeShadowLOD");
		_target.InitControllers();
	}
	
	public override void OnInspectorGUI () 
	{
		// Draw the default inspector
		DrawDefaultInspector();
		_ActorToolsInspector.Update(); // TODO: ???
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		//// Optimize Shadow
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(_OptimizeShadowProperty, GUILayout.Width(350));
		EditorGUILayout.EndHorizontal();
		if(_target._OptimizeShadows)
		{			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_firstTabSize));
			EditorGUILayout.PropertyField(_OptimizeShadowLODProperty, GUILayout.Width(350));
			EditorGUILayout.EndHorizontal();

			_target._OptimizeShadowLOD = _OptimizeShadowLODProperty.intValue;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		//// UseAlignmentTracker
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(_UseAlignmentTrackerProperty, GUILayout.Width(350));
		EditorGUILayout.EndHorizontal();
		if(_target._UseAlignmentTracker)
		{
			if(_target._AlignmentTracker)
				_target._AlignmentTracker.enabled = true;
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_firstTabSize));
			EditorGUILayout.PropertyField(_FixeUpdateForAlignmentTrackerProperty, GUILayout.Width(350));
			EditorGUILayout.EndHorizontal();
			
			if(_target._AlignmentTracker)
				_target._AlignmentTracker.fixedUpdate = _FixeUpdateForAlignmentTrackerProperty.boolValue;
		}
		else
		{
			if(_target._AlignmentTracker)
				_target._AlignmentTracker.enabled = false;
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		//// Foot IK Type
		////////////////////////////////////////////////////////////////////////////////////////////////
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("", GUILayout.Width(_firstTabSize));
		_ShowIKType = EditorGUILayout.Foldout(_ShowIKType, "Foot IK Type");
		if(_ShowIKType)
		{
			EditorGUILayout.EndHorizontal();
			
			/*EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			GUILayout.Label(" Filtering options ");
			EditorGUILayout.EndHorizontal();*/
			
			// filter selection
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
			_IKType = (FootIKComputingType) EditorGUILayout.EnumPopup("IK type", _IKType);
			EditorGUILayout.EndHorizontal();
						
			switch(_IKType)
			{
			case FootIKComputingType.Disabled:
				_target.ForceIK(false);
				break;
				
			case FootIKComputingType.Enabled:
				_target.ForceIK(true);
				break;
				
			case FootIKComputingType.Automatic:
				_target.AutoIK();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				EditorGUILayout.PropertyField(_AutoInplaceProperty, GUILayout.Width(350));
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("", GUILayout.Width(_secondTabSize));
				EditorGUILayout.PropertyField(_AutoMovingProperty, GUILayout.Width(350));
				EditorGUILayout.EndHorizontal();
				break;
				
			default:
				break;
			}
		}
		else
		{
			EditorGUILayout.EndHorizontal();
		}

		if(GUI.changed)
		{
			//Debug.Log ("GUI.changed");
			EditorUtility.SetDirty(_target);
		}
		
		_target.UpdateDebugMode();

		_ActorToolsInspector.ApplyModifiedProperties();
	}
}
