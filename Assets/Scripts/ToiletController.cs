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
        Init(transitionOffTime: plungeTime, key: KeyCode.Space);
        anim = GetComponent<Animator>();
        message = GetComponentInChildren<Text>();
        message.text = "";

        defaultPickupSprite = Resources.Load<Sprite>("Plunger");
        SpawnPickup(defaultPickupSprite, "Plunger");
        defaultSoundClip = (AudioClip)Resources.Load("flush");
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
                PlaySFX(defaultSoundClip);
                anim.Play("Flushing");
            }
        }

        if (transitionOffTimer.Done)
        {
            state = State.Off;
            Done = true;
            GameObject.Find("Player").GetComponent<MCController>().ConsumeItem();
        }
        // overflowTimer.Set(Random.Range(15, 45));
    }
}
