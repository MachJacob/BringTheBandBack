using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cymbol : MonoBehaviour
{
    void Start()
    {
        //sticks = FMODUnity.RuntimeManager.CreateInstance("event:/Drums/Sticks");
        //sticks.start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
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
