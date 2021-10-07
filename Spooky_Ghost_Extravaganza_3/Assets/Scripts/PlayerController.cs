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

    [SerializeField]
    private GameObject scareEffect;
    Object circle;

    //cooldown
    private float scaredTimer = 0.0f;

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

        //update scare timer
        if (scaredTimer >= 0.0f)
        {
            if (scaredTimer <= 1.25f && circle != null)
            {
                Destroy(circle);
            }
            scaredTimer -= Time.deltaTime;
        }
    }

    public void CheckForScareInput(List<GameObject> npcs)
    {
        if (Input.GetKeyDown(KeyCode.F) && scaredTimer <= 0.0f)
        {
            scaredTimer = 2.0f;

            circle = Object.Instantiate(scareEffect, player.transform.position, Quaternion.identity);
            //for (int i = 0; i < npcs.Count; i++)
            //{
            //    float distSquared = Mathf.Pow(npcs[i].transform.position.x - player.transform.position.x, 2) + Mathf.Pow(npcs[i].transform.position.y - player.transform.position.y, 2);

            //    //Squaring the radius to avoid a square root
            //    if (Mathf.Pow(scareRadius, 2) > distSquared)
            //    {
            //        npcs[i].GetComponent<NpcController>().UpdateScared(true);
            //    }
            //}
        }
        //else
        //{
        //    for (int i = 0; i < npcs.Count; i++)
        //    {
        //        npcs[i].GetComponent<NpcController>().UpdateScared(false);
        //    }
        //}
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
