using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashingMachineController : InteractiveController {

    private Animator anim;
    private const float WASH_TIME = 10, NUM_CLOTHES = 3;
    private Timer onTimer;

    private MCController pc;
    private int loads;
    private Text message;
    private bool washed = false;

	// Use this for initialization
	void Start () {
        Init();
        anim = GetComponent<Animator>();
        onTimer = new Timer(WASH_TIME);
        pc = GameObject.Find("Player").GetComponent<MCController>();
        loads = 0;
        message = GetComponentInChildren<Text>();
        message.text = "";
        defaultSoundClip = (AudioClip)Resources.Load("washing_machine");


        for (int i = 0; i < NUM_CLOTHES; i++)
        {
            int num = Random.Range(1, 4);

            Sprite[] sprite = Resources.LoadAll<Sprite>("Laundry2");
            GameObject pickupObj = SpawnPickup(sprite[num], "Laundry");

            pickupObj.transform.Translate(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
        }
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
            anim.Play("On");
            PlaySFX(defaultSoundClip);
        }
        if (state == State.TransitionOff)
        {
            StopSFX();
            message.text = "Done";
            anim.Play("Off");
        }
    }

    public override void UpdateOff()
    {
        if (loads == NUM_CLOTHES)
        {
            base.UpdateOff();
        }

        if (collidingWithPlayer)
        {
            if (Input.GetKeyDown(activeKey) && pc.GetHeldItemName().Contains("Laundry"))
            {
                if(loads < NUM_CLOTHES)
                {
                    pc.ConsumeItem();
                    loads++;
                    message.text = "Loads: " + loads + "/" + NUM_CLOTHES;
                }  
            }
        }
    }

    public override void UpdateOn()
    {
        onTimer.Update();

        if (onTimer.Done)
            state = State.TransitionOff;

        message.text = "";
    }
    public override void UpdateTransitionOff()
    {
        if(collidingWithPlayer && Input.GetKey(activeKey))
        {
            Done = true;
            message.text = "";
            state = State.Off;
        }
    }
}
