using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lines : MonoBehaviour
{
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
        line.useWorldSpace = false;
        line.positionCount = 361;

        line.material.SetColor("_Color", Color.white);

        GameObjectEx.DrawCircle(gameObject, 3f, ref line);
    }

    [System.Obsolete]
    void Update()
    {
        float[] SpectrumData = AudioListener.GetSpectrumData(2048, 0, FFTWindow.Hamming);

        Vector2 FireScale = line.transform.localScale;
        FireScale.x = 0.5f + Mathf.Pow(SpectrumData.Sum() / 16f, 2f) * 3f;
        FireScale.y = 0.5f + Mathf.Pow(SpectrumData.Sum() / 16f, 2f) * 3f;
        line.transform.localScale = Vector2.MoveTowards(line.transform.localScale, FireScale, 0.2f);

    }
}
