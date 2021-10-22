using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        timerText.text = "Time Left: " + string.Format("{0,2:00}:{1,2:00}", (int)currGameTime / 60, (int)currGameTime % 60);

        if (currGameTime <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
