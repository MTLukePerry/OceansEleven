using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSpawner : InteractiveObject
{
    [SerializeField] private GameObject _prefabToSpawn;

    private Transform _spawnPosition;

    private void Start()
    {
        _spawnPosition = transform.Find("SpawnSpot");
    }

    public override void InteractedWith(bool interacting, ObjectProperties heldObject)
    {
        base.InteractedWith(interacting, heldObject);

        if (interacting && _prefabToSpawn != null)
        {
            Instantiate(_prefabToSpawn, _spawnPosition.position, Quaternion.identity);
        }
    }
}
