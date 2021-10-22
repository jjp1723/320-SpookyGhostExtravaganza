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
        if (collision.gameObject.tag == "Player")
        {
            //Audio
            FindObjectOfType<AudioManager>().Play("Pickup1");

            pointsManagerScript.AddPointsToPlayer(collision.gameObject.name, candyPointsVal);

            //gameAudio.Play("Pickup1");
            Destroy(npcDrop);
        }
    }
}

