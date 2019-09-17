using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public static float[] correntTime=new float[2];
    private static GameObject[] enemies;
    public static GameObject robot;
    private static Vector3 friendlyposition;
    private static GameObject[] gatePos;
    private GameObject[] gate;
    public static int dethCount = 3;
    public static bool isOne = false;
    public static bool isTwo = false;
    private EnemyRobot enemyRobot;

    public static List<Vector3>[] EnemiesPositionData { get; set; } = new List<Vector3>[2];
    //getcomp
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        correntTime[0] = 300;
        correntTime[1] = 300;
        EnemiesPositionData[0]=new List<Vector3>();
        EnemiesPositionData[1]=new List<Vector3>();
    }
    //checkPointでのsave
    public void CPSave(int gateNum)
    {
        correntTime[gateNum] = TimerScript.time;
        friendlyposition = transform.position;

        Debug.Log(friendlyposition);
        foreach (var t in enemies)
        {
            EnemiesPositionData[gateNum].Add(t.transform.position);
        }

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
    public IEnumerator CPLoad(int gateNum)
    {
        Image black_out;
        black_out = GameObject.Find("Black").GetComponent<Image>();
        black_out.color = new Color(0, 0, 0, 256);
        transform.GetComponent<PlayerMove>().controller.enabled = false;
        transform.position = friendlyposition;
        transform.GetComponent<PlayerMove>().controller.enabled = true;
        TimerScript.time = correntTime[gateNum];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyRobot = enemies[i].GetComponent<EnemyRobot>();
            enemyRobot.StopAllCoroutines();
            yield return null;
            enemies[i].transform.position = EnemiesPositionData[gateNum][i];
            enemyRobot.ReStertCoroutine(EnemiesPositionData[gateNum][i]);
        }
        dethCount--;
        if (gateNum==1)
        {
            isTwo=false;
        }
        black_out.color = new Color(0, 0, 0, 0);
    }
    
    
}
