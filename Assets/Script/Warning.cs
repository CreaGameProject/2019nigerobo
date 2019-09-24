using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject ui;
    public float interval = 0.25f;
    private bool warn=false;
    private bool warnin = false;
    private BGMScript bgmScript;

    // Start is called before the first frame update
    void Start()
    {
        bgmScript = GameObject.FindWithTag("MainCamera").GetComponent<BGMScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Warn()
    {
        StartCoroutine(invincibleTime(0.5f));
        StartCoroutine(Blink());
    }
    IEnumerator invincibleTime(float time)
    {
        //Debug.Log("wai");
        //warn = false;
        warnin = true;
        yield return new WaitForSeconds(4f);      // 処理を待機.
        warnin = false;
        //Debug.Log("真姫");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (!warnin) yield break;
            ui.SetActive(true);
            yield return new WaitForSeconds(interval);
            ui.SetActive(false);
            yield return new WaitForSeconds(interval);
        }
    }
}
