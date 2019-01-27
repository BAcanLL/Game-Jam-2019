using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TelePhoneController : InteractiveController {
 
    private Rigidbody2D pRBody;
    private Timer nextCallTimer;
    private float nextCallTime;
	// Use this for initialization
	void Start () {
        Init(transitionOffTime: 2, key:KeyCode.Space);
        nextCallTime = Random.Range(15, 45);
        nextCallTimer = new Timer(nextCallTime);
        Debug.Log("TIME TIL NEXT CALL: " + nextCallTime);
	}

	// Update is called once per frame
	void Update ()
    {
        base.Update();
        // Debug.Log(state);
	}
    public override void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        nextCallTimer.Update();
        // Debug.Log(nextCallTimer.time.ToString());
        if(nextCallTimer.Done) {
            state = State.TransitionOn;
        }
    }
    public override void UpdateTransitionOn()
    {
        // pass
        state = State.On;
    }

    public override void UpdateTransitionOff()
    {
        base.UpdateTransitionOff();
        nextCallTimer.Set(Random.Range(15, 45));
    }
}
