using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField]
    private GameObject npcDrop;

    private Vector2 moveDir;

    private bool isScared = false;
    public bool IsScared { get => isScared; set => UpdateScared(value); }

    private float scaredTimer = 2.0f;

    void Start()
    {
        transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

        //set moveDir to random direction
        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    public void Move()
    {
        Vector3 pos = transform.position;
        transform.position += (Vector3)moveDir * Time.deltaTime;
        if (pos.x > 5f && moveDir.x > 0)
        {
            moveDir.x *= -1;
        }
        if (pos.x < -5f && moveDir.x < 0)
        {
            moveDir.x *= -1;
        }
        if (pos.y > 5f && moveDir.y > 0)
        {
            moveDir.y *= -1;
        }
        if (pos.y < -5f && moveDir.y < 0)
        {
            moveDir.y *= -1;
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
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "ScareCircle(Clone)")
        {
            if (!isScared)
            {
                GameObject drop = Instantiate(npcDrop);
                drop.transform.position = transform.position;

                float randNum = Random.Range(0.0f, 1.0f);
                drop.GetComponent<Rigidbody2D>().velocity = 3 * new Vector3(randNum, 1.0f - randNum);
            }

            Debug.Log("Scared");
            UpdateScared(true);
        }
    }
}
