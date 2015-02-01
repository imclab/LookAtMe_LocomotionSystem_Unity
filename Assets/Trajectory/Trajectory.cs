using System.Collections;
using System.Collections.Generic;

/***
 * This class describes a trajectory using a certain data-structure.
 ***/

public class Trajectory {

	protected List<TrajectoryPoint> points;
	protected int i;

	public Trajectory(string file) {
		TrajectoryParser parser = new TrajectoryParser(file);
		points = parser.parse();
		parser = null;
		i = 0;
	}

	public Trajectory(List<TrajectoryPoint> _points) {
		points = _points;
		i = 0;
	}

	public Trajectory(TrajectoryPoint tp) {
		points = new List<TrajectoryPoint>();
		points.Add(tp);
		i = 0;
	}

	public void reset() {
		i = 0;
	}

	public TrajectoryPoint beginning() {
		return points[0];
	}

	public float beginningTime() {
		return points[0].T;
	}

	public bool arrived() {
		return i >= points.Count;
	}

	public TrajectoryPoint arrival() {
		return points[points.Count-1];
	}

	public TrajectoryPoint next() {
		if (i == points.Count)
			return null;

		return points[i++];
	}

	/* same as next but does not move cursor in trajectory */
	public TrajectoryPoint peekNext() {
		if (i == points.Count)
			return null;

		TrajectoryPoint peek = points[i++];
		i--;
		return peek;
	}

	public List<TrajectoryPoint> next(int n) {
		if (i == points.Count)
			return null;

		int n_ = (int) System.Math.Min(n, points.Count-i);
		return points.GetRange(i++, n_);
	}

	public int count() {
		return points.Count;
	}

	public void addLast(TrajectoryPoint tmp) {
		points.Add(tmp);
	}

	public void addFirst(TrajectoryPoint tmp) {
		points[0] = tmp; /* FIXME too dirty */
	}

	public void addFirstCurrent(TrajectoryPoint tmp) {
		points.Insert(i, tmp); /* FIXME really? */
	}

	/*
	 * ToString method override
	 */

	public override string ToString () {
		string trString = "time;x;y";

		foreach (TrajectoryPoint point in points)
			trString += "\n" + point.ToString();

		return trString;
	}

	/*
	 * Treatment: change here the method to call.
	 * You can add your own methods below (private).
	 */

	public void treatment() {
		basic();
	}

	private void basic() {
		if (points.Count > 0) {
			float max_dist = 1f; // 0.1f;
			List<TrajectoryPoint> np = new List<TrajectoryPoint>();
			np.Add(points[0]);

			for (int i = 1; i < points.Count; ++i) {
				if (TrajectoryPoint.distance(np[np.Count-1], points[i]) > max_dist) {
					np.Add(points[i]);
				}
			}

			points = np;
		}
	}

}
