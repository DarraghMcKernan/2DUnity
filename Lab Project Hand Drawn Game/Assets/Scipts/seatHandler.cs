using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seatHandler : MonoBehaviour
{
    public bool seatTaken = false;
    // Start is called before the first frame update
    void Start()
    {
        seatTaken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeSeat()
    {
        seatTaken = true;
        Debug.Log("SeatTaken");
    }
    public void leaveSeat()
    {
        seatTaken = false;
        Debug.Log("SeatLeft");
    }

}
