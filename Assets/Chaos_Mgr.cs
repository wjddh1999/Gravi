using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos_Mgr : MonoBehaviour
{
    public GameObject[] ChaosObj;

    Vector3 RandVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ChaosObj.Length; i++)
        {
            RandVec = new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-25.0f, 25.0f), 0.0f);

            ChaosObj[i].GetComponent<Rigidbody2D>().AddForce(RandVec);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

    }
}
