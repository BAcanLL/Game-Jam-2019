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

    private GameObject gObj;
    private State state;
    private float activeDelay;
    private float timeStartActivating;
    private float timeStartActive;
    private float activatedTime;
    private KeyCode activeKey;

	// Use this for initialization
	void Start () {
        state = State.Off;
        activeDelay = 0;
        activatedTime = 1;
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
        float delay=0,
        float activatedTime0=0,
        KeyCode key=KeyCode.Space)
    {
        gObj = GameObject.Find(objectName);
        state = initialState;
        activeDelay = delay;
        activatedTime = activatedTime0;
        activeKey = key;
    }

    // Run when the state changes to on
    public void OnActivate()
    {
        timeStartActive = Time.time;
    }

    // Run when the state changes to off
    public void OnDeactivate()
    {
    }

    //Runs continually when the state is active
    public void UpdateActive()
    {
        // Once the active state is done running, stop
        if(activatedTime < Time.time - timeStartActive)
        {
            state = State.Off;
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
            timeStartActivating = Time.time;
            state = State.Activating;
        }

        // If we're activating and the user is still pressing the button then we'll turn to the on state
        if(state == State.Activating && Input.GetKey(activeKey))
        {
            // Check if we're past any delay time
            if(activeDelay < Time.time - timeStartActivating)
            {
                state = State.On;
                OnActivate();
            }
        }
        // Otherwise the button wasn't held down long enough and the item deactivates
        else
        {
            state = State.Off;
        }
    }
}
