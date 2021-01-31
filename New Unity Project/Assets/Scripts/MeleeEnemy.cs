using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemies
{
    public GameObject playersHealth;
    //public GameObject hitObject;

    public float damage = 20.0f;
    private float minAttackDistance;

    FMOD.Studio.EventInstance attack;

    void Start()
    {
        playersHealth = GameObject.FindWithTag("Player");

        attack = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/Eat");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Hit();
            //collision.gameObject.GetComponent<Player>().DealDamage(damage);

            attack.setParameterByName("Eat", 0);
            attack.start();
        }
    }

    public void Hit()
    {
        playersHealth.GetComponent<Player>().DealDamage(damage);
        //Debug.Log("Oof, my health is: " + playersHealth);
    }
    void Update()
    {
        
    }
}
