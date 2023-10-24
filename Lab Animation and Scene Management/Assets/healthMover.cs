using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthMover : MonoBehaviour
{
    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector2(rig.velocity.x, -1f);
        if (rig.transform.position.y <= -0.5)
        {
            Destroy(this.gameObject);
        }
    }
}
