using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Rigidbody2D boxRig;
    bool goingRight = true;
    public static int difficulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        int direction = Random.Range(0, 2);
        if (direction == 0)
        {
            goingRight = true;
            boxRig.position = new Vector2(-10.0f,boxRig.position.y);
        }
        if (direction == 1)
        {
            goingRight = false;
            boxRig.position = new Vector2(10.0f, boxRig.position.y);
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goingRight == true)
        {
            boxRig.velocity = new Vector2(1f + (difficulty*1.5f), boxRig.velocity.y);
        }
        if (goingRight == false)
        {
            boxRig.velocity = new Vector2(-1f - (difficulty * 1.5f), boxRig.velocity.y);
        }

        if (boxRig.position.x < -12.0f)
        {
            Destroy(this.gameObject);
        }
        if (boxRig.position.x > 12.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
