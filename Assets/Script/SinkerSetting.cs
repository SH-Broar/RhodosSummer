using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SinkerSetting : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Update()
    {
        txt.text =  PlayerPrefs.GetInt("Sink", 0).ToString() + "ms";
    }

    public void up()
    {
        PlayerPrefs.SetInt("Sink", PlayerPrefs.GetInt("Sink", 0) + 1);
    }
    public void down()
    {
        PlayerPrefs.SetInt("Sink", PlayerPrefs.GetInt("Sink", 0) - 1);
    }
    
}
