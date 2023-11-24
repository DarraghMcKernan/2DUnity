using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeatSelector : MonoBehaviour
{
    public GameObject seatHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void resetSeats()
    {
        
    }

    public Vector3 takeSeat()
    {
        //bools seatTaken[checkSeat] = true
        //vector3 seatPos[checkSeat];
        //if true cant select seat
        //set a pos 
        //return vector3 seatPos[checkSeat]
        bool seatFound = false;
        int pickRoom = Random.Range(0, 6);
        int pickSeat = Random.Range(0, 10);

        

        return transform.position;
    }
}
