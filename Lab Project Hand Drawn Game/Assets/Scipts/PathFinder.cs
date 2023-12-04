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
    Transform currentTarget = null;
    public Transform fallBack;
    NavMeshAgent agent;
    RaycastHit2D[] results;

    float cooldown = 0.3f; // Cooldown time in seconds
    float lastUpdateTime;

    ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        results = new RaycastHit2D[20];

        int studentLayerMask = 1 << LayerMask.NameToLayer("StudentLayer");
        contactFilter = new ContactFilter2D();
        contactFilter.layerMask = studentLayerMask;


        //this.transform.position = new Vector3(UnityEngine.Random.Range(1.0f, 6.0f), UnityEngine.Random.Range(4.0f, 0.0f), 0);
    }

    void Update()
    {
        if (Time.time - lastUpdateTime >= cooldown)
        {
            currentTarget = checkForTargets();
            if (currentTarget == null)
            {
                currentTarget = fallBack;
            }
            agent.SetDestination(currentTarget.position);
            //Debug.Log("Current Target: " + currentTarget);
            
            lastUpdateTime = Time.time;
        }
        if (currentTarget != null)
        {
            RotateTowardsTarget(currentTarget.position);
        }
    }



    Transform checkForTargets()
    {
        Physics2D.CircleCast(transform.position, 10f, Vector2.zero, new ContactFilter2D(), results);

        int closest = -1;
        float closestDistance = Mathf.Infinity;

        for (int index = 0; index < results.Length; index++)
        {
            if (results[index].rigidbody != null)
            {
                if (results[index].collider.CompareTag("Student"))
                {
                    if (checkIfInView(results[index].transform) == true && RaycastToCheckObstacle(this.transform.position, results[index].transform.position) == true)
                    {
                        float currentDistance = Vector2.Distance(results[index].transform.position, transform.position);
                        if(currentDistance > 7f)
                        {
                            //Debug.Log("Too far away");
                        }
                        else if (closest == -1 || closestDistance > currentDistance) 
                        {
                            closestDistance = currentDistance;
                            closest = index;
                        }
                    }
                }
            }
        }

        if (closest != -1)
        {
            return results[closest].transform;
        }
        else return null;
    }


    bool checkIfInView(Transform t_transform)
    {
        Vector3 directionToTarget = t_transform.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.up, directionToTarget);

        // Check if the angle to the target is within 45 degrees
        if (angleToTarget <= 30f)
        {
            return true;
        }

        return false;
    }

    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    bool RaycastToCheckObstacle(Vector3 start, Vector3 end)
    {
        RaycastHit2D hit = Physics2D.Linecast(start, end);

        if (hit.collider.CompareTag("Walls"))
        {
            return false;
        }
        else return true;
    }
}
