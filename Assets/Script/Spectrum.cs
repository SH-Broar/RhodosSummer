using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spectrum : MonoBehaviour
{
    public int CreateStickNum = 0;          //생성될 막대 개수
    public float interval = 0;              //생성될 막대 간격
    public List<GameObject> Sticks;         //GameObject 막대 생성
    public List<GameObject> _Sticks;         //GameObject 막대 생성

    public GameObject Yellow = null;        // 노란색 막대 선언
    public GameObject Image = null;
    public GameObject Image2 = null;

    void Awake()
    {
        for (int i = 0; i < CreateStickNum; i++)
        {
            GameObject obj = (GameObject)Instantiate(Yellow);                   //
            obj.transform.parent = GameObject.Find("Spectrum").transform;       //  노란색 막대를 interval 만큼의 간격으로
            obj.transform.localScale = new Vector3(0.08f,0.5f);                             //  CreateStickNum개수만큼 생성
            obj.transform.localPosition = new Vector2(0 + interval * i, 0);     //
            obj.transform.localRotation = Quaternion.Euler(0,0,0);

            GameObject _obj = (GameObject)Instantiate(Yellow);                   //
            _obj.transform.parent = GameObject.Find("Spectrum2").transform;       //  노란색 막대를 interval 만큼의 간격으로
            _obj.transform.localScale = new Vector3(0.08f, 0.5f);                             //  CreateStickNum개수만큼 생성
            _obj.transform.localPosition = new Vector2(0 + interval * i, 0);     //
            _obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Sticks.Add(obj);                                                    // 생성된 막대를 list에 추가
            _Sticks.Add(_obj);                                                    // 생성된 막대를 list에 추가
        }
    }

    [System.Obsolete]
    void Update()
    {
        float[] SpectrumData = AudioListener.GetSpectrumData(2048, 0, FFTWindow.Hamming);          // 스펙트럼데이터 배열에 오디오가 듣고있는 스펙트럼데이터를 대입
        for (int i = 0; i < Sticks.Count; i++)
        {
            Vector2 FirstScale = Sticks[i].transform.localScale;                                    // 처음 막대기 스케일을 변수로 생성
            FirstScale.y = 0.0f + SpectrumData[i] * 18;                                            // 막대기 y를 스펙트럼데이터에 맞게 늘림
            Sticks[i].transform.localScale = Vector2.MoveTowards(Sticks[i].transform.localScale, FirstScale, 0.2f);     // 스펙트럼데이터에 맞게 늘어난 스케일을 처음스케일로 0.1의 속도만큼 바꿈
        }
        for (int i = 0; i < _Sticks.Count; i++)
        {
            Vector2 FirstScale = _Sticks[i].transform.localScale;                                    // 처음 막대기 스케일을 변수로 생성
            FirstScale.y = 0.0f + SpectrumData[i] * 18;                                            // 막대기 y를 스펙트럼데이터에 맞게 늘림
            _Sticks[i].transform.localScale = Vector2.MoveTowards(_Sticks[i].transform.localScale, FirstScale, 0.2f);     // 스펙트럼데이터에 맞게 늘어난 스케일을 처음스케일로 0.1의 속도만큼 바꿈
        }

        Vector2 FireScale = Image.transform.localScale;
        FireScale.x = 0.6f + 1.2f * SpectrumData.Sum() / 32f;
        FireScale.y = 0.6f + 1.2f * SpectrumData.Sum() / 32f;
        Image.transform.localScale = Vector2.MoveTowards(Image.transform.localScale, FireScale, 0.2f);

    }
}