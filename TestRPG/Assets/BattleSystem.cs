using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
	public BattleGrid battleGrid;
	
	public Text dialogueText;

	bool acceptPlayerAction = false;

	// for moving
	bool selectingUnit = false;
	bool selectingTile = false;
	BattleUnit selectedUnit;
	BattleTile selectedTile;

	int currentTurn;
	// we can handle this more elegantly with changing turn order
	// for now currentTurn is an index into the 'units' list, who's
	// order is simply the turn order for now 
	List<BattleUnit> units;

	// this is a singleton structure
	public static BattleSystem main;

	void Start()
	{
		// if there's already a BattleSystem object instantiated 
		// (and thus was set to 'main' **static** variable),
		// delete this one
		if (main != null)
		{
			Destroy(gameObject);
			return;
		}	
		else
			main = this;

		units = new List<BattleUnit>(FindObjectsByType<BattleUnit>(FindObjectsSortMode.None));

		StartCoroutine(RunBattle());
	}

	IEnumerator RunBattle()
	{
		acceptPlayerAction = false;
		dialogueText.text = "Uh...";
		yield return new WaitForSeconds(0.5f);
		dialogueText.text = "Uh... Oh...";
		yield return new WaitForSeconds(0.5f);
		dialogueText.text = "Uhoh...";
		dialogueText.fontStyle = FontStyle.Bold;
		yield return new WaitForSeconds(1f);

		while (true)
		{
			BattleUnit unit = units[currentTurn];

			dialogueText.text = unit.name + "'s turn...";
			dialogueText.fontStyle = FontStyle.Normal;
			yield return new WaitForSeconds(0.5f);

			if (unit.playerControlled)
			{
				dialogueText.text = "Select an action...";
				dialogueText.fontStyle = FontStyle.Normal;
				acceptPlayerAction = true;
				selectedUnit = unit;
				yield return new WaitUntil(() => !acceptPlayerAction);
			}
			else
			{
				dialogueText.text = unit.name + " attacks!";
				dialogueText.fontStyle = FontStyle.Normal;
				yield return new WaitForSeconds(1.0f);
			}

			currentTurn = (currentTurn + 1) % units.Count;

			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator MoveUnit(BattleUnit unit, BattleTile to)
	{
		dialogueText.text = "Moving the unit...";
		yield return new WaitForSeconds(0.25f);
		
		BattleTile oldTile = unit.tile;
		if (oldTile)
			oldTile.unit = null;

		unit.tile = to;
		to.unit = unit;

		unit.GetComponent<SpriteRenderer>().color = Color.white;
		to.GetComponent<SpriteRenderer>().color = Color.white;

		yield return new WaitForSeconds(1.0f);
		
		acceptPlayerAction = false;
	}

	void Update()
	{
		UpdateUnits();

		if (selectedUnit)
			selectedUnit.GetComponent<SpriteRenderer>().color = Color.red;
		if (selectedTile)
			selectedTile.GetComponent<SpriteRenderer>().color = Color.red;

		if ((selectingUnit || selectingTile) && Input.GetMouseButtonDown(0))
		{
			Vector3 clickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			// move these variables somewhere else...
			ContactFilter2D filter = new ContactFilter2D();
            RaycastHit2D[] hits = new RaycastHit2D[8];

            Physics2D.Raycast(new Vector2(clickPos.x, clickPos.y), Vector2.zero, filter, hits);
			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider == null)
					continue;

				var unit = hit.collider.GetComponent<BattleUnit>();
				if (selectingUnit && unit != null)
				{
					selectedUnit = unit;
					break;
				}

				var tile = hit.collider.GetComponent<BattleTile>();
				if (selectingTile && tile != null)
				{
					selectedTile = tile;
					break;
				}
			}
		}

		if (selectingUnit)
		{
			dialogueText.fontStyle = FontStyle.Normal;
			dialogueText.text = "Select a unit to move...";

			if (selectedUnit != null)
			{
				selectingUnit = false;
				selectingTile = true;
			}
		}

		if (selectingTile)
		{
			dialogueText.fontStyle = FontStyle.Normal;
			dialogueText.text = "Select a tile to move to...";

			if (selectedTile != null && selectedTile.unit == null)
			{
				selectedTile.GetComponent<SpriteRenderer>().color = Color.red;
				selectingTile = false;

				StartCoroutine(MoveUnit(selectedUnit, selectedTile));
				selectedUnit = null;
				selectedTile = null;
			}
			else
			{
				selectedTile = null;
			}
		}
	}

	void UpdateUnits()
	{
	}

	public void OnAttackButton()
	{
		if (!acceptPlayerAction)
			return;

		// StartCoroutine(PlayerAttack());
	}

	public void OnMoveButton()
	{
		if (!acceptPlayerAction)
			return;

		selectedUnit = units[currentTurn];
		selectedTile = null;
		selectingUnit = false;
		selectingTile = true;
	}

	public void OnAbilityButton()
	{
		if (!acceptPlayerAction)
			return;

		selectedUnit.stats.abilities
		selectedTile = null;
		selectingUnit = false;
		selectingTile = true;
	}


}
