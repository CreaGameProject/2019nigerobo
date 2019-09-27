using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readerCsv : MonoBehaviour
{
    //[System.Serializable]
    //public class InitializeEnemy
    //{
    //    public 
    //}
    [SerializeField]TextAsset csvFile;
    List<string[]> csvDates = new List<string[]>();
    //
    public List<Vector2Int> createEnemyPosition = new List<Vector2Int>();


    // Start is called before the first frame update
    void Awake()
    {

        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDates.Add(line.Split('\t'));
        }

        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (csvDates[i][49-j] == "2")
                {

                    createEnemyPosition.Add(new Vector2Int(49-j,49-i));

                }
            }
        }




    }

    void Start()
    {

        GameObject prefab = (GameObject)Resources.Load("Prefabs/masterCube");


        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (csvDates[i][j] == "1")
                {
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
