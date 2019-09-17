using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public int enemyNum = 10;
    public Vector2Int mapRange=new Vector2Int(50,50);
    private bool[,] maps;
    private List<Vector2Int> enemiesPosition = new List<Vector2Int>();
    
    public void Start()
    {
        CSVReader csvreader = GameObject.FindWithTag("MainCamera").GetComponent<CSVReader>();
        System.Random r = new System.Random();
        GameObject enemyObj = (GameObject) Resources.Load("Prefabs/enemy_jam_master");
        maps = csvreader.LoadMap(mapRange);
        while (enemiesPosition.Count <= enemyNum)
        {
            Vector2Int tmp = new Vector2Int(r.Next(mapRange.x), r.Next(mapRange.y));
            
            if (maps[tmp.x, tmp.y] & IsDuplicate(enemiesPosition, tmp))
            {
                enemiesPosition.Add(tmp);
                Instantiate(enemyObj, new Vector3(tmp.x, 0.5f, tmp.y),transform.rotation);
            }
        }
    }

    //summery 配列に重複がある場合falseをリターン
    private bool IsDuplicate(List<Vector2Int> a,Vector2Int b)
    {
        foreach (var VARIABLE in a)
        {
            if (VARIABLE == b)
            {
                return false;
            }
        }
        return true;
    }
}
