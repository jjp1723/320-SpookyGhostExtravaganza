using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        obstacle = gameObject;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().StopPlayer();
            Debug.Log("Hit Block");
        }
    }
}
