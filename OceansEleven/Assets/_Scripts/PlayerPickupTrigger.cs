using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupTrigger : MonoBehaviour
{
    public Action _triggerCallback;

    private List<ObjectProperties> _pickupObjectsAvailable = new List<ObjectProperties>();
    private List<ObjectProperties> _interactiveObjectsAvailable = new List<ObjectProperties>();

    public List<ObjectProperties> PickupObjectsAvailable
    {
        get
        {
            return _pickupObjectsAvailable;
        }
    }

    public List<ObjectProperties> InteractiveObjectsAvailable
    {
        get
        {
            return _interactiveObjectsAvailable;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var objProperties = other.gameObject.GetComponent<ObjectProperties>();
        if (objProperties != null)
        {
            if (objProperties._canPickup)
            {
                _pickupObjectsAvailable.Add(objProperties);
            }
            else if (objProperties is InteractiveObject)
            {
                _interactiveObjectsAvailable.Add(objProperties);
            }
        }

        if (_triggerCallback != null)
        {
            _triggerCallback();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var objProperties = other.gameObject.GetComponent<ObjectProperties>();
        if (objProperties != null)
        {
            if (_pickupObjectsAvailable.Contains(objProperties))
            {
                _pickupObjectsAvailable.Remove(objProperties);
            }

            if (_interactiveObjectsAvailable.Contains(objProperties))
            {
                var interaction = (InteractiveObject)objProperties;
                if (interaction.BeingInteractedWith)
                {
                    interaction.InteractedWith(false);
                }
                _interactiveObjectsAvailable.Remove(objProperties);
            }
        }
    }
}
