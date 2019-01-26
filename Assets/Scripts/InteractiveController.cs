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
        Off,
        TransitionOn,
        On,
        TransitionOff
    }

    protected State state;
    private Timer transitionOnTimer;
    private Timer transitionOffTimer;
    private KeyCode activeKey;
    private bool collidingWithPlayer = false;
	
    protected void nextState() {
        if(state == State.TransitionOff) {
            state = State.Off;
        }
        else state++;
    }

	// Update is called once per frame
	protected void Update () {
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

    protected void Init(
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
            nextState();
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
            nextState();
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
                nextState();
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
