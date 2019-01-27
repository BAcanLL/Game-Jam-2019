using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineController : InteractiveController {

    Animator anim;
    Timer t;

	// Use this for initialization
	void Start () {
        Init(transitionOnTime:3);
        anim = GetComponent<Animator>();
        t = new Timer(3);
    }
    
    // Update is called once per frame
    void Update () {
        base.Update();

        if (state == State.TransitionOn)
        {
            Debug.Log("Loading Machine");
        }
        if (state == State.On)
        {
            t.Update();
            if (t.Done == true)
            {
                state = State.Off;
            }
            anim.Play("On");
        }
        if (state == State.Off)
        {
            t.Reset();
            anim.Play("Off");
        }
    }
}
