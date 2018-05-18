using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int _score = 0;
    private bool _updateScore = true;

    public int Score
    {
        get
        {
            return _score;
        }
    }

    public bool UpdateScore
    {
        get
        {
            return _updateScore;
        }

        set
        {
            _updateScore = value;
        }
    }

    private void Awake()
    {
        SingletonManager.RegisterSingleton<ScoreManager>(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var scoreObj = other.gameObject.GetComponent<ScoreObject>();
        if (scoreObj != null)
        {
            _score += scoreObj.ScoreWorth;
            UpdateScoreText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var scoreObj = other.gameObject.GetComponent<ScoreObject>();
        if (scoreObj != null)
        {
            _score -= scoreObj.ScoreWorth;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        if (_updateScore)
        {
            scoreText.text = "Score: " + _score;
        }
    }

    public void InitiateEndOfGame()
    {
        _updateScore = false;
        scoreText.text = "Final Score: " + _score;
    }
}
