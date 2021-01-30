using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    //private GameObject rangedObject;
    public GameObject playersHealth;
    public float damage = 20.0f;

    public GameObject[] rangedGameobject;
    private Transform[] exitPoints;

    protected Rigidbody2D RangedRB;

    private float speed;

    private Transform target;

    public GameObject bullet;
    private float fireRate;
    private float nextFire;

    void Start()
    {
        //playersHealth = GameObject.FindWithTag("Player");
        //RangedRB = GetComponent<Rigidbody2D>();

        fireRate = 1f;
        nextFire = Time.deltaTime;
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
        }

    }
    void FixedUpdate()
    {
        Shoot();
    }
}
