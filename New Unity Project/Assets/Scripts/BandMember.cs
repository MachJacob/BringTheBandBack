using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandMember : MonoBehaviour
{
    [SerializeField] LayerMask platformLayerMask;

    bool inAir = false;

    FMOD.Studio.EventInstance jump;

    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        //Fmod Instances
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");

        //Fmod Parameters
        jump.setParameterByName("jumpState", 0);

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
            Debug.Log(velocity);
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
        }
        Destroy(GetComponent<Collider2D>());
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
}
