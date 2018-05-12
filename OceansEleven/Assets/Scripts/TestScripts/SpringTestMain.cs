using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTestMain : MonoBehaviour {

    public static SpringTestMain S = null;

    [SerializeField] List<SpringJoint> _springJoints;
    [SerializeField] List<TriggerWeightCounter> _triggerMass;
    [SerializeField] float _maxSpringDist = 10.0f;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else if (S != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
	
	void LateUpdate ()
    {
        for (int i = 0; i < _springJoints.Count;i++)
        {
            if (_triggerMass[i]._totalWeight <= _maxSpringDist)
            {
                _springJoints[i].maxDistance = _triggerMass[i]._totalWeight;
            }
        }
	}
}
