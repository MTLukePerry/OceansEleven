﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _flag;


	void Update () {
        if (Input.anyKeyDown){

            _title.SetActive(false);
            _flag.SetActive(false);
            _ui.SetActive(true);
        } 
	}
}