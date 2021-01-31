using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 vel;
    private SpriteRenderer spr;
    public float moveSpeed;
    public float smoothTime;
    public float jumpVel;

    private bool[] band;

    [SerializeField] LayerMask platformLayerMask;   //platforms that can be jumped on

    bool inAir = false;

    FMOD.Studio.EventInstance footstep;
    FMOD.Studio.EventInstance jump;
    FMOD.Studio.EventInstance music;

    public GameObject drumStick; 
    public GameObject cymbol;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        band = new bool[5];

        //Fmod Instances
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps"); 
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MusicRandom");
        //Fmod Parameters
        jump.setParameterByName("jumpState", 1);
        //music start
        music.setParameterByName("chord", 0);
        music.start();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && band[0])
        {
            int dir = 1;
            if (spr.flipX) dir = -1;
            GameObject stick = Instantiate(drumStick, transform.position + Vector3.right * dir * 1.15f, Quaternion.identity);
            stick.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * 100);
            stick.GetComponent<SpriteRenderer>().flipX = spr.flipX;
        }
        if (Input.GetButtonDown("Fire2") && band[0])
        {
            int dir = 1;
            if (spr.flipX) dir = -1;
            GameObject stick = Instantiate(cymbol, transform.position + Vector3.right * dir * 1.55f, Quaternion.identity);
            stick.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * 100);
            stick.GetComponent<SpriteRenderer>().flipX = spr.flipX;
        }

        //music FMOD Shit
        FMOD.Studio.PLAYBACK_STATE state;
        music.getPlaybackState(out state);

        if (state == FMOD.Studio.PLAYBACK_STATE.STOPPING)
        {
            music.setParameterByName("chord", Random.Range(0, 5));
            music.start();
        }
    }

    void FixedUpdate()
    {
        float moveVel = Input.GetAxis("Horizontal");
        
        Vector2 targetVel = new Vector2(moveVel * moveSpeed, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothTime);  //smooth movement

        if (Input.GetAxis("Jump") > 0 && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            jump.setParameterByName("jumpState", 1);
            jump.start();
        }

        if (rb.velocity.x > 0.1)    //change direction if moving
        {
            spr.flipX = false;
        }
        else if (rb.velocity.x < -0.1)
        {
            spr.flipX = true;
        }

        if (!IsGrounded())
        {
            inAir = true;
        }

        if(IsGrounded() && inAir)
        {
            //getting velocity
            Vector3 playerVelocity = rb.GetRelativePointVelocity(new Vector3(0.0f, 0.0f, 0.0f));           
            if(playerVelocity.y < 0) { playerVelocity.y = 0 - playerVelocity.y; }
            if(playerVelocity.y > 10) { playerVelocity.y = 10; }
            Debug.Log(playerVelocity.y);
            jump.setParameterByName("downVelocity", playerVelocity.y);
            GetComponent<Band>().SetBandVel(playerVelocity.y);

            inAir = false;
            jump.setParameterByName("jumpState", 0);
            jump.start();
            footstep.start();
        }
    }

    private bool IsGrounded()   //circle cast to check if ground is close
    {
        float extraHeight = .75f;
        RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, extraHeight, platformLayerMask);

        return raycastHit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (.75f * Vector3.down), 0.5f);
    }

    public void EnableBand(int _idx)
    {
        band[_idx] = true;          //0 drums index refers to charactor

        switch (_idx)
        {
            case 0:
                music.setParameterByName("BassPlayer", 1);
                break;
            case 1:
                music.setParameterByName("PianoPlayer", 1);
                break;
            case 2:
                music.setParameterByName("GuitarPlayer", 1);
                break;
            case 3:
                music.setParameterByName("Drummer", 1);
                break;
            case 4:
                music.setParameterByName("SaxPlayer", 1);
                break;
        }
    }

}
