using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public int level;
    public int stage;
    public string str = "1-1";

    private int isDead;
    private int isFullCombo;

    public void next()
    {

        if (level == -1 && stage == -1)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            Time.timeScale = 1;
        }
        else//나가셈 그냥
        {
            SceneManager.LoadScene("MainScene");
        }
        if (level >= 10)
        {
            SceneManager.LoadScene("level" + (level + 1));
        }
        else if (level == 0 && stage == 0)
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (PlayerPrefs.GetInt("isLife") == 0)
        { 
        }
        else if (level == 1 && stage == 1)
        {
            if (PlayerPrefs.GetInt("MISS", 100) <= 0)
            {
                SceneManager.LoadScene("level2-2");
            }
            else
            {
                SceneManager.LoadScene("level2-1");
            }
        }
        else if (level == 6)
        {
            SceneManager.LoadScene("End");
        }
        else
        {
            if (level == 4)
            {
                if (PlayerPrefs.GetInt("Clear4-1", 0) > 0 && PlayerPrefs.GetInt("Clear4-2", 0) > 0)
                {
                    SceneManager.LoadScene("level5-1");
                }
                else
                {
                    SceneManager.LoadScene("End");
                }
            }
            else
            {
                SceneManager.LoadScene("level" + (level + 1) + "-" + stage);
            }
        }
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            SceneManager.LoadScene("MainScene2");
        }
        else
        {
            SceneManager.LoadScene("level" + str);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
