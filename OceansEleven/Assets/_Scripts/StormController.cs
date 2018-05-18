using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StormController : MonoBehaviour {

    public Light mainLight;
    public GameObject waves;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FlashLightning(){
        mainLight.DOIntensity(5.0f, 0.1f);   
    }

    public void RegularLightning()
    {
        mainLight.DOIntensity(1.0f, 0.1f);
    }
}
