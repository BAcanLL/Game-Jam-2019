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
    private Direction facing = Direction.front;

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
        }
        if (Input.GetKey(rMoveKey))
        {
            newMoveDir += new Vector2(1, 0);
        }
        if (Input.GetKey(uMoveKey))
        {
            newMoveDir += new Vector2(0, 1);
            facing = Direction.back;
        }
        if (Input.GetKey(dMoveKey))
        {
            newMoveDir += new Vector2(0, -1);
            facing = Direction.front;
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

        Move(newMoveDir);
    }

    private void Move(Vector2 newMoveDir)
    {
        Vector2 pos = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset;

        int layMask = 1 << 8; // Layer mask for environment layer

        RaycastHit2D hitX = Physics2D.Raycast(pos, new Vector2(newMoveDir.x,0), 0.75f, layMask);
        RaycastHit2D hitY = Physics2D.Raycast(pos, new Vector2(0, newMoveDir.y), 0.5f, layMask);

        if (hitX.collider != null)
        {
            newMoveDir = new Vector2(0, newMoveDir.y);
        }
        if (hitY.collider != null)
        {
            newMoveDir = new Vector2(newMoveDir.x, 0);
        }

        transform.Translate(newMoveDir);
    }
}
