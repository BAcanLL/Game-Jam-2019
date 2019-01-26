using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class to handle interactive components in the scene
 * 
 * Reimplement functions if necessary - If using your own Start() function then call Init()
 * 
 */
public class InteractiveController : MonoBehaviour {

    public enum State
    {
        On,
        Off,
        Activating
    }

    protected GameObject gObj;
    protected State state;
    private Timer activatingTimer;
    private Timer activeTimer;
    private KeyCode activeKey;

	// Use this for initialization
	void Start () {
        state = State.Off;
        activatingTimer = new Timer(0);
        activeTimer = new Timer(1);
        activeKey = KeyCode.Space;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == State.On)
        {
            UpdateActive();
        }
        else if(state == State.Off || state == State.Activating)
        {
            UpdateInactive();
        }
	}

    public void Init(
        string objectName, 
        State initialState=State.Off,
        float activationDelay=0,
        float activatedTime=0,
        KeyCode key=KeyCode.Space)
    {
        gObj = GameObject.Find(objectName);
        state = initialState;
        activatingTimer = new Timer(activationDelay);
        activeTimer = new Timer(activatedTime);
        activeKey = key;
    }

    // Run when the state changes to on
    public void OnActivate()
    {
    }

    // Run when the state changes to off
    public void OnDeactivate()
    {
    }

    //Runs continually when the state is active
    public void UpdateActive()
    {
        // Once the active state is done running, stop
        if(activeTimer.Done)
        {
            state = State.Off;
        }
        else
        {
            activeTimer.Update();
        }
    }

    /* Runs continually when state is off
     * Handles checking length of time activate button has been pressed
     */
    public void UpdateInactive()
    {
        // Only activates once, switches us to an activating state
        if(Input.GetKeyDown(activeKey))
        {
            activatingTimer.Reset();
            state = State.Activating;
        }

        // If we're activating and the user is still pressing the button then we'll turn to the on state
        if(state == State.Activating && Input.GetKey(activeKey))
        {
            // Check if we're past any delay time
            if(activatingTimer.Done)
            {
                state = State.On;
                activeTimer.Reset();
                OnActivate();
            }
            else
            {
                activatingTimer.Update();
            }
        }
        // Otherwise the button wasn't held down long enough and the item deactivates
        else
        {
            state = State.Off;
        }
    }
}
