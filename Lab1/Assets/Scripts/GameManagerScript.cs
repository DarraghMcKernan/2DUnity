using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int playerLives;
    public int EnemiesAlive;
    public int playerScore;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI retryText;
    public TextMeshProUGUI highscoreText;

    public bool gameOver = false;

    int highscore = 0;

    // Start is called before the first frame update
    public static GameManagerScript instance;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Score: " + playerScore.ToString();
        //initialise the game
    }
    void Update()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Score: " + playerScore.ToString();
        if(highscore<playerScore)
        {
            highscore = playerScore;
        }
        highscoreText.text = "Highscore: " + highscore.ToString();

        if (gameOver == true)
        {
            playerLives = 3;
            playerScore = 0;
            retryText.enabled = true;
        }
        else retryText.enabled = false;
    }

    public void increaseScore()
    {
        playerScore += 10;
    }
    public void decereaseHealth()
    {
        playerLives--;
        if(playerLives < 0 )
        {
            gameOver = true;
        }
    }
}
