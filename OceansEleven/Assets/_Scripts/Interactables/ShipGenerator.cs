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

    private float _currentFuel = 75;
    private bool _fueling = false;

    private void Start()
    {
        manager = SingletonManager.GetInstance<GameModeManager>();
    }

    private void Update()
    {
        if (manager.gameHasStarted)
        {
            if (!_fueling)
            {
                _currentFuel -= _decayRate * Time.deltaTime;
            }
            else
            {
                _currentFuel += _fillRate * Time.deltaTime;
            }
            _currentFuel = Mathf.Clamp(_currentFuel, 0, 100);

            var normalFuel = _currentFuel / 100;
            if (_uiSlider != null)
            {
                _uiSlider.fillAmount = Mathf.Clamp(normalFuel, 0, 1);
            }

            manager.UpdateBoatProgress(normalFuel);
        }
    }

    public override void InteractedWith(bool interacting, ObjectProperties heldObject)
    {
        base.InteractedWith(interacting, heldObject);

        _fueling = interacting;
    }
}
