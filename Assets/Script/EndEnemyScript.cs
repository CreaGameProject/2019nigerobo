using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEnemyScript : MonoBehaviour
{

    private Animator animator;
    private int ani=0;
    private bool Isanimation=true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("end_Enemy", ani);
        if (Isanimation == true)
        {
            StartCoroutine(EndEnemyMotion());
        }
    }

    IEnumerator EndEnemyMotion()
    {
        Isanimation = false;
        ani = Random.Range(0, 3);
        yield return new WaitForSeconds(1.0f);
        Isanimation = true;
        Debug.Log(ani);
    }
}
