using UnityEngine;
using System.Collections;
using System;

public class AgentManager : AgentBasic {

	public User user;
	private Vector2 velocity = Vector2.zero;
	private bool INIT = false;
	private TrajectoryPoint prev = null;

	public void Update() {
		if (INIT) {
			TrajectoryPoint tmp = new TrajectoryPoint(getPosition2D()+(velocity*MyTime.deltaTime()), MyTime.fixedTime());
			agentUpdatePositionOrientation(tmp);
		} else if (MyTime.fixedTime() > 1f) {
			float dy = getPosition2D().y - user.getPosition2D().y, vuser = user.AGENT_MAX_SPEED;
			float dt = dy/vuser;
			float dx = user.getPosition2D().x - getPosition2D().x;
			float v = dx/dt;
			velocity = Vector2.right * v;
			prev = new TrajectoryPoint(getPosition2D(), MyTime.fixedTime());
			INIT = true;
		}
	}

	private void agentUpdatePositionOrientation(TrajectoryPoint step) {
		if (step == null)
			throw new ArgumentException("Null step.");

		/* direction computation */
		Vector2 direction = step.Pos - prev.Pos;

		/* proportionnal computation */
		Vector2 newPosition = prev.Pos;
		newPosition += (direction) * (MyTime.fixedTime() - prev.T) / (step.T - prev.T);
		setPosition2D(newPosition); /* normally no NaN exception thrown */
		
		/* orientation update (TODO angular speed limit) */
		float newOrientation = 0f;
		if (direction.sqrMagnitude > 0f)
			newOrientation = (float) Mathf.Atan2(-direction.y, direction.x);
		if (!double.IsNaN(newOrientation))
			setOrientation((float) Mathf.Rad2Deg*newOrientation + 90f); /* +90f: for humanoids? */
	}
}
