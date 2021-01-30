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

    FMOD.Studio.EventInstance damageTaken;

    void Start()
    {
        health = maxHealth;

        damageTaken = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Damage");
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        damageTaken.start(); 
    }

    void Update()
    {
        //something something update player stats
    }
}
