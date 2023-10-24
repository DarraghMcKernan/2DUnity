using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum States
{
    Idle = 0,
    Running = 1,
    Jumping = 2,
    Falling = 3,
    Sliding = 4,
    Dead = 5,
    DeadFloor = 6
}
public class catController : MonoBehaviour
{

    public Rigidbody2D rig;
    public Animator anim;
    public BoxCollider2D boxCollider;
    public States state;
    public LayerMask groundLayer;
    float horizontalInput;

    public int lives = 3;

    public float jumpVerticalPushOff;
    Vector2 savedlocalScale;

    float horizonatlSpeed = 2;

    bool grounded = true;

    public int maxSlideTimer = 0;
    private int currentSlideTimer = 0;
    bool goingRight = true;
    private int slideCooldownTimer = 0;
    private int maxSlideCooldown = 150;

    int deadTimer = 0;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        savedlocalScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if(currentSlideTimer > 0)
        {
            currentSlideTimer = currentSlideTimer - 1;
        }
        if(slideCooldownTimer > 0)
        {
             slideCooldownTimer = slideCooldownTimer - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // what do the below lines do ?
        if (horizontalInput > 0.001f)
        {           
            if(grounded == true && currentSlideTimer == 0)
            {
                transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
                goingRight = true;
            }
            
        }
        else if (horizontalInput < -0.001f)
        {          
            if (grounded == true && currentSlideTimer == 0)
            {
                transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);//flips scale depending on direction
                goingRight = false;
            }                
        }

        if (grounded == true && state < States.Dead)
        {
            state = States.Idle;
        }
        if (currentSlideTimer > 0 && state < States.Dead)
        {
            state = States.Sliding;
        }

        if (state == States.Idle && grounded == true)
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    state = States.Jumping;
            //    Debug.Log("Space Pressed");
            //}
            if (Input.GetKey(KeyCode.D))
            {
                state = States.Running;
            }
            if (Input.GetKey(KeyCode.A))
            {
                state = States.Running;
            }
            if (Input.GetKey(KeyCode.Space) && grounded == true)
            {
                rig.velocity = new Vector2(0.0f, jumpVerticalPushOff);
                state = States.Jumping;
                grounded = false;
            }
        }

        if (state == States.Running)
        {
            if(Input.GetKey(KeyCode.LeftShift) && currentSlideTimer == 0 && slideCooldownTimer == 0)
            {
                slideCooldownTimer = maxSlideCooldown;
                currentSlideTimer = maxSlideTimer;
                state = States.Sliding;
            }
            if(Input.GetKey(KeyCode.Space) && grounded == true)
            {
                rig.velocity = new Vector2(0.0f, jumpVerticalPushOff);
                state = States.Jumping;
                grounded = false;
            }
        }

        if (state == States.Jumping)
        {
            if (goingRight == true)
            {
                horizontalInput = 1.0f;
            }
            if (goingRight == false)
            {
                horizontalInput = -1.0f;
            }
            Vector2 tempVelocity = rig.velocity;
            tempVelocity.y = tempVelocity.y * 0.996f;
            if(tempVelocity.y <=0.1f)
            {
                tempVelocity.y = 0.1f;
                state = States.Falling;
            }
            rig.velocity = tempVelocity;
            //test if the cat is moving down the screen. Change state to falling
            //If it hits the ground then state is idle
        }
        if (state == States.Sliding)
        {
            if (goingRight == true)
            {
                horizontalInput = 1.0f;
            }
            if (goingRight == false)
            {
                horizontalInput = -1.0f;
            }
            //If moving and left shift is not pressed then run
            //it no input idle

        }
        if (state == States.Falling)
        {
            if (goingRight == true)
            {
                horizontalInput = 1.0f;
            }
            if (goingRight == false)
            {
                horizontalInput = -1.0f;
            }
            Vector2 tempVelocity = rig.velocity;
            tempVelocity.y = tempVelocity.y * 1.007f;
            if(tempVelocity.y > 0)
            {
                tempVelocity.y = -tempVelocity.y;
            }
            rig.velocity = tempVelocity;
            //If moving and left shift is not pressed then run
            //it no input idle

        }

        if (state == States.Dead)
        {
            deadTimer-=1;
            if (deadTimer <= 0)
            {
                state = States.DeadFloor;
            }
        }

        if (state == States.DeadFloor)
        {
            deadTimer -= 1;
            if (deadTimer <= -900)
            {
                SceneManager.LoadScene(4);
            }
        }

            rig.velocity = new Vector2(horizontalInput * horizonatlSpeed, rig.velocity.y);

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            if(state != States.Dead)
            {
                state = States.Idle;
                //use a grounded bool
                grounded = true;
            }
        }
        if (collision.gameObject.tag == "health")
        {
            Destroy(collision.gameObject);
            GameManager.lives++;
        }
        if (collision.gameObject.tag == "Crate")
        {
            if(state == States.Sliding)
            {
                Destroy(collision.gameObject);
                GameManager.score++;
                //Debug.Log("cat broke crate");
            }
            else
            {
                Destroy(collision.gameObject);
                GameManager.lives--;
                //ebug.Log("crate hit cat");
                if (GameManager.lives <= 0)
                {
                    state = States.Dead;
                    deadTimer = 900;
                }
            }
        }
    }
}
