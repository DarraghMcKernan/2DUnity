using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateSpawner : MonoBehaviour
{
    public GameObject crateObj;

    private int spawnTimer;
    public int maxSpawnTimer = 200;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 200;
        generateCrate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer--;
        if (spawnTimer < 0)
        {
            spawnTimer = maxSpawnTimer - ((Crate.difficulty-1) * 125);
            generateCrate();
        }
    }
    void generateCrate()
    {
        GameObject tempCrate = crateObj;
        Vector2 spawnPosition = new Vector2(20.0f, -2.2f);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(tempCrate, spawnPosition, spawnRotation);
    }
}
