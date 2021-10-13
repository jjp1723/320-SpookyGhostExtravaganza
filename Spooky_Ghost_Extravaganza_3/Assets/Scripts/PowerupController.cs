using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private GameObject powerup;
    public AudioManager gameAudio;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        powerup = gameObject;
    }

    public void CheckForInput()
    {
        //test that position works
        if (Input.GetKeyDown(KeyCode.M))
        {
            Move();
        }
    }

    //this can be used to position it on screen
    private void Move()
    {
        powerup.transform.position = new Vector2(Random.Range(-5f,5f), Random.Range(-5f, 5f));
    }

    private void Move(float x, float y)
    {
        powerup.transform.position = new Vector2(x, y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //Audio
            gameAudio.Play("Powerup");

            Debug.Log("Picked up Power up!");
            Destroy(powerup);
        }
    }
}
