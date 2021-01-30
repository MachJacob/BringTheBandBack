using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemies
{
    public GameObject playersHealth;
    public GameObject hitObject;

    public float damage = 25.0f;

    void Start()
    {
        playersHealth = GameObject.FindWithTag("Player");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Hit();
            //collision.gameObject.GetComponent<Player>().DealDamage(damage);
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
