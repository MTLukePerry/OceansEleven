using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : ObjectProperties
{
    [SerializeField] private bool _anyObjectForInteraction = false;
    [SerializeField] private ToolType _toolRequiredForInteraction = ToolType.None;
    [SerializeField] private Liquid _LiquidRequired = Liquid.None;

    [SerializeField] private bool _consumesObjectOnUse = false;

    public bool BeingInteractedWith { get; set; }

    public virtual void InteractedWith(bool interacting, ObjectProperties heldObject)
    {
        BeingInteractedWith = interacting;
    }

    public bool MeetsInteractionRequirements(ObjectProperties heldObject)
    {
        if (_anyObjectForInteraction && heldObject == null)
        {
            return false;
        }

        bool checkTool = _toolRequiredForInteraction != ToolType.None;
        bool checkLiquid = _LiquidRequired != Liquid.None;

        if (checkTool || checkLiquid)
        {
            if (heldObject == null || !(heldObject is ToolObject))
            {
                return false;
            }
            else
            {
                ToolObject tool = (ToolObject)heldObject;
                if (checkTool && tool.ToolClassification != _toolRequiredForInteraction)
                {
                    return false;
                }
                if (checkLiquid && (!tool.CanFill || tool.ContainingLiquid != _LiquidRequired))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
