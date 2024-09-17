using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card[] cards = new Card[0];
    GameObject cardSlots;
    // Start is called before the first frame update
    void Start()
    {
        cardSlots = GameObject.Find("Card Slots");
        foreach (Transform cardSlot in cardSlots.transform)
        {
            cardSlot.GetChild(1).GetComponent<CardSlot>().SetCard(cards[Random.Range(0, cards.Length)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}