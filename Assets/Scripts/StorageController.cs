using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : InteractiveController {

    private MCController pc;

	// Use this for initialization
	void Start () {
        Init();
        pc = GameObject.Find("Player").GetComponent<MCController>();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public override void UpdateOn()
    {
        if(collidingWithPlayer)
        {
            if (Input.GetKeyDown(pc.dropKey) && pc.GetHeldItemName() == "Food")
            {
                Done = true;
                pc.ConsumeItem();
            }
        } 
    }
}
