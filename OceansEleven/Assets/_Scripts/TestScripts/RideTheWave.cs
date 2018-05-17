using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideTheWave : MonoBehaviour
{
    private float _initialHeight;
    private float _intialWaterHeight;

    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        _initialHeight = transform.position.y;
        RaycastHit hit;
        Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Water"));
        _intialWaterHeight = hit.point.y;
    }

    private void LateUpdate ()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Water")))
        {
            transform.position = new Vector3(
                transform.position.x + Time.deltaTime * velocity.x, 
                _initialHeight + (hit.point.y - _intialWaterHeight), 
                transform.position.z + Time.deltaTime * velocity.z);

            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
    }
}
