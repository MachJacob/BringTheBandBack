using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour
{
    FMOD.Studio.EventInstance sticks;
    void Start()
    {
        sticks = FMODUnity.RuntimeManager.CreateInstance("event:/Drums/Sticks");
        sticks.start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            sticks.setParameterByName("SticksContact", 1);
            sticks.start();
        }   
     }
}
