using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    //private GameObject rangedObject;
    public GameObject playersHealth;
    public float damage = 10.0f;

    public GameObject[] rangedGameobject;
    private Transform[] exitPoints;

    private float speed;

    private Transform target;

    public GameObject bullet;
    private float fireRate;
    private float nextFire;

    FMOD.Studio.EventInstance spit;

    void Start()
    {
        //playersHealth = GameObject.FindWithTag("Player");
        //RangedRB = GetComponent<Rigidbody2D>();

        fireRate = 5f;
        nextFire = Time.deltaTime;

        spit = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy2/Attack");
    }
    public void Shoot()
    {
        //Instantiate(rangedGameobject[0], transform.position, Quaternion.identity);
        //target = GameObject.Find("Player").transform;
        //Vector2 direction = target.position - transform.position;
        //RangedRB.velocity = direction.normalized * speed;

        if(Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;

            spit.start();
        }

    }
    void FixedUpdate()
    {
        Shoot();
    }
}
