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
    private Rigidbody2D rb;

    private BoxCollider2D boxCollider;

    [SerializeField]
    private GameObject scareEffect;
    Object circle;

    public AudioManager gameAudio;

    //cooldown
    private float scaredTimer = 0.0f;

    //powerup variables
    private int broomUse = 0;

    void Start()
    {
        player = gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        boxCollider = player.GetComponent<BoxCollider2D>();
    }

    public void CheckForMovementInput()
    {
        bool keyPressed = false;
        Vector2 velocityVec = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))
        {
            velocityVec.y  += moveSpeed;
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocityVec.y -= moveSpeed;
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocityVec.x -= moveSpeed;
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocityVec.x += moveSpeed;
            keyPressed = true;
        }

        if(!keyPressed)
        {
            StopPlayer();
        }
        else
        {
            //if (velocityVec.x != 0 && velocityVec.y != 0)
            //{
            //    if (rb.velocity.y != 0.0f)
            //    {
            //        velocityVec.x = 0.0f;
            //    }
            //    else
            //    {
            //        velocityVec.y = 0.0f;
            //    }
            //}
            MovePlayer(velocityVec);
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

            //set radius to be scareRadius
            scareEffect.transform.localScale = new Vector3(scareRadius, scareRadius, 0);

            circle = Object.Instantiate(scareEffect, player.transform.position, Quaternion.identity, transform);
            //for (int i = 0; i < npcs.Count; i++)
            //{
            //    float distSquared = Mathf.Pow(npcs[i].transform.position.x - player.transform.position.x, 2) + Mathf.Pow(npcs[i].transform.position.y - player.transform.position.y, 2);

            //    //Squaring the radius to avoid a square root
            //    if (Mathf.Pow(scareRadius, 2) > distSquared)
            //    {
            //        npcs[i].GetComponent<NpcController>().UpdateScared(true);
            //    }
            //}

            //Audio "BOO!"
            gameAudio.Play("Ghost");
        }
        //else
        //{
        //    for (int i = 0; i < npcs.Count; i++)
        //    {
        //        npcs[i].GetComponent<NpcController>().UpdateScared(false);
        //    }
        //}
    }

    public void StopPlayer()
    {
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }
    
    private void MovePlayer(Vector2 distToMove)
    {
        rb.velocity = (Vector3) distToMove;
    }

    private void ScareNPC(GameObject npc)
    {
        Debug.Log("Scare Key");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //increase scare radius
        if (collision.gameObject.name == "Megaphone")
        {
            scareRadius = 10.0f;
        }

        if(collision.gameObject.name == "Broom")
        {
            broomUse = 3;
        }

        if(collision.gameObject.name == "Obstacle")
        {
            Debug.Log("Hit Obstacle");
            if (broomUse > 0)
            {
                boxCollider.isTrigger = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Obstacle")
        {
            broomUse--;
            boxCollider.isTrigger = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Obstacle")
        {

            broomUse--;
            boxCollider.isTrigger = false;
        }
    }
}
