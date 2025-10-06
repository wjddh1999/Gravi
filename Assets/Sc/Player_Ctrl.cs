using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum Mod
{
    red,
    blue,
    green,
    yellow,
    Count
}

public enum PlayMode
{
    Single,
    Multi
}

public class Player_Ctrl : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 textVec = Vector3.zero;
    public Image Range;
    public GameObject RangEff;
    public Canvas playerCnavas;
    public GameObject HelpText;

    public GameObject PlayerBody;
    public GameObject Player_Run;
    public GameObject Player_Idle;

    Quaternion CharRot = Quaternion.identity;

    public Mod curMod = Mod.red;
    [HideInInspector]public static PlayMode playMod = PlayMode.Single;

    float speed = 15.0f;
    float h;
    Vector2 m_DirVec;

    bool RedAll = false;
    bool BlueAll = false;
    bool GreenAll = false;
    bool YellowAll = false;

    bool isCatch = false;

    GameObject MoveTarget = null;
    public GameObject MovePos;
    float CurX = -1.3f;

    float CurY = 1.0f;

    int CacG = 0;

    float ReverseCool = 1.0f;
    float CurTime = 1.0f;

    AudioClip JumpAudio;
    AudioClip ReversAudio;

    Vector3 Gap = Vector3.zero;
    float Power = 0.0f;

    public Image PowerBar;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;

        if (SceneManager.GetActiveScene().buildIndex != 0)
            HelpText.SetActive(false);

        rb = GetComponent<Rigidbody2D>();

        JumpAudio = Resources.Load<AudioClip>("Tone2A_MajorTriadUp");
        ReversAudio = Resources.Load<AudioClip>("magic_02");
    }

    // Update is called once per frame
    void Update()
    {
        GravityCtrl(); 
        Grab();
    }

    void PlayerColor()
    {
        if (curMod == Mod.red)
        {
            Range.color = new Color32(255, 0, 0, 70);
        }
        else if (curMod == Mod.blue)
        {
            Range.color = new Color32(0, 0, 255, 70);
        }
        else if (curMod == Mod.green)
        {
            Range.color = new Color32(0, 255, 0, 70);
        }
        else if (curMod == Mod.yellow)
        {
            Range.color = new Color32(255, 255, 0, 70);
        }

        if ((int)curMod >= (int)Mod.Count)
        {
            curMod = 0;
        }

        if ((int)curMod < 0)
        {
            curMod = Mod.yellow;
        }
    }

    void GravityCtrl()
    {
        CurTime += Time.deltaTime;

        PlayerColor();

        if (Input.GetKeyDown(KeyCode.Space))
        {//캐릭터 중력반전
            rb.gravityScale = -rb.gravityScale;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.3f);
            GetComponent<AudioSource>().PlayOneShot(JumpAudio);
        }

        if (playMod == PlayMode.Single)
        {//싱글일때
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {//범위 내 중력반전 모드 변경
                curMod = curMod + 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {//범위 내 중력반전 모드 변경
                curMod = curMod - 1;
            }
        }
        else if (playMod == PlayMode.Multi)
        {//멀티일때
            //색상 지정 설정
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {//범위 내 오브젝트 중력 반전
            if (ReverseCool <= CurTime)
            {
                Game_Mgr.Inst.RGDistance(curMod);
                GetComponent<AudioSource>().PlayOneShot(ReversAudio);
                GameObject Eff = Instantiate(RangEff);
                Eff.transform.SetParent(playerCnavas.transform);
                Eff.transform.position = this.transform.position;
                Destroy(Eff, 1.0f);
                CurTime = 0.0f;
            }
            else
            {
                return;
            }
        }
    }

    void Grab()
    {
        PowerBar.fillAmount = Power / 1500.0f;

        if (Power < 500.0f)
            PowerBar.color = Color.blue;
        else if (Power < 900.0f)
            PowerBar.color = Color.green;
        else if (Power < 1350.0f)
            PowerBar.color = Color.yellow;
        else
            PowerBar.color = Color.red;

        if (h > 0)
            CurX = 1.3f;
        else if (h < 0)
            CurX = -1.3f;

        CurY = (rb.gravityScale / Mathf.Abs(rb.gravityScale));
        MovePos.transform.localPosition = new Vector3(CurX, CurY, 0.0f);

        if (MoveTarget == null)
            isCatch = false;

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCatch == false)
            {//잡고 있는 큐브가 없을 때
                if (MoveTarget != null)
                {//잡은 큐브에 가해지는 모든 힘, 충돌판정 제거
                    MoveTarget.GetComponent<BoxCollider2D>().isTrigger = true;
                    CacG = (int)MoveTarget.GetComponent<Rigidbody2D>().gravityScale;
                    MoveTarget.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    MoveTarget.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    isCatch = true;
                }
            }
            else
            {//잡고 있는 큐브가 있을 때
                if (MoveTarget != null)
                {//잡고 있는 큐브를 놓으며 중력, 충돌판정 되돌리기
                    MoveTarget.GetComponent<BoxCollider2D>().isTrigger = false;
                    MoveTarget.GetComponent<Rigidbody2D>().gravityScale = CacG;
                    MoveTarget.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    MoveTarget = null;
                    isCatch = false;
                }
            }            
        }

        if (isCatch == true)
        {
            MoveTarget.transform.position = MovePos.transform.position;
            
            if (Input.GetKeyDown(KeyCode.X) == true)
            {
                MoveTarget.GetComponent<BoxCollider2D>().isTrigger = false;
                Gap = MoveTarget.transform.position - transform.position;
            }

            if (Input.GetKey(KeyCode.X) == true)
            {//키를 누르고 있는 경우 파워 채우기
                if (Power < 1500.0f)
                {
                    Power += 35.0f;
                    Debug.Log(Power);
                }
            }
            
            if (Input.GetKeyUp(KeyCode.X) == true)
            {//키를 때는 경우 모인 힘만큼 들고 있는 큐브 던지기
                Gap.x *= Power;
                Gap.y *= Power / 2.0f;
                MoveTarget.GetComponent<Rigidbody2D>().AddForce(Gap);
                MoveTarget.GetComponent<Rigidbody2D>().gravityScale = CacG;
                MoveTarget = null;
                isCatch = false;
                //던진 뒤에 초기화
                Gap = Vector3.zero;
                Power = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal"); // 방향키로 수평 방향 이동 입력 받기

        Vector3 scale = PlayerBody.transform.localScale;

        scale.y = Mathf.Abs(scale.y) * Mathf.Sign(rb.gravityScale);

        if (h < 0f)
            scale.x = Mathf.Abs(scale.x);
        else if (h > 0f)
            scale.x = -Mathf.Abs(scale.x);

        PlayerBody.transform.localScale = scale;

        if (h != 0.0f)
        {
            m_DirVec = (Vector2.right * h);
            if (1.0f < m_DirVec.magnitude)
                m_DirVec.Normalize();

            Player_Idle.SetActive(false);
            Player_Run.SetActive(true);

            transform.Translate(m_DirVec * speed * Time.deltaTime);
        }
        else
        {
            Player_Idle.SetActive(true);
            Player_Run.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Red" ||
            coll.gameObject.tag == "Blue" ||
            coll.gameObject.tag == "Green" ||
            coll.gameObject.tag == "Yellow")
        {
            if (MoveTarget == null)
            {
                if (coll.gameObject != MoveTarget)
                    MoveTarget = coll.gameObject;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "RedSwitch")
        {
            RedAll = true;
        }
        else if (coll.tag == "BlueSwitch")
        {
            BlueAll = true;
        }
        else if (coll.tag == "GreenSwitch")
        {
            GreenAll = true;
        }
        else if (coll.tag == "YellowSwitch")
        {
            YellowAll = true;
        }

        if (coll.gameObject.tag == "Red" ||
            coll.gameObject.tag == "Blue" ||
            coll.gameObject.tag == "Green" ||
            coll.gameObject.tag == "Yellow")
        {
            if (MoveTarget == null)
            {//잡을 수 있는 큐브가 없는 경우에
             //먼저 범위에 들어온 큐브를 할당
                if (coll.gameObject != MoveTarget)
                    MoveTarget = coll.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "RedSwitch")
        {
            RedAll = false;
        }
        else if (coll.tag == "BlueSwitch")
        {
            BlueAll = false;
        }
        else if (coll.tag == "GreenSwitch")
        {
            GreenAll = false;
        }
        else if (coll.tag == "YellowSwitch")
        {
            YellowAll = false;
        }

        if (coll.gameObject.tag == "Red" ||
            coll.gameObject.tag == "Blue" ||
            coll.gameObject.tag == "Green" ||
            coll.gameObject.tag == "Yellow")
        {
            if (isCatch == false)
                MoveTarget = null;
        }
    }
}
