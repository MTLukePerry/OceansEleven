using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private string[] _activeControllers;

    private const int _maxPlayerCount = 4;

    private void Awake()
    {
        SingletonManager.RegisterSingleton(this);
    }

    private void Start ()
    {
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
            return _activeControllers.Length;
        }
    }
}
