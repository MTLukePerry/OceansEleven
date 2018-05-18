using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().StartRespawn();
        }
        else if (other.gameObject.GetComponent<Cannon>() != null)
        {
            SingletonManager.GetInstance<NetTargetManager>().DropOnBoat(other.gameObject);
        }
        else if (other.gameObject.tag != "Indestructible")
        {
            Destroy(other.gameObject);
        }
    }
}
