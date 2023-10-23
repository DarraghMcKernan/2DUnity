using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficulty : MonoBehaviour
{
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
