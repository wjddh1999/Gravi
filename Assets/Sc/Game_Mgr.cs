using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    GameObject Player;

    GameObject[] Reds;
    GameObject[] Blues;
    GameObject[] Greens;
    GameObject[] Yellows;

    Rigidbody2D RedRb;
    Rigidbody2D BlueRb;
    Rigidbody2D GreenRb;
    Rigidbody2D YellowRb;


    GameObject[] curColors;

    Rigidbody2D curcolorRb;

    public GameObject cube;

    float CoolTime = 20.0f;
    float CurTime = 18.0f;

    [HideInInspector] public int RedCount = 0;
    [HideInInspector] public int BlueCount = 0;
    [HideInInspector] public int GreenCount = 0;
    [HideInInspector] public int YellowCount = 0;

    [HideInInspector] public bool IsRedClear = false;
    [HideInInspector] public bool IsBlueClear = false;
    [HideInInspector] public bool IsGreenClear = false;
    [HideInInspector] public bool IsYellowClear = false;

    public static Game_Mgr Inst;

    [HideInInspector] public int Counter = 0;

    private void Awake()
    {
        Inst = this;
        SceneMgr.SceneNum = SceneManager.GetActiveScene().buildIndex - 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadingManager.LoadScene("TitleScene");

        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
        {
            Toy();

            //if (Input.GetKeyDown(KeyCode.G) == true)
            //{
            //    ReverseGravityAll();
            //}

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if (Player_Ctrl.playMod == PlayMode.Single)
                    Player_Ctrl.playMod = PlayMode.Multi;
                else
                    Player_Ctrl.playMod = PlayMode.Single;
            }

            if (Game_Mgr.Inst.IsRedClear == true &&
            Game_Mgr.Inst.IsBlueClear == true &&
            Game_Mgr.Inst.IsGreenClear == true &&
            Game_Mgr.Inst.IsYellowClear == true)
            {
                SceneMgr.SceneNum++;

                if (10 < SceneMgr.SceneNum)
                {
                    Application.Quit();
                    return;
                }

                LoadingManager.LoadScene("PlayScene" + SceneMgr.SceneNum.ToString("N0"));
            }
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SceneMgr.SceneNum++;

        //    if (10 < SceneMgr.SceneNum)
        //    {
        //        Application.Quit();
        //        return;
        //    }

        //    LoadingManager.LoadScene("PlayScene" + SceneMgr.SceneNum.ToString("N0"));
        //}
    }

    void Toy()
    {
        //if (CoolTime < 0.0f)
        //{
        //    LoadingManager.LoadScene("TitleScene");
        //}

        if(35 < Counter)
            LoadingManager.LoadScene("TitleScene");

        CurTime += Time.deltaTime;

        if (CoolTime <= CurTime)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(cube);
                Counter++;
            }
            CoolTime -= 1.0f;
            CurTime = 0.0f;
        }
    }

    public void ReverseGravityAll()
    {
        Physics2D.gravity = -Physics2D.gravity;
    }
    public void RGRedAll()
    {
        Reds = GameObject.FindGameObjectsWithTag("Red");

        for (int i = 0; i < Reds.Length; i++)
        {
            RedRb = Reds[i].GetComponent<Rigidbody2D>();
            RedRb.gravityScale = -RedRb.gravityScale;
        }
    }
    public void RGBlueAll()
    {
        Blues = GameObject.FindGameObjectsWithTag("Blue");

        for (int i = 0; i < Blues.Length; i++)
        {
            BlueRb = Blues[i].GetComponent<Rigidbody2D>();
            BlueRb.gravityScale = -BlueRb.gravityScale;
        }
    }
    public void RGGreenAll()
    {
        Greens = GameObject.FindGameObjectsWithTag("Green");

        for (int i = 0; i < Greens.Length; i++)
        {
            GreenRb = Greens[i].GetComponent<Rigidbody2D>();
            GreenRb.gravityScale = -GreenRb.gravityScale;
        }
    }
    public void RGYellowAll()
    {
        Yellows = GameObject.FindGameObjectsWithTag("Yellow");

        for (int i = 0; i < Yellows.Length; i++)
        {
            YellowRb = Yellows[i].GetComponent<Rigidbody2D>();
            YellowRb.gravityScale = -YellowRb.gravityScale;
        }
    }

    public void RGDistance(Mod playerMod)
    {//각 색상에 해당하는 오브젝트 미리 찾아두기
        Reds = GameObject.FindGameObjectsWithTag("Red");
        Blues = GameObject.FindGameObjectsWithTag("Blue");
        Greens = GameObject.FindGameObjectsWithTag("Green");
        Yellows = GameObject.FindGameObjectsWithTag("Yellow");

        if (playerMod == Mod.red)
        {   //중력장 모드에 따른 현재 변환 색상 할당
            curColors = Reds;
            //할당 된 색상 rigidbody 저장
            curcolorRb = RedRb;
        }
        else if (playerMod == Mod.blue)
        {
            curColors = Blues;
            curcolorRb = BlueRb;
        }
        else if (playerMod == Mod.green)
        {
            curColors = Greens;
            curcolorRb = GreenRb;
        }
        else if (playerMod == Mod.yellow)
        {
            curColors = Yellows;
            curcolorRb = YellowRb;
        }

        for (int i = 0; i < curColors.Length; i++)
        {
            if (Vector2.Distance(Player.transform.position, curColors[i].transform.position) < (15.5f * 1.5f))
            {//할당된 색상의 큐브중에 적용 범위 안에 있는 큐브만 중력 반전
                curcolorRb = curColors[i].GetComponent<Rigidbody2D>();
                curcolorRb.gravityScale = -curcolorRb.gravityScale;
            }
            else
                continue;
        }
    }
}
