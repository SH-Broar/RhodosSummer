using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevel : MonoBehaviour
{
    public Animator anim;
    public Animator Lines;
    public Animator clears;
    public GameObject Arrowpanel;

    private AudioSource Audio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void set(int i)
    {
        //if (PlayerPrefs.GetInt("Level",1) == i)
        {
            Audio.Play();

            //PlayerPrefs.SetInt("Level", i);
            //Lines.SetTrigger("GameStart");
            //clears.SetTrigger("GameStart");
            anim.SetTrigger("GameStart");
            //SceneManager.LoadScene("level" + i);
        }

    }
}
