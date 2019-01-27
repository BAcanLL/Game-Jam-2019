using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : InteractiveController
{
    private Timer overflowTimer;
    private Timer plungeTimer;
    private float overflowTime;
    private float plungeTime;
    // bool hasPlunger = false;

	// Use this for initialization
	void Start () {
        plungeTime = Random.Range(1, 5);
        overflowTime = Random.Range(15, 45);
        Init(transitionOffTime: plungeTime, key: KeyCode.P);
        overflowTimer = new Timer(overflowTime);
        Debug.Log("TIME TIL NEXT OVERFLOW: " + overflowTime);
	}

    private bool hasPlunger() {
        MCController player = GameObject.Find("Player").GetComponent<MCController>();

        if(player.pickedUpItem && player.pickedUpItem.name == "Plunger") {
            // Debug.Log("HAS PLUNGER");
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        // Debug.Log(state);
    }

    public override void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        overflowTimer.Update();
        // Debug.Log(overflowTimer.time.ToString());
        if(overflowTimer.Done) {
            state = State.TransitionOn;
        }
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
            Debug.Log("GOTTEM");
            state = State.TransitionOff;
        }
    }

    public override void UpdateTransitionOff()
    {
        base.UpdateTransitionOff();
        // overflowTimer.Set(Random.Range(15, 45));
    }
}
