using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemies
{
    public GameObject players_health;

    void Start()
    {
        players_health = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Hit();
        }
    }

    private void Hit()
    {
        //finish once the player has actual health

        //players_health.GetComponent<>
    }

    // Update is called once per frame
    void Update()
    {
        //if(this.gameObject == tag.GetType())
    }
}
