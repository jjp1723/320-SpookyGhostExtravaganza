using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private Text p1ScoreText;
    private Text p2ScoreText;
    private Text titleText;

    void Start()
    {
        p1ScoreText = GameObject.Find("P1Score").GetComponent<Text>();
        p2ScoreText = GameObject.Find("P2Score").GetComponent<Text>();
        titleText = GameObject.Find("TitleText").GetComponent<Text>();

        PointsManager pm = GameObject.Find("PointsManager").GetComponent<PointsManager>();

        int p1Score = pm.GetScore("Player1");
        int p2Score = pm.GetScore("Player2");

        p1ScoreText.text = "Player 1 Score: " + p1Score;
        p2ScoreText.text = "Player 2 Score: " + p2Score;

        if (p1Score == p2Score)
        {
            titleText.text = "It's a Tie!!!";
        }
        else if (p1Score > p2Score)
        {
            titleText.text = "Player 1 Wins!!!";
        }
        else
        {
            titleText.text = "Player 2 Wins!!!";
        }
    }
}