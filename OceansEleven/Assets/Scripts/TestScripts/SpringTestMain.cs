using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTestMain : MonoBehaviour {

    public static SpringTestMain S = null;

    [SerializeField] SpringJoint topLeftSpring;
    [SerializeField] SpringJoint topRightSpring;
    [SerializeField] SpringJoint bottomLeftSpring;
    [SerializeField] SpringJoint bottimRightSpring;



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
	
	// Update is called once per frame
	void Update () {
		
	}
}
