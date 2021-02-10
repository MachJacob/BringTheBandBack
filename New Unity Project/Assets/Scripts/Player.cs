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
    FMOD.Studio.EventInstance deathSound;

    void Start()
    {
        health = maxHealth;
        alive = true;
        damageTaken = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Damage");
        deathSound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Death");
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        damageTaken.start();

        if (health <= 0)
        {
            alive = false;
            //Destroy(this.gameObject);
        }
    }

    public void GainHealth(float healthUP)
    {
        health += healthUP;
    }

    public void Update()
    {
        //Update for player stats & stuff

        if (health <= 0 && alive == false)
        {
            Debug.Log("You are dead............");
            deathSound.start();
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }

        if (healthbar) //healthbar stat
        {
            healthbar.fill = health / maxHealth;
        }
    }
}
