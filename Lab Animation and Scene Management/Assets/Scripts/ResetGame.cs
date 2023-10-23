using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void restartButton()
    {
        SceneManager.LoadScene(2);
        GameManager.difficultyLevel = 1;
        GameManager.lives = 3;
        GameManager.score = 0;
    }
}