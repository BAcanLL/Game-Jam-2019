using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : InteractiveController {

    private Animator anim;

    private const float HOMEWORK_TIME = 2;

	// Use this for initialization
	void Start () {
        Init(transitionOnTime:HOMEWORK_TIME);
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (state == State.Off)
        {
            anim.Play("Off");
        }
        else if (state == State.TransitionOn) 
        {
            anim.Play("On");
        }
        else if (state == State.On)
        {
            Done = true;
            anim.Play("On");
        }
        Debug.Log(Done);
        base.Update();
    }

    public override void UpdateOn()
    {
        state = State.Off;
    }
}
