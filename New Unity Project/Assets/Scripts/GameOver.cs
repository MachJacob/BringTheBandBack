using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    FMOD.Studio.EventInstance click;
    FMOD.Studio.EventInstance death;

    void Start()
    {
        click = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Click");
        death = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Death");
        death.start(); 
    }
    public void OnReplayPressed()
    {
        // switcher.LoadScene()
        SceneManager.LoadScene(1);
        click.start();
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene(0);
        click.start();
    }
}
