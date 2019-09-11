using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    static public List<float> correntTime { get; set; } = new List<float>();
    static private GameObject[] enemies;
    static private GameObject robot;
    static private Vector3 friendlyposition;
    private GameObject[] gate;
    static public int dethCount = 3;
    static public bool isOne = false;
    static public bool isTwo = false;

    static public List<List<Vector3>> EnemiesPositionData { get; set; } = new List<List<Vector3>>();
    //getcomp
    void start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        robot = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(robot.transform.position);
    }
    //checkPointでのsave
    static public void CPSave(int gateNum)
    {
        Debug.Log(gateNum);
        //correntTime[gateNum] = TimerScript.time;
        //friendlyposition = robot.transform.position;
//        for (int i = 0; i < enemies.Length; i++)
//        {
//            EnemiesPositionData[gateNum].Add(enemies[i].transform.position);
//        }

        switch (gateNum)
        {
            case 0:
                isOne = true;
                break;
            case 1:
                isTwo = true;
                break;
        }
    }
    
    //任意点でのロード
    static public void CPLoad(int gateNum)
    {
        Image black_out;
        robot.transform.position = friendlyposition;
        black_out = GameObject.Find("Black").GetComponent<Image>();
        black_out.color = new Color(0, 0, 0, 256);

        TimerScript.time = correntTime[gateNum];
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
