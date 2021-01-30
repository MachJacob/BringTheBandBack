using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject playersHealth;

    public float moveSpeed;

    Rigidbody2D rb;

    Player target;

    Vector2 moveDirection;

    public float damage = 10f;

    FMOD.Studio.EventInstance bullet;

    void Start()
    {
        bullet = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy2/Bullet");
        bullet.setParameterByName("BulletHit", 0);
        bullet.start();

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        //rb.velocity = moveDirection.normalized;
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bullet.setParameterByName("BulletHit", 1);

        if (collision.gameObject.tag == "Player")
        {
            //playersHealth.GetComponent<Player>().DealDamage(10f);
            collision.gameObject.GetComponent<Player>().DealDamage(damage / 2);
            Debug.Log("HIT");

            bullet.setParameterByName("BulletCollision", 1);
            bullet.start();

            Destroy(gameObject);
        }
        else
        {
            bullet.setParameterByName("BulletHit", 1);
            bullet.setParameterByName("BulletCollision", 0);
            bullet.start();
        }
    }
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //playersHealth.GetComponent<Player>().DealDamage(10.0f);
    //        //collision.gameObject.GetComponent<Player>().DealDamage(10f);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
