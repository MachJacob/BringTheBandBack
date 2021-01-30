using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("The Players Stats")]
    [SerializeField] 
    public float maxHealth = 100f;
    public float health;
    public bool alive;

    public HealthBar healthbar;

    FMOD.Studio.EventInstance damageTaken;

    void Start()
    {
        health = maxHealth;
        alive = true;
        damageTaken = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Damage");
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        damageTaken.start();

        if (health == 0)
        {
            alive = false;
            Debug.Log("IM DED LADS");
        }
    }

    public void Update()
    {
        //something something update player stats




        if (healthbar) //healthbar stat
        {
            healthbar.fill = health / maxHealth;
        }

    }
}
