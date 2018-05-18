using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAimer : MonoBehaviour
{
    [SerializeField] private float _rateOfMovement = 2f;

    private bool _move = true;

    private Vector3 _moveDirection = new Vector3(0, 0, 1);

    public bool Move
    {
        get
        {
            return _move;
        }

        set
        {
            _move = value;
        }
    }

    public Vector3 MoveDirection
    {
        get
        {
            return _moveDirection;
        }

        set
        {
            _moveDirection = value;
        }
    }

    void Update ()
    {
        if (_move)
        {
            transform.position += _moveDirection * (_rateOfMovement * Time.deltaTime);
        }
    }
}
