using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BattleUnit : MonoBehaviour
{
    public bool playerControlled;
    public bool isCorpse;

    public UnitData stats;
    public int currentHP;
	public int damageAdder;
	// increases your chance to dodge an attack, i.e. for an enemy to miss their attack on you
	public float dodgeChanceModifier;
	// multiply by the [0 - 1] ability hitChance, used to decrease hit chance under certain statuses
	public float hitChanceModifier;

	[HideInInspector]
    public BattleTile tile;

    SpriteRenderer rend;

	List<Status> statuses;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		if (!stats)
			stats = new UnitData();
        rend.sprite = stats.sprite;
    }
    
	public void Inflict(StatusEffect effect)
	{

	}

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > stats.MaxHP)
			currentHP = stats.MaxHP;
	}

}
