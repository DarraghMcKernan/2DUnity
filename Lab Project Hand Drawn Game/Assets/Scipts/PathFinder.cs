using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class PathFinder : MonoBehaviour
{
    //[SerializeField] Transform targets;
    Vector3 currentTarget = new Vector3(0,0,0);
    private Vector3 fallBack;
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

        int studentLayerMask = 64;
        contactFilter = new ContactFilter2D();
        contactFilter.layerMask = studentLayerMask;
        contactFilter.useLayerMask = true;

        fallBack = new Vector3(Random.Range(10.0f, -8.0f), Random.Range(4.0f, -4.0f), 0);
        //this.transform.position = new Vector3(UnityEngine.Random.Range(1.0f, 6.0f), UnityEngine.Random.Range(4.0f, 0.0f), 0);
    }

    void Update()
    {
        if (Time.time - lastUpdateTime >= cooldown)
        {
            fallBack = new Vector3(Random.Range(10.0f, -10.0f), Random.Range(4.0f, -4.0f), 0);
            currentTarget = checkForTargets();
            if (currentTarget == null)
            {
                currentTarget = new Vector3(0, 0, 0);//new Vector3(Random.Range(6.0f, -6.0f), Random.Range(4.0f, -4.0f), 0);
            }
            agent.SetDestination(currentTarget);
            //Debug.Log("Current Target: " + currentTarget);
            
            lastUpdateTime = Time.time;
        }
        if (currentTarget != null)
        {
            RotateTowardsTarget(currentTarget);
        }
    }



    Vector3 checkForTargets()
    {
        Physics2D.CircleCast(transform.position, 10f, Vector2.zero, contactFilter, results);

        int closest = -1;
        float closestDistance = Mathf.Infinity;

        for (int index = 0; index < results.Length; index++)
        {
            if (results[index].rigidbody != null)
            {
                if (checkIfInView(results[index].transform) == true && RaycastToCheckObstacle(this.transform.position, results[index].transform.position) == true)
                {
                    float currentDistance = Vector2.Distance(results[index].transform.position, transform.position);
                    if (currentDistance > 7f)
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

        if (closest != -1)
        {
            return results[closest].transform.position;
        }
        else return new Vector3(0,0,0);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Student"))
        {
            GameManager.studentsAlive--;
            Destroy(collision.gameObject);
            GameManager.studentsAlive--;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.lives--;
            GameManager.zombiesAlive--;
            Destroy(this.gameObject);
        }
    }
}
