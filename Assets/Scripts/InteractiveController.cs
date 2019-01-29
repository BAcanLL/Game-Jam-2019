using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class to handle interactive components in the scene
 * 
 * Reimplement functions if necessary - If using your own Start() function then call Init()
 * 
 */
public class InteractiveController : MonoBehaviour {

    public enum State
    {
        Off,
        TransitionOn,
        On,
        TransitionOff
    }

    public bool Done;
    protected State state;
    protected Timer transitionOnTimer;
    protected Timer transitionOffTimer;
    protected KeyCode activeKey;
    protected bool collidingWithPlayer = false;
    protected AudioClip defaultSoundClip;
    protected GameObject pickupPrefab, locations;
    protected Sprite defaultPickupSprite = null;
	
    protected void nextState() {
        if (state == State.TransitionOff)
        {
            state = State.Off;
        }
        else state++;
    }

	// Update is called once per frame
	protected void Update () {
		if(state == State.On)
        {
            UpdateOn();
        }
        else if(state == State.Off)
        {
            UpdateOff();
        }
        else if(state == State.TransitionOn)
        {
            UpdateTransitionOn();
        }
        else if (state == State.TransitionOff)
        {
            UpdateTransitionOff();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidingWithPlayer = false;
        }
    }

    protected void Init(
        State initialState=State.Off,
        float transitionOnTime=0,
        float transitionOffTime=0,
        KeyCode key=KeyCode.Space)
    {
        state = initialState;
        transitionOffTimer = new Timer(transitionOffTime);
        transitionOnTimer = new Timer(transitionOnTime);
        activeKey = key;
        Done = false;

        pickupPrefab = (GameObject)Resources.Load("pickup_prefab");
        locations = GameObject.Find("Locations");
    }

    // Run when the state changes to on
    public virtual void StartOn()
    {
    }

    // Run when the state changes to off
    public virtual void StartOff()
    {
    }

    // Run when the state changes to transition on
    public virtual void StartTransitionOn()
    {
    }

    // Run when the state changes to transition off
    public virtual void StartTransitionOff()
    {
    }

    //Runs continually when the state is on
    public virtual void UpdateOn()
    {
        // Only activates once, switches us to an activating state
        if (collidingWithPlayer && Input.GetKeyDown(activeKey))
        {
            transitionOffTimer.Reset();
            state = State.TransitionOff;
        }
    }

    /* Runs continually when state is off
     */
    public virtual void UpdateOff()
    {
        // Only activates once, switches us to an activating state
        if(collidingWithPlayer && Input.GetKeyDown(activeKey))
        {
            transitionOnTimer.Reset();
            state = State.TransitionOn;
        }
    }

    public virtual void UpdateTransitionOn()
    {
        if (collidingWithPlayer)
        {
            if (Input.GetKey(activeKey))
            {
                transitionOnTimer.Update();
            }
        }

        if (transitionOnTimer.Done)
        {          
            state = State.On;
        }
    }

    public virtual void UpdateTransitionOff()
    {
        if (collidingWithPlayer)
        {
            if (Input.GetKey(activeKey))
            {
                transitionOffTimer.Update();
            }
        }

        if (transitionOffTimer.Done)
        {
            state = State.Off;
        }
    }

    public void TurnOn()
    {
        state = State.On;
    }

    public void TurnOff()
    {
        state = State.Off;
    }

    public virtual void SelfDestruct()
    {
        Destroy(this);
    }

    public bool PlaySFX(AudioClip clip)
    {
        bool played = false;

        AudioSource source = GetComponent<AudioSource>();

        if (source == null)
            source = gameObject.AddComponent<AudioSource>();

        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);
            played = true;
        }

        return played;
    }

    public void StopSFX()
    {
        GetComponent<AudioSource>().Stop();
    }

    public GameObject SpawnPickup(Sprite sprite, string name)
    {
        int index = Random.Range(0, locations.transform.childCount);
        GameObject pickupObj = Instantiate(pickupPrefab, locations.transform.GetChild(index));
        pickupObj.GetComponent<SpriteRenderer>().sprite = sprite;
        pickupObj.name = name;

        return pickupObj;
    }

}
