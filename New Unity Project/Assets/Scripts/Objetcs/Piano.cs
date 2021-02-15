using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    FMOD.Studio.EventInstance Rolling;
    private GameObject piano;

    // Start is called before the first frame update
    void Start()
    {
        Rolling = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/Piano/Rolling");
        piano = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D RB = piano.GetComponent<Rigidbody2D>();
        Vector2 velocity = RB.velocity;
        float speed = velocity.magnitude;
        if (speed != 0)
        {
            Rolling.start();
        }
        else
        {
            Rolling.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    void OnDestroy()
    {
        Rolling.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
