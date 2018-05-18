﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private Slider _boatObjectiveSlider;

    [SerializeField] private float _fullSpeedProgress = 0.00005f;

    [SerializeField] private float _fullProgressBoundary = 0.4f;
    [SerializeField] private float _twoThirdsProgressBoundary = 0.15f;

    [SerializeField] private SimpleRotate _paddleWheelSpeed;

    private float _boatProgress = 0;

    private void Awake ()
    {
        SingletonManager.RegisterSingleton(this);
    }

    private void Start()
    {
        SingletonManager.GetInstance<ControllerManager>().SpawnPlayers();

        if (_boatObjectiveSlider == null)
        {
            Debug.LogError("Please fill the serialized \"_boatObjectiveSlider\" variable on the GameModeManager attached to " + gameObject.name);
        }
    }

    public void UpdateBoatProgress(float fuelPercentage)
    {
        float addedProgression = 0;
        if (fuelPercentage >= _fullProgressBoundary)
        {
            addedProgression += _fullSpeedProgress;
            _paddleWheelSpeed.rotationTime = 2.0f;
        }
        else if (fuelPercentage >= _twoThirdsProgressBoundary)
        {
            addedProgression += (_fullSpeedProgress * 0.66f);
            _paddleWheelSpeed.rotationTime = 5.0f;
        }
        else if (fuelPercentage > 0)
        {
            addedProgression += (_fullSpeedProgress * 0.33f);
            _paddleWheelSpeed.rotationTime = 30.0f;
        }
        _boatProgress += addedProgression;
        UpdateBoatPosition(_boatProgress);
    }

    private void UpdateBoatPosition(float normalizedProgress)
    {
        normalizedProgress = Mathf.Clamp(normalizedProgress, 0, 1);
        if (_boatObjectiveSlider != null)
        {
            _boatObjectiveSlider.value = normalizedProgress;
        }

        if (normalizedProgress == 1)
        {
            StartWinFlow();
        }
    }

    private void StartWinFlow()
    {
        SingletonManager.GetInstance<ScoreManager>().InitiateEndOfGame();
        Debug.Log("You've finished the game! Congrats!");
    }
}
