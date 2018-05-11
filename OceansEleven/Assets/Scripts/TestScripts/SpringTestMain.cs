using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTestMain : MonoBehaviour {

    public static SpringTestMain S = null;

    [SerializeField] List<SpringJoint> springJoints;
    [SerializeField] List<TriggerWeightCounter> triggerMass;
    [SerializeField] float maxSpringDist=3.0f;



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

	// Use this for initialization
	void Start () {
		
	}
	
	void LateUpdate () {
        for (int i = 0; i < springJoints.Count;i++){
            if (triggerMass[i].totalWeight <= maxSpringDist)
            {
                springJoints[i].maxDistance = triggerMass[i].totalWeight;
            }
        }
	}
}
