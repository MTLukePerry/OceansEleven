using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupTrigger : MonoBehaviour
{
    private PlayerController player;

    public Action _triggerCallback;

    private List<ObjectProperties> _pickupObjectsAvailable = new List<ObjectProperties>();
    private List<ObjectProperties> _pushObjectsAvailable = new List<ObjectProperties>();
    private List<ObjectProperties> _interactiveObjectsAvailable = new List<ObjectProperties>();

    public List<ObjectProperties> PickupObjectsAvailable
    {
        get
        {
            return _pickupObjectsAvailable;
        }
    }

    public List<ObjectProperties> PushObjectsAvailable
    {
        get
        {
            return _pushObjectsAvailable;
        }
    }

    public List<ObjectProperties> InteractiveObjectsAvailable
    {
        get
        {
            return _interactiveObjectsAvailable;
        }
    }

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
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
            else if (objProperties._canPush)
            {
                _pushObjectsAvailable.Add(objProperties);
            }

            if (objProperties is InteractiveObject)
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
            else if (_pushObjectsAvailable.Contains(objProperties))
            {
                _pushObjectsAvailable.Remove(objProperties);
            }

            if (_interactiveObjectsAvailable.Contains(objProperties))
            {
                var interaction = (InteractiveObject)objProperties;
                if (interaction.BeingInteractedWith)
                {
                    interaction.InteractedWith(false, null);
                    player.InteractingItem = null;
                }
                _interactiveObjectsAvailable.Remove(objProperties);
            }
        }
    }
}
