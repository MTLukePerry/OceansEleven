using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWeightCounter : MonoBehaviour
{

    public float _totalWeight;
    [SerializeField] private List<Collider> _colliders;

    void Start()
    {
        _colliders = new List<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        var objectProperties = other.gameObject.GetComponent<ObjectProperties>();
        if (objectProperties != null && !_colliders.Contains(other))
        {
            _colliders.Add(other);
            _totalWeight += objectProperties._fakeMass;
        }
    }

    void OnTriggerExit(Collider other)
    {
        var objectProperties = other.gameObject.GetComponent<ObjectProperties>();
        if (objectProperties != null && _colliders.Contains(other))
        {
            _colliders.Remove(other);
            _totalWeight -= objectProperties._fakeMass;
        }
    }


}
