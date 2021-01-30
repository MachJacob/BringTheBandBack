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

    FMOD.Studio.EventInstance footstep;
    FMOD.Studio.EventInstance jump;

    [SerializeField] LayerMask platformLayerMask;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveVel = Input.GetAxis("Horizontal");
        int jumpState = 0;

        Vector2 targetVel = new Vector2(moveVel * moveSpeed, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothTime);

        if (Input.GetAxis("Jump") > 0 && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            jumpState = 1;
            jump.setParameterByName("jumpState", jumpState);
            jump.start();
        }

        if (rb.velocity.x > 0.1)
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

    private bool IsGrounded()
    {
        float extraHeight = .75f;
        RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, extraHeight, platformLayerMask);

        return raycastHit.collider != null;
    }
}
