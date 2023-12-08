using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    private Vector3 spawnPoint;
    public Transform zombieSpawnPoint;

    private int spawnTimer;
    private int spawnCooldown = 120;
    public int zombieCount = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer > 0)
        {
            spawnTimer--;
        }
        else
        {
            spawnTimer = spawnCooldown;

            //zombieSpawnPoint.position = randomSpawnPoint();
            Instantiate(ZombiePrefab, zombieSpawnPoint);
        }
    }

    Vector3 randomSpawnPoint()
    {
        spawnPoint = new Vector3(0, 0, 0);
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            spawnPoint = new Vector3(spawnPoint.x - 12f, spawnPoint.y, spawnPoint.z);
        }
        if (rand == 1)
        {
            spawnPoint = new Vector3(spawnPoint.x + 12f, spawnPoint.y, spawnPoint.z);
        }
        if (rand == 2)
        {
            spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y + 10f, spawnPoint.z);
        }
        if (rand == 3)
        {
            spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y - 8f, spawnPoint.z);
        }
        //spawnPoint = new Vector3(0, 0, 0);
        return spawnPoint;
    }
}
