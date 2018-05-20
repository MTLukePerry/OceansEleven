using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSplashController : MonoBehaviour {

    [SerializeField] private Transform[] _pivots;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_pivots[0].position.y <= _pivots[1].position.y){

            GetComponent<Animator>().SetBool("bowIsLow", true);
        } else {
            GetComponent<Animator>().SetBool("bowIsLow", false);
        }
	}
}
