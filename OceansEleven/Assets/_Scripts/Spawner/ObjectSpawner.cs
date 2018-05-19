using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnedObjectConfig
    {
        public GameObject gameObject;
        public int weight = 1;
    }

    public List<SpawnedObjectConfig> _spawnableObjects = new List<SpawnedObjectConfig>();
    public int _initialWaitSeconds = 10;
    public int _waitBetweenSpawnSecondsMin = 10;
    public int _waitBetweenSpawnSecondsMax = 20;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_initialWaitSeconds);

        while (true)
        {
            Spawn();

            int randomWait = (new System.Random()).Next(_waitBetweenSpawnSecondsMin, _waitBetweenSpawnSecondsMax + 1);
            yield return new WaitForSeconds(randomWait);
        }
    }

    private void Spawn()
    {
        if (_spawnableObjects.Count <= 0)
        {
            Debug.LogWarning("No spawnable objects have been set");
            return;
        }

        int totalWeight = 0;
        foreach(var spawnableObject in _spawnableObjects)
        {
            totalWeight += spawnableObject.weight;
        }

        int randomWeight = (new System.Random()).Next(0, totalWeight + 1);

        GameObject objectToSpawn = null;
        int weight = 0;
        foreach (var spawnableObject in _spawnableObjects)
        {
            weight += spawnableObject.weight;
            objectToSpawn = spawnableObject.gameObject;

            if (weight >= randomWeight)
            {
                break;
            }
        }

        Instantiate(objectToSpawn, transform.position, this.transform.rotation);
    }
}
