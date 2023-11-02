using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// temporary, I don't think I want classes to be in an enum...
public enum UnitClass
{

}

// temporary, I don't think I want abilities to be defined in this file 
// or as an enum...
public enum UnitAbility
{
	Fireball,
	Tackle
}

[CreateAssetMenu(fileName = "Unit", menuName = "General RPG/Unit", order = 0)]
public class UnitData : ScriptableObject
{
    public int Level;
	public int Damage;
	public int MaxHP;
	public int Strength;

	public UnitAbility[] abilities;

	// will eventually be a whole animator...
	public Sprite sprite;
}
