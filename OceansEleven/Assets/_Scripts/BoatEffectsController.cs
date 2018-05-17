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
        _anim.SetTrigger("engineBreak");

    }
    public void FixEngine()
    {
        _anim.SetTrigger("engineFix");
    }


}
