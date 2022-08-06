using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorSetting : MonoBehaviour
{
    public Animator self;
    public NextButton nb;
    public Image tri;
    public string tt;
    void Start()
    {
        self.SetInteger("ClearTag", PlayerPrefs.GetInt("Clear" + tt, 0));
        int c =PlayerPrefs.GetInt("Contidion" + tt, 0);

        if (c == 1)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        else if (c == 2)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 0f);
        }
        else if (c == 3)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
        }
    }

    public void setPR()
    {
        if (PlayerPrefs.GetInt("Clear" + tt, 0) > 0)
        {
            nb.str = tt;
            tri.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 25f, 1f);

            switch(tt)
            {
                case "1-1":
                    tri.GetComponentInChildren<Text>().text = "BAD_SECTOR";
                    break;
                case "2-1":
                    tri.GetComponentInChildren<Text>().text = "compound";
                    break;
                case "2-2":
                    tri.GetComponentInChildren<Text>().text = "Deformer";
                    break;
                case "3-1":
                    tri.GetComponentInChildren<Text>().text = "Stages On Grid";
                    break;
                case "3-2":
                    tri.GetComponentInChildren<Text>().text = "NID -Conflagration-";
                    break;
                case "4-1":
                    tri.GetComponentInChildren<Text>().text = "Sphere Impact";
                    break;
                case "4-2":
                    tri.GetComponentInChildren<Text>().text = "Plazma Laser";
                    break;
                case "5-1":
                    tri.GetComponentInChildren<Text>().text = "INNOCENT";
                    break;
                case "6-1":
                    tri.GetComponentInChildren<Text>().text = "second dynamo";
                    break;
            }

            tri.GetComponentInChildren<Text>().text += "\n" + PlayerPrefs.GetInt("Score" + tt, 0);
        }
    }
}
