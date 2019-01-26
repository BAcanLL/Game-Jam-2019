using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCController : MonoBehaviour
{

    private GameObject player;
    private KeyCode lMoveKey;
    private KeyCode rMoveKey;
    private KeyCode uMoveKey;
    private KeyCode dMoveKey;
    public float speed = 0.1f;
    Vector2 newMoveDir;

    private Animator anim;

    private Rigidbody2D pRBody;

    // Use this for initialization
    void Start()
    {
        // Init components
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        pRBody = GetComponent<Rigidbody2D>();

        lMoveKey = KeyCode.A;
        rMoveKey = KeyCode.D;
        uMoveKey = KeyCode.W;
        dMoveKey = KeyCode.S;
        pRBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(newMoveDir);
        newMoveDir = Vector2.zero;
        // Add any movement vectors the player input
        if (Input.GetKey(lMoveKey))
        {
            newMoveDir += new Vector2(-1, 0);
        }
        if (Input.GetKey(rMoveKey))
        {
            newMoveDir += new Vector2(1, 0);
        }
        if (Input.GetKey(uMoveKey))
        {
            newMoveDir += new Vector2(0, 1);
            anim.Play("Walk_back");
        }
        if (Input.GetKey(dMoveKey))
        {
            newMoveDir += new Vector2(0, -1);
            anim.Play("Walk_front");
        }
        if (!Input.anyKey)
        {
            pRBody.velocity = Vector2.zero;
            anim.Play("Idle");
        }

        newMoveDir.Normalize();
        newMoveDir *= speed;

        //pRBody.AddForce(newMoveDir);

        transform.Translate(newMoveDir);
    }
}
