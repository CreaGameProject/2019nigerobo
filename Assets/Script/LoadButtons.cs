using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtons : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] private GameObject button1,button2;
    CheckPoint checkPoint = new CheckPoint();

    // Update is called once per frame
    void Update()
    {
        if (CheckPoint.dethCount > 0)
        {
            button1.SetActive(CheckPoint.isOne);
            button2.SetActive(CheckPoint.isTwo);
        }else if (CheckPoint.dethCount<=0)
        {
            button1.SetActive(false);
            button2.SetActive(false);
        }
    }

    public void OnClick(int gateNum)
    {
        checkPoint.CPLoad(gateNum);
    }
}
