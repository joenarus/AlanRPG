﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, END }

public class BattleSystem : MonoBehaviour
{
    #region Singleton
    public static BattleSystem instance;
    void Awake()
    {
        instance = this;
        instance.state = BattleState.END;
    }
    #endregion
    public GameObject player;
    public GameObject enemy; //TODO: List of nearby enemies engaged in combat
    public BattleState state;

    public GameObject playerAbilityCanvas;

    CharacterStats playerStats;
    CharacterStats enemyStats;
    // Start is called before the first frame update

    public void StartBattle(GameObject _player, GameObject _enemy)
    {
        player = _player;
        enemy = _enemy;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerStats = player.GetComponent<CharacterStats>();
        enemyStats = enemy.GetComponent<CharacterStats>();
        Debug.Log("Battle started between " + player.name + " and " + enemy.name);
        //TODO: Move characters to appropriate places
        //Enable UI for combat
        playerAbilityCanvas.SetActive(true);
        //TODO: Text for combat initiation/initiative
        PlayerMotor playerMotor = player.GetComponent<PlayerMotor>();
        playerMotor.MoveToPoint(player.transform.position);
        playerMotor.StopFollowingTarget();


        yield return new WaitForSeconds(2f);

        //Handle speed/initiative here
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        // CHOOSE ACTION
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyStats.TakeDamage(playerStats.damage.GetValue());
        state = BattleState.ENEMYTURN;
        // damage enemy
        yield return new WaitForSeconds(2f); //TODO: Wait for length of animation
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        //TODO: "Enemy is acting"

        yield return new WaitForSeconds(1f); //Attack animation delay

        bool isDead = playerStats.TakeDamage(enemyStats.damage.GetValue());

        //Set HP <- kick off event?

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        playerAbilityCanvas.SetActive(false);
        if (state == BattleState.WON)
        {
            Debug.Log("You won the battle");
            //TODO: Win
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("You lost the battle");
            //TODO: LOSE
        }
        state = BattleState.END;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }
}
