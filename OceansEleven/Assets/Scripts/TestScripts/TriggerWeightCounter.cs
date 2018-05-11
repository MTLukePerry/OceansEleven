using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWeightCounter : MonoBehaviour
{

    public float totalWeight;
    [SerializeField] private List<Collider> colliders;

    void Start()
    {
        colliders = new List<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other))
        {
            colliders.Add(other);
            //todo check if has component
            totalWeight += other.gameObject.GetComponent<ObjectProperties>().fakeMass;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other))
        {
            colliders.Remove(other);
            //todo check if has component
            totalWeight -= other.gameObject.GetComponent<ObjectProperties>().fakeMass;
        }
    }


}
