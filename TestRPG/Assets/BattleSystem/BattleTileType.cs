using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tile", menuName = "BattleSystem/BattleTileType", order = 1)]
public class BattleTileType : ScriptableObject
{
    public string typeName;
    public Sprite sprite;
}
