using UnityEngine;
using System.Collections;

public class LookManager : MonoBehaviour {

	public LookAtController controller = null;
	public Transform target;
	private static float DISTANCE = 5f; // http://www.columbia.edu/~rmk7/HC/HC_Readings/Argyle.pdf
	private static float visionFieldDeg = 160f;

	public void Start() {
		if (controller == null)
			controller = GetComponent<LookAtController>();
	}

	public void Update() {
		if (controller == null || !canILook()) {
			controller.headLookVector = Vector3.forward;
		} else {
			controller.headLookVector = new Vector3(target.position.x, 0f, target.position.z);
		}
	}

	private bool canILook() {
		float distance = Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(transform.position.x, transform.position.z));
		
		Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
		Vector2 left = forward.Rotate(-visionFieldDeg/2f);
		Vector2 right = forward.Rotate(visionFieldDeg/2f);
		Vector2 me = new Vector2(transform.position.x, transform.position.z);
		bool inside = myIsInsideTriangle(new Vector2(target.position.x, target.position.z),
									   me + left,
									   me,
									   me + right);
		Debug.Log(inside);
		return distance < DISTANCE && inside;
	}

	private bool isInsideTriangle(Vector2 s, Vector2 a, Vector2 b, Vector2 c) {
	    float as_x = s.x-a.x;
	    float as_y = s.y-a.y;

	    bool s_ab = (b.x-a.x)*as_y-(b.y-a.y)*as_x > 0;

	    if((c.x-a.x)*as_y-(c.y-a.y)*as_x > 0 == s_ab) return false;

	    if((c.x-b.x)*(s.y-b.y)-(c.y-b.y)*(s.x-b.x) > 0 != s_ab) return false;

	    return true;
	}

	private bool myIsInsideTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
		float denominator = ((b.y - c.y)*(a.x - c.x) + (c.x - b.x)*(a.y - c.y));
		float x = ((b.y - c.y)*(p.x - c.x) + (c.x - b.x)*(p.y - c.y)) / denominator;
		float y = ((c.y - a.y)*(p.x - c.x) + (a.x - c.x)*(p.y - c.y)) / denominator;
		float z = 1 - x - y;

		return 0 <= x && x <= 1 && 0 <= y && y <= 1 && 0 <= z && z <= 1;
	}
}
