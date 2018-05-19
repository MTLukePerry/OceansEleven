using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEffectsController : MonoBehaviour {

    private Animator _anim;

	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator>();
	}
	
    public void BreakEngine()
    {
        _anim.SetInteger("engineState", 1);

    }
    public void FixEngine()
    {
        _anim.SetInteger("engineState", 0);
    }


}
