using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipGenerator : InteractiveObject
{
    [SerializeField] private Image _uiSlider;

    [SerializeField] private float _fillRate = 0.035f;
    [SerializeField] private float _decayRate = 0.015f;

    private float _currentFuel = 50;
    private bool _fueling = false;

    private void Update()
    {
        if (!_fueling)
        {
            _currentFuel -= (1 * _decayRate);
        }
        else
        {
            _currentFuel += (1 * _fillRate);
        }
        _currentFuel = Mathf.Clamp(_currentFuel, 0, 100);

        if (_uiSlider != null)
        {
            var normalFuel = _currentFuel / 100;
            _uiSlider.fillAmount = Mathf.Clamp(normalFuel, 0, 1);
        }
    }

    public override void InteractedWith(bool interacting)
    {
        base.InteractedWith(interacting);

        _fueling = interacting;
    }
}
