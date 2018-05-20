using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipGenerator : InteractiveObject
{
    [SerializeField] private Image _uiSlider;

    [SerializeField] private float _fillRate = 10f;
    [SerializeField] private float _decayRate = 2f;

    private GameModeManager manager = null;

    [SerializeField] private Animator _boatAnim;

    [SerializeField ]private float _currentFuel = 75;
    private bool _fueling = false;

    [SerializeField] Color32[] _fillColourChange;
    private AudioSource _audio;

    private void Start()
    {
        manager = SingletonManager.GetInstance<GameModeManager>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (manager.gameHasStarted)
        {
            if (!_fueling)
            {
                _currentFuel -= _decayRate * Time.deltaTime;
                _audio.mute = true;
            }
            else
            {
                _currentFuel += _fillRate * Time.deltaTime;
                _audio.mute = false;
            }
            _currentFuel = Mathf.Clamp(_currentFuel, 0, 100);

            var normalFuel = _currentFuel / 100;
            if (_uiSlider != null)
            {
                _uiSlider.fillAmount = Mathf.Clamp(normalFuel, 0, 1);
            }

            manager.UpdateBoatProgress(normalFuel);
        }

        if (_currentFuel <=0){
            _boatAnim.SetInteger("engineState",1);
        } else {
            _boatAnim.SetInteger("engineState", 0);
        }


        if (_uiSlider.fillAmount <= 0.19){
            _uiSlider.color = _fillColourChange[2];
        } else if (_uiSlider.fillAmount >= 0.20 && _uiSlider.fillAmount <= 0.39 )
        {
            _uiSlider.color = _fillColourChange[1];
        }
        else if(_uiSlider.fillAmount >= 0.40)
        {
            _uiSlider.color = _fillColourChange[0];
        }
    }

    public override void InteractedWith(bool interacting, ObjectProperties heldObject)
    {
        base.InteractedWith(interacting, heldObject);

        _fueling = interacting;
    }
}
