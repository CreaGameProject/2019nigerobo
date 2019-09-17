using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadButtons : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] private GameObject button1,button2;
    CheckPoint checkPoint;
    private GameObject[] enemies;
    private EnemyRobot[] enemyRobot;
    private bool[] isChase;
    private BGMScript bgmScript;
    private int enemyNum;
    
    //追加9/17
    private void Start()
    {
        checkPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckPoint>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bgmScript = GameObject.FindWithTag("MainCamera").GetComponent<BGMScript>();
        enemyNum = enemies.Length;
        enemyRobot = new EnemyRobot[enemyNum];
        Debug.Log(enemyNum);
        for (int i = 0; i < enemyNum; i++)
        {
            enemyRobot[i] = enemies[i].GetComponent<EnemyRobot>();
        }
    }


    void Update()
    {
        if (CheckPoint.dethCount > 0)
        {
            button1.SetActive(CheckPoint.isOne);
            button2.SetActive(CheckPoint.isTwo);
        }
        else if (CheckPoint.dethCount<=0)
        {
            button1.SetActive(false);
            button2.SetActive(false);
        }

        isChase = new bool[enemyNum];
        for (int i = 0; i < enemyNum; i++)
        {
            if (isChase[i] = enemyRobot[i].FindPlayer())
            {
                bgmScript.EmargencyBGM();
            }
        }

        if (isChase.Distinct().Count()==1)
        {
            bgmScript.NormalBGM();
        }
    }

    public void OnClick(int gateNum)
    {
        StartCoroutine(checkPoint.CPLoad(gateNum));
    }
}
