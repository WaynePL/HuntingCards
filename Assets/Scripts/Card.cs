using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string description;
    public int damage;
    public Location location;
    public NextTurn nextTurn;
    public int handPosition;

    void Start()
    {
        transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString();
        transform.GetChild(2).GetComponent<TextMesh>().text = gameObject.name;
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
        nextTurn.CardSelected(this);
    }

}

public enum Location
{
    Deck = 0,
    Field = 1,
    Discard = 2
}
