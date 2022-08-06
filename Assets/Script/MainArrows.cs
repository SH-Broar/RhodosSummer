using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainArrows : MonoBehaviour
{
    public Image self;
    public List<Sprite> lists;
    public string stageTag;
    public Text songNamer;
    public SetLevel SL;
    public NextButton NB;
    int distance;

    void Start()
    {
        distance = PlayerPrefs.GetInt("Level", 0) + 1;
        self.sprite = lists[distance];
        //NB.str = "1-1";
        setTT(distance);
        setSN(stageTag);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftA();
            PlayerPrefs.SetInt("Level", distance);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightA();
            PlayerPrefs.SetInt("Level", distance);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SL.set(distance);
        }
    }

    public void LeftA()
    {
        if (distance > 0)
            distance--;
        self.sprite = lists[distance];
        setTT(distance);
        setSN(stageTag);
    }

    public void RightA()
    {
        if (distance < lists.Count - 1)
            distance++;
        self.sprite = lists[distance];
        setTT(distance);
        setSN(stageTag);
    }

    void setTT(int num)
    {
        switch(num)
        {
            case 0:
                stageTag = "0-1";
                break;
            case 1:
                stageTag = "1-1";
                break;
            case 2:
                stageTag = "2-1";
                break;
            case 3:
                stageTag = "2-2";
                break;
            case 4:
                stageTag = "3-1";
                break;
            case 5:
                stageTag = "3-2";
                break;
            case 6:
                stageTag = "4-1";
                break;
            case 7:
                stageTag = "4-2";
                break;
            case 8:
                stageTag = "5-1";
                break;
            case 9:
                stageTag = "6-1";
                break;
            default:
                stageTag = (num-1).ToString();
                break;
        }
        PlayerPrefs.SetInt("Level", num-1);
        PlayerPrefs.SetString("LevelStr", stageTag);
    }

    void setSN(string name)
    {
        switch (name)
        {
            case "0-1":
                songNamer.text = "Mist Moon";
                break;
            case "1-1":
                songNamer.text = "BAD_SECTOR";
                break;
            case "2-1":
                songNamer.text = "compound";
                break;
            case "2-2":
                songNamer.text = "Deformer";
                break;
            case "3-1":
                songNamer.text = "Stages On Grid";
                break;
            case "3-2":
                songNamer.text = "NID -Conflagration-";
                break;
            case "4-1":
                songNamer.text = "Sphere Impact";
                break;
            case "4-2":
                songNamer.text = "Plazma Laser";
                break;
            case "5-1":
                songNamer.text = "INNOCENT";
                break;
            case "6-1":
                songNamer.text = "second dynamo";
                break;
            case "9":
                songNamer.text = "Ice Breaker!!";
                break;
            case "10":
                songNamer.text = "ruins";
                break;
            case "11":
                songNamer.text = "chaos.overcome(true)";
                break;
            case "12":
                songNamer.text = "interpreted artifact";
                break;
            case "13":
                songNamer.text = "Impel vibe [battle field]";
                break;
            case "14":
                songNamer.text = "Instant Phenomenon";
                break;
            case "15":
                songNamer.text = "Lost April";
                break;
        }
        NB.str = stageTag;
    }
}
