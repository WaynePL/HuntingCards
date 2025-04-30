using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class NextTurn : MonoBehaviour
{
    public int turnNumber = 1;
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
    public bool potionSelected;
    Color nextTurnColor;
    public Deck deck;
    public Card selectedCard;
    public Action selectedAction;
    GameObject actionObject;
    public Action potion;

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
            string actionName = "";
            
            int totalTurns = 0;
            if (cardSelected) 
            {
                actionName = selectedCard.actionName;
                totalTurns = selectedCard.time;
                deck.DiscardCard(selectedCard);
                if (selectedCard.craftable)
                {
                    if (selectedCard.actionName == "Herb")
                    {
                        potion.quantity += 1;
                        potion.UpdateQuantity();
                    }
                }
            }
            if (actionSelected)
            {
                actionName = selectedAction.actionName;
                totalTurns = selectedAction.time;
                selectedAction.DeselectAction();
                if (actionName == "Potion")
                {
                    potion.quantity -= 1;
                    potion.UpdateQuantity();
                }
            }
            UnsetAction();
            turnNumber += totalTurns;
            transform.GetComponentInChildren<TextMesh>().text = "Turn Number: " + turnNumber.ToString();
            int remainingTurns = monster.attack.turns + monster.attack.startTurn - turnNumber;
            if (remainingTurns <= 0)
            {
                if(actionName != "Dodge Roll")
                {
                    if(actionName == "Defend")
                    {
                        incomingTurn.damageToPlayer = monster.attack.damage / 2;
                    }
                    else
                    {
                        incomingTurn.damageToPlayer = monster.attack.damage;
                    }
                }
                monster.SetMonsterAttack(turnNumber + remainingTurns);
                monster.UpdateMonsterAttack(turnNumber);
            }
            else 
            {
                incomingTurn.damageToPlayer = 0;
                monster.UpdateMonsterAttack(turnNumber);
            }
            selectedAction = null;
            selectedCard = null;
            actionSelected = false;
            cardSelected = false;

            player.DamagePlayer(incomingTurn.damageToPlayer);
            monster.DamageMonster(incomingTurn.damageToMonster);
            player.HealPlayer(incomingTurn.healtoPlayer);
            player.TirePlayer(incomingTurn.staminaUsed);
        
        }
    }

    public void CardSelected(Card card)
    {
        if (card)
        {
            cardSelected = true;
            selectedCard = card;
            ActionSelected(null);
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
            incomingTurn.healtoPlayer = action.heal;
            SetAction(action);

        }
        else
        {
            actionSelected = false;
            incomingTurn.damageToMonster = 0;
            incomingTurn.staminaUsed = 0;
            incomingTurn.healtoPlayer = 0;
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
        GameObject nameText = new GameObject();
        nameText.transform.parent = actionObject.transform;
        nameText.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y + 2, player.transform.position.z);
        nameText.AddComponent<MeshRenderer>();
        nameText.AddComponent<TextMesh>();
        nameText.GetComponent<TextMesh>().text = baseAction.actionName;
        nameText.GetComponent<TextMesh>().color = Color.black;
        nameText.GetComponent<TextMesh>().fontSize = 30;
        nameText.transform.localScale *= 0.4f;

        GameObject turnText = new GameObject();
        turnText.transform.parent = actionObject.transform;
        turnText.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y + 1, player.transform.position.z);
        turnText.AddComponent<MeshRenderer>();
        turnText.AddComponent<TextMesh>();
        turnText.GetComponent<TextMesh>().text = "Turns: " + baseAction.time;
        turnText.GetComponent<TextMesh>().color = Color.black;
        turnText.GetComponent<TextMesh>().fontSize = 30;
        turnText.transform.localScale *= 0.4f;

        GameObject staminaText = new GameObject();
        staminaText.transform.parent = actionObject.transform;
        staminaText.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y , player.transform.position.z);
        staminaText.AddComponent<MeshRenderer>();
        staminaText.AddComponent<TextMesh>();
        staminaText.GetComponent<TextMesh>().text = "Stamina: " + baseAction.staminaCost;
        staminaText.GetComponent<TextMesh>().color = Color.black;
        staminaText.GetComponent<TextMesh>().fontSize = 30;
        staminaText.transform.localScale *= 0.4f;

        GameObject descriptionText = new GameObject();
        descriptionText.transform.parent = actionObject.transform;
        descriptionText.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y - 1, player.transform.position.z);
        descriptionText.AddComponent<MeshRenderer>();
        descriptionText.AddComponent<TextMesh>();
        descriptionText.GetComponent<TextMesh>().text = baseAction.description;
        descriptionText.GetComponent<TextMesh>().color = Color.black;
        descriptionText.GetComponent<TextMesh>().fontSize = 30;
        descriptionText.transform.localScale *= 0.4f;
    }

    public void UnsetAction()
    {
        foreach (Transform child in actionObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void AdvanceTurn()
    {
        turnNumber += 1;
        transform.GetComponentInChildren<TextMesh>().text = "Turn Number: " + turnNumber.ToString();
    }

}
[Serializable]
public class Turn
{
    public int damageToPlayer = 0, damageToMonster = 0, healtoPlayer = 0, staminaUsed = 0;
}
