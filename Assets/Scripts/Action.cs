using System;
using UnityEngine;

public class Action : BaseAction
{
    public int staminaCost = 0;
    public int damage = 0;

    public NextTurn nextTurn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        transform.GetChild(1).GetComponent<TextMesh>().text = "Damage: " + damage.ToString();

        transform.GetChild(2).GetComponent<TextMesh>().text = "Stamina: " + staminaCost.ToString();
    
        

        transform.GetChild(3).GetComponent<TextMesh>().text = actionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (nextTurn.cardSelected || nextTurn.actionSelected) 
        {
            if (nextTurn.actionSelected && nextTurn.selectedAction != this)
            {
                nextTurn.selectedAction.DeselectAction();
                nextTurn.ActionSelected(this);
                transform.position = new Vector3(transform.position.x, -15, transform.position.z);
            }
            else
            {
                nextTurn.ActionSelected(null);
                DeselectAction();
            }
            if(nextTurn.selectedCard != null) 
            {
                nextTurn.CardSelected(null);
                nextTurn.ActionSelected(this);
                transform.position = new Vector3(transform.position.x, -15, transform.position.z);
            }
        }
        else
        {
            nextTurn.ActionSelected(this);
            transform.position = new Vector3(transform.position.x, -15, transform.position.z);
        }
    }

    public void DeselectAction()
    {
        transform.position = new Vector3(transform.position.x, -17, transform.position.z);
        nextTurn.UnsetAction();
    }
}
