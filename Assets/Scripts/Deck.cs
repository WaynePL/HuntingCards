using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> currentAreaDeck = new List<Card>();
    public List<Card> cardsInDeck = new List<Card>();
    public List<Card> cardsInField = new List<Card>();
    public List<Card> cardsInDiscard = new List<Card>();
    public int currentArea = 1;
    public Monster monster;
    public NextTurn nextTurn;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Card card in currentAreaDeck)
        {
            GameObject cardObject = Instantiate(card.gameObject);
            cardsInDeck.Add(cardObject.GetComponent<Card>());
            cardObject.SetActive(false);
        }
        foreach (Card card in playerDeck)
        {
            GameObject cardObject = Instantiate(card.gameObject);

            cardsInDeck.Add(cardObject.GetComponent<Card>());
            cardObject.SetActive(false);
        }
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

        Card card = cardsInDeck[Random.Range(0, cardsInDeck.Count)];
        cardsInField.Add(card);
        cardsInDeck.Remove(card);
        card.SetLocation(Location.Field);

        card.handPosition = handPosition;
        int cardPosition = (handPosition * -8) + 8;
        card.gameObject.transform.position = new Vector3(cardPosition, -17, -10);
        card.gameObject.SetActive(true);
    }

    public void DiscardCard(Card discardedCard)
    {
        discardedCard.SetLocation(Location.Discard);
        cardsInField.Remove(discardedCard);
        cardsInDiscard.Add(discardedCard);
        DealCard(discardedCard.handPosition);
        discardedCard.gameObject.SetActive(false);
        if (cardsInDeck.Count == 0)
        {
            cardsInDeck = cardsInDiscard;
            cardsInDiscard = new List<Card>();
            foreach (Card newCard in cardsInDeck)
            {
                newCard.SetLocation(Location.Deck);
            }
        }
    }

    public void ChangeDeck(AreaDeck areaDeck)
    {
        nextTurn.CardSelected(null);
        foreach (Card card in cardsInField)
        {
            Destroy(card.gameObject);
        }
        cardsInField.Clear();
        foreach (Card card in cardsInDiscard)
        {
            Destroy(card.gameObject);
        }
        cardsInDiscard.Clear();
        foreach (Card card in cardsInDeck)
        {
            Destroy(card.gameObject);
        }
        cardsInDeck.Clear();

        List<Card> tempDeck = areaDeck.cardsInDeck;
        areaDeck.cardsInDeck = currentAreaDeck;
        currentAreaDeck = tempDeck;

        if (monster.currentArea != currentArea)
        {
            monster.gameObject.SetActive(false);
        }
        else
        {
            monster.gameObject.SetActive(true);
        }
        foreach (Card card in currentAreaDeck)
        {
            GameObject cardObject = Instantiate(card.gameObject);
            cardsInDeck.Add(cardObject.GetComponent<Card>());
            cardObject.SetActive(false);
        }
        foreach (Card card in playerDeck)
        {
            GameObject cardObject = Instantiate(card.gameObject);
            cardsInDeck.Add(cardObject.GetComponent<Card>());
            cardObject.SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            DealCard(i);
        }
    }
}