using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string description;
    public int damage;
    public Location location;

    void Start()
    {
        transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString();
        transform.GetChild(2).GetComponent<TextMesh>().text = gameObject.name;
    }   

    public void SetLocation(Location location)
    {
        this.location = location;
    }
    
}

public enum Location
{
    Deck = 0,
    Field = 1,
    Discard = 2
}
