using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] List<GameObject> waypointList = new List<GameObject>();

    [SerializeField] float zombieSpawnDelay = 3f;

    private void Start()
    {
        StartCoroutine(ZombieSpawnRoutine());
    }

    private int GetRandomPosition()
    {
        return Random.Range(0, waypointList.Count);
    }

    private void SpawnZombie()
    {
        Vector3 offset = new Vector3(1.5f, 0, 9.5f);
        int spawnPos = GetRandomPosition();
        Debug.Log("Spawning at waypoint: " +  spawnPos);
        Instantiate(zombiePrefab, waypointList[spawnPos].transform.position, Quaternion.Euler(0,180,0));
    }

    IEnumerator ZombieSpawnRoutine()
    {
        while (true)
        {
            SpawnZombie();
            yield return new WaitForSeconds(zombieSpawnDelay);
        }
    }
}
