using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject thiefObjectToSpawn;
    [SerializeField] GameObject playerObjectToSpawn;

    GameObject[] objects;

    void Update()
    {
        objects = GameObject.FindGameObjectsWithTag("Grabbable");
        int thiefObjectCount = 0;
        int playerObjectCount = 0;
        foreach (var grabObject in objects)
        {
            if (grabObject.GetComponent<Collider>().enabled == true)
            {
                if (grabObject.GetComponent<ThiefScoreObject>() != null)
                {
                    thiefObjectCount += 1;
                }
                if (grabObject.GetComponent<PlayerScoreObject>() != null)
                {
                    playerObjectCount += 1;
                }
            }
        }
        if (thiefObjectCount < 2) ObjectSpawn(thiefObjectToSpawn);
        if (playerObjectCount < 2) ObjectSpawn(playerObjectToSpawn);

        /*
        foreach (var spawnObject in objects)
        {
            if (spawnObject.GetComponent<Collider>().enabled == true)
            {
                if (thiefObjectCount < 2 && spawnObject.GetComponent<ThiefScoreObject>() != null) ObjectSpawn(thiefObjectToSpawn);
                if (playerObjectCount < 2 && spawnObject.GetComponent<PlayerScoreObject>() != null) ObjectSpawn(playerObjectToSpawn);
            }
        }
        */
    }

    void ObjectSpawn(GameObject spawnObject)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-8, 9), 10, Random.Range(-8, 9));
        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }
}
