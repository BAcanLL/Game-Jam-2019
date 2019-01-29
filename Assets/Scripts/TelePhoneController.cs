using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelephoneController : InteractiveController {
 
    //private Timer nextCallTimer;
    //private float nextCallTime;
    Animator anim;
    private bool dun = false, answered = false, hungUp = false;
    private Text message;
    private string caller;
    static string[] callers = { "Mom", "Dad", "Grandma", "Chris from next door" };
    private const float RING_DELAY = 0.5f;
    private Timer ringTimer = new Timer(RING_DELAY);
    private AudioClip talking, click;

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
        talking = Resources.Load<AudioClip>("talking");
        click = Resources.Load<AudioClip>("phone_click");
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

            if(!hungUp)
            {
                StopSFX();
                PlaySFX(click);
                hungUp = true;
            }

        }
    }
    public override void UpdateOff()
    {
    }

    public override void UpdateTransitionOff()
    {
        base.UpdateTransitionOff();

        if (!answered)
        {
            StopSFX();
            PlaySFX(click);
            answered = true;
        }
        else if(collidingWithPlayer && Input.GetKey(activeKey))
        {
            PlaySFX(talking);
            message.text = "Calling " + caller + "... ";
        }
        else
        {
            StopSFX();
            message.text = caller + " on hold.";
        }
    }

    private string GetNewCaller()
    {
        int nextCaller = Random.Range(0, callers.Length);
        return callers[nextCaller];
    }
}