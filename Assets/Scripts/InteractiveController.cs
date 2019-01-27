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

    public bool Done { get; protected set; }
    protected State state;
    protected Timer transitionOnTimer;
    protected Timer transitionOffTimer;
    protected KeyCode activeKey;
    protected bool collidingWithPlayer = false;
	
    protected void nextState() {
        if (state == State.TransitionOff)
        {
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
        // Debug.Log(state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collidingWithPlayer = true;
            // Debug.Log("collide");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
        Done = false;
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

    //Runs continually when the state is on
    public virtual void UpdateOn()
    {
        // Only activates once, switches us to an activating state
        if (collidingWithPlayer && Input.GetKeyDown(activeKey))
        {
            transitionOffTimer.Reset();
            state = State.TransitionOff;
        }
    }

    /* Runs continually when state is off
     */
    public virtual void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        if(collidingWithPlayer && Input.GetKeyDown(activeKey))
        {
            transitionOnTimer.Reset();
            state = State.TransitionOn;
        }
    }
    

    // Not sure why UpdateTransition doesn't work
    /*
    protected void UpdateTransition(Timer timer, State toState)

    {
        if (collidingWithPlayer && Input.GetKeyDown(activeKey))
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
    } */

    public virtual void UpdateTransitionOn()
    {
        if (collidingWithPlayer)
        {
            if (Input.GetKey(activeKey))
            {
                transitionOnTimer.Update();
            }
        }

        if (transitionOnTimer.Done)
        {          
            state = State.On;
        }
    }

    public virtual void UpdateTransitionOff()
    {
        if (collidingWithPlayer)
        {
            if (Input.GetKey(activeKey))
            {
                transitionOffTimer.Update();
            }
        }

        if (transitionOffTimer.Done)
        {
            state = State.Off;
        }
    }
}
