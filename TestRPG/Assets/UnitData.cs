using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// temporary, I don't think I want classes to be in an enum...
public enum UnitClass
{

}

[CreateAssetMenu(fileName = "Unit", menuName = "Battle System/Unit Data", order = 0)]
public class UnitData : ScriptableObject
{
    public int Level;
	public int Damage;
	public int MaxHP;
	public int Strength;

	public ActiveAbility[] abilities;

	// will eventually be a whole animator...
	public Sprite sprite;
}
