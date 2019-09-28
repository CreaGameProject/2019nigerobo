using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    //デフォルトのBGM
    public AudioClip audioClip1;
    //敵に見つかった時のBGM
    public AudioClip audioClip2;
    private AudioSource audioSource;
    
    // 敵のコンポ
    private List<EnemyRobot> robots = new List<EnemyRobot>();
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.Play();
        StartCoroutine(EnemyAdd());
    }

    IEnumerator EnemyAdd()
    {
        yield return null;
        // コンポをリストに追加
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")) robots.Add(enemy.GetComponent<EnemyRobot>());
        Debug.Log(robots.Count);
        yield break;
    }

    //敵の追跡から逃れたとき
    public void NormalBGM()
    {
        if (audioSource.clip != audioClip1)
        {
            audioSource.clip = audioClip1;
            audioSource.Play();
        }
    }

    //敵の視界にプレイヤーが入った時
    public void EmargencyBGM()
    {
        if (audioSource.clip != audioClip2)
        {
            audioSource.clip = audioClip2;
            audioSource.Play();
        }
    }

    private void Update()
    {
        bool isChase = false;
        foreach (var robot in robots) isChase = isChase || (robot.state == EnemyState.Chase);
        if(isChase)
            EmargencyBGM();
        else
            NormalBGM();
    }
}
