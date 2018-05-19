using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

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
            GetComponent<PlayableDirector>().Play();
            isWindy = true;
            /*this.GetComponent<Animator>().SetTrigger("startStorm");

            waves.GetComponent<WaveGen>()._scale = 1.5f;
            StartCoroutine(TurnOffAfterSeconds());
            */
        }
        else
        {
            isWindy = false;
            /*
            this.GetComponent<Animator>().SetTrigger("endStorm");

            waves.GetComponent<WaveGen>()._scale = 0.6f;
            */
        }
    }

    private IEnumerator TurnOffAfterSeconds()
    {
        yield return new WaitForSeconds(20);
        ActivateStorm();
    }



   
}
