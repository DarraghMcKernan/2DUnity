using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeatSelector : MonoBehaviour
{
    public GameObject seatHolder;

    private List<Transform> chairs = new List<Transform>();

    public int classTimer = 0;
    public int timeBetweenClasses = 600;

    void Start()
    {
        if (seatHolder == null)
        {
            seatHolder = gameObject;
        }

        chairs.Clear();

        foreach (Transform child in seatHolder.transform)
        {
            chairs.Add(child);
        }

        foreach (Transform child in chairs)
        {
            Vector3 childPosition = child.position;

            seatHandler myScript = child.GetComponent<seatHandler>();
            if (myScript != null)
            {
                myScript.seatTaken = false;
            }
        }

        classTimer = timeBetweenClasses;
    }

    private void FixedUpdate()
    {
        if(classTimer > 0)
        {
            classTimer--;
        }
        else
        {
            classTimer = timeBetweenClasses;
        }
        if (classTimer == 50)
        {
            leaveAllSeats();
        }
    }

    public Vector3 takeSeat()
    {
        //bools seatTaken[checkSeat] = true
        //vector3 seatPos[checkSeat];
        //if true cant select seat
        //set a pos 
        //return vector3 seatPos[checkSeat]
        bool seatFound = false;
        Transform foundSeatPos = null;
        while(seatFound == false)
        {
            int randomSeat = Random.Range(0, chairs.Count);
            seatHandler childScript = chairs[randomSeat].GetComponent<seatHandler>();
            if (childScript.seatTaken == true)
            {
                //Debug.Log("Seat Taken Already");
            }
            else
            {
                seatFound = true;
                childScript.seatTaken = true;
                foundSeatPos = chairs[randomSeat];
            }
        }
        return foundSeatPos.transform.position;
    }
    public void leaveAllSeats()
    {
        foreach (Transform child in chairs)
        {
            Vector3 childPosition = child.position;

            seatHandler myScript = child.GetComponent<seatHandler>();
            if (myScript != null)
            {
                myScript.seatTaken = false;
            }
        }
    }
}