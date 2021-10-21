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

    [SerializeField]
    private GameObject broomPrefab;
    GameObject broomInstance;
    Broomstick broomComponent;

    [SerializeField]
    private bool isPlayer1;

    private AudioManager gameAudio;

    //cooldown
    private float scaredTimer = 0.0f;

    //powerup variables
    private int broomUse = 0;

    void Start()
    {
        gameAudio = FindObjectOfType<AudioManager>();
        player = gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        boxCollider = player.GetComponent<BoxCollider2D>();
    }

    public void CheckForInput()
    {
        CheckForMovementInput();
        CheckForScareInput();
    }

    private void CheckForMovementInput()
    {
        bool keyPressed = false;
        Vector2 velocityVec = new Vector2(0.0f, 0.0f);

        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                velocityVec.y += moveSpeed;
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
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                velocityVec.y += moveSpeed;
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
        }

        if(!keyPressed)
        {
            StopPlayer();
        }
        else
        {
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

    private void CheckForScareInput()
    {
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.F) && scaredTimer <= 0.0f)
            {
                scaredTimer = 2.0f;

                //set radius to be scareRadius
                scareEffect.transform.localScale = new Vector3(scareRadius, scareRadius, 0);

                circle = Object.Instantiate(scareEffect, player.transform.position, Quaternion.identity, transform);

                //Audio "BOO!"
                gameAudio.Play("Ghost");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift) && scaredTimer <= 0.0f)
            {
                scaredTimer = 2.0f;

                //set radius to be scareRadius
                scareEffect.transform.localScale = new Vector3(scareRadius, scareRadius, 0);

                circle = Object.Instantiate(scareEffect, player.transform.position, Quaternion.identity, transform);

                //Audio "BOO!"
                gameAudio.Play("Ghost");
            }
        }
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
        PowerupController powerup = collision.gameObject.GetComponent<PowerupController>();
        if (powerup)
        {
            //increase scare radius
            if (powerup.type == "Megaphone")
            {
                scareRadius = 10.0f;
            }

            if (powerup.type == "Broom")
            {
                broomUse = 3;
                broomInstance = Object.Instantiate(broomPrefab, player.transform.position, Quaternion.identity, transform);
                broomInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 40.31f);
                broomInstance.transform.position += new Vector3(0.07f, -0.3f, 0.0f);
                broomComponent = broomInstance.GetComponent<Broomstick>();
            }
        }

        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit Obstacle");
            if (broomUse > 0)
            {
                boxCollider.isTrigger = true;
                //Audio
                gameAudio.Play("Broom");
                //Visual
                switch(broomUse)
                {
                    case 3:
                        broomComponent.HideLeft();
                        break;
                    case 2:
                        broomComponent.HideRight();
                        break;
                    case 1:
                        broomComponent = null;
                        Destroy(broomInstance);
                        break;
                }
            }
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if(collision.gameObject.name == "Obstacle")
    //    {
    //        broomUse--;
    //        boxCollider.isTrigger = false;
    //    }
    //}

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Obstacle")
        {

            broomUse--;
            boxCollider.isTrigger = false;
        }
    }
}
