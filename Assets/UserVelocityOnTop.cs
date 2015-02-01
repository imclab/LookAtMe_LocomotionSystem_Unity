using UnityEngine;
using System.Collections;

public class UserVelocityOnTop : User {
	
	public override void updatePositionOrientation(float agentMaxSpeed) {
		
		/* on x and y axis, 2D version */
		Vector2 vel = Vector2.zero;

        vel.x = getHorizontalAxis();
        vel.y = getVerticalAxis();

		if (vel.sqrMagnitude > 1f) {
			vel = vel.normalized * agentMaxSpeed;
		} else {
			vel *= agentMaxSpeed;
		}
		
		/* new position computation */
		Vector2 newPosition = getPosition2D() + vel * MyTime.deltaTime();
		
		/* position update */
		setPosition2D(newPosition);
		
		/* orientation computation */
		float newOrientation = 0f;
		
		if (velocity.sqrMagnitude > 0f)
			newOrientation = (float) Mathf.Atan2(-vel.y, vel.x);

		/* orientation update */
		if (!double.IsNaN(newOrientation))
			setOrientation((float) Mathf.Rad2Deg*(newOrientation));

		velocity = vel; /* TODO Check how it affects RVO computations */
	}
}
