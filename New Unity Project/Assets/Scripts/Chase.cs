using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{

    [SerializeField]
    public Transform playerTransform;

    public GameObject enemyShooter;

    public float moveSpeed = 0.1f;
    public float moveCooldown;
    public float timer;
    public float rotationSpeed = 5;

    public float maxDistance = 10.0f;
    public float minDistance = 1.0f;

    private float gravity = 12.0f;

    public Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        moveCooldown = 3f;
        timer = moveCooldown;
    }
    void FixedUpdate()
    {
        //if (timer > 0)
        //{
        //    timer -= Time.deltaTime;
        //}
        //if (timer <= 0)
        //{
        //    ChaseFunction();
        //    timer = moveCooldown;
        //}

        ChaseFunction();
    }

    public void ChaseFunction()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance >= minDistance && Vector2.Distance(transform.position, playerTransform.position) < 15)
        {
            Vector3 moveForce = playerTransform.transform.position - transform.position;
            moveForce.y = 0;
            rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y) - Vector2.up * Vector2.right, moveSpeed * Time.deltaTime));
            
            moveForce.Normalize();
            rb.AddForce(moveForce * moveSpeed);

            //if (transform.position.y >= moveForce.y)
            //{
                  //playerTransform.transform.position * -Vector2.up.x, moveSpeed
            //    transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            //}

            //if (transform.position.y <= 2.75f)
            //{
            //     //transform.position = new Vector3(0.0f, -2.0f, 0.0f);
            //    //rb.AddForce(new Vector2(0, transform.up.y / gravity), ForceMode2D.Impulse);
            //}

        }

        if (distance <= maxDistance)
        {
            
        }
    }
}











//transform.LookAt(playerTransform);

//if(Vector2.Distance(transform.position , playerTransform.position) >= minDistance)
//{
//    transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
//}
//if (Vector2.Distance(transform.position, playerTransform.position) <= maxDistance)
//{
//    //Here Call any function U want Like Shoot at here or something
//}

//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.transform.position - transform.position), rotationSpeed * Time.deltaTime);

//transform.position += transform.forward * Time.deltaTime * moveSpeed;


//float distance = Vector3.Distance(transform.position, playerTransform.position);

//if (distance < maxDistance)
//{
//    //Vector3 moveForce = playerTransform.transform.position - transform.position;
//    rb.MovePosition(Vector2.MoveTowards(transform.position, playerTransform.transform.position, moveSpeed));
//    //moveForce.Normalize();
//    //rb.AddForce(moveForce * moveSpeed);
//}





