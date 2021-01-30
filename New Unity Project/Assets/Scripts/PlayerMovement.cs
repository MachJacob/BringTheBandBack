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

    [SerializeField] LayerMask platformLayerMask;   //platforms that can be jumped on

    FMOD.Studio.EventInstance footstep;
    FMOD.Studio.EventInstance jump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        //Fmod Instances
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps"); 
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");

        //Fmod Parameters
        //jump.setParameterByName("jumpState", 0);
    }

    void FixedUpdate()
    {
        float moveVel = Input.GetAxis("Horizontal");
        int jumpState = 0;

        Vector2 targetVel = new Vector2(moveVel * moveSpeed, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothTime);  //smooth movement

        if (Input.GetAxis("Jump") > 0 && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            jumpState = 1;
            jump.setParameterByName("jumpState", jumpState);
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

        if (jumpState == 1 && IsGrounded())
        {
            jumpState = 0;
            jump.setParameterByName("jumpState", jumpState);
            jump.start();
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


}
