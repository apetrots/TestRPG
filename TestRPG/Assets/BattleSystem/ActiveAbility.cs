using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ActiveAbilityBehavior
{
    None, // an ability that needs no custom behavior
    Hematophagy,
    Threnody,
    Calloused,
    ShroudingStrike,
}

// info that is sent to ExecuteAbility when an ability is executed, contains data that would be useful for the execution
public struct ActiveAbilityInfo
{
    public bool wasHit;

    // position the ability was casted from, i.e. can change if "castFromTarget" is true in ability 
    public Vector2Int castedFrom;

    public int damageDone;
}

[CreateAssetMenu(fileName = "Ability", menuName = "Battle System/Active Ability", order = 2)]
public class ActiveAbility : ScriptableObject
{
    public int powerCost;

    public ActiveAbilityBehavior behavior;
    public float hitChance; // out of 100% (0.0 to 1.0)

    public int minHeal, maxHeal; 
    public int minDamage, maxDamage; 
    public StatusEffect[] inflictions; 

    // whether or not the target selects units or if it's tile-based 
    public bool targettedAttack;

    [Header("Targetted Attack")]
    // if = -1, no selection radius limit
    public int targetSelectRadius = -1;
    // some abilities "cast" from a different position
    public bool castFromTarget;

    [Header("Non-Targetted (tile-based) Attack")]
    // the tiles affected by this attack, grid position relative to the casting location (can be horizontally flipped based on the unit's facing direction)
    public Vector2Int[] affectedTilesRelative;
    
    // if ability is targettedAttack, target in targetSelectRadius, and this returns then the ability's target can be selected 
    public bool CanSelectTarget(BattleUnit target, BattleUnit caster)
    {
        switch (behavior)
        {
            case ActiveAbilityBehavior.Threnody:
                return target.isCorpse; // can select ONLY if it's a corpse
        }

        return true;
    }

    // master function to execute the custom behavior every type of ability after general ability function done
    public void OnExecuteAbility(BattleUnit caster, ActiveAbilityInfo info, BattleSystem system)
    {
        // even if the attack was a miss...
        switch (behavior)
        {
            case ActiveAbilityBehavior.ShroudingStrike:
                // caster.Inflict()
            break;
        }

        if (!info.wasHit)
            return;
        
        switch (behavior)
        {
            case ActiveAbilityBehavior.Hematophagy:
            // "caster gains HP equal to half the damage dealt, rounded up"
                caster.Heal(Mathf.CeilToInt(info.damageDone / 2.0f));
            break;
            case ActiveAbilityBehavior.Calloused:
            // "caster gains HP equal to half the damage dealt, rounded up"
                caster.Heal(Mathf.CeilToInt(info.damageDone / 2.0f));
            break;
            
        }
    }
}
