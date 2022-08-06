using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onOffA : MonoBehaviour
{
    public GameObject panel;
    public int col;

    public void Start()
    {
        switch (col)
        {
            case 1:
                if (PlayerPrefs.GetInt("HardJudge", 0) % 2 == 1)
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                    PlayerPrefs.SetInt("HardJudge", 1);
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("LineColor", 0) % 2 == 1)
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                    PlayerPrefs.SetInt("LineColor", 1);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("Stealth", 0) % 2 == 1)
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                    PlayerPrefs.SetInt("Stealth", 1);
                }
                break;
        }
    }

    public void MO()
    {
        panel.SetActive(true);
    }

    public void ME()
    {
        panel.SetActive(false);
    }
}
