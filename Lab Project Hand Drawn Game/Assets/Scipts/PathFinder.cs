using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform targets;
    Transform currentTarget;
    NavMeshAgent agent;
    RaycastHit2D[] results;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        results = new RaycastHit2D[5];
        //currentTarget = targets.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance = Mathf.Infinity);
        Physics2D.CircleCast(this.transform.position, 10f, new Vector2(0, 0), new ContactFilter2D(), results, 10);


        if(results[0].collider.CompareTag("Student"))
        {
            currentTarget.position = results[0].transform.position;
        }




        agent.SetDestination(currentTarget.position);
    }
}
