using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
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
            score += scoreObj.ScoreWorth;
            UpdateScoreText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var scoreObj = other.gameObject.GetComponent<ScoreObject>();
        if (scoreObj != null)
        {
            score -= scoreObj.ScoreWorth;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
