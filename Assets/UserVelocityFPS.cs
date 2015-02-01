using UnityEngine;
using System.Collections;

/* Velocity Control Law (classic FPS-style camera)
 * Using Y-axis on Joystick to change velocity towards orientation, and rotation on the Joystick to change rotation
 * This script takes care of the user's current orientation
 * FPS-Style */

public class UserVelocityFPS : User {
			
	public override void updatePositionOrientation(float agentMaxSpeed) {
		
		/* input reading: velocity and orientation */
		float velY = getVerticalAxis();
		velY *= agentMaxSpeed;
		
		float rot = getHorizontalAxis();
		setOrientation(getOrientation() + rot);
		
		/* rotation of the input to be suited for our agent */
		Vector2 vel2 = Vector2.up;
		float tmp = (float) Mathf.Deg2Rad*(getOrientation());
		
		vel2.x = velY*Mathf.Sin(tmp);
		vel2.y = velY*Mathf.Cos(tmp);
		
		/* applying new position */
		setPosition2D(getPosition2D() + vel2 * MyTime.deltaTime());
		
		velocity = vel2;
	}
}
