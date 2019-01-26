using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TelePhoneController : InteractiveController {
 
    private Rigidbody2D pRBody;
    private Timer nextCallTimer;
    private int nextCallTime;
	// Use this for initialization
	void Start () {
        Init(transitionOffTime: 2, key:KeyCode.F);
        int nextCallTime = Random.Range(15, 45);
        nextCallTimer = new Timer(nextCallTime);
        Debug.Log("TIME TIL NEXT CALL: " + nextCallTimer.time);
	}

	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}
    private void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        if(nextCallTimer.Done) {
            nextState();
        }
    }
    private void UpdateTransitionOn()
    {
        // pass
        nextState();
    }

    private  void UpdateTransitionOff()
    {
        UpdateTransition(transitionOffTimer, State.Off);
        nextCallTimer.Set(Random.Range(15, 45));
    }
}
