using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _flag;

    [SerializeField] private GameObject _stormGO;
    private bool isWindy = false;


    private void Start()
    {
        _title.SetActive(true);
        _flag.SetActive(true);
        _ui.SetActive(false);
    }

    void Update () {
        if (Input.anyKeyDown){

            _title.SetActive(false);
            _flag.SetActive(false);
            _ui.SetActive(true);
        } 
        if (Input.GetKeyUp(KeyCode.S)){
            if (!isWindy)
            {
                _stormGO.GetComponent<Animator>().SetTrigger("startStorm");
                isWindy = true;
            } else {
                _stormGO.GetComponent<Animator>().SetTrigger("endStorm"); 
            }
        }
	}
}
