using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : InteractiveController {

    Animator anim;

	// Use this for initialization
	void Start () {
        Init();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

        if (state == State.On)
        {
            anim.Play("On");
        }
        if (state == State.Off)
        {
            anim.Play("Off");
        }
	}
}
