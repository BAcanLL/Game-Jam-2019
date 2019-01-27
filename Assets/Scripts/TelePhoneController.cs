using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelephoneController : InteractiveController {
 
    //private Timer nextCallTimer;
    //private float nextCallTime;
    Animator anim;
    bool dun = false;
    private Text message;
    private string caller;
    static string[] callers = { "mom", "dad", "grandma", "telemarketer", "friend" };
    private const float RING_DELAY = 0.5f;
    private Timer ringTimer = new Timer(RING_DELAY);

	// Use this for initialization
	void Start () {
        Init(transitionOffTime: 5, initialState: State.On);
        //nextCallTime = Random.Range(3, 5);
        //nextCallTimer = new Timer(nextCallTime);
        anim = GetComponent<Animator>();
        message = GetComponentInChildren<Text>();
        message.text = "";
        caller = GetNewCaller();
        defaultSoundClip = (AudioClip)Resources.Load("phone_ring");
        ringTimer.Reset();
	}

	// Update is called once per frame
	void Update ()
    {
        base.Update();

        if (state == State.TransitionOff)
        {
            dun = true;
        }
        if (state == State.On)
        {
            //Debug.Log(state);
            anim.Play("On");

            if (ringTimer.Done)
            {
                PlaySFX(defaultSoundClip);
                ringTimer.Reset();
            }

            ringTimer.Update();
        }
        if (state == State.Off)
        {
            //Debug.Log(state);
            anim.Play("Off");
            if (dun) Done = true;
            message.text = "";
        }
    }
    public override void UpdateOff()
    {
    }

    public override void UpdateTransitionOff()
    {
        base.UpdateTransitionOff();
        message.text = "Calling " + caller + "... " + transitionOffTimer.GetPercentDone();
    }

    private string GetNewCaller()
    {
        int nextCaller = Random.Range(0, 5);
        return callers[nextCaller];
    }
}