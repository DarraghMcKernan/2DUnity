using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public int speed;
    public Vector2 movement;
    public Vector2 bulletSpeed;
    public Rigidbody2D rg;
    public GameObject bullet;
    public GameObject bounceBall;
    const float MAX_COOLDOWN = 10.0f;
    float cooldownTimer = 0;
    float cooldownBouncerTimer = 0;


    private void Update()
    {
        if(GameManagerScript.instance.gameOver==false)
        {
            float deltaX = Input.GetAxisRaw("Horizontal");
            float deltaY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(deltaX, deltaY);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = MAX_COOLDOWN;
                    Shoot();
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (cooldownBouncerTimer <= 0)
                {
                    cooldownBouncerTimer = MAX_COOLDOWN*6;
                    ShootBouncer();
                }
            }
        }
        if(GameManagerScript.instance.gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameManagerScript.instance.gameOver = false;
            }
            rg.transform.position = new Vector3(0.0f, -4.0f, 0.0f);
        }
    }

    void FixedUpdate()
    {
        rg.MovePosition(rg.position + movement * speed * Time.fixedDeltaTime);

        cooldownTimer--; ;
        if(cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
        cooldownBouncerTimer--; ;
        if (cooldownBouncerTimer < 0)
        {
            cooldownBouncerTimer = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            rg.transform.position = new Vector3(10.0f, rg.transform.position.y, 0.0f);
        }
        if (collision.gameObject.CompareTag("RightWall"))
        {
            rg.transform.position = new Vector3(-10.0f, rg.transform.position.y, 0.0f);
        }
    }


    void Shoot()
    {
        GameObject playersBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        playersBullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        // destroy bullet after one and a  half of seconds
        Destroy(playersBullet, 2.5f);
    }
    void ShootBouncer()
    {
        GameObject playersBullet = Instantiate(bounceBall, gameObject.transform.position, Quaternion.identity);
        float temp = UnityEngine.Random.Range(-3.0f, 3.0f);
        bulletSpeed.x = temp;
        playersBullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        
        // destroy bullet after one and a  half of seconds
        Destroy(playersBullet, 2.5f);
    }
}
