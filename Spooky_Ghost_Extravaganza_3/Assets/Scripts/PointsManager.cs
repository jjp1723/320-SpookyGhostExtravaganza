using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    private GameObject pointsManager;
    private Text p1ScoreText;

    //Stores all of the players scores
    private Dictionary<string, int> playerScores;

    // Start is called before the first frame update
    void Start()
    {
        pointsManager = gameObject;
        playerScores = new Dictionary<string, int>();
        p1ScoreText = GameObject.Find("ScoreTextP1").GetComponent<Text>();
    }

    public void AddPointsToPlayer(string playerID, int pointsToAdd)
    {
        if (playerScores.ContainsKey(playerID))
        {
            playerScores[playerID] += pointsToAdd;
        }
        else
        {
            playerScores.Add(playerID, pointsToAdd);
        }

        p1ScoreText.text = "Score: " + playerScores[playerID];
    }

}
