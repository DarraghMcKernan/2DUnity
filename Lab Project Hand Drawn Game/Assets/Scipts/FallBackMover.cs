using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallBackMover : MonoBehaviour
{
    private int moveTimer;
    private int moveCooldown = 180;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveTimer > 0)
        {
            moveTimer--;
        }
        else
        {
            moveTimer = moveCooldown;
            this.transform.position = new Vector3(Random.Range(10.0f, -10.0f),Random.Range(4.0f,-4.0f),0);
        }
    }
}
