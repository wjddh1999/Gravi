using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CubeColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Count
}

public class CubeCtrl : MonoBehaviour
{
    public CubeColor curCube = CubeColor.Red;

    // Start is called before the first frame update
    void Start()
    {
        curCube = (CubeColor)Random.Range(0, (int)CubeColor.Count);
        ColorCalc(curCube);
    }

    // Update is called once per frame
    void Update()
    {
        if (curCube == CubeColor.Red && Game_Mgr.Inst.IsRedClear == true ||
            curCube == CubeColor.Blue && Game_Mgr.Inst.IsBlueClear == true ||
            curCube == CubeColor.Green && Game_Mgr.Inst.IsGreenClear == true ||
            curCube == CubeColor.Yellow && Game_Mgr.Inst.IsYellowClear == true)
            curCube = (CubeColor)Random.Range(0, (int)CubeColor.Count);

        //if (curCube == CubeColor.Blue && Game_Mgr.Inst.IsBlueClear == true)
        //    curCube++;
        //if (curCube == CubeColor.Green && Game_Mgr.Inst.IsGreenClear == true)
        //    curCube++;
        //if (curCube == CubeColor.Yellow && Game_Mgr.Inst.IsYellowClear == true)
        //    curCube = CubeColor.Red;

        if (Game_Mgr.Inst.IsRedClear == true &&
            Game_Mgr.Inst.IsBlueClear == true &&
            Game_Mgr.Inst.IsGreenClear == true &&
            Game_Mgr.Inst.IsYellowClear == true)
            Destroy(this.gameObject);

        if (this.tag == curCube.ToString())
        {
            return;
        }
        else
        {
            ColorCalc(curCube);
        }
    }

    public void ColorCalc(CubeColor Cub)
    {
        this.tag = Cub.ToString();

        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Cub.ToString() + "_Cube");
    }
}
