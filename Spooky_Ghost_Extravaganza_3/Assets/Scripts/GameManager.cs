using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlayerController playerController;

    [SerializeField]
    private GameObject powerup;
    private PowerupController powerupController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        powerupController = powerup.GetComponent<PowerupController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController.CheckForPlayerInput();
        powerupController.CheckForInput();
    }
}
