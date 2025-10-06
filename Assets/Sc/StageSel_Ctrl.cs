using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSel_Ctrl : MonoBehaviour
{
    Image CenterStageImg;
    Image RightStageImg;
    Image LeftStageImg;

    public GameObject CenterBox;
    public GameObject RightBox;
    public GameObject LeftBox;

    Animator CenterBoxAnim;
    Animator RightBoxAnim;
    Animator LeftBoxAnim;

    int StageNum = 1;

    bool CanAnim = true;
    float Timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        CenterStageImg = CenterBox.GetComponentInChildren<Image>();
        RightStageImg = RightBox.GetComponentInChildren<Image>();
        LeftStageImg = LeftBox.GetComponentInChildren<Image>();

        CenterBoxAnim = CenterBox.GetComponent<Animator>();
        RightBoxAnim = RightBox.GetComponent<Animator>();
        LeftBoxAnim = LeftBox.GetComponent<Animator>();

        CenterStageImg.sprite = Resources.Load<Sprite>("StageImg/" + StageNum.ToString() + "StageImg");
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAnim == false)
        {
            Timer += Time.deltaTime;
            if (1.0f < Timer)
            {
                CenterStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");

                CanAnim = true;
                Timer = 0.0f;
            }
        }

        BoxCtrl();
    }

    void BoxCtrl()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CanAnim == false)
                return;

            CanAnim = false;

            CenterStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");

            CenterBoxAnim.SetTrigger("RightKey");
            RightBoxAnim.SetTrigger("RightKey");

            if (10 <= StageNum)
            {
                StageNum = 1;
                RightStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");
                return;
            }

            StageNum++;

            RightStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CanAnim == false)
                return;

            CanAnim = false;

            CenterBoxAnim.SetTrigger("LeftKey");
            LeftBoxAnim.SetTrigger("LeftKey");

            CenterStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");

            if (StageNum <= 1)
            {
                StageNum = 10;
                LeftStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");
                return;
            }

            StageNum--;

            LeftStageImg.sprite = Resources.Load<Sprite>("StageImg/" + (StageNum).ToString() + "StageImg");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (StageNum == 10)
                LoadingManager.LoadScene("PlayScene10");
            else
                LoadingManager.LoadScene("PlayScene" + StageNum.ToString("N0"));
        }
    }
}
