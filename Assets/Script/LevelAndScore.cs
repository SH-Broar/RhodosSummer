using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelAndScore : MonoBehaviour
{
    public MainArrows MA;
    public Text level;
    public Text Score;


    // Update is called once per frame
    void Update()
    {
        Score.text = PlayerPrefs.GetInt("Score" + MA.stageTag, 0).ToString();
        int c = PlayerPrefs.GetInt("Contidion" + MA.stageTag, 0);

        Score.color = Color.white;
        //if (PlayerPrefs.GetInt("Clear" + MA.stageTag, 0) > 0)
        //{
        //    if (c == 1)
        //    {
        //        Score.color = Color.white;
        //    }
        //    else if (c == 2)
        //    {
        //        Score.color = Color.yellow;

        //    }
        //    else if (c == 3)
        //    {
        //        Score.color = Color.red;
        //    }
        //}
        //else
        //{
        //    Score.color = Color.gray;
        //}

        switch (MA.stageTag)
        {
            case "0-1":
                level.text = "1";
                level.color = Color.blue;
                break;
            case "1-1":
                level.text = "2";
                level.color = Color.blue;
                break;
            case "2-1":
                level.text = "3";
                level.color = Color.blue;
                break;
            case "2-2":
                level.text = "3";
                level.color = Color.blue;
                break;
            case "3-1":
                level.text = "4";
                level.color = Color.green;
                break;
            case "3-2":
                level.text = "5";
                level.color = Color.green;
                break;
            case "4-1":
                level.text = "8";
                level.color = Color.red;
                break;
            case "4-2":
                level.text = "7";
                level.color = Color.red;
                break;
            case "5-1":
                level.text = "8";
                level.color = Color.red;
                break;
            case "6-1":
                level.text = "10";
                level.color = new Color (0.2f,0.07f,0.3f);
                break;
            case "9":
                level.text = "4";
                level.color = Color.green;
                break;
            case "10":
                level.text = "5";
                level.color = Color.green;
                break;
            case "11":
                level.text = "7";
                level.color = Color.red;
                break;
            case "12":
                level.text = "9";
                level.color = Color.red;
                break;
            case "13":
                level.text = "10";
                level.color = new Color(0.2f, 0.07f, 0.3f);
                break;
            case "14":
                level.text = "6";
                level.color = Color.green;
                break;
            case "15":
                level.text = "9";
                level.color = Color.red;
                break;


            default:
                level.text = "S";
                break;
        }
    }
}
