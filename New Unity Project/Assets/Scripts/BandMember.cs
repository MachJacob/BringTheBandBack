using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandMember : MonoBehaviour
{
    public int mem;

    [SerializeField] LayerMask platformLayerMask;

    bool inAir = false;

    FMOD.Studio.EventInstance jump;
    FMOD.Studio.EventInstance footstep;

    //walking
    private SpriteRenderer spr;
    public Sprite still;
    public Sprite moving;
    public Rigidbody2D player;
    bool playerAttached = false;
    int spriteNum = 0;

    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        

        //Fmod Instances
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps");

        //Fmod Parameters
        jump.setParameterByName("jumpState", 0);

        //walking annimation
        spr = GetComponent<SpriteRenderer>();
        InvokeRepeating("walking", 0, 0.2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGrounded() && !inAir)
        {
            inAir = true;
            //jump.setParameterByName("jumpState", 1);
            //jump.start();
        }

        if (IsGrounded() && inAir)
        {
            //getting velocity
            inAir = false;

            if (velocity < 0) { velocity = 0 - velocity; }
            if (velocity > 10) { velocity = 10; }
            jump.setParameterByName("downVelocity", velocity);
            jump.setParameterByName("jumpState", 0);
            jump.start();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Band>().AddBandMember(gameObject);
            collision.GetComponent<PlayerMovement>().EnableBand(mem);

            Destroy(GetComponent<Collider2D>());

            playerAttached = true;
        }
    }

    private bool IsGrounded()   //circle cast to check if ground is close
    {
        float extraHeight = .75f;
        RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, extraHeight, platformLayerMask);


        return raycastHit.collider != null;
    }

    public void SetVel(float _vel)
    {
        velocity = _vel;
    }

    private void walking()
    {
        Vector3 playerVelocity = player.GetRelativePointVelocity(new Vector3(0.0f, 0.0f, 0.0f));

        if(player != null)
        {
            if (playerVelocity.x != 0 && spriteNum == 0 && playerAttached == true)
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
        
    }
}
