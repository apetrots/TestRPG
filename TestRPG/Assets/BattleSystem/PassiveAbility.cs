using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PassiveAbilityBehavior
{
    None, // an ability that needs no custom behavior
    Focus
}

// info that is sent to ExecuteAbility when an ability is executed, contains data that would be useful for the execution
public struct PassiveAbilityInfo
{
}

[CreateAssetMenu(fileName = "Ability", menuName = "Battle System/Passive Ability", order = 3)]
public class PassiveAbility : ScriptableObject
{
    public PassiveAbilityBehavior behavior;

    public StatusEffect[] inflictions;

    // master function to execute the custom behavior every type of ability after general ability function done
    public void OnExecuteAbility(BattleUnit caster, ActiveAbilityInfo info, BattleSystem system)
    {
    }
}
