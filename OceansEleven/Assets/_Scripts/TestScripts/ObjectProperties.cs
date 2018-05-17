using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour
{
    public float _fakeMass = 1;
    public bool _canPickup;
    public bool _canPush;

    private void Start ()
    {
        if (_canPickup)
        {
            _canPush = false;
        }
        //todo set mass to equal fake mass?
    }

}
