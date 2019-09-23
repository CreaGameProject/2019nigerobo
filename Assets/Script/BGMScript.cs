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
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.Play();
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
}
