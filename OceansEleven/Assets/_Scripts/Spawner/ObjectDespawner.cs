using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Indestructible")
        {
            Destroy(collision.gameObject);
        }
    }
}
