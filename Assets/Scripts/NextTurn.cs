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
    public int recordedPlayerPosition;
    public int intendedMonsterDirection;
    public Stack<Stack<Action>> monsterMove = new Stack<Stack<Action>>();
    public List<int> playerDamagePositions = new List<int>();
    public List<CardSlot> cardSlots;
    public GameObject cardSlotsGameObject;
    Color nextTurnColor;
    public CardSlot selectedCardSlot;
    public Deck deck;
    // Start is called before the first frame update
    void Start()
    {
        cardSlotsGameObject = GameObject.Find("Card Slots");
        cardSlots = cardSlotsGameObject.GetComponent<CardSlots>().cardSlots;
        monsterMove.Push(new Stack<Action>());
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
            selectedCardSlot.DeselectCard();
            deck.DiscardCard(selectedCardSlot);
        }
        
        //monster action
        Stack<Action> actions;
        if (monsterMove.Peek().Count == 0)
        {
            monsterMove.Pop();
        }
        if (monsterMove.Count == 0)
        {
            //new action
            actionEnd = turnNumber;
            // int randomAction = new System.Random().Next(0, 2);
            int randomAction = 1;
            playerDamagePositions.Clear();
            switch (randomAction)
            {
                case 0:
                    monsterMove = new MonsterActions().ClawSwipe();
                    CountActions(monsterMove);
                    playerDamagePositions.Add(monster.currentPosition + 1);
                    playerDamagePositions.Add(monster.currentPosition);
                    playerDamagePositions.Add(monster.currentPosition - 1);
                    break;
                case 1:
                    monsterMove = new MonsterActions().Tackle();
                    CountActions(monsterMove);
                    recordedPlayerPosition = player.currentPosition;
                    intendedMonsterDirection = monster.currentPosition < player.currentPosition ? 1 : -1;
                    playerDamagePositions.Add(monster.currentPosition);
                    playerDamagePositions.Add(monster.currentPosition + intendedMonsterDirection);
                    playerDamagePositions.Add(monster.currentPosition + 2 * intendedMonsterDirection);
                    break;
                case 2:
                    monsterMove = new MonsterActions().Tackle();
                    CountActions(monsterMove);
                    recordedPlayerPosition = player.currentPosition;
                    intendedMonsterDirection = monster.currentPosition < player.currentPosition ? 1 : -1;
                    break;
            }
            foreach (CardSlot cardSlot in cardSlots)
            {
                foreach (int damagePosition in playerDamagePositions)
                {
                    if (cardSlot.cardPosition == damagePosition)
                    {
                        cardSlot.damageBorder.enabled = true;
                    }
                }
            }
        }
        

        actions = monsterMove.Peek();
       
        Action action = actions.Pop();
        incomingTurn.playerDamage = action.damage;
        incomingTurn.monsterDirection = action.moveDeriction != 0 ? action.moveDeriction : intendedMonsterDirection;
        incomingTurn.monsterMove = action.moveDistance * incomingTurn.monsterDirection;
        monster.MoveMonster(incomingTurn.monsterMove);
        if (action.damage > 0)
        {
            foreach (CardSlot cardSlot in cardSlots)
            {
                cardSlot.damageBorder.enabled = false;
            }
        }
        foreach (int damagePosition in playerDamagePositions)
        {
            if (player.currentPosition == damagePosition)
            {
                player.DamagePlayer(incomingTurn.playerDamage);
            }
        }
        turnNumber++;
    }

    private void CountActions( Stack<Stack<Action>> actions )
    {
        foreach (Stack<Action> newActions in actions)
        {
            foreach (Action action in newActions)
            {
                actionEnd = actionEnd + 1;
            }
        }
    }
}

[System.Serializable]
public class Turn
{
    public int playerDamage = 0, monsterDamage = 0;
    public int monsterMove = 0;
    public int monsterDirection = 0;
}

public class MonsterActions
{

    public Stack<Stack<Action>> ClawSwipe()
    {
        Stack<Stack<Action>> actionList = new Stack<Stack<Action>>();
        actionList.Push(Wait(2));
        actionList.Push(Damage(10));
        actionList.Push(Wait(1));
        return actionList;
    }


    public Stack<Stack<Action>> Tackle()
    {
        Stack<Stack<Action>> actionList = new Stack<Stack<Action>>();
        actionList.Push(Wait(1));
        actionList.Push(Move(2, 0, 10));
        actionList.Push(Wait(3));
        return actionList;
    }

    private Stack<Action> Wait(int length)
    {
        Stack<Action> actions = new Stack<Action>();
        for (int i = 0; i < length; i++)
        {
            actions.Push(new Action(0, 0, 0));
        }   
        return actions;
    }
    public Stack<Action> Damage(int damage)
    {
        Stack<Action> actions = new Stack<Action>();
        actions.Push(new Action(0, 0, damage));

        return actions;

    }

    public Stack<Action> Move(int moveDistance, int moveDeriction, int damage)
    {
        Stack<Action> actions = new Stack<Action>();
        actions.Push(new Action(moveDistance, moveDeriction, damage));
        return actions;
    }
}
[System.Serializable]
public class Action
{
    public int moveDistance;
    public int moveDeriction;
    public int damage;

    public Action(int moveDistance, int moveDeriction, int damage)
    {
        this.moveDistance = moveDistance;
        this.moveDeriction = moveDeriction;
        this.damage = damage;
    }
}