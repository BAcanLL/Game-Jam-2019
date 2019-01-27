using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TelephoneController : InteractiveController {
 
    private Timer nextCallTimer;
    private float nextCallTime;
    Animator anim;

	// Use this for initialization
	void Start () {
        Init(/*transitionOffTime: 2,*/ key:KeyCode.Space);
        nextCallTime = Random.Range(3, 5);
        nextCallTimer = new Timer(nextCallTime);
        anim = GetComponent<Animator>();
        Debug.Log("TIME TIL NEXT CALL: " + nextCallTime);
	}

	// Update is called once per frame
	void Update ()
    {
        base.Update();
        // Debug.Log(state);

        if (state == State.TransitionOn)
        {
        }
        if (state == State.On)
        {
            anim.Play("On");
        }
        if (state == State.Off)
        {
            anim.Play("Off");
        }
    }
    public override void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        nextCallTimer.Update();
        // Debug.Log(nextCallTimer.time.ToString());
        if(nextCallTimer.Done) {
            state = State.On;
        }
    }
    //public override void UpdateTransitionOn()
    //{
    //    // pass
    //    state = State.On;
    //}

    public override void UpdateTransitionOff()
    {
        base.UpdateTransitionOff();
        // nextCallTimer.Set(Random.Range(15, 45));
    }
}
