using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameObject.FindWithTag("MainCamera").GetComponent<MoveSceneScript>().MoveGameClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
