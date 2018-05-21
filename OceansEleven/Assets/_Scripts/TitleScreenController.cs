using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _startObjects;
    [SerializeField] private GameObject[] _audioObjects;
    [SerializeField] private Animator _wavesAnim;
    [SerializeField] private Animator _splashAnim;
    private bool _delayedStart = true;

    private bool _gameStarted=false;

    private void Awake()
    {
        //Time.timeScale = 1;
        _title.SetActive(true);
        _flag.SetActive(true);
        _ui.SetActive(false);
        _startObjects.SetActive(false);
        _audioObjects[0].SetActive(true);
        _audioObjects[1].SetActive(false);
        StartCoroutine(DelayInput());
    }

    void Update () {
        if (!_gameStarted)
        {
            if (!_delayedStart)
            {
                if (Input.anyKeyDown)
                {

                    _title.SetActive(false);
                    _flag.SetActive(false);
                    _ui.SetActive(true);
                    _startObjects.SetActive(true);
                    Camera.main.GetComponent<Animator>().SetTrigger("startGame");
                    GetComponent<GameModeManager>().gameHasStarted = true;
                    //Time.timeScale = 1;
                    _wavesAnim.SetTrigger("startOcean");
                    _splashAnim.SetBool("gameHasStarted", true);
                    _audioObjects[0].SetActive(false);
                    _audioObjects[1].SetActive(true);
                    _gameStarted = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape)){
            Destroy(GameObject.Find("Main"));
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    private IEnumerator DelayInput()
    {
        yield return new WaitForSeconds(1.0f);
            _delayedStart = false;
    }
}

