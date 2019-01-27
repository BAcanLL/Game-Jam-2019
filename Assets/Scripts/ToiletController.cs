using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToiletController : InteractiveController
{
    private float plungeTime;
    private Text message;
    private Animator anim;
	// Use this for initialization
	void Start () {
        plungeTime = Random.Range(2, 5);
        Init(transitionOffTime: plungeTime, key: KeyCode.P);
        anim = GetComponent<Animator>();
        message = GetComponentInChildren<Text>();
        message.text = "";
	}

    private bool hasPlunger() {
        MCController player = GameObject.Find("Player").GetComponent<MCController>();

        if(player.GetHeldItemName() == "Plunger") {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void UpdateOff()
    {
        anim.Play("Default");
        state = State.TransitionOn;
        /* 
        // Only activates once, switches us to an activating state
        overflowTimer.Update();
        // Debug.Log(overflowTimer.time.ToString());
        if(overflowTimer.Done) {
            state = State.TransitionOn;
        }
        */
    }

    public override void UpdateTransitionOn()
    {
        // overflow animation
        state = State.On;
    }

    public override void UpdateOn()
    {
        // Only activates once, switches us to an activating state
        if (collidingWithPlayer && Input.GetKeyDown(activeKey) && hasPlunger()) {
            state = State.TransitionOff;
            anim.Play("Flushing");
        }
    }

    public override void UpdateTransitionOff()
    {
        if (collidingWithPlayer)
        {
            if (Input.GetKey(activeKey) && hasPlunger())
            {
                transitionOffTimer.Update();
                int percentDone = transitionOffTimer.GetPercentDone();
                message.text = "toilet plunging:  " + percentDone + "%";
                anim.Play("Flushing");
            }
        }

        if (transitionOffTimer.Done)
        {
            state = State.Off;
            Done = true;
        }
        // overflowTimer.Set(Random.Range(15, 45));
    }
}
