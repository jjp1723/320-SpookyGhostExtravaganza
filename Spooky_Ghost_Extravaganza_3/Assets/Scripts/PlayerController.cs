using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float scareRadius = 5.0f;

    void Start()
    {
        player = gameObject;
    }

    public void CheckForMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(new Vector2(0.0f, moveSpeed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(new Vector2(0.0f, -moveSpeed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(new Vector2(-moveSpeed, 0.0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(new Vector2(moveSpeed, 0.0f));
        }
    }

    public void CheckForScareInput(List<GameObject> npcs)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                float distSquared = Mathf.Pow(npcs[i].transform.position.x - player.transform.position.x, 2) + Mathf.Pow(npcs[i].transform.position.y - player.transform.position.y, 2);

                //Squaring the radius to avoid a square root
                if (Mathf.Pow(scareRadius, 2) > distSquared)
                {
                    npcs[i].GetComponent<NpcController>().SetScared(true);
                }
            }
        }
    }

    private void MovePlayer(Vector2 distToMove)
    {
        player.transform.position += (Vector3) distToMove * Time.deltaTime;
    }

    private void ScareNPC(GameObject npc)
    {
        Debug.Log("Scare Key");
    }
}
