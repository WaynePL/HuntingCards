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
    public List<List<Action>> monsterActions;
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

        if (turnNumber >= actionEnd)
        {
            //new action
            actionEnd = turnNumber;
            int randomAction = new System.Random().Next(0, 3);
            switch (randomAction)
            {
                case 0:
                    monsterActions = new MonsterActions().ClawSwipe();
                    CountActions(monsterActions);

                    break;
                case 1:
                    monsterActions = new MonsterActions().Tackle();
                    CountActions(monsterActions);
                    
                    break;
                case 2:
                    monsterActions = new MonsterActions().Tackle();
                    CountActions(monsterActions);
                    
                    break;
            }
        }

        //player action

        //monster action
        if ( monsterActions.Count > 0 )
        {

            List<Action> actions = monsterActions[0];
            
            if (actions.Count == 0)
            {
                monsterActions.Remove(actions);
                actions = monsterActions[0];
            }
            
            Action action = actions[0];
            incomingTurn.playerDamage = action.damage;
            incomingTurn.monsterMove = action.moveDistance;
            incomingTurn.monsterDirection = action.moveDeriction;
            monster.MoveMonster(action.moveDistance);
            player.DamagePlayer(action.damage);
            actions.Remove(action);
            turnNumber++;
        }
    }

    private void CountActions( List<List<Action>> actions )
    {
        foreach (List<Action> newActions in actions)
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

    public List<List<Action>> ClawSwipe()
    {
        List<List<Action>> actionList = new List<List<Action>>();
        actionList.Add(Wait(2));
        actionList.Add(Damage(10));
        actionList.Add(Wait(1));
        return actionList;
    }


    public List<List<Action>> Tackle()
    {
        List<List<Action>> actionList = new List<List<Action>>();
        actionList.Add(Wait(1));
        actionList.Add(Move(2, 1, 10));
        actionList.Add(Wait(3));
        return actionList;
    }

    private List<Action> Wait(int length)
    {
        List<Action> actions = new List<Action>();
        for (int i = 0; i < length; i++)
        {
            actions.Add(new Action(0, 0, 0));
        }   
        return actions;
    }
    public List<Action> Damage(int damage)
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(0, 0, damage));

        return actions;

    }

    public List<Action> Move(int moveDistance, int moveDeriction, int damage)
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(moveDistance, moveDeriction, damage));
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