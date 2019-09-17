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

    public static List<List<Vector3>> EnemiesPositionData { get; set; } = new List<List<Vector3>>();
    //getcomp
    void Start()
    {
        Debug.Log("1");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        correntTime[0] = 300;
        correntTime[1] = 300;
    }
    //checkPointでのsave
    public void CPSave(int gateNum)
    {
        correntTime[gateNum] = TimerScript.time;
        friendlyposition = transform.position;

        Debug.Log(friendlyposition);
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemiesPositionData[gateNum].Add(enemies[i].transform.position); 
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
    public void CPLoad(int gateNum)
    {
        Debug.Log("deth counter" + dethCount + friendlyposition);
        //FadeOut.Fade();
        Image black_out;
        black_out = GameObject.Find("Black").GetComponent<Image>();
        black_out.color = new Color(0, 0, 0, 256);
        transform.GetComponent<PlayerMove>().controller.enabled = false;
        transform.position = friendlyposition;
        transform.GetComponent<PlayerMove>().controller.enabled = true;
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
