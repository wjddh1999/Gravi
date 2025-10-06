using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZoneCheck : MonoBehaviour
{
    GameObject ElcParticle;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            GetComponentInChildren<Text>().text = "0 / " + (SceneMgr.SceneNum / 2 + 1);

        ElcParticle = Resources.Load<GameObject>("CFXR3 Hit Electric C (Air)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (this.tag == "RedZone")
        {
            if (coll.tag == "Red")
            {
                Game_Mgr.Inst.RedCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.RedCount)
                {
                    Game_Mgr.Inst.IsRedClear = true;
                    Destroy(gameObject);
                    Game_Mgr.Inst.Counter--;
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.RedCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
        if (this.tag == "BlueZone")
        {
            if (coll.tag == "Blue")
            {
                Game_Mgr.Inst.BlueCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.BlueCount)
                {
                    Game_Mgr.Inst.IsBlueClear = true;
                    Destroy(gameObject);
                    Game_Mgr.Inst.Counter--;
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.BlueCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
        if (this.tag == "GreenZone")
        {
            if (coll.tag == "Green")
            {
                Game_Mgr.Inst.GreenCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.GreenCount)
                {
                    Game_Mgr.Inst.IsGreenClear = true;
                    Destroy(gameObject);
                    Game_Mgr.Inst.Counter--;
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.GreenCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
        if (this.tag == "YellowZone")
        {
            if (coll.tag == "Yellow")
            {
                Game_Mgr.Inst.YellowCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.YellowCount)
                {
                    Game_Mgr.Inst.IsYellowClear = true;
                    Destroy(gameObject);
                    Game_Mgr.Inst.Counter--;
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.YellowCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (this.tag == "StartZone")
        {
            if (coll.gameObject.name.Contains("CubePrefab"))
                LoadingManager.LoadScene("SelectScene");
        }
        if (this.tag == "EndZone")
        {
            if (coll.gameObject.name.Contains("CubePrefab"))
                Application.Quit();
        }

        if (this.tag == "RedZone")
        {
            if (coll.tag == "Red")
            {
                Game_Mgr.Inst.RedCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.RedCount)
                {
                    Game_Mgr.Inst.IsRedClear = true;
                    Destroy(gameObject);
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.RedCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }    
        }
        if (this.tag == "BlueZone")
        {
            if (coll.tag == "Blue")
            {
                Game_Mgr.Inst.BlueCount++;
                if ((SceneMgr.SceneNum / 2 + 1)  <= Game_Mgr.Inst.BlueCount)
                {
                    Game_Mgr.Inst.IsBlueClear = true;
                    Destroy(gameObject);
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.BlueCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
        if (this.tag == "GreenZone")
        {
            if (coll.tag == "Green")
            {
                Game_Mgr.Inst.GreenCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.GreenCount)
                {
                    Game_Mgr.Inst.IsGreenClear = true;
                    Destroy(gameObject);
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.GreenCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
        if (this.tag == "YellowZone")
        {
            if (coll.tag == "Yellow")
            {
                Game_Mgr.Inst.YellowCount++;
                if ((SceneMgr.SceneNum / 2 + 1) <= Game_Mgr.Inst.YellowCount)
                {
                    Game_Mgr.Inst.IsYellowClear = true;
                    Destroy(gameObject);
                }
                GetComponentInChildren<Text>().text = Game_Mgr.Inst.YellowCount.ToString("N0") + " / " + (SceneMgr.SceneNum / 2 + 1);

                GameObject Elc = Instantiate(ElcParticle);
                Elc.transform.position = coll.transform.position;
                Destroy(Elc, 1.0f);

                Destroy(coll.gameObject);
            }
        }
    }
}
