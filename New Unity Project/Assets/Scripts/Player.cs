using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("The Players Stats")]
    [SerializeField] 
    float maxHealth = 100;
    private float health;
    private bool alive;

    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }

    void Update()
    {
        //something something update player stats
    }
}
