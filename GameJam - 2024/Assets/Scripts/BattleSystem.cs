using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Battlestate { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattlestation;
    public Transform enemyBattlestation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Battlestate state;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = Battlestate.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattlestation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattlestation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "The Ice King " + enemyUnit.unitName + " has been angered and now wants to scwabble....";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = Battlestate.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        SoundManager.Instance.PlaySound2D("Attack");
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You landed your attack";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = Battlestate.WON;
            EndBattle();
        }
        else
        {
            state = Battlestate.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        SoundManager.Instance.PlaySound2D("NextTurn");
        dialogueText.text = enemyUnit.unitName + " has Attacked!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = Battlestate.LOST;
            EndBattle();
        }
        else
        {
            state = Battlestate.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == Battlestate.WON)
        {
            SoundManager.Instance.PlaySound2D("NextTurn");
            dialogueText.text = "You've dealt with that icey thorn in your side...";
        }else if (state == Battlestate.LOST)
        {
            SoundManager.Instance.PlaySound2D("NextTurn");
            dialogueText.text = "The Ice King ended you...";
        }     
    }



    void PlayerTurn()
    {
        SoundManager.Instance.PlaySound2D("NextTurn");
        dialogueText.text = "Choose an action ";
    }

    public void OnAttackButton()
    {
        if (state != Battlestate.PLAYERTURN)
        {
            
            return;
        }
            

        StartCoroutine(PlayerAttack());
    }
    IEnumerator PlayerHeal()
    {
        SoundManager.Instance.PlaySound2D("Heal");
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed Stregnth";

        yield return new WaitForSeconds(2f);

        state = Battlestate.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnHealButton()
    {
        if (state != Battlestate.PLAYERTURN)
        {
            
            return;
        }


        StartCoroutine(PlayerHeal());
    }

}
