using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _startObjects;

    private void Awake()
    {
        //Time.timeScale = 1;
    }

    void Update () {
        if (Input.anyKeyDown){

            _title.SetActive(false);
            _flag.SetActive(false);
            _ui.SetActive(true);
            _startObjects.SetActive(true);
            Camera.main.GetComponent<Animator>().SetTrigger("startGame");
            GetComponent<GameModeManager>().gameHasStarted = true;
            //Time.timeScale = 1;
        } 
    }
}

