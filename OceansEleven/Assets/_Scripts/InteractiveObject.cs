using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : ObjectProperties
{
    [SerializeField] private ToolType _toolRequiredForInteraction = ToolType.None;
    [SerializeField] private Liquid _LiquidRequired = Liquid.None;

    public bool BeingInteractedWith { get; set; }

    public virtual void InteractedWith(bool interacting)
    {
        BeingInteractedWith = interacting;
    }

    public bool MeetsInteractionRequirements(ObjectProperties heldObject)
    {
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
