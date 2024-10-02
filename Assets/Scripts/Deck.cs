using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> cardsInDeck = new List<Card>();
    public List<Card> cardsInField = new List<Card>();
    public List<Card> cardsInDiscard = new List<Card>();

    public CardSlots cardSlots;
    // Start is called before the first frame update
    void Start()
    {
        cardsInDeck = playerDeck;
        cardSlots = GameObject.Find("Card Slots").GetComponent<CardSlots>();
        foreach (CardSlot cardSlot in cardSlots.cardSlots)
        {
            DealCard(cardSlot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealCard(CardSlot cardSlot)
    {
        if (cardsInDeck.Count == 0)
        {
            cardsInDeck = cardsInDiscard;
            cardsInDiscard = new List<Card>();
            foreach (Card newCard in cardsInDeck)
            {
                newCard.SetLocation(Location.Deck);
            }
        }
        Card card = cardsInDeck[Random.Range(0, cardsInDeck.Count)];
        cardSlot.SetCard(card);
        cardsInDeck.Remove(card);
        cardsInField.Add(card);
        card.SetLocation(Location.Field);
    }
    public void DiscardCard(CardSlot cardSlot)
    {

        Card card = cardSlot.cardPrefab;
        
        cardsInField.Remove(card);
        cardsInDiscard.Add(card);
        card.SetLocation(Location.Discard);
        Destroy(cardSlot.currentCard.gameObject);
        DealCard(cardSlot);
        
        
    }
}