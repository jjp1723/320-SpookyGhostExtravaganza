using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_DropController : MonoBehaviour
{
    private GameObject npcDrop;
    public AudioManager gameAudio;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        npcDrop = gameObject;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //Audio
            //gameAudio.Play("Powerup");
            Destroy(npcDrop);
        }
    }
}

