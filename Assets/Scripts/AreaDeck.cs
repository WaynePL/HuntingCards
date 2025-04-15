using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaDeck : MonoBehaviour
{
    public int area;
    public Areas areaName;
    public List<Card> cardsInDeck = new List<Card>();
    public Deck deck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        areaName = (Areas)area;
        transform.GetComponentInChildren<TextMesh>().text = areaName.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        int tempArea = deck.currentArea;
        deck.currentArea = area;
        area = tempArea;
        areaName = (Areas)area;
        deck.ChangeDeck(this);
        transform.GetComponentInChildren<TextMesh>().text = areaName.ToString();
    }

}

public enum Areas
{
    Plains = 1,
    Woods = 2,
    Cliffs = 3,
    Caves = 4
}
