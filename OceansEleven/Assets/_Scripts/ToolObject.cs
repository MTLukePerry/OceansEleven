using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : ObjectProperties
{
    [SerializeField] private ToolType _toolClassification;

    [SerializeField] private bool _canFill = false;
    [SerializeField] private Liquid _containingLiquid;

    private float _fillAmount;
    [SerializeField] private float _startingFillAmount = 0;

    public ToolType ToolClassification
    {
        get
        {
            return _toolClassification;
        }
    }

    public bool CanFill
    {
        get
        {
            return _canFill;
        }
    }

    public Liquid ContainingLiquid
    {
        get
        {
            return _containingLiquid;
        }
    }

    private void Start()
    {
        _fillAmount = _startingFillAmount;
    }
}

public enum ToolType
{
    None,
    Wrench,
    OilCan,
    Mop,
    Bucket,
    All
}

public enum Liquid
{
    None,
    Water,
    Oil,
    All
}
