using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnReplayPressed()
    {
        // switcher.LoadScene()
        SceneManager.LoadScene(1);
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
