using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;

    private float moveSpeed;
    private float scareRadius;
    private Rigidbody2D rb;

    private BoxCollider2D boxCollider;

    [SerializeField]
    private GameObject scareEffect;
    GameObject circle;

    [SerializeField]
    private GameObject broomPrefab;
    GameObject broomInstance;
    Broomstick broomComponent;

    [SerializeField]
    private GameObject skateboardPrefab;
    GameObject skateboardInstance;

    [SerializeField]
    private bool isPlayer1;

    private AudioManager gameAudio;

    //cooldown
    private float scaredTimer = 0.0f;
    private float scareCooldown = 2.0f;
    private float powerupCooldown = 30.0f;

    //powerup variables
    private int broomUse = 0;
    [SerializeField]
    private float defualtScareRadius = 4.0f;
    private float upgradedScareRadius;
    private float scareRadiusTimer = 0.0f;

    [SerializeField]
    private float defaultMoveSpeed = 3.0f;
    private float upgradedMoveSpeed;
    private float moveSpeedTimer = 0.0f;

    public bool hasCascade = false;
    private float cascadeTimer = 0.0f;

    void Start()
    {
        gameAudio = FindObjectOfType<AudioManager>();
        player = gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        boxCollider = player.GetComponent<BoxCollider2D>();

        moveSpeed = defaultMoveSpeed;
        upgradedMoveSpeed = defaultMoveSpeed * 2;
        scareRadius = defualtScareRadius;
        upgradedScareRadius = defualtScareRadius * 2;

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
            if (Input.GetKey(KeyCode.UpArrow))
            {
                velocityVec.y += moveSpeed;
                keyPressed = true;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                velocityVec.y -= moveSpeed;
                keyPressed = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                velocityVec.x -= moveSpeed;
                keyPressed = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
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
            if (scaredTimer <= (scareCooldown - 0.5f) && circle != null)
            {
                Destroy(circle);
            }
            scaredTimer -= Time.deltaTime;
        }

        //update scare radius powerup
        if (scareRadiusTimer >= 0.0f)
        {
            scareRadiusTimer -= Time.deltaTime;
        }
        //else
        //{
        //    scareRadius = defualtScareRadius;
        //    scaredTimer = 0.0f;
        //}

        if (moveSpeedTimer >= 0.0f)
        {
            moveSpeedTimer -= Time.deltaTime;
        }
        //else
        //{
        //    moveSpeed = defaultMoveSpeed;
        //    moveSpeedTimer = 0.0f;
        //}

        if (cascadeTimer >= 0.0f)
        {
            cascadeTimer -= Time.deltaTime;
        }
        //else
        //{
        //    hasCascade = false;
        //    cascadeTimer = 0.0f;
        //}
    }

    private void CheckForScareInput()
    {
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.F) && scaredTimer <= 0.0f)
            {
                scaredTimer = scareCooldown;

                //set radius to be scareRadius
                scareEffect.transform.localScale = new Vector3(scareRadius, scareRadius, 0);

                circle = Object.Instantiate(scareEffect, player.transform.position, Quaternion.identity, transform);
                circle.GetComponent<SpriteRenderer>().sortingOrder = 1000;

                //Audio "BOO!"
                gameAudio.Play("Ghost");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift) && scaredTimer <= 0.0f)
            {
                scaredTimer = scareCooldown;

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
            switch (powerup.type)
            {
                case "Megaphone":
                    scareRadius = upgradedScareRadius;
                    scareRadiusTimer = powerupCooldown;
                    break;
                case "Broom":
                    broomUse = 3;
                    broomInstance = Object.Instantiate(broomPrefab, player.transform.position, Quaternion.identity, transform);
                    broomInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 40.31f);
                    broomInstance.transform.position += new Vector3(0.07f, -0.3f, 0.0f);
                    broomComponent = broomInstance.GetComponent<Broomstick>();
                    break;
                case "Skateboard":
                    moveSpeed = upgradedMoveSpeed;
                    moveSpeedTimer = powerupCooldown;
                    skateboardInstance = Object.Instantiate(skateboardPrefab, player.transform.position, Quaternion.identity, transform);
                    skateboardInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 40.31f);
                    skateboardInstance.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                    skateboardInstance.transform.position += new Vector3(0.07f, -0.5f, 0.0f);
                    break;
                case "Cascade":
                    hasCascade = true;
                    cascadeTimer = powerupCooldown;
                    break;

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
