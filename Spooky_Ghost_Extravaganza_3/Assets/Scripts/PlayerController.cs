using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = gameObject;
    }

    public void CheckForPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ScareNPC();
        }

        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(new Vector2(0.0f, 1.0f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(new Vector2(0.0f, -1.0f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(new Vector2(-1.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(new Vector2(1.0f, 0.0f));
        }
    }

    private void MovePlayer(Vector2 distToMove)
    {
        player.transform.position += (Vector3) distToMove * Time.deltaTime;
    }

    private void ScareNPC()
    {
        Debug.Log("Scare Key");
    }
}
