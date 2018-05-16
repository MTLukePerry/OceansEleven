using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 300;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _intitialThrowStrength = 500;

    private Rigidbody _rigidbody;
    private PlayerPickupTrigger _pickupTrigger;
    private ObjectProperties _pickedUpItem;
    private Transform _pickedUpItemSlot;

    private int _controllerNumber = 0;

    private void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pickupTrigger = GetComponentInChildren<PlayerPickupTrigger>();
        _pickedUpItemSlot = transform.Find("PickupItemSlot");
    }

    private void Update()
    {
        if (_controllerNumber > 0)
        {
            PlayerControls();
        }

        if (_pickedUpItem != null)
        {
            _pickedUpItem.transform.position = _pickedUpItemSlot.position;
        }
    }

    private void PlayerControls ()
    {
        var vertical = Input.GetAxis("Vertical_P" + _controllerNumber);
        var horizontal = Input.GetAxis("Horizontal_P" + _controllerNumber);

        if (vertical != 0 || horizontal != 0)
        {
            MoveCharacter(horizontal, vertical);
        }

        if (Input.GetButtonDown("Interact_P" + _controllerNumber)) // TODO
        {
            PickupItem();
        }

        if (Input.GetAxis("Trigger_P" + _controllerNumber) < 0)
        {
            ThrowItem();
        }
    }

    private void MoveCharacter(float horizontal, float vertical)
    {
        Vector3 movement = CalculateMoveDirection(horizontal, vertical);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (movement * 10), Color.green);
        //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (-movement * 10), Color.green);
        _rigidbody.AddForce(movement * _speed);
    }


    private Vector3 CalculateMoveDirection(float horizontal, float vertical)
    {
        var movement = new Vector3(horizontal, 0, vertical);
        //var testNorm = Vector3.Normalize(test);

        //var newTest = transform.position + test;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity))
        {
            //TODO - make the force direction parallel to ship floor for smoother movement

            //Vector3 moveThisWay = new Vector3(hit.point.x, hit.point.y + transform.lossyScale.y, hit.point.z);
            //var difference = Vector3.up - hit.normal;
            //Debug.DrawLine(hit.point, hit.point + (hit.normal * 100), Color.green);
            //return (new Vector3(testNorm.x, difference.y, testNorm.z)) * test.magnitude;

            //return Vector3.Normalize(moveThisWay) * test.magnitude;
        }
        return movement;
    }

    public void AssignController(int controller)
    {
        _controllerNumber = controller;
        var textureManager = SingletonManager.GetInstance<TextureManager>();
        if (textureManager != null)
        {
            GetComponent<Renderer>().material = textureManager.GetMaterialFromPlayerNumber(controller);
        }
    }

    private void PickupItem()
    {
        if (_pickedUpItem == null)
        {
            bool pickedUp = false;
            if (_pickupTrigger.ObjectsAvailable.Count > 0)
            {
                //TODO depends on whcih item is closest to the centre of the trigger
                _pickedUpItem = _pickupTrigger.ObjectsAvailable[0];
                pickedUp = true;
            }

            if (pickedUp && _pickedUpItem != null)
            {
                _pickedUpItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //todo set _pickedUpItemSlot position high enough that it can carry any item variable on the size of the thing you'repicking up
                _pickedUpItem.transform.position = _pickedUpItemSlot.position;
            }
        }
        else
        {
            ReleaseItem();
        }
    }

    private void ThrowItem()
    {
        if (_pickedUpItem != null)
        {
            ReleaseItem(transform.forward);
        }
    }

    private void ReleaseItem(Vector3 forceDirection = new Vector3())
    {
        _pickedUpItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        if (forceDirection != default(Vector3))
        {
            _pickedUpItem.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * (_intitialThrowStrength * _strength) + (GetComponent<Rigidbody>().velocity * 20)); // TODO variable throw based on strength
        }
        _pickedUpItem = null;
    }
}
