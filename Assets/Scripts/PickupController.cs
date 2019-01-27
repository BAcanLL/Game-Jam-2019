using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public GameObject user;

	// Use this for initialization
	void Start () {
        user = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (user)
        {
            Rigidbody2D rBody = gameObject.GetComponent<Rigidbody2D>();
            rBody.MovePosition(user.GetComponent<Rigidbody2D>().position);
        }
    }

    
}
