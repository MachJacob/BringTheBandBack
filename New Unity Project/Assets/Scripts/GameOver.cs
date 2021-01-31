using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    FMOD.Studio.EventInstance click;
    void Start()
    {
        click = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Click");
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
