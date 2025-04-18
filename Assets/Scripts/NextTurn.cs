using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class NextTurn : MonoBehaviour
{
    public int turnNumber = 0;
    public int actionEnd = 0;
    public Action monsterAction;
    public Stack<Stack<Action>> monsterActions;
    public MeshRenderer meshRenderer;
    public Player player;
    public Monster monster;
    public int currentPlayerDamage = 10;
    public int currentMonsterDamage = 10;
    public Turn incomingTurn;
    public bool cardSelected;
    public bool actionSelected;
    Color nextTurnColor;
    public Deck deck;
    public Card selectedCard;
    public Action selectedAction;
    GameObject actionObject;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        monster = GameObject.Find("Monster").GetComponent<Monster>();
        nextTurnColor = meshRenderer.material.color;
        actionObject = new GameObject("Action");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
    }

    void OnMouseExit()
    {
    }

    void OnMouseDown()
    {

        //player action
        if (cardSelected || actionSelected)
        {
            
        
            player.DamagePlayer(incomingTurn.damageToPlayer);
            monster.DamageMonster(incomingTurn.damageToMonster);
            player.HealPlayer(incomingTurn.healtoPlayer);
            player.TirePlayer(incomingTurn.staminaUsed);
            int totalTurns = 0;
            if (cardSelected) 
            {
                totalTurns = selectedCard.time;
                cardSelected = false;
                deck.DiscardCard(selectedCard);
                selectedCard = null;

            }
            if (actionSelected)
            {
                totalTurns = selectedAction.time;
                actionSelected = false;
                selectedAction.DeselectAction();
                selectedAction = null;
            }
            UnsetAction();
            turnNumber += totalTurns;
            transform.GetComponentInChildren<TextMesh>().text = "Turn Number: " + turnNumber.ToString();
            if (turnNumber % 5 == 0) monster.runAway();
            if (monster.currentArea != deck.currentArea)
            {
                monster.gameObject.SetActive(false);
            }
        
        }
    }

    public void CardSelected(Card card)
    {
        if (card)
        {
            cardSelected = true;
            selectedCard = card;
            ActionSelected(null);
            if (card.damage > 0)
            {
                incomingTurn.damageToMonster = card.damage;
            }
            incomingTurn.healtoPlayer = card.heal > 0 ? card.heal : 0;
            SetAction(card);

        }
        else
        {
            cardSelected = false;
            if (selectedCard)
            {
                selectedCard.DeselectCard();
                selectedCard = null;
            }
        }
        SetNextTurnColor();
    }

    public void ActionSelected(Action action)
    {
        
        if (action)
        {
            actionSelected = true;

            selectedAction = action;
            incomingTurn.damageToMonster = action.damage;
            incomingTurn.staminaUsed = action.staminaCost;
            SetAction(action);

        }
        else
        {
            actionSelected = false;
            if (selectedAction)
            {
                selectedAction.DeselectAction();
                selectedAction = null;
            }
        }
        SetNextTurnColor();
    }

    public void SetNextTurnColor()
    {
        if (cardSelected || actionSelected)
        {
            nextTurnColor.a = 1f;
        }
        else 
        {
            nextTurnColor.a = 0.5f;   
        }
        meshRenderer.material.color = nextTurnColor;
    }

    private void SetAction(BaseAction baseAction)
    {
        GameObject actionObjectText = new GameObject();
        actionObjectText.transform.parent = actionObject.transform;
        actionObjectText.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y + 2, player.transform.position.z);
        actionObjectText.AddComponent<MeshRenderer>();
        actionObjectText.AddComponent<TextMesh>();
        actionObjectText.GetComponent<TextMesh>().text = baseAction.name + " " + baseAction.time;
        actionObjectText.GetComponent<TextMesh>().color = Color.black;
        actionObjectText.GetComponent<TextMesh>().fontSize = 30;
        actionObjectText.transform.localScale *= 0.4f;
    }

    public void UnsetAction()
    {
        Destroy(actionObject.transform.GetChild(0).gameObject);
    }
}
[Serializable]
public class Turn
{
    public int damageToPlayer = 0, damageToMonster = 0, healtoPlayer = 0, staminaUsed = 0;
}
