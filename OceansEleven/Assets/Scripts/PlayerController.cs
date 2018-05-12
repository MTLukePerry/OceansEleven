using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _controllerNumber = 0;

    private void Start ()
    {
		
	}

	private void Update ()
    {
		if (_controllerNumber > 0)
        {
            PlayerControls();
        }
	}

    void PlayerControls()
    {

    }

    public void AssignController(int controller)
    {
        _controllerNumber = controller;
    }
}
