using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cymbol : MonoBehaviour
{
    FMOD.Studio.EventInstance cymbol;

    void Start()
    {
        cymbol = FMODUnity.RuntimeManager.CreateInstance("event:/Drums/Cymbal");
        cymbol.setParameterByName("CymbalCollision", 0);
        cymbol.start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        cymbol.setParameterByName("CymbalCollision", 1);
        cymbol.start();

        //sticks.setParameterByName("SticksContact", 1);

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    sticks.setParameterByName("StickCollision", 0);
        //    sticks.start();
        //}
        //else
        //{
        //    sticks.setParameterByName("StickCollision", 1);
        //    sticks.start();
        //}
    }
}
