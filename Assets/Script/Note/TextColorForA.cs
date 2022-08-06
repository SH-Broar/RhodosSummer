using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextColorForA : MonoBehaviour
{
    private Text tx;
    // Start is called before the first frame update
    void Start()
    {
        tx = gameObject.GetComponent<Text>();

        int i = 0;
        if (!(PlayerPrefs.GetInt("LineColor", 0) % 2 == 0))
            i++;

        if (!(PlayerPrefs.GetInt("HardJudge", 0) % 2 == 0))
            i++;

        if (!(PlayerPrefs.GetInt("Stealth", 0) % 2 == 0))
            i++;

        if (i == 3)
        {
            tx.color = new Color(1f, 0f, 0f);
        }
        else if (i == 2)
        {
            tx.color = new Color(1f, 1f, 0f);
        }
        else if (i == 1)
        {
            tx.color = new Color(1f, 1f, 1f);
        }
    }
}
