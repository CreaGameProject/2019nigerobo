using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readerCsv : MonoBehaviour
{
    TextAsset csvFile;
    List<string[]> csvDates = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        csvFile = Resources.Load("map1") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDates.Add(line.Split('\t'));
        }

        GameObject prefab = (GameObject)Resources.Load("Prefabs/masterCube");

        for (int i= 0;i<50;i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (csvDates[i][j]=="1") {
                    GameObject obj = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = new Vector3(i, 0, j);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
