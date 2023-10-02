using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class verticalEnemyScript : MonoBehaviour
{
    public Rigidbody2D enemyBody;
    public int speed = 1;
    public Vector2 movement;
    public Vector2 resetHeight;
    int maxSpawnHeight = 20;
    bool reset = false;

    Vector2 minimumPosition;
    Vector2 maximumPosition; 

    // Start is called before the first frame update
    void Start()
    {
        minimumPosition = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        movement.y = -1;
        resetHeight.y = 10;

        //enemyBody.MovePosition((Random.Range(-10.0f, 10.0f), Random.Range(6, 12),0));
        enemyBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, maxSpawnHeight), 0);
    }
    void Update()
    {
        if (GameManagerScript.instance.gameOver == true)
        {
            enemyBody.transform.position = new Vector3(0,50,0);
            reset = true;
        }
        if (GameManagerScript.instance.gameOver == true && reset == true)
        {
            reset = false;
            enemyBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, maxSpawnHeight), 0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        enemyBody.MovePosition(enemyBody.position + movement * speed * Time.fixedDeltaTime);
        if (enemyBody.position.y < minimumPosition.y)
        {
            enemyBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, maxSpawnHeight), 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, maxSpawnHeight), 0);
            Destroy(collision.gameObject);
            GameManagerScript.instance.increaseScore();
            GameManagerScript.instance.increaseScore();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyBody.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(6, maxSpawnHeight), 0);
            GameManagerScript.instance.decereaseHealth();
        }
    }

}
