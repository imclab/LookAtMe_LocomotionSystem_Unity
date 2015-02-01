using UnityEngine;
using System.Collections;

public class PlatformCharacterController : MonoBehaviour {

	private bool _NavMeshAgentWasEnabled;
	private CharacterMotor _Motor;
	public float walkMultiplier = 0.5f;
	public bool defaultIsWalk = false;

	// We have to deactivate the NavMeshAgent when using PlatformCharacterController.
	public void OnEnable()
	{
		// PlatformCharacterController needs CharacterMotor. 
		_Motor = GetComponent<CharacterMotor>();
		if (_Motor == null)
		{
			Debug.LogError("Motor is null!!");
		}
		_Motor.enabled = true;

		NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
		if (null != navMeshAgent)
		{
			_NavMeshAgentWasEnabled = navMeshAgent.enabled;
			navMeshAgent.enabled = false;
		}
		else
		{
			_NavMeshAgentWasEnabled = false;
		}

		if (_NavMeshAgentWasEnabled)
		{
			Debug.LogWarning("PlatformCharacterController has deactivated NavMeshAgent");
		}
	}

	// So if NavMeshAgent was enabled, we reactivate it.
	public void OnDisable()
	{
		if (_NavMeshAgentWasEnabled)
		{
			_Motor.enabled = false; // NavMeshAgent don't need CharacterMotor.
			Debug.LogWarning("PlatformCharacterController has deactivated CharacterMotor");
			GetComponent<NavMeshAgent>().enabled = true;
			Debug.LogWarning("PlatformCharacterController has reactivated NavMeshAgent");
		}
	}
	
	// Update is called once per frame
	public void Update () {
		// Get input vector from kayboard or analog stick and make it length 1 at most
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if (directionVector.magnitude>1) directionVector = directionVector.normalized;
		directionVector = directionVector.normalized * Mathf.Pow(directionVector.magnitude, 2);
		
		// Rotate input vector into camera space so up is camera's up and right is camera's right
		directionVector = Camera.main.transform.rotation * directionVector;
		
		// Rotate input vector to be perpendicular to character's up vector
		Quaternion camToCharacterSpace = Quaternion.FromToRotation(Camera.main.transform.forward*-1, transform.up);
		directionVector = (camToCharacterSpace * directionVector);
		
		// Make input vector relative to Character's own orientation
		directionVector = Quaternion.Inverse(transform.rotation) * directionVector;
		
		if (walkMultiplier!=1) {
			if ( (Input.GetKey("left shift") || Input.GetKey("right shift") /*|| Input.GetButton("Sneak")*/) != defaultIsWalk ) {
				directionVector *= walkMultiplier;
			}
		}
		
		// Apply direction
		_Motor.desiredMovementDirection = directionVector;
	}
}
