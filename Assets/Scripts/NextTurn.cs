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
        }
        
        player.DamagePlayer(incomingTurn.playerDamage);
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

    public void CardSelected(Card card)
    {
        cardSelected = true;
    }
}

[System.Serializable]
public class Turn
{
    public int playerDamage = 0, monsterDamage = 0;
}

public class MonsterActions
{

    public Stack<Stack<Action>> ClawSwipe()
    {
        Stack<Stack<Action>> actionList = new Stack<Stack<Action>>();
        actionList.Push(Wait(2));
        actionList.Push(Damage(20));
        actionList.Push(Wait(1));
        return actionList;
    }


    public Stack<Stack<Action>> Tackle()
    {
        Stack<Stack<Action>> actionList = new Stack<Stack<Action>>();
        actionList.Push(Wait(1));
        actionList.Push(Damage(10));
        actionList.Push(Wait(3));
        return actionList;
    }

    private Stack<Action> Wait(int length)
    {
        Stack<Action> actions = new Stack<Action>();
        for (int i = 0; i < length; i++)
        {
            actions.Push(new Action(0));
        }   
        return actions;
    }
    public Stack<Action> Damage(int damage)
    {
        Stack<Action> actions = new Stack<Action>();
        actions.Push(new Action(damage));

        return actions;

    }

}
[System.Serializable]
public class Action
{
    public int damage;

    public Action( int damage)
    {
        this.damage = damage;
    }
}