using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public int speed = 1;
    public int deletionTimer = 60;
    public int addScore = 20;
    public int loseScore = -50;
    //private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotation = transform.eulerAngles.z;

        rotation = rotation + 90.0f;
        float angleInRadians = rotation * Mathf.Deg2Rad;

        Vector2 bulletDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        GetComponent<Rigidbody2D>().velocity = bulletDirection * speed;

        deletionTimer--;
        if(deletionTimer <0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(collision.gameObject);
            PlayerController.score += 20;
            GameManager.zombiesAlive--;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Student"))
        {
            Destroy(collision.gameObject);
            PlayerController.score -= 50;
            GameManager.studentsAlive--;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {

        }
        else Destroy(this.gameObject);
    }
}
