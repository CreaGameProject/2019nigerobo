using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public GameObject gameManager;
    //制限時間（分）
    private int minute;
    //制限時間（秒）
    private float seconds;
    //前回Updata時の秒数
    private float oldSeconds;
    GameObject timerText;
    public static float time = 300;
    // Start is called before the first frame update
    void Start()
    {
        this.timerText = GameObject.Find("Time");
        oldSeconds = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //制限時間が0秒以下の時
        if (time <= 0f)
        {
            this.gameManager.GetComponent<MoveSceneScript>().MoveGameOver();
            return;
        }
        time -= Time.deltaTime;
        minute = (int)time / 60;
        seconds = time - minute * 60;
        if ((int)seconds != (int)oldSeconds)
        {
            this.timerText.GetComponent<Text>().text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
    }

    
}
