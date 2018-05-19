using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StormController : MonoBehaviour {

    public Light mainLight;
    public GameObject waves;

    private bool isWindy = false;

    private void Awake()
    {
        SingletonManager.RegisterSingleton<StormController>(this);
    }

    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            ActivateStorm();
        }
    }

    public void ActivateStorm()
    {
        if (!isWindy)
        {
            this.GetComponent<Animator>().SetTrigger("startStorm");
            isWindy = true;
            waves.GetComponent<WaveGen>()._scale = 1.4f;
            StartCoroutine(TurnOffAfterSeconds());
        }
        else
        {
            this.GetComponent<Animator>().SetTrigger("endStorm");
            isWindy = false;
            waves.GetComponent<WaveGen>()._scale = 0.6f;
        }
    }

    private IEnumerator TurnOffAfterSeconds()
    {
        yield return new WaitForSeconds(20);
        ActivateStorm();
    }

    public void FlashLightning(){
        mainLight.DOIntensity(5.0f, 0.1f);   
    }

    public void RegularLightning()
    {
        mainLight.DOIntensity(1.0f, 0.1f);
    }


   
}
