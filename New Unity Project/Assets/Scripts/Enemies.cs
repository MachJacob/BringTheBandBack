using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D enemy_rigidbody;

    float enemy_health = 50f;
    //public float walking_distance = 10.0f;

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
}
