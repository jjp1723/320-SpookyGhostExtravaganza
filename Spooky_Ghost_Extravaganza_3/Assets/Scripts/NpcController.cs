using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField]
    private GameObject npcDrop;

    private Vector2 moveDir;
    private float moveSpeed = 2.0f;

    private bool isScared = false;
    public bool IsScared { get => isScared; set => UpdateScared(value); }

    private float scaredTimer = 2.0f;

    //Demon-Child sprites
    private Sprite demon;
    private Sprite demonScream;

    [SerializeField]
    private float[] xBounds = { -10.0f, 10.0f };
    [SerializeField]
    private float[] yBounds = { -10.0f, 10.0f };

    //for cascade scare
    private GameObject circle;

    void Start()
    {
        transform.position = new Vector2(Random.Range(xBounds[0], xBounds[1]), Random.Range(yBounds[0], yBounds[1]));

        float theta = Random.Range(0, Mathf.PI * 2);

        //set moveDir to random direction
        moveDir = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));

        //Loading the sprites
        demon = Resources.Load<Sprite>("Demon-Child-Front_0");
        demonScream = Resources.Load<Sprite>("Demon-Child-Scream");
    }

    public void Move()
    {
        Vector3 pos = transform.position;
        transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;
        if (pos.x > xBounds[1] && moveDir.x > 0)
        {
            moveDir.x *= -1;
            moveDir.y = Mathf.Sin(Random.Range(Mathf.PI / 2, 3 * Mathf.PI / 2));
        }
        if (pos.x < xBounds[0] && moveDir.x < 0)
        {
            moveDir.x *= -1;
            moveDir.y = Mathf.Sin(Random.Range(-Mathf.PI / 2, Mathf.PI / 2));
        }
        if (pos.y > yBounds[1] && moveDir.y > 0)
        {
            moveDir.y *= -1;
            moveDir.x = Mathf.Cos(Random.Range(Mathf.PI, 2 * Mathf.PI));
        }
        if (pos.y < yBounds[0] && moveDir.y < 0)
        {
            moveDir.y *= -1;
            moveDir.x = Mathf.Cos(Random.Range(0, Mathf.PI));
        }

        UpdateScared(false);
    }

    public void UpdateScared(bool isScared)
    {
        if (this.isScared && scaredTimer >= 0.0f)
        {
            scaredTimer -= Time.deltaTime;
        }
        else
        {
            this.isScared = isScared;

            if (isScared)
            {
                scaredTimer = 2.0f;
                moveSpeed = 7.0f;
                //gameObject.GetComponent<Animator>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = demonScream;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                moveSpeed = 1.0f;

                gameObject.GetComponent<SpriteRenderer>().sprite = demon;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                //gameObject.GetComponent<Animator>().enabled = true;
                
                //if no longer scared destroy scare cirlce
                if (circle != null)
                {
                    Destroy(circle);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScareCircle(Clone)" || obj.name == "ScareCircle(Clone)(Clone)")
        {
            if (!isScared)
            {
                GameObject drop = Instantiate(npcDrop);
                drop.transform.position = transform.position;

                float randNum = Random.Range(0.0f, 1.0f);
                drop.GetComponent<Rigidbody2D>().velocity = 3 * new Vector3(randNum, 1.0f - randNum);

                PlayerController script = obj.transform.parent.gameObject.GetComponent<PlayerController>();

                if (script && script.hasCascade)
                {
                    circle = Object.Instantiate(obj.gameObject, this.transform.position, Quaternion.identity, transform);
                    circle.transform.localScale = new Vector3(3f, 3f, 0);
                }
            }

            Debug.Log("Scared");
            UpdateScared(true);
        }
    }
}
