using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour
{
    FMOD.Studio.EventInstance stickThrow;
    FMOD.Studio.EventInstance stickCollision;
    void Start()
    {
        stickCollision = FMODUnity.RuntimeManager.CreateInstance("event:/Enviroment/Objects/Drumsticks/Collision");
        stickThrow = FMODUnity.RuntimeManager.CreateInstance("event:/Enviroment/Objects/Drumsticks/Launch");
        stickThrow.start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        stickCollision.setParameterByName("SticksContact", 1);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            stickCollision.setParameterByName("StickCollision", 0);
            stickCollision.start();
        }
        else
        {
            stickCollision.setParameterByName("StickCollision", 1);
            stickCollision.start();
        }
     }
}
