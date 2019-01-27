using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskController : InteractiveController {

    private Animator anim;
    private Text message;
    private const float newMsgTime = 2f;
    private Timer msgTimer = new Timer(newMsgTime);

    private const float HOMEWORK_TIME = 7;

	// Use this for initialization
	void Start () {
        Init(transitionOnTime:HOMEWORK_TIME);
        anim = GetComponent<Animator>();
        message = GetComponentInChildren<Text>();
        message.text = "";
        msgTimer.Set(newMsgTime);
	}
	
	// Update is called once per frame
	void Update () {

        if (state == State.Off)
        {
            anim.Play("Off");

            msgTimer.Reset();
        }
        else if (state == State.TransitionOn) 
        {
            anim.Play("On");

            if (msgTimer.Done && Input.GetKey(activeKey))
            {
                SetMessage(GenRandMsg());
                msgTimer.Reset();
                Debug.Log("msg");
            }

            msgTimer.Update();
        }
        else if (state == State.On)
        {
            Done = true;
            anim.Play("Off");
        }

        FadeMessage();

        base.Update();
    }

    public override void UpdateOn()
    {
        state = State.Off;
    }

    private void FadeMessage()
    {
        if(message.color.a > 0)
        {
            float fadeIncrement = 0.01f;

            Color faded = message.color;
            faded.a = ((faded.a - fadeIncrement) < 0) ? 0 : (faded.a - fadeIncrement);
            message.color = faded;
        }
    }

    private void SetMessage(string newMessage)
    {
        message.text = newMessage;

        Color unfaded = message.color;
        unfaded.a = 1;
        message.color = unfaded;
    }

    private static string GenRandMsg()
    {
        string msg = "";

        string[] msgList = new string[3] { "y = mx + b", "mitochondria", "Nouns, verbs, ..." };

        msg = msgList[Random.Range(0, msgList.Length)];

        return msg;
    }

    public override void SelfDestruct()
    {
        message.text = "";

        base.SelfDestruct();
    }
}
