using System.Collections.Generic;
using UnityEngine;

public class AreaDeck : MonoBehaviour
{

    public List<Card> cardsInDeck = new List<Card>();
    public Deck deck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        deck.ChangeDeck(cardsInDeck);
    }
}
