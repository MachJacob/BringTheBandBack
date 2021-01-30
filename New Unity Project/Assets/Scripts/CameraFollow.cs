using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Vector3 offset;
    private Vector3 vel;
    public float smoothTime;
    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref vel, smoothTime);
    }
}
