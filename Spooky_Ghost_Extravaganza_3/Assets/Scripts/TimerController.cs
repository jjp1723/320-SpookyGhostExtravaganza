using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private float maxGameTimeSecs;
    private float currGameTime;
    private Text timerText;

    void Start()
    {
        currGameTime = maxGameTimeSecs;
        timerText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        currGameTime -= Time.deltaTime;
        timerText.text = "Time Left: " + (int)currGameTime / 60 + ":" + (int)currGameTime % 60;
    }
}
