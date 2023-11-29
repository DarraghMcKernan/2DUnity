using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PathFinder : MonoBehaviour
{
    //[SerializeField] Transform targets;
    Transform currentTarget;
    NavMeshAgent agent;
    RaycastHit2D[] results;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        results = new RaycastHit2D[20];

        //this.transform.position = new Vector3(UnityEngine.Random.Range(1.0f, 6.0f), UnityEngine.Random.Range(4.0f, 0.0f), 0);
    }

    void Update()
    {
        currentTarget = checkForTargets();

        agent.SetDestination(currentTarget.position);
    }



    Transform checkForTargets()
    {
        Physics2D.CircleCast(this.transform.position, 2f, new Vector2(0, 0), new ContactFilter2D(), results);

        int closest = 0;
        float closestDistance = Mathf.Infinity;
        for (int index = 0; index < results.Length; index++)
        {
            if (results[index].rigidbody != null &&results[index].collider.CompareTag("Student"))
            {
                if (checkIfInView(results[index].transform) == true)
                {
                    float currentDistance = Vector2.Distance(results[index].transform.position, results[closest].transform.position);

                    if (closestDistance > currentDistance)
                    {
                        closestDistance = currentDistance;
                        closest = index;
                    }
                } 
            }
        }


        float rotation = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(rotation -= 90, Vector3.forward);


        return results[closest].transform;
    }

    bool checkIfInView(Transform t_transform)
    {
        float angle = Vector2.Angle(this.transform.position, t_transform.position);

        float forwardAngle = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg;
        forwardAngle += 270;

        if(angle < forwardAngle + 45 && angle > forwardAngle - 45 )
        {
            Debug.Log("student: " + angle + " facing: " + forwardAngle);

            return true;
        }
        return false;
    }
}
