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
    FMOD.Studio.EventInstance atmos;

    public Sprite still;
    public Sprite moving;
    int spriteNum = 0;

    public GameObject drumStick; 
    public GameObject cymbol;
    public GameObject piano;
    private GameObject newPiano;

    private bool dubJump;

    private float doJump = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        band = new bool[5];

        //walking
        InvokeRepeating("walking", 0, 0.2f);

        //Fmod Instances
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps"); 
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MusicRandom");
        atmos = FMODUnity.RuntimeManager.CreateInstance("event:/Enviroment/Atmos/Haunting");
        //Fmod Parameters
        jump.setParameterByName("jumpState", 1);
        //music start
        music.setParameterByName("chord", Random.Range(0, 5));
        music.start();
        //atmos start
        atmos.start();

        dubJump = false;
    }

    private void Update()
    {
        //music FMOD 
        FMOD.Studio.PLAYBACK_STATE state;
        music.getPlaybackState(out state);

        if (state == FMOD.Studio.PLAYBACK_STATE.STOPPING || state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            music.start();
            music.setParameterByName("chord", Random.Range(0, 5));
        }

        if (Input.GetButtonDown("Fire1") && band[3])
        {
            int dir = 1;
            if (spr.flipX) dir = -1;
            GameObject stick = Instantiate(drumStick, transform.position + Vector3.right * dir * 1.15f, Quaternion.identity);
            stick.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * 100);
            stick.GetComponent<SpriteRenderer>().flipX = spr.flipX;
        }
        if (Input.GetButtonDown("Fire2") && band[3])
        {
            int dir = 1;
            if (spr.flipX) dir = -1;
            GameObject stick = Instantiate(cymbol, transform.position + Vector3.right * dir * 1.55f, Quaternion.identity);
            stick.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * 100);
            stick.GetComponent<SpriteRenderer>().flipX = spr.flipX;
        }
        if (Input.GetButtonDown("Fire3") && band[1])
        {
            Destroy(newPiano);
            int dir = 1;
            if (spr.flipX) dir = -1;
            newPiano = Instantiate(piano, transform.position + Vector3.right * dir * 1.55f, Quaternion.identity);
            newPiano.GetComponent<SpriteRenderer>().flipX = spr.flipX;
            newPiano.GetComponent<BoxCollider2D>().offset = Vector2.right * -0.6f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            doJump = 0.25f;
        }
        doJump -= Time.deltaTime;

        
    }

    void FixedUpdate()
    {
        float moveVel = Input.GetAxis("Horizontal");
        
        Vector2 targetVel = new Vector2(moveVel * moveSpeed, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothTime);  //smooth movement

        if (doJump > 0 && (IsGrounded() || (band[4] && !dubJump)))
        {
            
            dubJump = !IsGrounded();
            rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            jump.setParameterByName("jumpState", 1);
            jump.start();
            doJump = 0;
          
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

        if((IsGrounded() && inAir))
        {
            //getting velocity
            Vector3 playerVelocity = rb.GetRelativePointVelocity(new Vector3(0.0f, 0.0f, 0.0f));           
            if(playerVelocity.y < 0) { playerVelocity.y = 0 - playerVelocity.y; }
            if(playerVelocity.y > 10) { playerVelocity.y = 10; }
            jump.setParameterByName("downVelocity", playerVelocity.y);
            GetComponent<Band>().SetBandVel(playerVelocity.y);

            inAir = false;
            jump.setParameterByName("jumpState", 0);
            jump.start();
            footstep.start();

            dubJump = false;
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

    private void walking()
    {
        Vector3 playerVelocity = rb.GetRelativePointVelocity(new Vector3(0.0f, 0.0f, 0.0f));

        if (playerVelocity.x != 0 && spriteNum == 0)
        {
            spr.sprite = moving;
            spriteNum = 1;
        }
        else if(spriteNum == 1)
        {
            footstep.start();
            spr.sprite = still;
            spriteNum = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
        }
    }
}
