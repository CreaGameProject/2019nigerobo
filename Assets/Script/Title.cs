﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private int time = 0;
    bool a = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        time += 1;
        if (time > 60 && Input.anyKey && a)
        {
            a = false;
            SceneManager.LoadSceneAsync("Field");
        }
    }
}
