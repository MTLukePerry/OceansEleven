using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanMovement : MonoBehaviour
{
    public Vector3 _velocity = new Vector3(-5, 0, 0);

    void OnCollisionStay(Collision collisionInfo)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Boat")
        {
            if (collision.gameObject.GetComponent<RideTheWave>() == null &&
                !collision.rigidbody.isKinematic)
            {
                var rideTheWave = collision.gameObject.AddComponent<RideTheWave>();
                rideTheWave.velocity = _velocity;
                collision.rigidbody.isKinematic = true;
                collision.collider.isTrigger = true;
            }
        }
    }
}
