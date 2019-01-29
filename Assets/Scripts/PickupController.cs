using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public GameObject user;
    public Vector2 offset = new Vector2(0,1.5f);

	// Use this for initialization
	void Start () {
        user = null;
        Physics2D.IgnoreLayerCollision(9, 9);
    }
	
	// Update is called once per frame
	void Update () {
        if (user)
        {
            transform.position = user.GetComponent<Rigidbody2D>().position + offset;
        }
    }

    
}
