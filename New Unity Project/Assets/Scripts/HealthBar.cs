using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image backBar, fullBar;
    public float fill = 100f;

    void Start()
    {
        backBar = transform.GetChild(0).GetComponent<Image>();
        fullBar = transform.GetChild(1).GetComponent<Image>();
    }
    public void SetHealth(int health)
    {

    }
    void Update()
    {
        fullBar.fillAmount = fill;
    }
}
