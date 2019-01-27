using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : InteractiveController {

    private Animator anim;

    private const float BED_MAKING_TIME = 0;

    // Use this for initialization
    void Start()
    {
        Init(transitionOnTime: BED_MAKING_TIME);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if(state == State.Off)
        {
            anim.Play("Off");
        }
        else if (state == State.On)
        {
            anim.Play("On");
            Done = true;
        }

        base.Update();
    }
}
