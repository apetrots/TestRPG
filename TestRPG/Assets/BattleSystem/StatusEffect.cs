using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    Poison,
    Mud,
    Swamped
}

public struct Status
{
    public StatusEffect statusEffect;
    public int turnDuration;
}