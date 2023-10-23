using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //example   
    public static int difficultyLevel;
    private void Awake()
    {
        //numtimes = 0;
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);

        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);

        //}
    }

}
