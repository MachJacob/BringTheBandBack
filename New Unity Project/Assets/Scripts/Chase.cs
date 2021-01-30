using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{

    [SerializeField]
    public Transform playerTransform;

    public float moveSpeed = 0.1f;
    public float moveCooldown;
    public float timer;
    public float rotationSpeed = 5;

    public float maxDistance = 10.0f;
    public float minDistance = 1.0f;

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

        if (distance >= minDistance)
        {
            Vector3 moveForce = playerTransform.transform.position - transform.position;
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerTransform.transform.position * Vector2.right, moveSpeed * Time.deltaTime));
            moveForce.Normalize();
            rb.AddForce(moveForce * moveSpeed);
        }

        else if (distance <= maxDistance)
        {
            //Do something like a long ranged attack maybe? idk arrow shot or something
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





