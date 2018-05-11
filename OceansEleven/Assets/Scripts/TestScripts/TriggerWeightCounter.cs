using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWeightCounter : MonoBehaviour
{

    public float totalWeight;
    public List<Collider> colliders;

    void Start()
    {
        colliders = new List<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other))
        {
            colliders.Add(other);
            totalWeight += other.gameObject.GetComponent<ObjectProperties>().fakeMass;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other))
        {
            colliders.Remove(other);
            totalWeight -= other.gameObject.GetComponent<ObjectProperties>().fakeMass;
        }
    }


}
