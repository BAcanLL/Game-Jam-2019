using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TelePhoneController : InteractiveController {
 
    private Rigidbody2D pRBody;

	// Use this for initialization
	void Start () {
        Init(transitionOffTime: 2, key:KeyCode.F);
        gameObject;
        // init timer
	}

	// Update is called once per frame
	void Update () {
        // if state 1 is ready to go to state 2
        // call update

	}

    // update() {
        State = (State+1)%4;
    }
    // Spawn object

	// Place object

	// Start Ringing

    // Turn Off

	// Using Phone
	/*
	GameObject player;
    KeyCode lMoveKey;
    KeyCode rMoveKey;
    KeyCode uMoveKey;
    KeyCode dMoveKey;
    float speed;

    private Rigidbody2D pRBody;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        lMoveKey = KeyCode.A;
        rMoveKey = KeyCode.D;
        uMoveKey = KeyCode.W;
        dMoveKey = KeyCode.S;
        pRBody = player.GetComponent<Rigidbody2D>();
        speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newMoveDir = Vector2.zero;
        // Add any movement vectors the player input
		if(Input.GetKeyDown(lMoveKey))
        {
            newMoveDir += new Vector2(-1, 0);
        }
        if (Input.GetKeyDown(rMoveKey))
        {
            newMoveDir += new Vector2(1, 0);
        }
        if (Input.GetKeyDown(uMoveKey))
        {
            newMoveDir += new Vector2(0, 1);
        }
        if (Input.GetKeyDown(dMoveKey))
        {
            newMoveDir += new Vector2(0, -1);
        }

        newMoveDir.Normalize();
        newMoveDir *= speed;

        pRBody.AddForce(newMoveDir);
    } */
}
