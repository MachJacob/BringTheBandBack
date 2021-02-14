using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject controlsPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void PauseGame()

    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
