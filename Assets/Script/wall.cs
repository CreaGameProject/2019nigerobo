using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class wall : MonoBehaviour
{
    //[SerializeField] int length;
    //[SerializeField] int xIncrease;
    //[SerializeField] int zIncrease;

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/ten");

        for (float i = 0; i < 50; i+=1f)
        {
            for (float j = 0; j < 50; j+=1f)
            {
                
                    GameObject obj = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = new Vector3(i,j,0);
                    obj.transform.Rotate(90, 0, 0);
                    

            }
        }
        //int x=0;
        //int z=0;
        //for (int i = 0; i < length; i++)
        //{

        //    GameObject obj = (GameObject)Instantiate(prefab,transform.position, Quaternion.identity);
        //    obj.transform.parent = transform;
        //    obj.transform.localPosition = new Vector3(x, 0, z);
        //    x += xIncrease;
        //    z += zIncrease;

        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
