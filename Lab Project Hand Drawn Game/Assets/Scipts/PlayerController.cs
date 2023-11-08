using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;

    public GameObject bulletPrefab;
    private int bulletCooldownTimer = 0;
    public int maxBulletCooldown = 10;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
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
