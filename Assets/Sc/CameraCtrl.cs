using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCtrl : MonoBehaviour
{
    GameObject[] Players;
    float MaxDistance = 0.0f;

    float CameraX, CameraY;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = 25.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Player_Ctrl.playMod == PlayMode.Multi)
        //{
        //    MultCam();
        //}
        //else
        //{
        //    Camera.main.transform.position = new Vector3(0, 0, -10);
        //    Camera.main.orthographicSize = 25.0f;
        //}
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            CamCtrl();
    }

    void CamCtrl()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        if (Player_Ctrl.playMod == PlayMode.Multi)
        {
            CameraX = 0;
            CameraY = 0;
            MaxDistance = 0;
            for (int i = 0; i < Players.Length; i++)
            {
                for (int j = 0; j < Players.Length; j++)
                {
                    if (MaxDistance < Vector2.Distance(Players[j].transform.position, Players[i].transform.position))
                    {
                        MaxDistance = Vector2.Distance(Players[j].transform.position, Players[i].transform.position);
                    }
                }

                CameraX += Players[i].transform.position.x;
            }

            Camera.main.transform.position = new Vector3(CameraX / Players.Length, 0.0f, -10);

            if (50 < MaxDistance)
            {
                Camera.main.orthographicSize = (MaxDistance / 2);
            }
            else if (MaxDistance <= 50)
                Camera.main.orthographicSize = 25.0f;
        }
        else if (Player_Ctrl.playMod == PlayMode.Single)
        {
            Camera.main.transform.position = new Vector3(Players[0].transform.position.x, Players[0].transform.position.y, -10);

            if (Players[0].transform.position.y <= -35.0f)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -35.0f, -10.0f);
            }
            else if (35.0f <= Players[0].transform.position.y)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 35.0f, -10.0f);
            }

            if (Players[0].transform.position.x <= -14.0f)
            {
                Camera.main.transform.position = new Vector3(-14.0f, Camera.main.transform.position.y, -10.0f);
            }
            else if (14.0f <= Players[0].transform.position.x)
            {
                Camera.main.transform.position = new Vector3(14, Camera.main.transform.position.y, -10.0f);
            }
        }
    }
}
