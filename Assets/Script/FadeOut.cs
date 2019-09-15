using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField]static public float fadeSpeed = 0.01f;
    static private float _alfa;

    static private Image _fadeImage;

    static private bool flag = false;

    static private float i;
    // Start is called before the first frame update
    void Start()
    {
        _fadeImage = GetComponent<Image>();
        _alfa = _fadeImage.color.a;
        _alfa = 200;
        
    }

    static public void Fade()
    {
        Debug.Log("fade start alfa:" + _alfa);
        _fadeImage.enabled = true;
        while (i<1)
        {
            flag = true;
            fadeSpeed = Math.Abs(fadeSpeed);
        }
        _fadeImage.color = new Color(_fadeImage.color.r,_fadeImage.color.g,_fadeImage.color.b,1f);
    }

    static public void FadeIn()
    {
        _fadeImage.enabled = true;
        while (i>0)
        {
            flag = false;
            fadeSpeed = -1 * Math.Abs(fadeSpeed);
        }
        _fadeImage.color = new Color(_fadeImage.color.r,_fadeImage.color.g,_fadeImage.color.b,0f);
    }

    private void Update()
    {
        if (flag)
        {
            _fadeImage.color = new Color(_fadeImage.color.r,_fadeImage.color.g,_fadeImage.color.b,i);
            i += fadeSpeed;
        }
    }
}
