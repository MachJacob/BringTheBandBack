using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D enemy_rigidbody;

    public float enemy_health = 50f;
    public float drumstickDamage;
    //public float walking_distance = 10.0f;

    public GameObject drumStickObject;

    void Start()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void DealDamage(float damage)
    {
        enemy_health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if (/*collision.gameObject.CompareTag("Player") &&*/ drumStickObject   /* && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 1*/)
        //{
        //    DealDamage(50);
        //}

        if (collision.gameObject.CompareTag("Drumstick"))
        {
            DealDamage(drumstickDamage);
        }
    }
}
