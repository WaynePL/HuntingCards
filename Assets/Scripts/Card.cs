using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : BaseAction
{
    public int damage;
    public int heal;
    public Location location;
    public NextTurn nextTurn;
    public int handPosition;
    void Start()
    {
        transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString();
        transform.GetChild(2).GetComponent<TextMesh>().text = actionName;
        nextTurn = GameObject.Find("Next Turn Button").GetComponentInChildren<NextTurn>();
    }   

    public void SetLocation(Location location)
    {
        this.location = location;
    }

    public Location GetLocation()
    {
        return location;
    }

    void OnMouseDown()
    {
        if (nextTurn.cardSelected || nextTurn.actionSelected) 
        {
            if (nextTurn.cardSelected && nextTurn.selectedCard != this)
            {
                nextTurn.selectedCard.DeselectCard();
                nextTurn.CardSelected(this);
                transform.position = new Vector3(transform.position.x, -5, transform.position.z);
            }
            else
            {
                nextTurn.CardSelected(null);
                DeselectCard();
            }
            if (nextTurn.selectedAction != null)
            {
                nextTurn.ActionSelected(null);
                nextTurn.CardSelected(this);
                transform.position = new Vector3(transform.position.x, -5, transform.position.z);
            }
        }
        else
        {
            nextTurn.CardSelected(this);
            transform.position = new Vector3(transform.position.x, -5, transform.position.z);
        }
    }

    public void DeselectCard()
    {
        transform.position = new Vector3(transform.position.x, -7, transform.position.z);
        nextTurn.UnsetAction();
    }

}

public enum Location
{
    Deck = 0,
    Field = 1,
    Discard = 2
}
