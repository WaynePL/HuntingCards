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
    Color nextTurnColor;
    public Deck deck;
    public Card selectedCard;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        monster = GameObject.Find("Monster").GetComponent<Monster>();
        nextTurnColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (cardSelected)
        {
            nextTurnColor.a = 1f;
        }
        else 
        {
            nextTurnColor.a = 0.5f;   
        }
        meshRenderer.material.color = nextTurnColor;
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
        if (cardSelected)
        {
            cardSelected = false;
            
        
            player.DamagePlayer(incomingTurn.damageToPlayer);
            monster.DamageMonster(incomingTurn.damageToMonster);
            player.HealPlayer(incomingTurn.healtoPlayer);
            deck.DiscardCard(selectedCard);
            selectedCard = null;
            turnNumber++;
        }
    }

    public void CardSelected(Card card)
    {
        if (card)
        {
            cardSelected = true;
            selectedCard = card;
            if (card.damage > 0)
            {
                incomingTurn.damageToMonster = card.damage;
            }
            incomingTurn.healtoPlayer = card.heal > 0 ? card.heal : 0;
            
            incomingTurn.damageToPlayer = (turnNumber % 2 == 0) ? 0 : 10;
        }
        else
        {
            cardSelected = false;
            selectedCard = null;
        }
    }
}

[System.Serializable]
public class Turn
{
    public int damageToPlayer = 0, damageToMonster = 0, healtoPlayer = 0;
}
