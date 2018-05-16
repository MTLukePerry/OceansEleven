using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
	private void Awake ()
    {
        SingletonManager.RegisterSingleton(this);
    }

    private void Start()
    {
        SingletonManager.GetInstance<ControllerManager>().SpawnPlayers();
    }
}
