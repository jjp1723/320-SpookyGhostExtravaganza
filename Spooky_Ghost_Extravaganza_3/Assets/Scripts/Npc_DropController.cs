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
            //(Since it's a prefab, the manager can't directly be connected, so we use FindObject. It's just slower.)
            FindObjectOfType<AudioManager>().Play("Pickup1");
            //gameAudio.Play("Pickup1");
            Destroy(npcDrop);
        }
    }
}

