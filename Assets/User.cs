using UnityEngine;
using System.Collections;

/* Interface for the joystick control law
 * HOW DOES THE JOYSTICK AFFECT THE USER-CONTROLLED AGENT'S MOVEMENT?
 * There is several control laws to help you control your agent with any device */

public abstract class User : AgentBasic {

	protected Vector2 velocity;

	protected float SPEED = 100f;

    public bool SELF_UPDATED = true;
    public float AGENT_MAX_SPEED = 1.5f;

    public string INPUT = "";
    public bool MIDDLE_VR = true;
    protected string vertAxis = "";
    protected string horiAxis = "";

	/* Input control law to be implemented inside */
	public abstract void updatePositionOrientation(float agentMaxSpeed);

    public void Update()
    {
        updatePositionOrientation(AGENT_MAX_SPEED);
    }

    public void Awake()
    {
        velocity = Vector2.zero;

        if (MIDDLE_VR) {
            INPUT = "";
            return;
        }

        if (INPUT == "JOYSTICK ROTATE")
        {
            vertAxis = "Joystick Vertical";
            horiAxis = "Joystick Right Horizontal";
        }
        else if (INPUT == "KEYBOARD")
        { /* keyboard's directional keys */
            vertAxis = "Vertical";
            horiAxis = "Horizontal";
        }
        else
        { /* joystick with directional command */
            vertAxis = "Joystick Vertical";
            horiAxis = "Joystick Horizontal";
        }
    }

    protected float getVerticalAxis()
    {
        #if UNITY_STANDALONE_WIN
        if (MIDDLE_VR && MiddleVR.VRDeviceMgr != null)
            return MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
        #endif

        return Input.GetAxis(vertAxis);
    }

    protected float getHorizontalAxis()
    {
        #if UNITY_STANDALONE_WIN
        if (MIDDLE_VR && MiddleVR.VRDeviceMgr != null)
            return MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
        #endif

        return Input.GetAxis(horiAxis);
    }
}