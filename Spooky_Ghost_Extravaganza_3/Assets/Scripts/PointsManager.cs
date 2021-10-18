using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private GameObject pointsManager;

    //Stores all of the players scores
    private Dictionary<string, int> playerScores;

    // Start is called before the first frame update
    void Start()
    {
        pointsManager = gameObject;
        playerScores = new Dictionary<string, int>();
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

        Debug.Log(playerID + ": " + playerScores[playerID]);
    }

}
