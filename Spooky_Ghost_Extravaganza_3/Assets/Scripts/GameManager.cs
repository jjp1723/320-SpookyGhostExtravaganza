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

    [SerializeField]
    private GameObject npc;
    private NpcController npcController;

    private List<GameObject> npcs = new List<GameObject>();
    private List<NpcController> npcControllers = new List<NpcController>();

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        powerupController = powerup.GetComponent<PowerupController>();

        //create 3 npcs
        for (int i = 0; i < 3; i++)
        {
            npcs.Add(Object.Instantiate(npc));
            npcControllers.Add(npcs[i].GetComponent<NpcController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerController.CheckForPlayerInput();
        powerupController.CheckForInput();

        for (int i = 0; i < npcs.Count; i++)
        {
            npcControllers[i].Move();
        }
    }
}
