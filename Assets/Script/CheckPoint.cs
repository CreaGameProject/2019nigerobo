using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    static public List<float> correntTime { get; set; }=new List<float>();
    static private GameObject[] enemies;
    private GameObject[] gate;
    static public int dethCount;
    static public bool isOne;
    static public bool isTwo;

    static public List<List<Vector3>> EnemiesPositionData { get; set; } = new List<List<Vector3>>();
    //getcomp
    void start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        isOne = false;
        isTwo = false;
        dethCount = 3;
    }
    //checkPointでのsave
    static public void CPSave(int gateNum)
    {
        //correntTime[gateNum] = TimerScript.time;
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemiesPositionData[gateNum].Add(enemies[i].transform.position);
        }
    }
    
    //任意点でのロード
    static public void CPLoad(int gateNum)
    {
        Image black_out;

        black_out = GameObject.Find("Black").GetComponent<Image>();
        black_out.color = new Color(0, 0, 0, 256);

        //TimerScript.time = correntTime[gateNum];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = EnemiesPositionData[gateNum][i];
        }

        dethCount--;
        if (gateNum==1)
        {
            isTwo=false;
        }

        black_out.color = new Color(0, 0, 0, 0);
    }
    
    
}
