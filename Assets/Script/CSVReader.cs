using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;

public class CSVReader : MonoBehaviour
{
    //[SerializeField] GameObject notDelete;
    [SerializeField] TextAsset csvFile;
    //public static int namecount;//LISTの番号指定
    private Vector2Int mapRange;
    public CSVReader(Vector2Int len)
    {
        mapRange = len;
    }
    public bool[,] LoadMap(Vector2Int len)
    {
        mapRange = len;
        int row = mapRange.y - 1;
        bool[,] maps = new bool[mapRange.x, mapRange.y];
//        csvFile = Resources.Load(csvName) as TextAsset;
        StringReader reder = new StringReader(csvFile.text);
        while (reder.Peek() != -1)
        {
            string line = reder.ReadLine();//読み取り
            string[] tmp = line.Split('\t');//','で分解
            for (int i = 0; i < tmp.Length; i++)
            {
                maps[i, row] = tmp[i] == "0";
//                Debug.Log(tmp[i]);
//                maps[row ,i]=Convert.ToBoolean(tmp[i]);//tmpの中身をboolに変換してmapsにadd
            }
            row--;
        }
        return maps;
    }
}