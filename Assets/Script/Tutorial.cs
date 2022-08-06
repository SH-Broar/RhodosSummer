using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    void Start()
    {
        Invoke("next", 10f);
    }

    void next()
    {
        SceneManager.LoadScene("level" + PlayerPrefs.GetString("LevelStr", "1-1"));
    }
}
