using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public GameObject playersHealth;
    public Text text;
    int score;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "" + score.ToString();

        if ((score == 8) || (score == 16) || (score == 24) || (score == 32) && playersHealth.GetComponent<Player>().health != 100)
        {
            playersHealth.GetComponent<Player>().GainHealth(20);
        }

    }
}

