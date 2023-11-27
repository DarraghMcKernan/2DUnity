using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class StudentBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    public SeatSelector getSeat;
    Vector3 currentSeat;
    int classTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    { 
        agent.SetDestination(currentSeat);
    }

    private void FixedUpdate()
    {
        if (classTimer > 0)
        {
            classTimer--;
        }
        else
        {
            currentSeat = getSeat.takeSeat();
            classTimer = 600;
        }
    }
}
