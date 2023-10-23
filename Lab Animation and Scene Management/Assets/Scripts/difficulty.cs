using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficulty : MonoBehaviour
{
    public Button easy;
    public Button hard;
    public Button impossible;
    public Color defaultColour;
    public Color activeColour;
    ColorBlock active;
    ColorBlock defaultC;

    private void Start()
    {
        active = easy.colors;
        active.normalColor = activeColour;
        easy.colors = defaultC;

        defaultC = hard.colors;
        defaultC.normalColor = defaultColour;
    }

    private void Update()
    {
        if(Crate.difficulty == 1)
        {
            easy.colors = defaultC;
        }
        else easy.colors = active;

        if (Crate.difficulty == 2)
        {
            hard.colors = defaultC;
        }
        else hard.colors = active;

        if (Crate.difficulty == 3)
        {
            impossible.colors = defaultC;
        }
        else impossible.colors = active;
    }

    public void easyPressed()
    {
        Crate.difficulty = 1;
        Debug.Log("Normal");
    }
    public void hardPressed()
    {
        Crate.difficulty = 2;
        Debug.Log("Hard");
    }
    public void impossiblePressed()
    {
        Crate.difficulty = 3;
        Debug.Log("Impossible");
    }
}
