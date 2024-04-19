using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;

    private float zEnemySpawn = 36.0f;
    private float zPowerupSpawn = 26.0f;
    public float xSpawnRange = 12.0f;
    //private float zSpawnRange = 8.0f;
    //private float zPowerupRange = 5.0f;
    private float ySpawn = 0.25f;

    private float powerupSpawnTime = 20.0f;
    private float enemySpawnTime = 1.0f;
    private float startDelay = 1.0f;
    private bool started = false;
    private GameObject enemyClone;
    private GameObject powerupClone;

    List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField] GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void StartGame()
    {
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
        started = true;
        xSpawnRange = 12.0f;
        ySpawn = .25f;
        if (manager.level == 3)
        {
            xSpawnRange = 6.0f;
        }
        if (manager.level == 4)
        {
            ySpawn = 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameActive == true && started == false)
        {
            StartGame();
        }
        else if (manager.isGameActive == false)
        {
            CancelInvoke();
            gameObjects.Clear();
            started = false;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndex = Random.Range(0, enemies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        if ((manager.level == 1 || manager.level == 2) && randomIndex == 3)
        {
            spawnPos = new Vector3(randomX, ySpawn + 3, zEnemySpawn);
        }
        if (manager.level == 4 && randomIndex == 2)
        {
            spawnPos = new Vector3(randomX, ySpawn + 2, zEnemySpawn);
        }
        if (manager.level == 6)
        {
            float randomY = Random.Range(2, 10);
            spawnPos = new Vector3(randomX, randomY, zEnemySpawn);
        }
        enemyClone = Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
        enemyClone.name = "enemy"+randomIndex;
        gameObjects.Add(enemyClone);
    }

    void SpawnPowerup()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        //float randomZ = Random.Range(zSpawnRange, zSpawnRange * 2);
        int randomIndex = Random.Range(0, powerups.Length);
        
        Vector3 spawnPos = new Vector3(randomX, ySpawn, zPowerupSpawn);


        powerupClone = Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].gameObject.transform.rotation);
        powerupClone.name = "powerup" + randomIndex;
        gameObjects.Add(powerupClone);
    }
}
