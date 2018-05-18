using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StormController : MonoBehaviour {

    public Light mainLight;
    public GameObject waves;

    private bool isWindy = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (!isWindy)
            {
                this.GetComponent<Animator>().SetTrigger("startStorm");
                isWindy = true;
                waves.GetComponent<WaveGen>()._scale = 2;
            }
            else
            {
                this.GetComponent<Animator>().SetTrigger("endStorm");
                isWindy = false;
                waves.GetComponent<WaveGen>()._scale = 1;
            }
        }
	}

    public void FlashLightning(){
        mainLight.DOIntensity(5.0f, 0.1f);   
    }

    public void RegularLightning()
    {
        mainLight.DOIntensity(1.0f, 0.1f);
    }


   
}
