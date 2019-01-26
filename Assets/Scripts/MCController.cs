using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCController : MonoBehaviour
{
    // Player
    private GameObject player;

    // Movement vars
    public float speed = 0.1f;
    private KeyCode lMoveKey;
    private KeyCode rMoveKey;
    private KeyCode uMoveKey;
    private KeyCode dMoveKey;
    private Vector2 newMoveDir;
    private enum Direction { front, back, left, right, none };
    private Direction facing = Direction.front, moving = Direction.front, cantMove = Direction.none;

    // Animation vars
    private Animator anim;

    // Physics vars
    private Rigidbody2D pRBody;

    // Use this for initialization
    void Start()
    {
        // Init component references
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
        newMoveDir = Vector2.zero;

        // Add any movement vectors the player input
        if (Input.GetKey(lMoveKey))
        {
            newMoveDir += new Vector2(-1, 0);
            moving = Direction.left;
        }
        if (Input.GetKey(rMoveKey))
        {
            newMoveDir += new Vector2(1, 0);
            moving = Direction.right;
        }
        if (Input.GetKey(uMoveKey))
        {
            newMoveDir += new Vector2(0, 1);
            facing = Direction.back;
            moving = facing;
        }
        if (Input.GetKey(dMoveKey))
        {
            newMoveDir += new Vector2(0, -1);
            facing = Direction.front;
            moving = facing;
        }

        // Play walking animations
        if(newMoveDir != Vector2.zero)
        {
            if (facing == Direction.front)
                anim.Play("Walk_front");
            else if (facing == Direction.back)
                anim.Play("Walk_back");
        }

        //Idle when no inputs
        if (!Input.anyKey)
        {
            pRBody.velocity = Vector2.zero;
            if (facing == Direction.front)
                anim.Play("Idle_front");
            else if (facing == Direction.back)
                anim.Play("Idle_back");
        }

        newMoveDir.Normalize();
        newMoveDir *= speed;

        if(moving != cantMove)
            transform.Translate(newMoveDir);       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Furniture"))
        {
            cantMove = moving;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Furniture"))
        {
            cantMove = Direction.none;
        }
    }
}
