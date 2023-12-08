using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int studentsAlive;
    public static int zombiesAlive;
    bool gameLost = false;
    bool gameWin = false;



    // Start is called before the first frame update
    void Start()
    {
        studentsAlive = 50;
        zombiesAlive = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if(studentsAlive <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        if(zombiesAlive <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
