using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button game;

    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

}
