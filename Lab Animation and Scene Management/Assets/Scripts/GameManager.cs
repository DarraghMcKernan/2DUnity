using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //example   
    public static int difficultyLevel;
    public static int score = 0;
    public static int lives = 3;

    public TMP_Text scoreText;
    public TMP_Text livesText;

    private void Start()
    {
        scoreText.text = "Crates Smashed: " + score;
        livesText.text = "Lives: " + lives;
        score = 0;
    }

    private void Update()
    {
        if(score >=0)
        {
            scoreText.text = "Crates Smashed: " + score;
        }
        
        livesText.text = "Lives: " + lives;
    }


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
