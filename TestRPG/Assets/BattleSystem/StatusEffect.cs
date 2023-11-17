using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    Poison,
    Mud,
    Swamped,
    Blinded,
    Focus,
    Sneak
}

public struct Status
{
    public StatusEffect effect;

    public bool decrementStackOnTurn;
    public int stack; // decremented each turn until 0 if decrementStackOnTurn = true (then dissapears)
}

// statuses can be applied to BattleUnits or BattleTiles (effecting the BattleUnit on it)
[CreateAssetMenu(fileName = "Status", menuName = "Battle System/Status Effect", order = 4)]
public class StatusEffect : ScriptableObject
{
    public StatusType type;
    public int stackMin, stackMax;

    // called on the turn that the status is first started
    public void OnInflictStatus(BattleUnit unit)
    {
        switch (type)
        {
            case StatusType.Blinded:
                unit.hitChanceModifier *= 0.5f;
                break;
            case StatusType.Sneak:
                unit.dodgeChanceModifier += 1.0f;
                break;
        }
    }

    // happens every turn the status is active (including the initial turn that OnInflictStatus is also called)
    public void OnExecuteStatus(BattleUnit unit, int stackChange)
    {
        switch (type)
        {
            case StatusType.Focus:
                unit.damageAdder += stackChange;
                unit.dodgeChanceModifier += stackChange / 100.0f;
                break;
        }
    }
    
    // called after the status ends (allows reverting of its effects)
    public void OnEndStatus(BattleUnit unit, int stackChange)
    {
        switch (type)
        {
            case StatusType.Blinded:
                // revert the halving of the hitChanceModifier during OnInflictStatus
                unit.hitChanceModifier *= 2.0f;
                break;
            case StatusType.Focus:
                unit.damageAdder += stackChange;
                unit.dodgeChanceModifier += stackChange / 100.0f;
                break;
            case StatusType.Sneak:
                unit.dodgeChanceModifier -= 1.0f;
                break;
        }
    }
}