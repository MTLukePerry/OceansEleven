using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().StartRespawn();
        }
        else if (collision.gameObject.tag != "Indestructible")
        {
            Destroy(collision.gameObject);
        }
    }
}
