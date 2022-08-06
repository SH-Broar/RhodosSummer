using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coll : MonoBehaviour
{
    public void Start()
    {
        //PlayerPrefs.SetInt("LineColor", 0);
        //PlayerPrefs.SetInt("HardJudge", 0);
        //PlayerPrefs.SetInt("Stealth", 0);
    }
    public void btn()
    {
        PlayerPrefs.SetInt("HardJudge", PlayerPrefs.GetInt("HardJudge", 0) + 1);
    }
    public void btn2()
    {
        PlayerPrefs.SetInt("LineColor", PlayerPrefs.GetInt("LineColor", 0) + 1);
    }
    public void btn3()
    {
        PlayerPrefs.SetInt("Stealth", PlayerPrefs.GetInt("Stealth", 0) + 1);
    }

    public void catcher(string str)
    {

    }
}
