﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{

    private GameObject Player;
    private Animator animator;
    [SerializeField] private Animation anim;
    private bool through = false;
    [SerializeField]
    int gate_num = 0;
    CheckPoint checkPoint = new CheckPoint();

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animation>();
        animator = gameObject.GetComponent<Animator>();
        checkPoint = Player.GetComponent<CheckPoint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(1).normalizedTime);
        if(Vector3.Distance(Player.transform.position, transform.position) < 2.0f && !through) //プレイヤーとゲートの距離が2,0以下のとき
        {
            animator.Play("Armature|ArmatureAction"); //アニメーションの再生
            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
            {
                
                animator.speed = 0.0f;
            }
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime >  0.5f)
            {
                animator.speed = 1.0f;
                
            }
            
        }

        if(Vector3.Distance(Player.transform.position, transform.position) < 0.5f && !through) //プレイヤーとゲートの距離が0.5以下でかつ、一度もそのゲートを通っていないとき
        {
            through = true; //すでにゲートを通ったことにする
            checkPoint.CPSave(gate_num); //CPSaveにゲート番号を渡す
        }
    }
}
