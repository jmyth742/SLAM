using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBarController : MonoBehaviour {

    public TMP_Text counter;

        //void Start () {
    //       Time.timeScale = 1;
    //}

    void Update () {
        float minutes = (int)(Time.timeSinceLevelLoad/60f);
        float seconds = (int)(Time.timeSinceLevelLoad % 60f);
        counter.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
