using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;

    public GameObject bulletPrefab;
    private int bulletCooldownTimer = 0;
    public int maxBulletCooldown = 40;

    public static int score = 0;

    private int lives = 3;

    private Vector3 direction;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives; 

        Vector3 movement = new Vector2 (movementX, movementY);

        gameObject.transform.position = gameObject.transform.position + (movement/8);

        bulletCooldownTimer--;
        if (bulletCooldownTimer < 0)
        {
            bulletCooldownTimer = 0;
            if (Input.GetMouseButton(0))
            {
                Instantiate(bulletPrefab, gameObject.transform.position, getRotationForBullet());
                bulletCooldownTimer = maxBulletCooldown;
            }
        }
    }

    public void changeScore(int t_score)
    {
        score += t_score;
    }


    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private Quaternion getRotationForBullet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return Quaternion.Euler(0, 0, angle-90);
    }
}
