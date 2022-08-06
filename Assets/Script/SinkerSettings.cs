using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SinkerSettings : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Update()
    {
        txt.text = PlayerPrefs.GetInt("Sinks", 0).ToString() + "'";
    }

    public void up()
    {
        PlayerPrefs.SetInt("Sinks", PlayerPrefs.GetInt("Sinks", 0) + 1);
    }
    public void down()
    {
        PlayerPrefs.SetInt("Sinks", PlayerPrefs.GetInt("Sinks", 0) - 1);
    }

}
