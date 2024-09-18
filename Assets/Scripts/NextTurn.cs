using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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
        meshRenderer.material.color = Color.green;
    }

    void OnMouseExit()
    {
        meshRenderer.material.color = Color.white;
    }

    void OnMouseDown()
    {
        Stack<Stack<Action>> monsterMove = new Stack<Stack<Action>>();
        Stack<Action> actions;
        if (monsterMove.Peek().Count == 0)
        {
            monsterMove.Pop();
        }
        actions = monsterMove.Peek();
        if (turnNumber >= actionEnd)
        {
            //new action
            actionEnd = turnNumber;
            int randomAction = new System.Random().Next(0, 3);
            switch (randomAction)
            {
                case 0:
                    monsterMove = new MonsterActions().ClawSwipe();
                    CountActions(monsterMove);

                    break;
                case 1:
                    monsterMove = new MonsterActions().Tackle();
                    CountActions(monsterMove);
                    
                    break;
                case 2:
                    monsterMove = new MonsterActions().Tackle();
                    CountActions(monsterMove);
                    
                    break;
            }
        }

        //player action

        //monster action
        if ( monsterActions.Count > 0 )
        {
            Action action = actions.Pop();
            incomingTurn.playerDamage = action.damage;
            incomingTurn.monsterMove = action.moveDistance;
            incomingTurn.monsterDirection = action.moveDeriction;
            monster.MoveMonster(action.moveDistance);
            player.DamagePlayer(action.damage);
            turnNumber++;
        }
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
        actionList.Push(Move(2, 1, 10));
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