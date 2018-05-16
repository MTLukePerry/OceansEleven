using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupTrigger : MonoBehaviour
{
    public Action _triggerCallback;

    private List<ObjectProperties> _objectsAvailable = new List<ObjectProperties>();

    public List<ObjectProperties> ObjectsAvailable
    {
        get
        {
            return _objectsAvailable;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var objProperties = other.gameObject.GetComponent<ObjectProperties>();
        if (objProperties != null && objProperties._canPickup)
        {
            _objectsAvailable.Add(objProperties);
        }

        if (_triggerCallback != null)
        {
            _triggerCallback();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var objProperties = other.gameObject.GetComponent<ObjectProperties>();
        if (objProperties != null && _objectsAvailable.Contains(objProperties))
        {
            _objectsAvailable.Remove(objProperties);
        }
    }
}
