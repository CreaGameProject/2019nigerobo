﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    static public float[] correntTime=new float[2];
    static private GameObject[] enemies;
    static public GameObject robot;
    static private Vector3 friendlyposition;
    static private GameObject[] gatePos;
    private GameObject[] gate;
    static public int dethCount = 3;
    static public bool isOne = false;
    static public bool isTwo = false;

    static public List<List<Vector3>> EnemiesPositionData { get; set; } = new List<List<Vector3>>();
    //getcomp
    void Start()
    {
        Debug.Log("1");
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        correntTime[0] = 300;
        correntTime[1] = 300;
    }
    //checkPointでのsave
    public void CPSave(int gateNum)
    {
        correntTime[gateNum] = TimerScript.time;
        friendlyposition = this.transform.position;
        Debug.Log(friendlyposition);
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
    public void CPLoad(int gateNum)
    {
        Debug.Log("deth counter" + dethCount);
        FadeOut.Fade();
        this.transform.position = friendlyposition;
        Debug.Log(robot.transform.position);
        TimerScript.time = correntTime[gateNum];
//        for (int i = 0; i < enemies.Length; i++)
//        {
//            enemies[i].transform.position = EnemiesPositionData[gateNum][i];
//        }
        dethCount--;
        if (gateNum==1)
        {
            isTwo=false;
        }
        FadeOut.FadeIn();
    }
    
    
}
