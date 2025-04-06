using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> cardsInDeck = new List<Card>();
    public List<Card> cardsInField = new List<Card>();
    public List<Card> cardsInDiscard = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        cardsInDeck = playerDeck;
        for (int i = 0; i < 5; i++)
        {
            DealCard(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealCard(int handPosition)
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
        cardsInDeck.Remove(card);
        cardsInField.Add(card);
        card.SetLocation(Location.Field);
        GameObject cardObject = Instantiate(card.gameObject);

        cardObject.GetComponent<Card>().handPosition = handPosition;
        int cardPosition = (handPosition * -8) + 8;
        cardObject.transform.position = new Vector3(cardPosition, -15, -10);
    }

    public void DiscardCard(Card discardedCard)
    {
        discardedCard.SetLocation(Location.Discard);
        cardsInField.Remove(discardedCard);
        cardsInDiscard.Add(discardedCard);
        DealCard(discardedCard.handPosition);
        discardedCard.gameObject.SetActive(false);
    }
}