using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
	public BattleGrid battleGrid;
	
	public Text dialogueText;

	bool acceptPlayerAction = false;

	bool moving = false;
	BattleUnit from;
	BattleTile to;

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
		acceptPlayerAction = true;
		dialogueText.text = "Uh...";
		yield return new WaitForSeconds(1f);
		dialogueText.text = "Uh... Oh...";
		yield return new WaitForSeconds(1f);
		dialogueText.text = "Uhoh...";
		dialogueText.fontStyle = FontStyle.Bold;
	}

	IEnumerator PlayerMove()
	{
		dialogueText.fontStyle = FontStyle.Normal;
		dialogueText.text = "Select a unit to move...";

		moving = true;
		
		yield return new WaitForSeconds(1f);
	}

	void Update()
	{
		UpdateUnits();

		if (moving && Input.GetMouseButtonDown(0))
		{
			Vector3 clickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			print(clickPos);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(clickPos.x, clickPos.y), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
		}
	}

	void UpdateUnits()
	{
		foreach (BattleUnit unit in units)
		{
			BattleTile tile = battleGrid.GetTile(unit.gridPosition);
			if (tile == null)
				Debug.LogWarning("Grid position " + unit.gridPosition + " doesn't exist/out of bounds of BattleGrid");
			else
				unit.transform.position = tile.transform.position;
		}
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

		StartCoroutine(PlayerMove());
	}


	// Unit playerUnit;
	// Unit enemyUnit;

	// public Text dialogueText;

	// public BattleHUD playerHUD;
	// public BattleHUD enemyHUD;

	// public BattleState state;

    // // Start is called before the first frame update
    // void Start()
    // {
	// 	state = BattleState.START;
	// 	StartCoroutine(SetupBattle());
    // }

	// IEnumerator SetupBattle()
	// {
	// 	GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
	// 	playerUnit = playerGO.GetComponent<Unit>();

	// 	GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
	// 	enemyUnit = enemyGO.GetComponent<Unit>();

	// 	dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

	// 	playerHUD.SetHUD(playerUnit);
	// 	enemyHUD.SetHUD(enemyUnit);

	// 	yield return new WaitForSeconds(2f);

	// 	state = BattleState.PLAYERTURN;
	// 	PlayerTurn();
	// }

	// IEnumerator PlayerAttack()
	// {
	// 	bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

	// 	enemyHUD.SetHP(enemyUnit.currentHP);
	// 	dialogueText.text = "The attack is successful!";

	// 	yield return new WaitForSeconds(2f);

	// 	if(isDead)
	// 	{
	// 		state = BattleState.WON;
	// 		EndBattle();
	// 	} else
	// 	{
	// 		state = BattleState.ENEMYTURN;
	// 		StartCoroutine(EnemyTurn());
	// 	}
	// }

	// IEnumerator EnemyTurn()
	// {
	// 	dialogueText.text = enemyUnit.unitName + " attacks!";

	// 	yield return new WaitForSeconds(1f);

	// 	bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

	// 	playerHUD.SetHP(playerUnit.currentHP);

	// 	yield return new WaitForSeconds(1f);

	// 	if(isDead)
	// 	{
	// 		state = BattleState.LOST;
	// 		EndBattle();
	// 	} else
	// 	{
	// 		state = BattleState.PLAYERTURN;
	// 		PlayerTurn();
	// 	}

	// }

	// void EndBattle()
	// {
	// 	if(state == BattleState.WON)
	// 	{
	// 		dialogueText.text = "You won the battle!";
	// 	} else if (state == BattleState.LOST)
	// 	{
	// 		dialogueText.text = "You were defeated.";
	// 	}
	// }

	// void PlayerTurn()
	// {
	// 	dialogueText.text = "Choose an action:";
	// }

	// IEnumerator PlayerHeal()
	// {
	// 	playerUnit.Heal(5);

	// 	playerHUD.SetHP(playerUnit.currentHP);
	// 	dialogueText.text = "You feel renewed strength!";

	// 	yield return new WaitForSeconds(2f);

	// 	state = BattleState.ENEMYTURN;
	// 	StartCoroutine(EnemyTurn());
	// }

	// public void OnAttackButton()
	// {
	// 	if (state != BattleState.PLAYERTURN)
	// 		return;

	// 	StartCoroutine(PlayerAttack());
	// }

	// public void OnHealButton()
	// {
	// 	if (state != BattleState.PLAYERTURN)
	// 		return;

	// 	StartCoroutine(PlayerHeal());
	// }

}
