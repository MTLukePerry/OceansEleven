using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private float _playerSpawnXDifference = 5f;
    [SerializeField] private float _playerSpawnY = 0.1f;
    [SerializeField] private float _playerSpawnZDifference = 2.5f;

    private string[] _activeControllers;

    private const int _maxPlayerCount = 4;

    private void Awake()
    {
        SingletonManager.RegisterSingleton(this);

        _activeControllers = Input.GetJoystickNames();
        for (int i = 0; i < _activeControllers.Length; i++)
        {
            Debug.Log(_activeControllers[i]);
        }
        Debug.Log(_activeControllers.Length);
    }

    private int PlayerCount
    {
        get
        {
            return Mathf.Clamp(_activeControllers.Length, 1, _maxPlayerCount);
        }
    }

    public void SpawnPlayers()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            var player = Instantiate(_playerPrefab, DetermineSpawnPosition(i + 1), Quaternion.identity);
            player.GetComponent<PlayerController>().AssignController(i + 1);
        }
    }

    private Vector3 DetermineSpawnPosition(int playerSpawning)
    {
        Vector3 startingPosition = new Vector3(0, _playerSpawnY, 0);
        bool isOdd = playerSpawning % 2 == 1;
        var spawnX = isOdd ? -_playerSpawnXDifference : _playerSpawnXDifference;
        switch (PlayerCount)
        {
            case 2:
                startingPosition = new Vector3(spawnX, _playerSpawnY, 0);
                break;
            case 3:
            case 4:
                var spawnZ = playerSpawning > 2 ? -_playerSpawnZDifference : _playerSpawnZDifference;
                startingPosition = new Vector3(spawnX, _playerSpawnY, spawnZ);
                break;
        }
        return startingPosition;
    }
}
