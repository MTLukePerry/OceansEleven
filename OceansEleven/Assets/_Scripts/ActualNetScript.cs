using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualNetScript : MonoBehaviour
{
    public GameObject gameObjectToIgnore;

    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(4);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other){

        Debug.Log("Collider is" + other.gameObject.name + "_" + other.gameObject.tag);
        {
            if (other.tag != "Indestructible" &&
                other.tag != "Boat" &&
                other.tag != "Net" &&
                other.gameObject != gameObjectToIgnore)
            {
                SingletonManager.GetInstance<NetTargetManager>().DropOnBoat(other.gameObject);
            }
        }
    }
}
