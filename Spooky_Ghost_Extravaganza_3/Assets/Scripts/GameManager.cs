using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    private PlayerController playerController1;

    [SerializeField]
    private GameObject player2;
    private PlayerController playerController2;

    [SerializeField]
    private GameObject powerup;
    private PowerupController powerupController;

    [SerializeField]
    private GameObject npc;
    private NpcController npcController;

    private List<GameObject> npcs = new List<GameObject>();
    private List<NpcController> npcControllers = new List<NpcController>();

    [SerializeField]
    private float npcSpawnIncrement;
    private float npcSpawnTimer = 0.0f;

    [SerializeField]
    private float powerupSpawnIncrement;
    private float powerupSpawnTimer = 0.0f;
    [SerializeField]
    private List<string> powerupTypes = new List<string>();

    void Start()
    {
        playerController1 = player1.GetComponent<PlayerController>();
        if (player2)
        {
            playerController2 = player2.GetComponent<PlayerController>();
        }

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
        playerController1.CheckForInput();
        if (playerController2)
        {
            playerController2.CheckForInput();
        }
        powerupController.CheckForInput();

        if(npcSpawnTimer < npcSpawnIncrement)
        {
            npcSpawnTimer += Time.deltaTime;
        }
        else
        {
            npcSpawnTimer = 0.0f;
            npcs.Add(Object.Instantiate(npc));
            npcControllers.Add(npcs[npcs.Count - 1].GetComponent<NpcController>());
        }

        for (int i = 0; i < npcs.Count; i++)
        {
            if (npcControllers[i])
            {
                npcControllers[i].Move();
            }
        }
    }
}
