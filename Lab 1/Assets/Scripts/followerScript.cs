using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class followerScript : MonoBehaviour
{
    public Rigidbody2D followerBody;
    public GameObject playerBody;
    public float followerSpeed;
    bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        followerBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, 20), 0);
        playerBody = GameObject.Find("Player");
    }
    private void Update()
    {
        if (GameManagerScript.instance.gameOver == true)
        {
            followerBody.transform.position = new Vector3(0, 50, 0);
            reset = true;
        }
        if (GameManagerScript.instance.gameOver == true && reset == true)
        {
            reset = false;
            followerBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, 20), 0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity;
        Vector2 direction = playerBody.transform.position - followerBody.transform.position;
        direction = direction.normalized; ;
        velocity = direction * followerSpeed;
        followerBody.MovePosition(followerBody.position += velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            followerBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, 20), 0);
            Destroy(collision.gameObject);
            GameManagerScript.instance.increaseScore();

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            followerBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, 20), 0);
            GameManagerScript.instance.decereaseHealth();
        }
    }
}
