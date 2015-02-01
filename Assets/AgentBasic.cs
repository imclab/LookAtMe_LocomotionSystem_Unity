using UnityEngine;
using System.Collections;
using System;

public abstract class AgentBasic : MonoBehaviour {
	
	public Vector2 getPosition2D() {
		return new Vector2(transform.position.x, transform.position.z);
	}
	
	public void setPosition2D(Vector2 v) {
		setPosition2D(v.x, v.y);
	}
	
	public void setPosition2D(float x, float y) {
		if (double.IsNaN(x) || double.IsNaN(y))
			throw new ArgumentException("NaN assign attempt for the new position.");

		if (double.IsInfinity(x) || double.IsInfinity(y))
			throw new ArgumentException("Infinity assign attempt for the new position.");
		
		Vector3 newPosition = new Vector3(x, transform.position.y, y);
		transform.position = newPosition;
	}
	
	public float getOrientation() {
		return transform.eulerAngles.y;
	}
	
	public void setOrientation(float angleInDegrees) {
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,
		                                    angleInDegrees,
		                                    transform.eulerAngles.z);
	}
}

