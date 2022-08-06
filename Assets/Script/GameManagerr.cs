using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerr : MonoBehaviour
{
    public Image img1;
    public Image img11;
    public Image img2;
    public Image img3;
    public GameObject BaseAnim;

    public GameObject EndPage;
    public Animator EPButton1;
    public Animator EPButton2;
    public Animator EPButton3;
    public AudioSource AC;

    public AudioSource FX;
    public AudioSource FX2;
    public bool NoteFX;

    public Animator barAnim;
    public RectTransform bar;

    public GameObject Red;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Purple;
    public RectTransform PatternHead;
    public Image Fill;

    private List<GameObject> R;
    private List<GameObject> Y;
    private List<GameObject> G;
    private List<GameObject> B;
    private List<GameObject> P;
    public Text CurrCombo;
    public Animator CurrComboAnim;
    public GameObject CurrComboGO;
    public int ComboTextOperrand;
    private Vector3 outerVec = new Vector3(5.8f, 0, 0);

    public Text Combo;
    private List<int> judgeNums;
    public string strForResult;

    public Text Result;
    private int combo;
    private int maxCombo;
    private int Score;
    private int HP;
    private bool isLife;

    private bool stealthBar;
    public bool lineColorOn;
    private bool hardJudge;

    private float t;

    public float perfectMS;

    private float oneBeat;
    private float barBeat;
    public float BPM;
    public float waitingOffset;
    public float waitingBeats;
    public double totalLongs;
    public double totalPipes;
    public string pattern;

    public float sinker;
    public float sinkers;

    public int HPDamage;
    public int HPRecover;

    private bool start = false;

    private bool endLock;

    private int[] noteCountForPooling;
    private int judge;

    //초당 회전각
    private double ticker;
    private float k;
    private int totalTagForThisNote;
    private int tmpTagForThisNote;
    private bool[] FtmpTagForThisNote = new bool [4];
    private bool[] FTagForThisNote = new bool[4];

    private int ignoreCounts;
    private int preBar;
    //---------------------------

    public Queue<(string, (float, int))> noteQueue = new Queue<(string, (float, int))>();

    void Start()
    {
        img1.fillAmount = 0;
        img11.fillAmount = 0;
        img2.fillAmount = 0;
        img3.fillAmount = 0;

        judge = 0;
        isLife = true;

        endLock = true;

        if (HPDamage == 0)
            HPDamage = 70;
        if (HPRecover == 0)
            HPRecover = 2;

        R = new List<GameObject>();
        Y = new List<GameObject>();
        G = new List<GameObject>();
        B = new List<GameObject>();
        P = new List<GameObject>();

        judgeNums = new List<int> { 0, 0, 0, 0 };
        Score = 0;
        combo = 0;
        HP = 1000;

        totalTagForThisNote = 0;

        ticker = totalPipes * 360.0 / totalLongs;

        oneBeat = 60f / BPM;
        barBeat = oneBeat * 4f;

        ignoreCounts = 0;

        sinker = PlayerPrefs.GetInt("Sink") * 0.001f;
        sinkers = PlayerPrefs.GetInt("Sinks");

        PlayerPrefs.SetInt("isLife", 1);

        ComboTextOperrand = PlayerPrefs.GetInt("ComboText", 0);

        noteCountForPooling = new int[5] { 0, 0, 0, 0, 0 };

        if (PlayerPrefs.GetInt("NoteFX",0) % 2 == 0)
        {
            NoteFX = false;
        }
        else
        {
            NoteFX = true;
        }

        if (PlayerPrefs.GetInt("LineColor", 0) % 2 == 0)
            lineColorOn = false;
        else
            lineColorOn = true;

        if ((PlayerPrefs.GetInt("HardJudge", 0) % 2 == 0))
            hardJudge = false;
        else
        {
            perfectMS *= 0.5f;
            hardJudge = true;
        }

        if (PlayerPrefs.GetInt("Stealth", 0) % 2 == 0)
            stealthBar = false;
        else
            stealthBar = true;

        bar.Rotate(0, 0, sinkers);

        StartCoroutine(GameStart());

    }

    void Update()
    {
        if (start)
        {
            k = Time.time - t;
            #region 노트 처리
            if (k < totalLongs)
            {
                judge = 2;

                if (Input.anyKeyDown)
                {
                    tmpTagForThisNote = 0;
                    FtmpTagForThisNote[0] = false;
                    FtmpTagForThisNote[1] = false;
                    FtmpTagForThisNote[2] = false;
                    FtmpTagForThisNote[3] = false;
#if UNITY_ANDROID
                    tmpTagForThisNote = Input.touchCount;
#else
                    if (Input.GetKeyDown(KeyCode.A))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.D))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.F))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.J))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.K))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.S))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.L))
                        tmpTagForThisNote++;
                    if (Input.GetKeyDown(KeyCode.Semicolon))
                        tmpTagForThisNote++;

                    if (Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.F7) )
                        FtmpTagForThisNote[0] = true;
                    if (Input.GetKeyDown(KeyCode.F5) || Input.GetKeyDown(KeyCode.F8))
                        FtmpTagForThisNote[1] = true;
                    if (Input.GetKeyDown(KeyCode.F3) || Input.GetKeyDown(KeyCode.F4) || Input.GetKeyDown(KeyCode.F9) || Input.GetKeyDown(KeyCode.F10))
                        FtmpTagForThisNote[2] = true;
                    if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.F11) || Input.GetKeyDown(KeyCode.F12))
                        FtmpTagForThisNote[3] = true;

                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        ComboTextOperrand = 0;
                        PlayerPrefs.SetInt("ComboText", ComboTextOperrand);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        ComboTextOperrand = 1;
                        PlayerPrefs.SetInt("ComboText", ComboTextOperrand);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        ComboTextOperrand = 2;
                        PlayerPrefs.SetInt("ComboText", ComboTextOperrand);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        ComboTextOperrand = 3;
                        PlayerPrefs.SetInt("ComboText", ComboTextOperrand);
                    }
#endif


                    if (noteQueue.Count > 0)
                    {
                        if (tmpTagForThisNote != 0 || FtmpTagForThisNote[0] == true || FtmpTagForThisNote[1] == true || FtmpTagForThisNote[2] == true || FtmpTagForThisNote[3] == true)
                        {
                            print("key : " + k + " latest note : " + noteQueue.Peek().Item2.Item1 + " decay : " + (noteQueue.Peek().Item2.Item1 - k + sinker));

                            //print("k : " + k + " note : " + noteQueue.Peek().Item2);
                            //Perfect
                            if (k >= noteQueue.Peek().Item2.Item1 - perfectMS + sinker && k <= noteQueue.Peek().Item2.Item1 + perfectMS + sinker)
                            {
                                totalTagForThisNote += tmpTagForThisNote;
                                FTagForThisNote[0] = FtmpTagForThisNote[0];
                                FTagForThisNote[1] = FtmpTagForThisNote[1];
                                FTagForThisNote[2] = FtmpTagForThisNote[2];
                                FTagForThisNote[3] = FtmpTagForThisNote[3];
                                CurrCombo.text = "Art!!";
                                CurrCombo.color = Color.magenta;
                                print("perfect");
                            }
                            //Good
                            else if (k >= noteQueue.Peek().Item2.Item1 - (perfectMS * 1.5f) + sinker && k <= noteQueue.Peek().Item2.Item1 + (perfectMS * 1.5f) + sinker)
                            {
                                totalTagForThisNote += tmpTagForThisNote;
                                FTagForThisNote[0] = FtmpTagForThisNote[0];
                                FTagForThisNote[1] = FtmpTagForThisNote[1];
                                FTagForThisNote[2] = FtmpTagForThisNote[2];
                                FTagForThisNote[3] = FtmpTagForThisNote[3];
                                judge = 1;
                                CurrCombo.text = "Good!";
                                CurrCombo.color = Color.blue;
                                print("good");
                            }
                            //Bad
                            else if (k >= noteQueue.Peek().Item2.Item1 - (perfectMS * 2f) + sinker && k <= noteQueue.Peek().Item2.Item1 + (perfectMS * 2f) + sinker)
                            {
                                //print("out");
                                totalTagForThisNote += tmpTagForThisNote;
                                FTagForThisNote[0] = FtmpTagForThisNote[0];
                                FTagForThisNote[1] = FtmpTagForThisNote[1];
                                FTagForThisNote[2] = FtmpTagForThisNote[2];
                                FTagForThisNote[3] = FtmpTagForThisNote[3];
                                judge = 0;
                                CurrCombo.text = "Miss";
                                CurrCombo.color = Color.gray;
                            }

                            //노트 처리
                            if (ConvertPatternToNums(noteQueue.Peek().Item1) == totalTagForThisNote || CompareNoteAndFTag(FTagForThisNote,noteQueue.Peek().Item1))
                            {
                                if (judge == 0)
                                {
                                    judge = 0;
                                    print("run");
                                    totalTagForThisNote = 0;
                                    FTagForThisNote[0] = false;
                                    FTagForThisNote[1] = false;
                                    FTagForThisNote[2] = false;
                                    FTagForThisNote[3] = false;
                                    ignoreCounts--;
                                    judgeNums[0]++;
                                    combo = 0;
                                    HP -= HPDamage;
                                    Score += (int)((0.1f - (noteQueue.Peek().Item2.Item1 - k + sinker)) * 10f);
                                    CurrComboGO.transform.position = deListNote(noteQueue.Peek().Item1, noteQueue.Peek().Item2.Item2, judge);
                                    noteQueue.Dequeue();
                                }
                                else
                                {
                                    totalTagForThisNote = 0;
                                    FTagForThisNote[0] = false;
                                    FTagForThisNote[1] = false;
                                    FTagForThisNote[2] = false;
                                    FTagForThisNote[3] = false;
                                    ignoreCounts--;
                                    if (judge == 2)
                                    {
                                        if (NoteFX)
                                        {
                                            FX.Play();
                                        }

                                        judgeNums[2]++;
                                    }
                                    else
                                    {
                                        if (NoteFX)
                                        {
                                            FX2.Play();
                                        }

                                        judgeNums[1]++;
                                    }
                                    combo++;
                                    CurrCombo.text += "\n" + combo;
                                    CurrComboAnim.SetTrigger("b");
                                    

                                    HP += HPRecover * judge;
                                    Score += 100 * judge + (10 * combo) + (int)((0.1f - (noteQueue.Peek().Item2.Item1 - k + sinker)) * 10f);
                                    CurrComboGO.transform.position = deListNote(noteQueue.Peek().Item1, noteQueue.Peek().Item2.Item2, judge);
                                    noteQueue.Dequeue();
                                    //print("pop!");
                                }
                            }

                        }
                    }
                }

                //놓침
                if (noteQueue.Count > 0)
                {
                    if (k > noteQueue.Peek().Item2.Item1 + (perfectMS * 2f) + sinker)
                    {
                        judge = 0;
                        print("run");
                        totalTagForThisNote = 0;
                        FTagForThisNote[0] = false;
                        FTagForThisNote[1] = false;
                        FTagForThisNote[2] = false;
                        FTagForThisNote[3] = false;
                        ignoreCounts--;
                        judgeNums[0]++;
                        combo = 0;
                        HP -= HPDamage;
                        CurrCombo.text = "Miss";
                        CurrCombo.color = Color.gray;
                        Score += (int)((0.1f - (noteQueue.Peek().Item2.Item1 - k + sinker)) * 10f);
                        CurrComboGO.transform.position = deListNote(noteQueue.Peek().Item1, noteQueue.Peek().Item2.Item2, judge);
                        noteQueue.Dequeue();
                    }
                }

                bar.Rotate(0, 0, (float)(-Time.deltaTime * ticker));
                if (HP < 0)
                    isLife = false;
                if (combo > maxCombo)
                    maxCombo = combo;

                if (img11.fillAmount == 1f)
                    Fill.fillAmount = HP / 1000f;
                if (!isLife)
                    Fill.fillAmount = 0f;

                int c = 0;
#region 노트 표시
                foreach (var note in noteQueue)
                {
                    c++;
                    if (c <= ignoreCounts)
                        continue;
                    //보이기 시작
                    if (note.Item2.Item1 < k + 2f * oneBeat)
                    {
                        SetNotePosition(note.Item1, note.Item2.Item1, note.Item2.Item2);
                        ignoreCounts++;
                    }
                    else
                    {
                        break;
                    }
                }
#endregion

#region 둠치
                float b = (bar.localEulerAngles.z) % 90;
                if (b > 70f)
                {
                    BaseAnim.transform.localScale = new Vector3(1f + ((-70f+b) * 0.002f), 1f + ((-70f+b) * 0.002f), 1f);
                }
                else
                {
                    BaseAnim.transform.localScale = new Vector3(1f,1f, 1f);
                }
#endregion
            }
#endregion
            else
            {
                GameEnd();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameEnd(true);
            }
        }
    }

    void SetNotePosition(string noteType, float time, int noteTiming)
    {
        float angle = (time % barBeat) * 360f / barBeat;

        if (!lineColorOn)
        {
            if (noteType == "1" || noteType == "5" || noteType == "6" || noteType == "7" || noteType == "b" || noteType == "c" || noteType == "d" || noteType == "f")
            {
                float distance = 60f;
                //R
                SetNote(distance * Mathf.Sin(Mathf.Deg2Rad * angle), distance * Mathf.Cos(Mathf.Deg2Rad * angle), noteTiming, time);
            }
            if (noteType == "2" || noteType == "5" || noteType == "8" || noteType == "9" || noteType == "b" || noteType == "c" || noteType == "e" || noteType == "f")
            {
                float distance = 99f;
                //Y
                SetNote(distance * Mathf.Sin(Mathf.Deg2Rad * angle), distance * Mathf.Cos(Mathf.Deg2Rad * angle), noteTiming, time);
            }
            if (noteType == "3" || noteType == "6" || noteType == "8" || noteType == "a" || noteType == "b" || noteType == "d" || noteType == "e" || noteType == "f")
            {
                float distance = 138f;
                //G
                SetNote(distance * Mathf.Sin(Mathf.Deg2Rad * angle), distance * Mathf.Cos(Mathf.Deg2Rad * angle), noteTiming, time);

            }
            if (noteType == "4" || noteType == "7" || noteType == "9" || noteType == "a" || noteType == "c" || noteType == "d" || noteType == "e" || noteType == "f")
            {
                float distance = 178f;
                //B
                SetNote(distance * Mathf.Sin(Mathf.Deg2Rad * angle), distance * Mathf.Cos(Mathf.Deg2Rad * angle), noteTiming, time);
            }
        }
        else
        {
            if (noteType == "1" || noteType == "5" || noteType == "6" || noteType == "7" || noteType == "b" || noteType == "c" || noteType == "d" || noteType == "f")
            {
                float distance = 60f;
                //R
                float x = distance * Mathf.Sin(Mathf.Deg2Rad * angle);
                float y = distance * Mathf.Cos(Mathf.Deg2Rad * angle);

                R[noteCountForPooling[0]].SetActive(true);
                R[noteCountForPooling[0]].GetComponent<DoomChi>().SetStart(BPM, time);
                R[noteCountForPooling[0]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[0] %= R.Count;
            }
            if (noteType == "2" || noteType == "5" || noteType == "8" || noteType == "9" || noteType == "b" || noteType == "c" || noteType == "e" || noteType == "f")
            {
                float distance = 99f;
                //Y
                float x = distance * Mathf.Sin(Mathf.Deg2Rad * angle);
                float y = distance * Mathf.Cos(Mathf.Deg2Rad * angle);

                Y[noteCountForPooling[1]].SetActive(true);
                Y[noteCountForPooling[1]].GetComponent<DoomChi>().SetStart(BPM, time);
                Y[noteCountForPooling[1]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[1] %= Y.Count;
            }
            if (noteType == "3" || noteType == "6" || noteType == "8" || noteType == "a" || noteType == "b" || noteType == "d" || noteType == "e" || noteType == "f")
            {
                float distance = 138f;
                //G
                float x = distance * Mathf.Sin(Mathf.Deg2Rad * angle);
                float y = distance * Mathf.Cos(Mathf.Deg2Rad * angle);

                G[noteCountForPooling[2]].SetActive(true);
                G[noteCountForPooling[2]].GetComponent<DoomChi>().SetStart(BPM, time);
                G[noteCountForPooling[2]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[2] %= G.Count;
            }
            if (noteType == "4" || noteType == "7" || noteType == "9" || noteType == "a" || noteType == "c" || noteType == "d" || noteType == "e" || noteType == "f")
            {
                float distance = 178f;
                //B
                float x = distance * Mathf.Sin(Mathf.Deg2Rad * angle);
                float y = distance * Mathf.Cos(Mathf.Deg2Rad * angle);

                B[noteCountForPooling[3]].SetActive(true);
                B[noteCountForPooling[3]].GetComponent<DoomChi>().SetStart(BPM, time);
                B[noteCountForPooling[3]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[3] %= B.Count;
            }
        }
    }

    void SetNote(float x, float y, int noteTiming, float time)
    {
        switch (noteTiming)
        {
            case 1:
                R[noteCountForPooling[0]].SetActive(true);
                R[noteCountForPooling[0]].GetComponent<DoomChi>().SetStart(BPM, time);
                R[noteCountForPooling[0]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[0] %= R.Count;
                break;
            case 2:
                B[noteCountForPooling[3]].SetActive(true);
                B[noteCountForPooling[3]].GetComponent<DoomChi>().SetStart(BPM, time);
                B[noteCountForPooling[3]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[3] %= B.Count;
                break;
            case 3:
                Y[noteCountForPooling[1]].SetActive(true);
                Y[noteCountForPooling[1]].GetComponent<DoomChi>().SetStart(BPM, time);
                Y[noteCountForPooling[1]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[1] %= Y.Count;
                break;
            case 4:
                G[noteCountForPooling[2]].SetActive(true);
                G[noteCountForPooling[2]].GetComponent<DoomChi>().SetStart(BPM, time);
                G[noteCountForPooling[2]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[2] %= G.Count;
                break;
            case 5:
                P[noteCountForPooling[4]].SetActive(true);
                P[noteCountForPooling[4]].GetComponent<DoomChi>().SetStart(BPM, time);
                P[noteCountForPooling[4]++].transform.localPosition = new Vector3(x, y, 0);
                noteCountForPooling[4] %= P.Count;
                break;
        }

    }

    Vector3 DelNote(int noteTiming, int judge)
    {
        Vector3 tmp = Vector3.zero;
        switch (noteTiming)
        {
            case 1:
                //R[0].SetActive(false);
                switch (judge)
                {
                    case 0:
                        R[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        R[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        R[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch(ComboTextOperrand)
                {
                    case 0:
                        tmp = R[0].transform.position;
                        break;
                    case 1:
                        tmp = Vector3.zero;
                        break;
                    case 2:
                        tmp = outerVec;
                        break;
                    case 3:
                        tmp = outerVec * 10f;
                        break;
                        
                }
                noteCountForPooling[0]--;
                R.Add(R[0]);
                R.RemoveAt(0);
                break;
            case 2:
                //B[0].SetActive(false);
                switch (judge)
                {
                    case 0:
                        B[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        B[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        B[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        tmp = B[0].transform.position;
                        break;
                    case 1:
                        tmp = Vector3.zero;
                        break;
                    case 2:
                        tmp = outerVec;
                        break;
                    case 3:
                        tmp = outerVec * 10f;
                        break;
                }
                noteCountForPooling[3]--;
                B.Add(B[0]);
                B.RemoveAt(0);
                break;
            case 3:
                //Y[0].SetActive(false);
                switch (judge)
                {
                    case 0:
                        Y[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        Y[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        Y[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        tmp = Y[0].transform.position;
                        break;
                    case 1:
                        tmp = Vector3.zero;
                        break;
                    case 2:
                        tmp = outerVec;
                        break;
                    case 3:
                        tmp = outerVec * 10f;
                        break;
                }
                noteCountForPooling[1]--;
                Y.Add(Y[0]);
                Y.RemoveAt(0);
                break;
            case 4:
                //G[0].SetActive(false);
                switch (judge)
                {
                    case 0:
                        G[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        G[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        G[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        tmp = G[0].transform.position;
                        break;
                    case 1:
                        tmp = Vector3.zero;
                        break;
                    case 2:
                        tmp = outerVec;
                        break;
                    case 3:
                        tmp = outerVec * 10f;
                        break;
                }
                noteCountForPooling[2]--;
                G.Add(G[0]);
                G.RemoveAt(0);
                break;
            case 5:
                //P[0].SetActive(false);
                switch (judge)
                {
                    case 0:
                        P[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        P[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        P[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        tmp = P[0].transform.position;
                        break;
                    case 1:
                        tmp = Vector3.zero;
                        break;
                    case 2:
                        tmp = outerVec;
                        break;
                    case 3:
                        tmp = outerVec * 10f;
                        break;
                }
                noteCountForPooling[4]--;
                P.Add(P[0]);
                P.RemoveAt(0);
                break;
        }
        return tmp;
    }

    Vector3 deListNote(string noteType, int noteTiming, int judge)
    {
        Vector3 returnVec = Vector3.zero;
        if (!lineColorOn)
        {
            if (noteType == "f")
            {
                returnVec = DelNote(noteTiming, judge);
            }
            if (noteType == "e" || noteType == "d" || noteType == "c" || noteType == "b" || noteType == "f")
            {
                returnVec = DelNote(noteTiming, judge);
            }
            if (noteType == "5" || noteType == "6" || noteType == "7" || noteType == "8" || noteType == "9" || noteType == "a" || noteType == "e" || noteType == "d" || noteType == "c" || noteType == "b" || noteType == "f")
            {
                returnVec = DelNote(noteTiming, judge);
            }

            returnVec = DelNote(noteTiming, judge);
        }
        else
        {
            if (noteType == "1" || noteType == "5" || noteType == "6" || noteType == "7" || noteType == "b" || noteType == "c" || noteType == "d" || noteType == "f")
            {
                switch (judge)
                {
                    case 0:
                        R[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        R[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        R[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        returnVec = R[0].transform.position;
                        break;
                    case 1:
                        returnVec = Vector3.zero;
                        break;
                    case 2:
                        returnVec = outerVec;
                        break;
                    case 3:
                        returnVec = outerVec * 10f;
                        break;
                }
                noteCountForPooling[0]--;
                R.Add(R[0]);
                R.RemoveAt(0);
            }
            if (noteType == "2" || noteType == "5" || noteType == "8" || noteType == "9" || noteType == "b" || noteType == "c" || noteType == "e" || noteType == "f")
            {
                switch (judge)
                {
                    case 0:
                        Y[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        Y[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        Y[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        returnVec = Y[0].transform.position;
                        break;
                    case 1:
                        returnVec = Vector3.zero;
                        break;
                    case 2:
                        returnVec = outerVec;
                        break;
                    case 3:
                        returnVec = outerVec * 10f;
                        break;
                }
                noteCountForPooling[1]--;
                Y.Add(Y[0]);
                Y.RemoveAt(0);
            }
            if (noteType == "3" || noteType == "6" || noteType == "8" || noteType == "a" || noteType == "b" || noteType == "d" || noteType == "e" || noteType == "f")
            {
                switch (judge)
                {
                    case 0:
                        G[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        G[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        G[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        returnVec = G[0].transform.position;
                        break;
                    case 1:
                        returnVec = Vector3.zero;
                        break;
                    case 2:
                        returnVec = outerVec;
                        break;
                    case 3:
                        returnVec = outerVec * 10f;
                        break;
                }
                noteCountForPooling[2]--;
                G.Add(G[0]);
                G.RemoveAt(0);
            }
            if (noteType == "4" || noteType == "7" || noteType == "9" || noteType == "a" || noteType == "c" || noteType == "d" || noteType == "e" || noteType == "f")
            {
                switch (judge)
                {
                    case 0:
                        B[0].GetComponent<DoomChi>().noteEnd();
                        break;
                    case 1:
                        B[0].GetComponent<DoomChi>().noteGood();
                        break;
                    case 2:
                        B[0].GetComponent<DoomChi>().notePerfect();
                        break;
                }
                switch (ComboTextOperrand)
                {
                    case 0:
                        returnVec = B[0].transform.position;
                        break;
                    case 1:
                        returnVec = Vector3.zero;
                        break;
                    case 2:
                        returnVec = outerVec;
                        break;
                    case 3:
                        returnVec = outerVec * 10f;
                        break;
                }
                noteCountForPooling[3]--;
                B.Add(B[0]);
                B.RemoveAt(0);
            }
        }
        return returnVec;
    }

    IEnumerator PatternLoad()
    {
        //note 설정
        List<Dictionary<string, object>> datas = CSVReader.Read("Pattern/" + pattern);

        for (int i = 0; i < datas.Count; ++i)
        {
            int byCultureBeatHalf = 1;
            if (datas[i]["noteType"].ToString() == "0")
                continue;
            float noteTime = 0f;

            if (int.Parse(datas[i]["bar"].ToString()) != 0)
                preBar = int.Parse(datas[i]["bar"].ToString());

            noteTime += preBar * barBeat;
            noteTime += float.Parse(datas[i]["half"].ToString()) * oneBeat * 2f;
            noteTime += float.Parse(datas[i]["4th"].ToString()) * oneBeat;
            noteTime += float.Parse(datas[i]["8th"].ToString()) * oneBeat * 0.5f;
            noteTime += float.Parse(datas[i]["16th"].ToString()) * oneBeat * 0.25f;
            noteTime += float.Parse(datas[i]["32nd"].ToString()) * oneBeat * 0.125f;
            noteTime += float.Parse(datas[i]["3rd"].ToString()) * barBeat / 3f;
            noteTime += float.Parse(datas[i]["6th"].ToString()) * oneBeat * 2f / 3f;
            noteTime += float.Parse(datas[i]["12th"].ToString()) * oneBeat / 3f;
            noteTime += float.Parse(datas[i]["24th"].ToString()) * oneBeat * 0.5f / 3f;

            if (!lineColorOn)
            {
                double totalBeats = 0;
                totalBeats += double.Parse(datas[i]["bar"].ToString());
                totalBeats += double.Parse(datas[i]["half"].ToString())* (1.0/2.0);
                totalBeats += double.Parse(datas[i]["4th"].ToString()) * (1.0/4.0);
                totalBeats += double.Parse(datas[i]["8th"].ToString()) * (1.0 / 8.0);
                totalBeats += double.Parse(datas[i]["16th"].ToString()) * (1.0/16.0);
                totalBeats += double.Parse(datas[i]["32nd"].ToString())* (1.0/32.0);
                totalBeats += double.Parse(datas[i]["3rd"].ToString()) * (1.0/3.0);
                totalBeats += double.Parse(datas[i]["6th"].ToString()) * (1.0/6.0);
                totalBeats += double.Parse(datas[i]["12th"].ToString()) * (1.0/12.0);
                totalBeats += double.Parse(datas[i]["24th"].ToString()) * (1.0 / 24.0);

                if (totalBeats % (1.0/4.0) == 0.0)
                {
                    byCultureBeatHalf = 1;
                }
                else if (totalBeats % (1.0 / 8.0) == 0.0)
                {
                    byCultureBeatHalf = 2;
                }
                else if (totalBeats % (1.0 / 16.0) < 0.001 || totalBeats % (1.0 / 16.0) > (1.0 / 16.0) - 0.001)
                {
                    byCultureBeatHalf = 3;
                }
                else if (totalBeats % (1.0 / 12.0) < 0.001 || totalBeats % (1.0 / 12.0) > (1.0/12.0) - 0.001)
                {
                    byCultureBeatHalf = 4;
                }
                else
                {
                    byCultureBeatHalf = 5;
                }
            }

            noteQueue.Enqueue((datas[i]["noteType"].ToString(), (noteTime, byCultureBeatHalf)));
        }

        for (int i = 0; i < 100; ++i)
        {
            var rIns = Instantiate(Red);
            rIns.GetComponent<DoomChi>().spinner = bar;
            rIns.GetComponent<DoomChi>().anim.enabled = false;
            rIns.SetActive(false);
            rIns.transform.SetParent(PatternHead);
            rIns.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            R.Add(rIns);
        }
        for (int i = 0; i < 100; ++i)
        {
            var bIns = Instantiate(Blue);
            bIns.GetComponent<DoomChi>().spinner = bar;
            bIns.GetComponent<DoomChi>().anim.enabled = false;
            bIns.SetActive(false);
            bIns.transform.SetParent(PatternHead);
            bIns.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            B.Add(bIns);
        }
        for (int i = 0; i < 70; ++i)
        {
            var yIns = Instantiate(Yellow);
            yIns.GetComponent<DoomChi>().spinner = bar;
            yIns.GetComponent<DoomChi>().anim.enabled = false;
            yIns.SetActive(false);
            yIns.transform.SetParent(PatternHead);
            yIns.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            Y.Add(yIns);
        }
        for (int i = 0; i < 70; ++i)
        {
            var gIns = Instantiate(Green);
            gIns.GetComponent<DoomChi>().spinner = bar;
            gIns.GetComponent<DoomChi>().anim.enabled = false;
            gIns.SetActive(false);
            gIns.transform.SetParent(PatternHead);
            gIns.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            G.Add(gIns);
        }
        for (int i = 0; i < 50; ++i)
        {
            var pIns = Instantiate(Purple);
            pIns.GetComponent<DoomChi>().spinner = bar;
            pIns.GetComponent<DoomChi>().anim.enabled = false;
            pIns.SetActive(false);
            pIns.transform.SetParent(PatternHead);
            pIns.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            P.Add(pIns);
        }

        yield return null;
    }

    int ConvertPatternToNums(string str)
    {
        if (str == "0")
        {
            Debug.LogError("Pattern Trimph Zero!");
            return 0;
        }
        else if (str == "1" || str == "2" || str == "3" || str == "4")
            return 1;
        else if (str == "5" || str == "6" || str == "7" || str == "8" || str == "9" || str == "a")
            return 2;
        else if (str == "b" || str == "c" || str == "d" || str == "e")
            return 3;
        else if (str == "f")
            return 4;
        else
            Debug.LogError("Pattern Trimph Zero!");

        return 0;
    }

    bool CompareNoteAndFTag(bool[] key, string str)
    {
        if (str == "0")
        {
            Debug.LogError("Pattern Trimph Zero!");
            return false;
        }

        switch(str)
        {
            case "1":
                if (key[0] == true && key[1] == false && key[2] == false && key[3] == false)
                    return true;
                break;
            case "2":
                if (key[0] == false && key[1] == true && key[2] == false && key[3] == false)
                    return true;
                break;
            case "3":
                if (key[0] == false && key[1] == false && key[2] == true && key[3] == false)
                    return true;
                break;
            case "4":
                if (key[0] == false && key[1] == false && key[2] == false && key[3] == true)
                    return true;
                break;
            case "5":
                if (key[0] == true && key[1] == true && key[2] == false && key[3] == false)
                    return true;
                break;
            case "6":
                if (key[0] == true && key[1] == false && key[2] == true && key[3] == false)
                    return true;
                break;             
            case "7":
                if (key[0] == true && key[1] == false && key[2] == false && key[3] == true)
                    return true;
                break;           
            case "8":
                if (key[0] == false && key[1] == true && key[2] == true && key[3] == false)
                    return true;
                break;                
            case "9":
                if (key[0] == false && key[1] == true && key[2] == false && key[3] == true)
                    return true;
                break;
            case "a":
                if (key[0] == false && key[1] == false && key[2] == true && key[3] == true)
                    return true;
                break;
            case "b":
                if (key[0] == true && key[1] == true && key[2] == true && key[3] == false)
                    return true;
                break;
            case "c":
                if (key[0] == true && key[1] == true && key[2] == false && key[3] == true)
                    return true;
                break;
            case "d":
                if (key[0] == true && key[1] == false && key[2] == true && key[3] == true)
                    return true;
                break;
            case "e":
                if (key[0] == false && key[1] == true && key[2] == true && key[3] == true)
                    return true;
                break;
            case "f":
                if (key[0] == true && key[1] == true && key[2] == true && key[3] == true)
                    return true;
                break;
        }

        return false;
        //Debug.LogError("Pattern Trimph Zero!");

    }

    void GameEnd(bool escape = false)
    {

        if (!endLock)
        {
            start = false;
            endLock = true;
            CurrCombo.text = "";
            if (!escape)
            {
                Combo.text = judgeNums[2] + "\n" + judgeNums[1] + "\n" + judgeNums[0];
                Result.text = maxCombo + "\n\n" + Score;

                if (!stealthBar)
                    barAnim.SetBool("End", true);



#region 결과 저장
                int score = 0;
                if (lineColorOn)
                    score++;
                if (hardJudge)
                    score++;
                if (stealthBar)
                    score++;

                if (PlayerPrefs.GetInt("Contidion" + strForResult, 0) < score)
                    PlayerPrefs.SetInt("Contidion" + strForResult, score);

                if (PlayerPrefs.GetInt("Score" + strForResult, 0) < Score)
                    PlayerPrefs.SetInt("Score" + strForResult, Score);

                if (maxCombo == judgeNums[2] && PlayerPrefs.GetInt("Clear" + strForResult, 0) < 3)
                {
                    PlayerPrefs.SetInt("Clear" + strForResult, 3);
                }
                else if (maxCombo == judgeNums[2] + judgeNums[1] && PlayerPrefs.GetInt("Clear" + strForResult, 0) < 2)
                {
                    PlayerPrefs.SetInt("Clear" + strForResult, 2);
                }
                else if (isLife && PlayerPrefs.GetInt("Clear" + strForResult, 0) < 1)
                {
                    PlayerPrefs.SetInt("Clear" + strForResult, 1);
                }

                PlayerPrefs.SetInt("MISS", judgeNums[0]);
#endregion

                StartCoroutine(ImagesEnd());
            }
            else
            {
                Combo.text = "-\n-\n-";
                Result.text = "--\n\n--";

                isLife = false;
                PlayerPrefs.SetInt("isLife", 0);

                barAnim.SetBool("End", true);

                PlayerPrefs.SetInt("MISS", judgeNums[0]);

                StartCoroutine(ImagesEnd());
            }
        }
    }

    IEnumerator GameStart()
    {
        EndPage.transform.localScale = new Vector2(0, 0);
        yield return StartCoroutine(PatternLoad());
        yield return new WaitForSeconds(1f);
        StartCoroutine(MusicStart());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Image1Start());
        StartCoroutine(Image2Start());
        StartCoroutine(Image3Start());
        StartCoroutine(Image4Start());
        yield return new WaitForSeconds(60f * waitingBeats / BPM);
    }


    IEnumerator Image1Start()
    {
        start = true;
        t = Time.time;
        while (img1.fillAmount < 1)
        {
            img1.fillAmount += (float)(ticker * Time.deltaTime / 360f);
            yield return null;
        }
        if (!stealthBar)
            barAnim.SetBool("Start", true);
    }
    IEnumerator Image2Start()
    {
        yield return new WaitForSeconds(oneBeat);
        while (img2.fillAmount < 1)
        {
            img2.fillAmount += (float)(ticker * Time.deltaTime / 360f);
            yield return null;
        }
    }
    IEnumerator Image3Start()
    {
        yield return new WaitForSeconds((oneBeat * 2f));
        while (img3.fillAmount < 1)
        {
            img3.fillAmount += (float)(ticker * Time.deltaTime / 360f);
            yield return null;
        }
    }
    IEnumerator Image4Start()
    {
        yield return new WaitForSeconds((oneBeat * 3f));
        while (img11.fillAmount < 1)
        {
            img11.fillAmount += (float)(ticker * Time.deltaTime / 360f);
            Fill.fillAmount = img11.fillAmount;
            yield return null;
        }
        endLock = false;
    }

    IEnumerator ImagesEnd()
    {
        
        while (img2.fillAmount > 0)
        {
            img2.fillAmount -= (float)(ticker * Time.deltaTime / 360f);
            img11.fillAmount -= (float)(ticker * Time.deltaTime / 360f);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        float scal = 0f;
        while (EndPage.transform.localScale.x < 1)
        {
            scal += (float)(3f * Time.deltaTime);
            EndPage.transform.localScale = new Vector2(scal, scal);
            
            yield return null;
        }
        EPButton1.SetTrigger("End");
        EPButton3.SetTrigger("End");
        if (isLife)
        {
            PlayerPrefs.SetInt("isLife", 1);
            EPButton2.SetTrigger("End");
        }
        else
        {
            PlayerPrefs.SetInt("isLife", 0);
        }
    }

    IEnumerator MusicStart()
    {
        yield return new WaitForSeconds(0.5f + waitingOffset);
        AC.Play();

    }
}
