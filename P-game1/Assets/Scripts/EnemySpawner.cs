using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnCount;
    [SerializeField] float enemySpawnBeginTime;
    [SerializeField] float enemySpawnRepeatTime;
    [SerializeField] float thiefSpawnBeginTime;
    [SerializeField] float thiefSpawnRepeatTime;
    [SerializeField] GameObject thief;
    [SerializeField] GameObject enemy;
    //GameObject thief;
    GameObject[] enemies;
    GameObject[] thieves;

    private Vector3 spawnPosition;
    private Vector3 thiefSpawnPosition;
    private float spawnCount;
    private bool isSpawnEngaged;


    void Start()
    {
        isSpawnEngaged = false;
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        //thief = GameObject.FindGameObjectWithTag("Thief");
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (spawnCount >= maxSpawnCount && isSpawnEngaged)
        {
            CancelInvoke();
            isSpawnEngaged = false;
            return;
        }
        if (!isSpawnEngaged)
        {
            InvokeRepeating("EnemySpawn", enemySpawnBeginTime, enemySpawnRepeatTime);
            InvokeRepeating("ThiefSpawn", thiefSpawnBeginTime, thiefSpawnRepeatTime);
            isSpawnEngaged = true;
            
        }
        spawnCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void EnemySpawn()
    {
        spawnPosition = new Vector3(Random.Range(-8, 9), 0, Random.Range(-8,9));
        Instantiate(enemy, spawnPosition, Quaternion.identity);

    }

    void ThiefSpawn()
    {
        thiefSpawnPosition = new Vector3(Random.Range(-8, 9), 0, Random.Range(-8, 9));
        if(thiefSpawnPosition == spawnPosition)
        {
            thiefSpawnPosition = new Vector3(Random.Range(-8, 9), 0, Random.Range(-8, 9));
        }
        Instantiate(thief, thiefSpawnPosition, Quaternion.identity);
    }


}
