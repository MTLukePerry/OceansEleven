using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 300;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _intitialThrowStrength = 500;

    [SerializeField] private float _baseTurnRate = 0.15f;
    private float _currentTurnRate;

    private Vector3 _respawnPosition = new Vector3(-8, 0.15f, -2.75f);
    [SerializeField] private float _respawnTime = 5.0f;
    private bool _respawning;

    private Rigidbody _rigidbody;
    private PlayerPickupTrigger _interactionTrigger;
    private Transform _pickedUpItemSlot;

    private Transform _pushItemSlot;

    private ObjectProperties _pickedUpItem;
    private ObjectProperties _pushItem;
    private InteractiveObject _interactingItem;

    private int _controllerNumber = 0;

    public InteractiveObject InteractingItem
    {
        get
        {
            return _interactingItem;
        }

        set
        {
            _interactingItem = value;
        }
    }

    private void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _interactionTrigger = GetComponentInChildren<PlayerPickupTrigger>();
        _pickedUpItemSlot = transform.Find("PickupItemSlot");
        _pushItemSlot = transform.Find("PushItemSlot");
        _currentTurnRate = _baseTurnRate;
        GameObject respawn = GameObject.Find("P" + _controllerNumber + "RespawnPoint");
        if (respawn != null)
        {
            _respawnPosition = respawn.transform.position;
        }
    }

    private void Update()
    {
        if (_controllerNumber > 0 && !_respawning)
        {
            PlayerControls();
        }

        if ((transform.position.x <= -25.0f || transform.position.y < -200 || transform.position.x >= 40.0f || transform.position.z <= -30.0f || transform.position.z >= 40.0f) && !_respawning)
        {
            StartRespawn();
        }

        if (_pickedUpItem != null)
        {
            _pickedUpItem.transform.position = _pickedUpItemSlot.position;
        }
        else if (_pushItem != null)
        {
            _pushItem.transform.position = _pushItemSlot.position;
            _pushItem.transform.forward = gameObject.transform.forward;
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

        if (Input.GetButtonDown("Pickup_P" + _controllerNumber))
        {
            PickupOrPushItem();
        }
        else if (Input.GetButtonDown("Interact_P" + _controllerNumber))
        {
            InteractItem();
        }
        else if (Input.GetAxis("Trigger_P" + _controllerNumber) < 0)
        {
            ThrowItem();
        }
    }

    private void MoveCharacter(float horizontal, float vertical)
    {
        Vector3 movement = CalculateMoveDirection(horizontal, vertical);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), _currentTurnRate);
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
            //TODO - make the force direction parallel to ship floor for smoother movement - VERY LOW PRIORITY may not even want

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

    private void PickupOrPushItem()
    {
        if (_pickedUpItem == null && _pushItem == null)
        {
            if (_interactionTrigger.PickupObjectsAvailable.Count > 0)
            {
                //TODO depends on whcih item is closest to the centre of the trigger
                _pickedUpItem = _interactionTrigger.PickupObjectsAvailable[0];
            }
            else if (_interactionTrigger.PushObjectsAvailable.Count > 0)
            {
                _pushItem = _interactionTrigger.PushObjectsAvailable[0];
            }

            if (_pickedUpItem != null)
            {
                _pickedUpItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //TODO set _pickedUpItemSlot position high enough that it can carry any item variable on the size of the thing you're picking up
                //_pickedUpItemSlot.position = gameObject.transform.position + new Vector3(0, (_pickedUpItem.GetComponent<Collider>().bounds.size.y * 1.1f) + 0.5f, 0);
                _pickedUpItem.transform.SetParent(gameObject.transform);
                _pickedUpItem.OnPickedUp(this);
            }
            else if (_pushItem != null)
            {
                _pushItem.transform.position += new Vector3(0,0.15f,0);
                _pushItemSlot.position = _pushItem.transform.position + (gameObject.transform.forward * 1);
                var rb = _pushItem.gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                _currentTurnRate -= (rb.mass / 5); // TODO MAKE ALL THIS WORK
            }
        }
        else
        {
            if (_pickedUpItem != null)
            {
                ReleasePickedUpItem();
            }
            else
            {
                ReleasePushedItem();
            }
        }
    }

    private void ThrowItem()
    {
        if (_pickedUpItem != null)
        {
            ReleasePickedUpItem(transform.forward);
        }
    }

    private void ReleasePickedUpItem(Vector3 forceDirection = new Vector3())
    {
        _pickedUpItem.transform.SetParent(null);
        _pickedUpItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        if (forceDirection != default(Vector3))
        {
            _pickedUpItem.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * (_intitialThrowStrength * _strength) + (GetComponent<Rigidbody>().velocity * 20)); // TODO variable throw based on strength
        }
        _pickedUpItem.OnDropped(this);
        _pickedUpItem = null;
    }

    private void ReleasePushedItem()
    {
        _pushItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _pushItem = null;
        _currentTurnRate = _baseTurnRate;
    }

    private void InteractItem()
    {
        if (_interactionTrigger.InteractiveObjectsAvailable.Count > 0)
        {
            //TODO depends on which item is closest to the player
            var objectToInteract = (InteractiveObject)_interactionTrigger.InteractiveObjectsAvailable[0];
            if (objectToInteract.MeetsInteractionRequirements(_pickedUpItem))
            {
                var item = _pickedUpItem;
                if (objectToInteract.ConsumesObjectOnUse && item != null)
                {
                    ReleasePickedUpItem();
                }
                _interactingItem = objectToInteract;
                objectToInteract.InteractedWith(true, item);
            }
        }
    }

    public void StartRespawn()
    {
        if (!_respawning)
        {
            _respawning = true;
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        var rtw = GetComponent<RideTheWave>();
        if (rtw)
        {
            Destroy(rtw);
        }
        _rigidbody.isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
        gameObject.transform.forward = transform.right;
        gameObject.transform.position = _respawnPosition;
        _respawning = false;
    }
}
