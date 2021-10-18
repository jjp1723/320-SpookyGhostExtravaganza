using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_DropController : MonoBehaviour
{
    private GameObject npcDrop;
    public AudioManager gameAudio;

    private GameObject pointsManager;
    private const int candyPointsVal = 100;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        npcDrop = gameObject;
        pointsManager = GameObject.Find("PointsManager");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //Audio
            //(Since it's a prefab, the manager can't directly be connected, so we use FindObject. It's just slower.)
            FindObjectOfType<AudioManager>().Play("Pickup1");

            pointsManager.GetComponent<PointsManager>().AddPointsToPlayer(collision.gameObject.name, candyPointsVal);

            //gameAudio.Play("Pickup1");
            Destroy(npcDrop);
        }
    }
}

