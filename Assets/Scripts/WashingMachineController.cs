using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashingMachineController : InteractiveController {

    Animator anim;
    Timer t;

    private MCController pc;
    private int loads;
    private Text message;

	// Use this for initialization
	void Start () {
        Init(transitionOnTime:3);
        anim = GetComponent<Animator>();
        t = new Timer(3);
        pc = GameObject.Find("Player").GetComponent<MCController>();
        loads = 0;
        message = GetComponentInChildren<Text>();
        message.text = "";

        defaultPickupSprite = Resources.Load<Sprite>("Laundry");
        SpawnPickup(defaultPickupSprite, "Laundry");
        SpawnPickup(defaultPickupSprite, "Laundry");
        SpawnPickup(defaultPickupSprite, "Laundry");
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
                Done = true;
                message.text = "";
                anim.Play("Off");
            }
            anim.Play("On");
        }
    }

    public override void UpdateOff()
    {
        if (loads == 3)
        {
            base.UpdateOff();
        }

        if (collidingWithPlayer)
        {
            if (Input.GetKeyDown(pc.dropKey) && pc.GetHeldItemName().Contains("Laundry"))
            {
                if(loads < 3)
                {
                    pc.ConsumeItem();
                    loads++;
                    message.text = "Loads: " + loads + "/3";
                }  
            }
        }
    }

    public override void UpdateTransitionOn()
    {
        base.UpdateTransitionOn();
        message.text = "Turning on machine... " + transitionOnTimer.GetPercentDone();
    }

    public override void UpdateOn()
    {
        base.UpdateOn();
        message.text = "Washing Clothes " + t.GetPercentDone();
    }
}
