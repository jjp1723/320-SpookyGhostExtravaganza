using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_DropController : MonoBehaviour
{
    private GameObject npcDrop;
    private AudioManager gameAudio;

    private GameObject pointsManager;
    private PointsManager pointsManagerScript;
    private const int candyPointsVal = 100;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = FindObjectOfType<AudioManager>();
        npcDrop = gameObject;
        pointsManager = GameObject.Find("PointsManager");
        pointsManagerScript = pointsManager.GetComponent<PointsManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (collision.gameObject.CompareTag("Player"))
        {
            //Audio
            PlayerController script = obj.gameObject.GetComponent<PlayerController>();

            if (script && script.isPlayer1)
                FindObjectOfType<AudioManager>().Play("Pickup1");
            else
                FindObjectOfType<AudioManager>().Play("Pickup2");

            pointsManagerScript.AddPointsToPlayer(collision.gameObject.name, candyPointsVal);

            Destroy(npcDrop);
        }
    }
}

