using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{

    [SerializeField] private int _scoreWorth = 1;

    public int ScoreWorth
    {
        get
        {
            return _scoreWorth;
        }

        set
        {
            _scoreWorth = value;
        }
    }
}
