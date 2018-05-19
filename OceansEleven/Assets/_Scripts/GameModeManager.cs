using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public bool gameHasStarted =false;
    [SerializeField] private Slider _boatObjectiveSlider;

    [SerializeField] private float _fullSpeedProgress = 0.01f;

    [SerializeField] private float _fullProgressBoundary = 0.4f;
    [SerializeField] private float _twoThirdsProgressBoundary = 0.15f;

    [SerializeField] private SimpleRotate _paddleWheelSpeed;

    private float _boatProgress = 0f;

    private bool _resetGameStarted = false;
    private bool _halfWay = false;

    private void Awake ()
    {
        SingletonManager.RegisterSingleton(this);
    }

    private void Start()
    {
        _resetGameStarted = false;

        SingletonManager.GetInstance<ControllerManager>().SpawnPlayers();

        if (_boatObjectiveSlider == null)
        {
            Debug.LogError("Please fill the serialized \"_boatObjectiveSlider\" variable on the GameModeManager attached to " + gameObject.name);
        }
    }

    public void UpdateBoatProgress(float fuelPercentage)
    {
        if (gameHasStarted)
        {
            float addedProgression = 0;
            if (fuelPercentage >= _fullProgressBoundary)
            {
                addedProgression += _fullSpeedProgress * Time.deltaTime;
                _paddleWheelSpeed.rotationTime = 2.0f;
            }
            else if (fuelPercentage >= _twoThirdsProgressBoundary)
            {
                addedProgression += (_fullSpeedProgress * 0.66f) * Time.deltaTime;
                _paddleWheelSpeed.rotationTime = 5.0f;
            }
            else if (fuelPercentage > 0)
            {
                addedProgression += (_fullSpeedProgress * 0.33f) * Time.deltaTime;
                _paddleWheelSpeed.rotationTime = 30.0f;
            }
            _boatProgress += addedProgression;
            UpdateBoatPosition(_boatProgress);

            if (_boatProgress > 0.5f && !_halfWay)
            {
                _halfWay = true;
                SingletonManager.GetInstance<StormController>().ActivateStorm();
            }
        }
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
        if (!_resetGameStarted)
        {
            bool _resetGameStarted = true;
            StartCoroutine(LoadFirstScene(10));
        }
    }

    private IEnumerator LoadFirstScene(int secondsWait)
    {
        yield return new WaitForSeconds(secondsWait);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
