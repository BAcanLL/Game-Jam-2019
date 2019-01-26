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
        TransitionOn,
        TransitionOff
    }

    protected State state;
    private Timer transitionOnTimer;
    private Timer transitionOffTimer;
    private KeyCode activeKey;
    private bool collidingWithPlayer = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if(state == State.On)
        {
            UpdateOn();
        }
        else if(state == State.Off)
        {
            UpdateOff();
        }
        else if(state == State.TransitionOn)
        {
            UpdateTransitionOn();
        }
        else if (state == State.TransitionOff)
        {
            UpdateTransitionOff();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collidingWithPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidingWithPlayer = false;
        }
    }

    public void Init(
        State initialState=State.Off,
        float transitionOnTime=0,
        float transitionOffTime=0,
        KeyCode key=KeyCode.Space)
    {
        state = initialState;
        transitionOffTimer = new Timer(transitionOffTime);
        transitionOnTimer = new Timer(transitionOnTime);
        activeKey = key;
    }

    // Run when the state changes to on
    public void StartOn()
    {
    }

    // Run when the state changes to off
    public void StartOff()
    {
    }

    // Run when the state changes to transition on
    public void StartTransitionOn()
    {
    }

    // Run when the state changes to transition off
    public void StartTransitionOff()
    {
    }

    //Runs continually when the state is active
    public void UpdateOn()
    {
        // Only activates once, switches us to an activating state
        if (collidingWithPlayer && Input.GetKeyDown(activeKey))
        {
            transitionOffTimer.Reset();
            state = State.TransitionOff;
            StartTransitionOff();
        }
    }

    /* Runs continually when state is off
     */
    public void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        if(collidingWithPlayer && Input.GetKeyDown(activeKey))
        {
            transitionOnTimer.Reset();
            state = State.TransitionOn;
            StartTransitionOn();
        }
    }
    
    private void UpdateTransition(Timer timer, State toState)
    {
        if (collidingWithPlayer && Input.GetKey(activeKey))
        {
            // Check if we're past any delay time
            if (timer.Done)
            {
                state = toState;
                if (state == State.On) StartOn();
                else StartOff();
            }
            else
            {
                timer.Update();
            }
        }
    }

    public void UpdateTransitionOn()
    {
        UpdateTransition(transitionOnTimer, State.On);
    }

    public void UpdateTransitionOff()
    {
        UpdateTransition(transitionOffTimer, State.Off);
    }
}
