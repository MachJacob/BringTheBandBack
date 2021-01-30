using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandMember : MonoBehaviour
{
    [SerializeField] LayerMask platformLayerMask;

    bool inAir = false;

    FMOD.Studio.EventInstance footstep;
    FMOD.Studio.EventInstance jump;

    // Start is called before the first frame update
    void Start()
    {
        //Fmod Instances
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps");
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");
        footstep.start();
        //Fmod Parameters
        jump.setParameterByName("jumpState", 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGrounded() && !inAir)
        {
            inAir = true;
            jump.setParameterByName("jumpState", 1);
            jump.start();
        }

        if (IsGrounded() && inAir)
        {
            inAir = false;
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
}
