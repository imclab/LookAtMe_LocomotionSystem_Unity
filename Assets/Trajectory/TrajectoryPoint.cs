using UnityEngine;
using System.Collections;

/***
 * This class is used to describe one point in a trajectory.
 * This trajectory must be in the common plane, described with (X, Y) axes.
 ***/

public class TrajectoryPoint {

	private Vector2 pos;
	private float t;

	public Vector2 Pos {
		get { return pos; }
	}

	public float T {
		get { return t; }
	}

	public TrajectoryPoint(Vector2 _pos, float _t) {
		pos = _pos;
		t = _t;
	}

	public TrajectoryPoint(float _x, float _y, float _t) {
		pos = new Vector2(_x, _y);
		t = _t;
	}

	public static float distance(TrajectoryPoint a, TrajectoryPoint b) {
		return Vector2.Distance(a.Pos, b.Pos);
	}

	public static float time(TrajectoryPoint a, TrajectoryPoint b) {
		return b.T - a.T;
	}

	public override string ToString () {
		return t + ";" + pos.x + ";" + pos.y;
	}
}
