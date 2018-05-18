using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Cannon : InteractiveObject
{
    [SerializeField] private Transform _firePosition;

    [SerializeField] private float _additionalUpAngle = 0.1f;
    [SerializeField] private float _cannonFirePower = 100.0f;

    [SerializeField] private int _ammoLimit = 2;
    private List<ObjectProperties> _ammo = new List<ObjectProperties>();

    [SerializeField] private float _litSecondsUntilFire = 1;

    [SerializeField] private GameObject _actualNet;
    private Animator _anim;

    private bool _firingCannon = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public override void InteractedWith(bool interacting, ObjectProperties heldObject)
    {
        base.InteractedWith(interacting, heldObject);
        if (interacting && !_firingCannon)
        {
            if (heldObject != null && _ammo.Count < _ammoLimit)
            {
                _ammo.Add(heldObject);
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.position = gameObject.transform.position - new Vector3(0, 1000, 0); // Extremely cheap hack - hide it away for now
            }
            else if (heldObject == null)
            {
                _firingCannon = true;
                StartCoroutine(LightCannon());
            }
        }
    }

    private IEnumerator LightCannon()
    {
        yield return new WaitForSeconds(_litSecondsUntilFire);
        StartCoroutine(FireCannon());
    }

    private IEnumerator FireCannon()
    {
        //Cannon fires
        for(int i = 0; i < _ammo.Count; i++)
        {
            var tool = _ammo[i].GetComponent<ToolObject>();
            if (tool.ToolClassification == ToolType.Net)
            {
                var ballNet = _ammo[i];
                var actualNet = Instantiate(_actualNet);
                _ammo[i] = actualNet.GetComponent<ObjectProperties>();
                Destroy(ballNet);

                var ans = actualNet.GetComponent<ActualNetScript>();
                ans.gameObjectToIgnore = gameObject;
            }

            var rb = _ammo[i].gameObject.GetComponent<Rigidbody>();
            _ammo[i].transform.position = _firePosition.position;
            rb.isKinematic = false;
            var normalizedFireRotation = Vector3.Normalize(gameObject.transform.forward + new Vector3(0, _additionalUpAngle, 0));
            rb.AddForce(normalizedFireRotation * _cannonFirePower * (rb.mass * 10));
            _anim.SetTrigger("FireCannon");
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            yield return new WaitForSeconds(0.1f);
        }
        _ammo.Clear();
        _firingCannon = false;
    }
}
