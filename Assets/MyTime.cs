using System;
using UnityEngine;

public static class MyTime {

	private static float beginning = 0f;
	public static bool MIDDLE_VR;
	
	public static void reset() {
		beginning = fixedTime();
	}
	
	public static float fixedTime() {
		#if UNITY_STANDALONE_WIN
		if (MIDDLE_VR && MiddleVR.VRKernel != null)
			return (float) MiddleVR.VRKernel.GetTimer().seconds() - beginning;
		#endif

		return Time.fixedTime - beginning; // Time.unscaledTime
	}

	public static float deltaTime() {
		#if UNITY_STANDALONE_WIN
		if (MIDDLE_VR && MiddleVR.VRKernel != null)
			return (float) MiddleVR.VRKernel.GetDeltaTime();
		#endif

		return Time.deltaTime; // Time.unscaledDeltaTime;
	}
}